using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Fabricas;
using PMP.Interfaces.Servicos;

namespace PMP.Client.PMP
{
    public partial class frmProcessarEmLote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcessar_OnClick(object sender, EventArgs e)
        {
           using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarcaDeRevista>())
           {
               servico.ProcesseEmLote(TextBox1.Text);
           }
        }
    }
}