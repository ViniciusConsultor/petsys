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
    public partial class ctrlProcurador : System.Web.UI.UserControl
    {
        public static event ProcuradorFoiSelecionadoEventHandler ProcuradorFoiSelecionado;
        public delegate void ProcuradorFoiSelecionadoEventHandler(IProcurador procurador);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa()
        {
            LimparControle();
        }

        public IProcurador ProcuradorSelecionado
        {
            get { return (IProcurador)ViewState[ClientID]; }
            set { ViewState.Add(ClientID, value); }
        }

        public string Nome
        {
            get { return cboProcurador.Text; }
            set { cboProcurador.Text = value; }
        }

        private void LimparControle()
        {
            var controle = cboProcurador as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboProcurador.ClearSelection();
            ProcuradorSelecionado = null;
            BotaoNovoEhVisivel = false;
        }

        protected void cboProcurador_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcurador>())
            {
                IProcurador procurador = null;

                if (string.IsNullOrEmpty(((RadComboBox)sender).SelectedValue))
                {
                    LimparControle();
                    return;
                }

                var id = Convert.ToInt64(((RadComboBox)sender).SelectedValue);
                procurador = servico.ObtenhaProcurador(id);

                ProcuradorSelecionado = procurador;

                if (ProcuradorFoiSelecionado != null)
                    ProcuradorFoiSelecionado(procurador);
            }
        }

        protected void cboProcurador_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcurador>())
            {
                cboProcurador.Items.Clear();

                foreach (var procurador in servico.ObtenhaProcuradorPeloNome(e.Text, 50))
                {
                    var item = new RadComboBoxItem(procurador.Pessoa.Nome, procurador.Pessoa.ID.ToString());

                    item.Attributes.Add("MatriculaAPI", procurador.MatriculaAPI);
                    item.Attributes.Add("NumeroRegistroProfissional", procurador.NumeroRegistroProfissional);

                    cboProcurador.Items.Add(item);
                    item.DataBind();
                }
            }

        }

        public bool EnableLoadOnDemand
        {
            set { cboProcurador.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboProcurador.ShowDropDownOnTextboxClick = value; }
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
                                                UtilidadesWeb.ExibeJanela(URL, "Procuradores", 800, 550),
                                                false);
        }

        private string ObtenhaURL()
        {
            var URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual();
            URL = String.Concat(URL, "MP/cdCadastroDeProcuradores.aspx");
            return URL;
        }
    }
}