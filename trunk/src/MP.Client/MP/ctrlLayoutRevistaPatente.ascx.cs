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
    public partial class ctrlLayoutRevistaPatente : System.Web.UI.UserControl
    {
        public static event LayoutPatenteFoiSelecionadoEventHandler LayoutPatenteFoiSelecionado;
        public delegate void LayoutPatenteFoiSelecionadoEventHandler(ILayoutRevistaPatente layoutRevistaPatente);
        private const string ID_VIEW_LAYOUT = "ID_VIEW_LAYOUT";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa()
        {
            ViewState[ID_VIEW_LAYOUT] = null;
            LimparControle();
            CarregueCombo();
        }

        private void LimparControle()
        {
            var controle = cboLayoutPatente as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboLayoutPatente.ClearSelection();
        }

        public string Codigo
        {
            get { return cboLayoutPatente.SelectedValue; }
            set { cboLayoutPatente.SelectedValue = value; }
        }

        private void CarregueCombo()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeLayoutRevistaPatente>())
            {
                if (LayoutRevistaPatentes == null)
                    LayoutRevistaPatentes = new List<ILayoutRevistaPatente>();

                foreach (ILayoutRevistaPatente layout in servico.SelecioneLayoutPeloNomeDoCampo(string.Empty, 50))
                {
                    var item = new RadComboBoxItem(layout.NomeDoCampo, layout.Codigo.ToString());
                    
                    item.Attributes.Add("DescricaoResumida", layout.DescricaoResumida);
                    item.Attributes.Add("TamanhoDoCampo", layout.TamanhoDoCampo.ToString());

                    cboLayoutPatente.Items.Add(item);
                    item.DataBind();

                    LayoutRevistaPatentes.Add(layout);
                }
            }
        }

        private IList<ILayoutRevistaPatente> LayoutRevistaPatentes
        {
            get { return (IList<ILayoutRevistaPatente>) ViewState[ID_VIEW_LAYOUT]; }
            set { ViewState[ID_VIEW_LAYOUT] = value; }
        }

        protected void cboLayoutPatente_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ILayoutRevistaPatente layoutRevistaPatente = null;

            if (string.IsNullOrEmpty(((RadComboBox)o).SelectedValue))
                return;

            int codigoSelecionado = int.Parse(((RadComboBox)o).SelectedValue);
            layoutRevistaPatente = LayoutRevistaPatentes.ToList().Find(layout => layout.Codigo == codigoSelecionado);

            if (LayoutPatenteFoiSelecionado != null)
                LayoutPatenteFoiSelecionado(layoutRevistaPatente);
        }

        protected void cboLayoutPatente_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeLayoutRevistaPatente>())
            {
                cboLayoutPatente.Items.Clear();
                LayoutRevistaPatentes = new List<ILayoutRevistaPatente>();

                foreach (ILayoutRevistaPatente layout in servico.SelecioneLayoutPeloNomeDoCampo(e.Text, 50))
                {
                    var item = new RadComboBoxItem(layout.NomeDoCampo, layout.Codigo.ToString());

                    item.Attributes.Add("DescricaoResumida", layout.DescricaoResumida);
                    item.Attributes.Add("TamanhoDoCampo", layout.TamanhoDoCampo.ToString());

                    cboLayoutPatente.Items.Add(item);
                    item.DataBind();

                    LayoutRevistaPatentes.Add(layout);
                }
            }
        }
    }
}