using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using MP.Interfaces.Negocio;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class ctrlNatureza : System.Web.UI.UserControl
    {
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
            var controle = cboNatureza as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboNatureza.ClearSelection();
        }


        public string Codigo
        {
            get { return cboNatureza.SelectedValue; }
            set { cboNatureza.SelectedValue = value; }
        }


        private void CarregueCombo()
        {
            foreach (var natureza in Natureza.ObtenhaTodas())
            {
                var item = new RadComboBoxItem(natureza.Codigo.ToString(), natureza.Nome);

                item.Attributes.Add("Codigo", natureza.Codigo.ToString());

                cboNatureza.Items.Add(item);
                item.DataBind();
            }
        }
    }
}