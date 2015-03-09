using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using PMP.Interfaces.Negocio.Filtros.Marca;
using PMP.Interfaces.Servicos;
using PMP.Interfaces.Utilidades;
using Telerik.Web.UI;

namespace PMP.Client.PMP
{
    public partial class frmPesquisaDeMarca : System.Web.UI.Page
    {

        private const string CHAVE_FILTRO_APLICADO = "CHAVE_FILTRO_APLICADO_PESQUISA_DE_MARCA";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ExibaTelaInicial();
        }

        private void ExibaTelaInicial()
        {
            Control controle1 = pnlFiltro;
            UtilidadesWeb.LimparComponente(ref controle1);

            Control controle2 = rdkProcessosDeMarcas;
            UtilidadesWeb.LimparComponente(ref controle2);

            ctrlOperacaoFiltro1.Inicializa();

            ctrlOperacaoFiltro1.Codigo = OperacaoDeFiltro.EmQualquerParte.ID.ToString();
            grdProcessosDeMarcas.DataSource = new List<DTOProcessoMarcaRevista>();
            grdProcessosDeMarcas.DataBind();
        }

        private IFiltro FiltroAplicado
        {
            get { return (IFiltro)ViewState[CHAVE_FILTRO_APLICADO]; }
            set { ViewState[CHAVE_FILTRO_APLICADO] = value; }
        }

        private void MostraProcessos(IFiltro filtro, int quantidadeDeProcessos, int offSet)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarcaDeRevista>())
            {
                grdProcessosDeMarcas.VirtualItemCount = servico.ObtenhaQuantidadeDeResultadoDaPesquisa(filtro);
                grdProcessosDeMarcas.DataSource = servico.ObtenhaResultadoDaPesquisa(filtro, quantidadeDeProcessos,
                                                                                     offSet);
                grdProcessosDeMarcas.DataBind();
            }

        }
        

        protected void cboTipoDeFiltro_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
         
        }

        protected void btnPesquisarPorTitular_OnClick_(object sender, ImageClickEventArgs e)
        {
            var operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPorTitular>();
            filtro.ValorDoFiltro = txtTitular.Text;
            filtro.UF = txtUF.Text;
            filtro.Pais = txtPais.Text;
            filtro.Operacao = operacao;

            FiltroAplicado = filtro;
            //Tratar o esquema da revista

            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
        }

        protected void btnPesquisarPorMarca_OnClick(object sender, ImageClickEventArgs e)
        {
            var operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPorMarca>();
            filtro.ValorDoFiltro = txtMarca.Text;
            filtro.NCL = txtNCL.Text;
            filtro.Operacao = operacao;
            FiltroAplicado = filtro;
            //Tratar o esquema da revista

            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
        }

        protected void btnPesquisarPorProcesso_OnClick(object sender, ImageClickEventArgs e)
        {
            var operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPorNumeroDeProcesso>();
            filtro.ValorDoFiltro = txtProcesso.Text;
            filtro.Operacao = operacao;

            FiltroAplicado = filtro;
            //Tratar o esquema da revista

            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
        }

        protected void btnPesquisarPorProcurador_OnClick(object sender, ImageClickEventArgs e)
        {
            var operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPorProcurador>();
            filtro.ValorDoFiltro = txtProcurador.Text;
            filtro.Operacao = operacao;
            FiltroAplicado = filtro;
            //Tratar o esquema da revista

            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
        }

        protected void grdProcessosDeMarcas_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                var offSet = 0;

                if (e.NewPageIndex > 0)
                    offSet = e.NewPageIndex * grdProcessosDeMarcas.PageSize;

                MostraProcessos(FiltroAplicado, grdProcessosDeMarcas.PageSize, offSet);

            }
        }

        protected void grdProcessosDeMarcas_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            
        }

        protected void grdProcessosDeMarcas_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            
        }

        
    }
}