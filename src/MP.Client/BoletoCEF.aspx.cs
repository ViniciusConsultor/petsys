using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoletoNet;

namespace MP.Client
{
    public partial class BoletoCEF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CHAVE_BOLETO"] != null)
            {
                var boleto = (BoletoBancario) Session["CHAVE_BOLETO"];
                pnlBoletos.Controls.Add(boleto);
            }
            
        }
    }
}