using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SiteCHM
{
    public partial class servicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack) return;

            var controle = (HtmlGenericControl)Page.Master.FindControl("liServicos");
            controle.Attributes.Add("class", "selected");
        }
    }
}