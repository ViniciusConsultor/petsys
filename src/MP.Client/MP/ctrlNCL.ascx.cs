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
    public partial class ctrlNCL : System.Web.UI.UserControl
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
            var controle = cboNCL as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboNCL.ClearSelection();
        }


        public string Codigo
        {
            get { return cboNCL.SelectedValue; }
            set { cboNCL.SelectedValue = value; }
        }


        private void CarregueCombo()
        {
            foreach (var ncl in NCL.ObtenhaTodas())
            {
                var item = new RadComboBoxItem(ncl.Codigo.ToString(), ncl.Codigo.ToString());

                item.Attributes.Add("Descricao", ncl.Descricao);
                item.Attributes.Add("Natureza", ncl.NaturezaDeMarca.Nome);

                cboNCL.Items.Add(item);
                item.DataBind();
            }
        }
    }
}