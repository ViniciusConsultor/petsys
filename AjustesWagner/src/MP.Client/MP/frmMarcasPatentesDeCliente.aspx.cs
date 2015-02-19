using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;
using MP.Interfaces.Negocio.Filtros.Patentes;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmMarcasPatentesDeCliente : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ExibaTelaInicial();
        }

        private void ExibaTelaInicial()
        {
            Control controle1 = pnlCliente;
            UtilidadesWeb.LimparComponente(ref controle1);

            Control controle2 = rdkProcessosDeMarcas;
            UtilidadesWeb.LimparComponente(ref controle2);

            Control controle3 = rdkProcessosDePatentes;
            UtilidadesWeb.LimparComponente(ref controle3);

            ctrlCliente.Inicializa();
            ctrlCliente.BotaoNovoEhVisivel = false;
            grdProcessosDePatentes.DataSource = new List<DTOProcessoDePatente>();
            grdProcessosDePatentes.VirtualItemCount = 0;
            grdProcessosDePatentes.DataBind();

            grdProcessosDeMarcas.DataSource = new List<DTOProcessoDeMarca>();
            grdProcessosDeMarcas.VirtualItemCount = 0;
            grdProcessosDeMarcas.DataBind();
        }

        private IFiltro ConstruaFiltroDeProcessosDeMarcasComClienteSelecionado()
        {
            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaPorCliente>();
            filtro.Operacao = OperacaoDeFiltro.IgualA;
            filtro.ValorDoFiltro = ctrlCliente.ClienteSelecionado.Pessoa.ID.Value.ToString();

            return filtro;
        }

        private IFiltro ConstruaFiltroDeProcessoDePatenteComClienteSelecionado()
        {
            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatentePorCliente>();
            filtro.Operacao = OperacaoDeFiltro.IgualA;
            filtro.ValorDoFiltro = ctrlCliente.ClienteSelecionado.Pessoa.ID.Value.ToString();

            return filtro;
        }

        private void Recarregue()
        {
            if (ctrlCliente.ClienteSelecionado == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione um cliente."), false);
                return;
            }

            MostraProcessosDeMarcas(ConstruaFiltroDeProcessosDeMarcasComClienteSelecionado(), grdProcessosDeMarcas.PageSize, 0);
            MostraProcessosDePatente(ConstruaFiltroDeProcessoDePatenteComClienteSelecionado(), grdProcessosDePatentes.PageSize, 0);
        }


        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {

                case "btnRecarregar":
                    Recarregue();
                    break;
            }
        }

        protected void btnPesquisarPorCliente_OnClick_(object sender, ImageClickEventArgs e)
        {
            Recarregue();
        }


        private void MostraProcessosDeMarcas(IFiltro filtro, int quantidadeDeProcessos, int offSet)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
            {
                grdProcessosDeMarcas.VirtualItemCount = servico.ObtenhaQuantidadeDeProcessosCadastrados(filtro, true);
                grdProcessosDeMarcas.DataSource = DTOProcessoDeMarca.ConvertaProcessoParaDTO(servico.ObtenhaProcessosDeMarcas(filtro, quantidadeDeProcessos, offSet, true));
                grdProcessosDeMarcas.DataBind();
            }

        }

        private void MostraProcessosDePatente(IFiltro filtro, int quantidadeDeProcessos, int offSet)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
            {
                grdProcessosDePatentes.VirtualItemCount = servico.ObtenhaQuantidadeDeProcessosCadastrados(filtro, true);
                grdProcessosDePatentes.DataSource = DTOProcessoDePatente.ConvertaProcessoParaDTO(servico.ObtenhaProcessosDePatentes(filtro, quantidadeDeProcessos, offSet, true));
                grdProcessosDePatentes.DataBind();
            }
        }

        protected void grdProcessosDeMarcas_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                var offSet = 0;

                if (e.NewPageIndex > 0)
                    offSet = e.NewPageIndex * grdProcessosDeMarcas.PageSize;

                MostraProcessosDeMarcas(ConstruaFiltroDeProcessosDeMarcasComClienteSelecionado(), grdProcessosDeMarcas.PageSize, offSet);
            }
        }

        private string ObtenhaURLDeCadastroDeCliente()
        {
            return String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "Nucleo/cdCliente.aspx");
        }

        protected void grdProcessosDeMarcas_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                id = Convert.ToInt64((e.Item.Cells[3].Text));

            switch (e.CommandName)
            {

                case "Modificar":
                    var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/cdProcessoDeMarca.aspx",
                                            "?Id=", id);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url,
                                                                                       "Modificar processo de marca",
                                                                                       800, 550, "MP_cdProcessoDeMarca_aspx"), false);
                    break;

                case "AbrirCliente":
                    var url3 = String.Concat(ObtenhaURLDeCadastroDeCliente(), "?Id=", e.CommandArgument);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url3,
                                                                                       "Cadastro de cliente",
                                                                                       800, 550, "Nucleo_cdCliente_aspx"), false);
                    break;
            }
        }

        protected void grdProcessosDeMarcas_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {

                var item = (GridDataItem)e.Item;


                ((LinkButton)(item["cliente"].Controls[0])).CommandArgument = item["idCliente"].Text;
                ((LinkButton)(item["cliente"].Controls[0])).CssClass = "hidelink";
            }
        }

        protected void grdProcessosDePatentes_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                var offSet = 0;

                if (e.NewPageIndex > 0)
                    offSet = e.NewPageIndex * grdProcessosDePatentes.PageSize;

                MostraProcessosDePatente(ConstruaFiltroDeProcessoDePatenteComClienteSelecionado(), grdProcessosDePatentes.PageSize, offSet);
            }
        }

        protected void grdProcessosDePatentes_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                id = Convert.ToInt64((e.Item.Cells[3].Text));

            switch (e.CommandName)
            {
                case "Modificar":
                    var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/cdProcessoDePatente.aspx",
                                            "?Id=", id);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url,
                                                                                       "Modificar processo de patente",
                                                                                       800, 550, "MP_cdProcessoDePatente_aspx"), false);
                    break;
            }
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.019";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }
    }
}