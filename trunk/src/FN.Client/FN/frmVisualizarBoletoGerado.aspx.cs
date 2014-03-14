using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FN.Client.FN
{
    public partial class frmVisualizarBoletoGerado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            string Id = null;

            if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
                Id = Request.QueryString["Id"];

            if (Id == null)
                return;
            
            ExibaBoleto(Id);
        }

        private void ExibaBoleto(string id)
        {
            var boleto = Session[id] as Control;

            pnlBoletoGerado.Controls.Add(boleto);

            Session[id] = null;
        }


    }
}