using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MP.Client.MP
{
    public partial class ctrlTemplateDeEmail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string TextoDoTemplate
        {
            get { return txtTextoDoTemplate.Content; }
            set { txtTextoDoTemplate.Content = value; }
        }

        public void AdicionaTextoNoTemplate(string texto)
        {
            txtTextoDoTemplate.Content = txtTextoDoTemplate.Content + " [" + texto + "] ";
        }

        public void Inicializa()
        {
            txtTextoDoTemplate.Content = "";
        }
    }
}