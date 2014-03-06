using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.FN.Servicos;
using FN.Interfaces.Negocio.Filtros.ContasAReceber;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class frmContasAReceber : SuperPagina
    {
        private const string CHAVE_FILTRO_APLICADO = "CHAVE_FILTRO_APLICADO_CONTAS_A_RECEBER";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ExibaTelaInicial();
        }

        private IFiltro FiltroAplicado
        {
            get { return (IFiltro)ViewState[CHAVE_FILTRO_APLICADO]; }
            set { ViewState[CHAVE_FILTRO_APLICADO] = value; }
        }

        private void MostraItens(IFiltro filtro, int quantidadeDeProcessos, int offSet)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
            {
                grdItensDeContasAReceber.VirtualItemCount = servico.ObtenhaQuantidadeDeProcessosCadastrados(filtro);
                grdItensDeContasAReceber.DataSource = servico.ObtenhaProcessosDeMarcas(filtro, quantidadeDeProcessos, offSet);
                grdItensDeContasAReceber.DataBind();
            }

        }

        private void EscondaTodosOsPanelsDeFiltro()
        {
            pnlCliente.Visible = false;
        }

        private void CarregaOpcoesDeFiltro()
        {
            cboTipoDeFiltro.Items.Clear();
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Situacao", "1"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Cliente", "2"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Data de vencimento", "3"));
        }

        private void ExibaTelaInicial()
        {
            Control controle1 = pnlFiltro;
            UtilidadesWeb.LimparComponente(ref controle1);

            Control controle2 = rdkProcessosDeMarcas;
            UtilidadesWeb.LimparComponente(ref controle2);

            CarregaOpcoesDeFiltro();
            EscondaTodosOsPanelsDeFiltro();
            //pnlProcesso.Visible = true;
            cboTipoDeFiltro.SelectedValue = "7";

            ctrlOperacaoFiltro1.Inicializa();
            ctrlCliente1.Inicializa();

            ctrlOperacaoFiltro1.Codigo = OperacaoDeFiltro.EmQualquerParte.ID.ToString();

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroContaAReceberSemFiltro>();
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensDeContasAReceber.PageSize, 0);
        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void cboTipoDeFiltro_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnPesquisarPorCliente_OnClick_(object sender, ImageClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void grdItensDeContasAReceber_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            throw new NotImplementedException();
        }


        protected void grdItensDeContasAReceber_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.FN.001";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }
    }
}