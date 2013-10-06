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
    public partial class ctrlProcedimentosInternos : System.Web.UI.UserControl
    {
        public static event ProcedimentosInternosFoiSelecionadoEventHandler ProcedimentosInternosFoiSelecionado;
        public delegate void ProcedimentosInternosFoiSelecionadoEventHandler(IProcedimentosInternos procedimentosInternos);

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
            Control controlePanel = pnlProcedimentosInternos;
            UtilidadesWeb.LimparComponente(ref controlePanel);
            ProcedimentosInternosSelecionado = null;
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

        public IProcedimentosInternos ProcedimentosInternosSelecionado
        {
            get { return (IProcedimentosInternos)ViewState[ClientID]; }
            set { ViewState.Add(this.ClientID, value); }
        }

        public bool EhObrigatorio
        {
            set { rfvProcedimentosInternos.Enabled = value; }
        }

        public string TextoItemVazio
        {
            set { cboProcedimentosInternos.EmptyMessage = value; }
        }

        protected void cboProcedimentosInternos_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            IList<IProcedimentosInternos> listaProcedimentosInternos = new List<IProcedimentosInternos>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcedimentosInternos>())
            {
                listaProcedimentosInternos = servico.obtenhaTodosProcedimentosInternos();
            }

            if (listaProcedimentosInternos.Count > 0)
            {
                foreach (var procedimentoInterno in listaProcedimentosInternos)
                {
                    var item = new RadComboBoxItem(procedimentoInterno.IdTipoAndamentoInterno.Value.ToString(), procedimentoInterno.IdTipoAndamentoInterno.Value.ToString());

                    item.Attributes.Add("DescricaoTipo",
                                        procedimentoInterno.DescricaoTipo ?? "Não informada");

                    this.cboProcedimentosInternos.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        protected void cboProcedimentosInternos_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            IProcedimentosInternos procedimentoInterno = null;

            if (string.IsNullOrEmpty(((RadComboBox)o).SelectedValue))
                return;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcedimentosInternos>())
            {
                procedimentoInterno = servico.obtenhaProcedimentosInternosPeloId(Convert.ToInt64(((RadComboBox)o).SelectedValue));
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

        public ctrlProcedimentosInternos()
	    {
		    Load += Page_Load;
	    }
    }
}