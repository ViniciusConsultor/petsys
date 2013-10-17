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
    public partial class ctrlTipoProcedimentoInterno : System.Web.UI.UserControl
    {
        public static event ProcedimentosInternosFoiSelecionadoEventHandler ProcedimentosInternosFoiSelecionado;
        public delegate void ProcedimentosInternosFoiSelecionadoEventHandler(ITipoDeProcedimentoInterno procedimentosInternos);

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
            Control controlePanel = cboProcedimentosInternos;
            UtilidadesWeb.LimparComponente(ref controlePanel);
            ProcedimentosInternosSelecionado = null;
            cboProcedimentosInternos.ClearSelection();
        }

        public bool EnableLoadOnDemand
        {
            set { cboProcedimentosInternos.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboProcedimentosInternos.ShowDropDownOnTextboxClick = value; }
        }

        public string IdTipoAndamentoInterno
        {
            get { return cboProcedimentosInternos.Text; }
            set { cboProcedimentosInternos.Text = value; }
        }

        public string DescricaoTipo
        {
            get { return cboProcedimentosInternos.Attributes["DescricaoTipo"]; }
            set { cboProcedimentosInternos.Attributes["DescricaoTipo"] = value; }
        }

        public ITipoDeProcedimentoInterno ProcedimentosInternosSelecionado
        {
            get { return (ITipoDeProcedimentoInterno)ViewState[ClientID]; }
            set { ViewState.Add(this.ClientID, value); }
        }

        public string TextoItemVazio
        {
            set { cboProcedimentosInternos.EmptyMessage = value; }
        }

        protected void cboProcedimentosInternos_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            IList<ITipoDeProcedimentoInterno> listaProcedimentosInternos = new List<ITipoDeProcedimentoInterno>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTipoDeProcedimentoInterno>())
            {
                listaProcedimentosInternos = servico.obtenhaTipoProcedimentoInternoPelaDescricao(e.Text);
            }

            if (listaProcedimentosInternos.Count > 0)
            {
                foreach (var procedimentoInterno in listaProcedimentosInternos)
                {
                    var item = new RadComboBoxItem(procedimentoInterno.Id.Value.ToString(), procedimentoInterno.Id.Value.ToString());

                    item.Attributes.Add("DescricaoTipo",
                                        procedimentoInterno.Descricao ?? "Não informada");

                    this.cboProcedimentosInternos.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        protected void cboProcedimentosInternos_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ITipoDeProcedimentoInterno procedimentoInterno = null;

            if (string.IsNullOrEmpty(((RadComboBox)o).SelectedValue))
                return;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTipoDeProcedimentoInterno>())
            {
                procedimentoInterno = servico.obtenhaTipoProcedimentoInternoPeloId(Convert.ToInt64(((RadComboBox)o).SelectedValue));
            }

            ProcedimentosInternosSelecionado = procedimentoInterno;

            if (ProcedimentosInternosFoiSelecionado != null)
            {
                ProcedimentosInternosFoiSelecionado(procedimentoInterno);
            }
        }

        public bool AutoPostBack
        {
            set { cboProcedimentosInternos.AutoPostBack = value; }
        }

        public ctrlTipoProcedimentoInterno()
	    {
		    Load += Page_Load;
	    }
    }
}