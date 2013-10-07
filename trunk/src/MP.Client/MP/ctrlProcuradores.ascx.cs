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
    public partial class ctrlProcuradores : System.Web.UI.UserControl
    {
        public static event ProcuradorFoiSelecionadoEventHandler ProcuradorFoiSelecionado;
        public delegate void ProcuradorFoiSelecionadoEventHandler(IProcurador procurador);

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
            Control controlePanel = pnlProcuradores;
            UtilidadesWeb.LimparComponente(ref controlePanel);
            ProcuradorSelecionado = null;
        }

        public bool EnableLoadOnDemand
        {
            set { cboProcuradores.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboProcuradores.ShowDropDownOnTextboxClick = value; }
        }

        public long? IdProcurador
        {
            get { return long.Parse(cboProcuradores.Text); }
            set { cboProcuradores.Text = value.ToString(); }
        }

        public string Nome
        {
            get { return cboProcuradores.Attributes["Nome"]; }
            set { cboProcuradores.Attributes["Nome"] = value; }
        }

        public IProcurador ProcuradorSelecionado
        {
            get { return (IProcurador)ViewState[ClientID]; }
            set { ViewState.Add(this.ClientID, value); }
        }

        public bool EhObrigatorio
        {
            set { rfvProcurador.Enabled = value; }
        }

        public string TextoItemVazio
        {
            set { cboProcuradores.EmptyMessage = value; }
        }

        protected void cboProcuradores_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            IList<IProcurador> listaProcuradores = new List<IProcurador>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcurador>())
                listaProcuradores = servico.ObtenhaTodosProcuradores();

            if (listaProcuradores.Count > 0)
            {
                foreach (var procurador in listaProcuradores)
                {
                    var item = new RadComboBoxItem(procurador.Pessoa.ID.ToString(), procurador.Pessoa.ID.ToString());
                    item.Attributes.Add("Nome", procurador.Pessoa.Nome ?? "Não informado");
                    cboProcuradores.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        protected void cboProcuradores_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            IProcurador procurador = null;

            if (string.IsNullOrEmpty(((RadComboBox)o).SelectedValue))
                return;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcurador>())
                procurador = servico.ObtenhaProcuradorPeloId(Convert.ToInt64(((RadComboBox)o).SelectedValue));

            ProcuradorSelecionado = procurador;

            if (ProcuradorSelecionado != null)
                ProcuradorFoiSelecionado(procurador);
        }

        public bool AutoPostBack
        {
            set { cboProcuradores.AutoPostBack = value; }
        }

        public ctrlProcuradores()
	    {
		    Load += Page_Load;
	    }
    }
}