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
    public partial class frmLeituraRevistaPatente : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                CarreguePaginaInicial();
        }

        private void CarreguePaginaInicial()
        {
            CarregueRevistasAProcessar();
            CarregueRevistasProcessadas();
        }

        private void CarregueRevistasAProcessar()
        {
            grdRevistasAProcessar.DataSource = new List<IRevistaDePatente>();
        }

        private void CarregueRevistasProcessadas()
        {
            grdRevistasJaProcessadas.DataSource = new List<IRevistaDePatente>();
        }

        protected void grdRevistasAProcessar_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void grdRevistasAProcessar_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdRevistasAProcessar_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        protected void grdRevistasJaProcessadas_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void grdRevistasJaProcessadas_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdRevistasJaProcessadas_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        protected void gridRevistaProcessos_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void gridRevistaProcessos_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void gridRevistaProcessos_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        protected void grdFiltros_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void grdFiltros_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdFiltros_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        protected void uplRevistaPatente_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {

        }

        protected void btnFiltrar_ButtonClick(object sender, EventArgs e)
        {
        }

        protected void btnLimpar_ButtonClick(object sender, EventArgs e)
        {
        }

        protected override string ObtenhaIdFuncao()
        {
            return "";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
        }
    }
}