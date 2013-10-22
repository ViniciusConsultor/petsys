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
    public partial class ctrlTipoDePatente : System.Web.UI.UserControl
    {
        public static event TipoDePatenteFoiSelecionadaEventHandler TipoDePatenteFoiSelecionada;
        public delegate void TipoDePatenteFoiSelecionadaEventHandler(ITipoDePatente tipoDePatente);

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
            Control controlePanel = cboTipoDePatente;
            UtilidadesWeb.LimparComponente(ref controlePanel);
            TipoDePatenteSelecionada = null;
            cboTipoDePatente.ClearSelection();
            BotaoNovoEhVisivel = false;
        }

        public bool EnableLoadOnDemand
        {
            set { cboTipoDePatente.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboTipoDePatente.ShowDropDownOnTextboxClick = value; }
        }

        public string DescricaoTipoDePatente
        {
            get { return cboTipoDePatente.Text; }
            set { cboTipoDePatente.Text = value; }
        }

        public string SiglaTipo
        {
            get { return cboTipoDePatente.Attributes["SiglaTipo"]; }
            set { cboTipoDePatente.Attributes["SiglaTipo"] = value; }
        }

        public ITipoDePatente TipoDePatenteSelecionada
        {
            get { return (ITipoDePatente)ViewState[ClientID]; }
            set { ViewState.Add(this.ClientID, value); }
        }

        public string TextoItemVazio
        {
            set { cboTipoDePatente.EmptyMessage = value; }
        }

        protected void cboTipoDePatente_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            IList<ITipoDePatente> listaTiposDePatente = new List<ITipoDePatente>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTipoDePatente>())
            {
                listaTiposDePatente = servico.obtenhaTipoDePatentePelaDescricaoComoFiltro(e.Text, 50);
            }

            if (listaTiposDePatente.Count > 0)
            {
                foreach (var tipoDePatente in listaTiposDePatente)
                {
                    var item = new RadComboBoxItem(tipoDePatente.DescricaoTipoDePatente, tipoDePatente.IdTipoDePatente.Value.ToString());

                    item.Attributes.Add("SiglaTipo",
                                        tipoDePatente.SiglaTipo ?? "Não informada");

                    this.cboTipoDePatente.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        protected void cboTipoDePatente_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ITipoDePatente tipoDePatente = null;

            if (string.IsNullOrEmpty(((RadComboBox)o).SelectedValue))
                return;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTipoDePatente>())
            {
                tipoDePatente = servico.obtenhaTipoDePatentePeloId(Convert.ToInt64(((RadComboBox)o).SelectedValue));
            }

            TipoDePatenteSelecionada = tipoDePatente;

            if (TipoDePatenteFoiSelecionada != null)
            {
                TipoDePatenteFoiSelecionada(tipoDePatente);
            }
        }

        public bool AutoPostBack
        {
            set { cboTipoDePatente.AutoPostBack = value; }
        }

        public ctrlTipoDePatente()
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
                                                UtilidadesWeb.ExibeJanela(URL, "Tipos de Patentes", 800, 550),
                                                false);
        }

        private string ObtenhaURL()
        {
            var URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual();
            URL = String.Concat(URL, "MP/cdTipoDePatente.aspx");
            return URL;
        }
    }
}