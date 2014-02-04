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
    public partial class ctrlMes : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa()
        {
            LimparControle();
            CarregueCombo();
        }

        private void CarregueCombo()
        {
            foreach (var mes in Mes.ObtenhaTodas())
            {
                var item = new RadComboBoxItem(mes.Descricao, mes.Codigo.ToString());

                item.Attributes.Add("Codigo", mes.Codigo.ToString());

                cboMes.Items.Add(item);
                item.DataBind();
            }
        }

        private void LimparControle()
        {
            var controle = cboMes as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboMes.ClearSelection();
        }

        public string Codigo
        {
            get { return cboMes.SelectedValue; }
            set
            {
                var mes = Mes.ObtenhaPorCodigo(Convert.ToInt32(value));

                if (mes != null)
                {
                    cboMes.SelectedValue = mes.Codigo.ToString();
                    cboMes.Text = mes.Descricao;
                }
            }
        }


    }

}