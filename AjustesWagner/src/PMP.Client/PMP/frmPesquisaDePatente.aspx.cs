using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Telerik.Web.UI;

namespace PMP.Client.PMP
{
    public partial class frmPesquisaDePatente :  SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.PMP.003";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
        }
    }
}