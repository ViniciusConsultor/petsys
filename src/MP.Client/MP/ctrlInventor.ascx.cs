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
    public partial class ctrlInventor : System.Web.UI.UserControl
    {
        public static event InventorFoiSelecionadoEventHandler InventorFoiSelecionado;
        public delegate void InventorFoiSelecionadoEventHandler(IInventor inventor);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa()
        {
            LimparControle();
        }

        private void LimparControle()
        {
            var controle = cboInventor as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboInventor.ClearSelection();
            BotaoNovoEhVisivel = false;
        }
        
        protected void cboInventor_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeInventor>())
            {
                cboInventor.Items.Clear();

                foreach (var inventor in servico.ObtenhaPorNomeComoFiltro(e.Text, 50))
                {
                    var item = new RadComboBoxItem(inventor.Pessoa.Nome, inventor.Pessoa.ID.ToString());

                    item.Attributes.Add("DataDoCadastro", inventor.DataDoCadastro.ToString());
                    item.Attributes.Add("InformacoesAdicionais", inventor.InformacoesAdicionais);

                    cboInventor.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        protected void cboInventor_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeInventor>())
            {
                IInventor inventor = null;

                if (string.IsNullOrEmpty(((RadComboBox)sender).SelectedValue))
                {
                    LimparControle();
                    return;
                }

                var codigoSelecionado = Convert.ToInt64(((RadComboBox)sender).SelectedValue);
                inventor = servico.Obtenha(codigoSelecionado);

                InventorSelecionado = inventor;

                if (InventorFoiSelecionado != null)
                    InventorFoiSelecionado(inventor);
            }
        }

        public IInventor InventorSelecionado
        {
            get { return (IInventor)ViewState[ClientID]; }
            set { ViewState.Add(ClientID, value); }
        }

        public bool EnableLoadOnDemand
        {
            set { cboInventor.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboInventor.ShowDropDownOnTextboxClick = value; }
        }

        public bool BotaoNovoEhVisivel
        {
            set { btnNovo.Visible = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            var principal = FabricaDeContexto.GetInstancia().GetContextoAtual();

            if (btnNovo.Visible)
                btnNovo.Visible = principal.EstaAutorizado(btnNovo.CommandArgument);

            base.OnPreRender(e);
        }

        protected void btnNovo_OnClick(object sender, ImageClickEventArgs e)
        {
            var URL = ObtenhaURL();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(URL, "Cadastro de inventor", 800, 550),
                                                false);
        }

        private string ObtenhaURL()
        {
            var URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual();
            URL = String.Concat(URL, "MP/cdInventor.aspx");
            return URL;
        }
    }
}