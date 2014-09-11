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
    public partial class ctrlApresentacao : System.Web.UI.UserControl
    {
        public void Inicializa()
        {
            LimparControle();
            CarregueCombo();
        }

        private void LimparControle()
        {
            var controle = cboApresentacao as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboApresentacao.ClearSelection();
        }

        public string Codigo
        {
            get { return cboApresentacao.SelectedValue; }
            set
            {
                var apresentacao = Apresentacao.ObtenhaPorCodigo(Convert.ToInt32(value));

                if (apresentacao != null)
                {
                    cboApresentacao.SelectedValue = apresentacao.Codigo.ToString();
                    cboApresentacao.Text = apresentacao.Nome;
                }
            }
        }


        private void CarregueCombo()
        {
            foreach (var apresentacao in Apresentacao.ObtenhaTodas())
            {
                var item = new RadComboBoxItem(apresentacao.Nome, apresentacao.Codigo.ToString());

                item.Attributes.Add("Codigo", apresentacao.Codigo.ToString());

                cboApresentacao.Items.Add(item);
                item.DataBind();
            }
        }
    }
}