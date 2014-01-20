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
    public partial class ctrlDespachoDePatentes : System.Web.UI.UserControl
    {
        public static event DespachoDePatentesFoiSelecionadaEventHandler DespachoDePatentesFoiSelecionada;
        public delegate void DespachoDePatentesFoiSelecionadaEventHandler(IDespachoDePatentes despachoDePatentes);

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
            var controlePanel = cboDespachoDePatentes as Control;
            UtilidadesWeb.LimparComponente(ref controlePanel);
            DespachoDePatentesSelecionada = null;
            cboDespachoDePatentes.ClearSelection();
            BotaoNovoEhVisivel = false;
        }

        public bool EnableLoadOnDemand
        {
            set { cboDespachoDePatentes.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboDespachoDePatentes.ShowDropDownOnTextboxClick = value; }
        }

        public string CodigoDespachoDePatentes
        {
            get { return cboDespachoDePatentes.Text; }
            set { cboDespachoDePatentes.Text = value; }
        }

        public IDespachoDePatentes DespachoDePatentesSelecionada
        {
            get { return (IDespachoDePatentes)ViewState[ClientID]; }
            set { ViewState.Add(this.ClientID, value); }
        }

        public string TextoItemVazio
        {
            set { cboDespachoDePatentes.EmptyMessage = value; }
        }

        protected void cboDespachoDePatentes_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            IList<IDespachoDePatentes> listaDespachoDePatentes = new List<IDespachoDePatentes>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDePatentes>())
                listaDespachoDePatentes = servico.ObtenhaPorCodigoDoDespachoComoFiltro(e.Text, 50);
            
            if (listaDespachoDePatentes.Count <= 0) return;

            foreach (var despachoDePatentes in listaDespachoDePatentes)
            {
                var item = new RadComboBoxItem(despachoDePatentes.Codigo, despachoDePatentes.IdDespachoDePatente.Value.ToString());
                    
                item.Attributes.Add("Titulo", despachoDePatentes.Titulo);
                cboDespachoDePatentes.Items.Add(item);
                item.DataBind();
            }
        }

        protected void cboDespachoDePatentes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!cboDespachoDePatentes.AutoPostBack) return;

            IDespachoDePatentes despachoDePatentes = null;

            if (string.IsNullOrEmpty(((RadComboBox)o).SelectedValue))
            {
                LimparControle();
                return;
            }

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDePatentes>())
                despachoDePatentes = servico.obtenhaDespachoDePatentesPeloId(Convert.ToInt64(((RadComboBox)o).SelectedValue));
            
            DespachoDePatentesSelecionada = despachoDePatentes;

            if (DespachoDePatentesFoiSelecionada != null)
                DespachoDePatentesFoiSelecionada(despachoDePatentes);
            
        }

        public bool AutoPostBack
        {
            set { cboDespachoDePatentes.AutoPostBack = value; }
        }

        public ctrlDespachoDePatentes()
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
                                                UtilidadesWeb.ExibeJanela(URL, "Despacho de patentes", 800, 550), false);
        }

        private string ObtenhaURL()
        {
            var URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual();
            URL = String.Concat(URL, "MP/cdDespachoDePatentes.aspx");
            return URL;
        }
    }
}