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
    public partial class ctrlPasta : System.Web.UI.UserControl
    {

        public static event PastaFoiSelecionadaEventHandler PastaFoiSelecionada;
        public delegate void PastaFoiSelecionadaEventHandler(IPasta pasta);

        public void Inicializa()
        {
            AutoPostBack = true;
            LimparControle();
        }

        private void LimparControle()
        {
            Control controlePanel = cboPasta;
            UtilidadesWeb.LimparComponente(ref controlePanel);
            PastaSelecionada = null;
            cboPasta.ClearSelection();
        }

        public bool EnableLoadOnDemand
        {
            set { cboPasta.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboPasta.ShowDropDownOnTextboxClick = value; }
        }

        public string Codigo
        {
            get { return cboPasta.Text; }
            set { cboPasta.Text = value; }
        }

        public IPasta PastaSelecionada
        {
            get { return (IPasta)ViewState[ClientID]; }
            set { ViewState.Add(ClientID, value); }
        }

        public string TextoItemVazio
        {
            set { cboPasta.EmptyMessage = value; }
        }


        protected void cboPasta_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            IList<IPasta> pastas = new List<IPasta>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePasta>())
                pastas = servico.obtenhaPeloCodigo(e.Text, 50);

            if (pastas.Count > 0)
                foreach (var pasta in pastas)
                {
                    var item = new RadComboBoxItem(pasta.Codigo, pasta.ID.Value.ToString());
                    item.Attributes.Add("Nome", pasta.Nome);
                    cboPasta.Items.Add(item);
                    item.DataBind();
                }
        }

        protected void cboPasta_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!cboPasta.AutoPostBack) return;
            
            IPasta pasta = null;

            if (string.IsNullOrEmpty(((RadComboBox)sender).SelectedValue))
            {
                PastaSelecionada = null;
                return;
            }

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePasta>())
                pasta = servico.obtenha(Convert.ToInt64(((RadComboBox)sender).SelectedValue));
            
            PastaSelecionada = pasta;

            if (PastaFoiSelecionada != null)
                PastaFoiSelecionada(pasta);
        }

        public bool AutoPostBack
        {
            set { cboPasta.AutoPostBack = value; }
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
                                                UtilidadesWeb.ExibeJanela(URL, "Pastas", 800, 550),
                                                false);
        }

        private string ObtenhaURL()
        {
            var URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual();
            URL = String.Concat(URL, "MP/cdPastas.aspx");
            return URL;
        }
    }
}