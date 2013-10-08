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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa()
        {
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
                foreach (ILayoutRevistaPatente layout in servico.ObtenhaTodos())
                {
                    var item = new RadComboBoxItem(layout.NomeDoCampo, layout.NomeDoCampo);
                    
                    item.Attributes.Add("DescricaoResumida", layout.DescricaoResumida);
                    item.Attributes.Add("TamanhoDoCampo", layout.TamanhoDoCampo.ToString());

                    cboLayoutPatente.Items.Add(item);
                    item.DataBind();
                }
            }
        }
    }
}