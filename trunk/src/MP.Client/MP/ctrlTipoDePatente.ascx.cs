using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            EhObrigatorio = false;
            AutoPostBack = true;
            LimparControle();
        }

        private void LimparControle()
        {
            Control controlePanel = pnlTipoDePatente;
            UtilidadesWeb.LimparComponente(ref controlePanel);
            TipoDePatenteSelecionada = null;
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

        public bool EhObrigatorio
        {
            set { rfvTipoDePatente.Enabled = value; }
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
                listaTiposDePatente = servico.obtenhaTodosTiposDePatentes();
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
    }
}