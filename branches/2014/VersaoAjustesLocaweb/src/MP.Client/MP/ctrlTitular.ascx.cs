using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class ctrlTitular : UserControl
    {
        public static event TitularFoiSelecionadoEventHandler TitularFoiSelecionado;
        public delegate void TitularFoiSelecionadoEventHandler(ITitular titular);

        protected void Page_Load(object sender, EventArgs e) { }

        public void Inicializa()
        {
            LimparControle();
        }

        private void LimparControle()
        {
            var controle = cboTitular as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboTitular.ClearSelection();
            BotaoNovoEhVisivel = false;
            TitularSelecionado = null;
        }

        protected void cboTitular_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTitular>())
            {
                cboTitular.Items.Clear();

                foreach (var titular in servico.ObtenhaPorNomeComoFiltro(e.Text, 50))
                {
                    var item = new RadComboBoxItem(titular.Pessoa.Nome, titular.Pessoa.ID.ToString());
                    item.Attributes.Add("DataDoCadastro", titular.DataDoCadastro.Value.ToString("dd/MM/yyyy"));
                    item.Attributes.Add("InformacoesAdicionais", titular.InformacoesAdicionais);
                    cboTitular.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        protected void cboTitular_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!cboTitular.AutoPostBack) return;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTitular>())
            {
                ITitular titular = null;

                if (string.IsNullOrEmpty(((RadComboBox)sender).SelectedValue))
                {
                    LimparControle();
                    return;
                }

                var codigoSelecionado = Convert.ToInt64(((RadComboBox)sender).SelectedValue);
                titular = servico.Obtenha(codigoSelecionado);

                TitularSelecionado = titular;

                if (TitularFoiSelecionado != null)
                    TitularFoiSelecionado(titular);
            }
        }

        public bool EnableLoadOnDemand
        {
            set { cboTitular.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboTitular.ShowDropDownOnTextboxClick = value; }
        }

        protected void btnNovo_OnClick(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.ExibeJanela(ObtenhaURL(), "Cadastro de titular", 800, 550, "MP_cdTitular_aspx"), 
                false);
        }

        private string ObtenhaURL()
        {
            return String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/cdTitular.aspx");
        }

        public bool BotaoNovoEhVisivel
        {
            set { btnNovo.Visible = value; }
        }

        public ITitular TitularSelecionado
        {
            get { return (ITitular)ViewState[ClientID]; }
            set { ViewState.Add(ClientID, value); }
        }

        protected override void OnPreRender(EventArgs e)
        {
            var principal = FabricaDeContexto.GetInstancia().GetContextoAtual();

            if (btnNovo.Visible)
                btnNovo.Visible = principal.EstaAutorizado(btnNovo.CommandArgument);

            base.OnPreRender(e);
        }
    }
}