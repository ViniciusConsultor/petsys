using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using PMP.Interfaces.Negocio.Filtros.Marca;
using PMP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace PMP.Client.PMP
{
    public partial class frmDetalharPesquisaDeMarca : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
                MostreDetalhes(Request.QueryString["Id"]);
        }

        private void MostreDetalhes(string ID)
        {
            
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarcaDeRevista>())
            {
                var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPorId>();
                filtro.ValorDoFiltro = ID;
                var processo =  servico.ObtenhaResultadoDaPesquisa(filtro, 1, 0)[0] ;

            }


        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            
        }

        protected override string ObtenhaIdFuncao()
        {
            return "";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }
    }
}