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
    public partial class ctrlNaturezaPatente : System.Web.UI.UserControl
    {
        public static event NaturezaPatenteFoiSelecionadaEventHandler NaturezaPatenteFoiSelecionada;
        public delegate void NaturezaPatenteFoiSelecionadaEventHandler(INaturezaPatente naturezaPatente);

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void Inicializa()
        {
            AutoPostBack = true;
            LimparControle();
        }

        private void LimparControle()
        {
            Control controlePanel = cboNaturezaPatente;
            UtilidadesWeb.LimparComponente(ref controlePanel);
            NaturezaPatenteFoiSelecionada = null;
            cboNaturezaPatente.ClearSelection();
            BotaoNovoEhVisivel = false;
        }

        public bool EnableLoadOnDemand
        {
            set { cboNaturezaPatente.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboNaturezaPatente.ShowDropDownOnTextboxClick = value; }
        }

        public string DescricaoNaturezaPatente
        {
            get { return cboNaturezaPatente.Attributes["Descricao"]; }
            set { cboNaturezaPatente.Attributes["Descricao"]  = value; }
        }

        public string SiglaTipo
        {
            get { return cboNaturezaPatente.Text; }
            set { cboNaturezaPatente.Text= value; }
        }

        public INaturezaPatente NaturezaPatenteSelecionada
        {
            get { return (INaturezaPatente)ViewState[ClientID]; }
            set { ViewState.Add(ClientID, value); }
        }

        public string TextoItemVazio
        {
            set { cboNaturezaPatente.EmptyMessage = value; }
        }

        protected void cboNaturezaPatente_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            IList<INaturezaPatente> listaNaturezaPatente = new List<INaturezaPatente>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeNaturezaPatente>())
                listaNaturezaPatente = servico.obtenhaNaturezaPatentePelaSiglaComoFiltro(e.Text, 50);

            if (listaNaturezaPatente.Count > 0)
            {
                foreach (var naturezaPatente in listaNaturezaPatente)
                {
                    var item = new RadComboBoxItem(naturezaPatente.SiglaNatureza, naturezaPatente.IdNaturezaPatente.Value.ToString());

                    item.Attributes.Add("Descricao", naturezaPatente.DescricaoNaturezaPatente ?? "Não informada");

                    cboNaturezaPatente.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        protected void cboNaturezaPatente_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            if (!cboNaturezaPatente.AutoPostBack) return;

            INaturezaPatente naturezaPatenteSelecionada = null;

            if (string.IsNullOrEmpty(((RadComboBox)o).SelectedValue))
            {
                LimparControle();
                return;
            }

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeNaturezaPatente>())
                naturezaPatenteSelecionada = servico.obtenhaNaturezaPatentePeloId(Convert.ToInt64(((RadComboBox)o).SelectedValue));

            NaturezaPatenteSelecionada = naturezaPatenteSelecionada;

            if (NaturezaPatenteFoiSelecionada != null)
                NaturezaPatenteFoiSelecionada(naturezaPatenteSelecionada);
        }

        public bool AutoPostBack
        {
            set { cboNaturezaPatente.AutoPostBack = value; }
        }

        public ctrlNaturezaPatente()
	    {
		    Load += Page_Load;
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
                                                UtilidadesWeb.ExibeJanela(URL, "Natureza de Patentes", 800, 550),
                                                false);
        }

        private string ObtenhaURL()
        {
            var URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual();
            URL = String.Concat(URL, "MP/cdNaturezaPatente.aspx");
            return URL;
        }
    }
}