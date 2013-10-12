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
    public partial class ctrlProcurador : System.Web.UI.UserControl
    {
        public static event ProcuradorFoiSelecionadoEventHandler ProcuradorFoiSelecionado;
        public delegate void ProcuradorFoiSelecionadoEventHandler(IProcurador procurador);
        private const string ID_VIEW_PROCURADOR = "ID_VIEW_PROCURADOR";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa()
        {
            ViewState[ID_VIEW_PROCURADOR] = null;
            LimparControle();
            CarregueCombo();
        }

        private void CarregueCombo()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcurador>())
            {
                if (Procuradores == null)
                    Procuradores = new List<IProcurador>();

                foreach (IProcurador procurador in servico.ObtenhaProcuradorPeloNome(string.Empty, 50))
                {
                    var item = new RadComboBoxItem(procurador.Pessoa.Nome, procurador.Pessoa.ID.ToString());

                    item.Attributes.Add("MatriculaAPI", procurador.MatriculaAPI);
                    item.Attributes.Add("NumeroRegistroProfissional", procurador.NumeroRegistroProfissional);

                    cboProcurador.Items.Add(item);
                    item.DataBind();

                    Procuradores.Add(procurador);
                }
            }
        }

        private IList<IProcurador> Procuradores
        {
            get { return (IList<IProcurador>)ViewState[ID_VIEW_PROCURADOR]; }
            set { ViewState[ID_VIEW_PROCURADOR] = value; }
        }

        private void LimparControle()
        {
            var controle = cboProcurador as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboProcurador.ClearSelection();
        }

        protected void cboProcurador_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            IProcurador procurador = null;

            if (string.IsNullOrEmpty(((RadComboBox)sender).SelectedValue))
                return;

            int codigoSelecionado = int.Parse(((RadComboBox)sender).SelectedValue);
            procurador = Procuradores.ToList().Find(procurador1 => procurador1.Pessoa.ID == codigoSelecionado);

            if (ProcuradorFoiSelecionado != null)
                ProcuradorFoiSelecionado(procurador);
        }

        protected void cboProcurador_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcurador>())
            {
                cboProcurador.Items.Clear();
                Procuradores = new List<IProcurador>();

                foreach (IProcurador procurador in servico.ObtenhaProcuradorPeloNome(e.Text, 50))
                {
                    var item = new RadComboBoxItem(procurador.Pessoa.Nome, procurador.Pessoa.ID.ToString());

                    item.Attributes.Add("MatriculaAPI", procurador.MatriculaAPI);
                    item.Attributes.Add("NumeroRegistroProfissional", procurador.NumeroRegistroProfissional);

                    cboProcurador.Items.Add(item);
                    item.DataBind();

                    Procuradores.Add(procurador);
                }
            }
        }
    }
}