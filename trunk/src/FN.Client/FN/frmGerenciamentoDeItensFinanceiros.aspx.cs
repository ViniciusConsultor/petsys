using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.FN.Negocio;
using Compartilhados.Interfaces.FN.Servicos;
using FN.Client.FN.Relatorios;
using FN.Interfaces.Negocio.Filtros.GerenciamentoDeItensFinanceiros;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class frmGerenciamentoDeItensFinanceiros : SuperPagina
    {
        private const string CHAVE_FILTRO_APLICADO = "CHAVE_FILTRO_APLICADO_GERENCIAMENTO_ITENS_FINANCEIROS";
        //private const int NUMERO_CELULA_ID_CLIENTE = 9;
        private const int NUMERO_CELULA_ID_ITEM_FINANCEIRO = 6;
        
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
                grdItensFinanceiros.VirtualItemCount = servico.ObtenhaQuantidadeDeItensFinanceiros(filtro);
                grdItensFinanceiros.DataSource = servico.ObtenhaItensFinanceiros(filtro, quantidadeDeProcessos, offSet);
                grdItensFinanceiros.DataBind();
                ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnRelatorio")).Visible = grdItensFinanceiros.VirtualItemCount > 0;
            }
        }

        private void EscondaTodosOsPanelsDeFiltro()
        {
            pnlCliente.Visible = false;
            pnlPeriodoDeVencimento.Visible = false;
            pnlDescricao.Visible = false;
        }

        private void CarregaOpcoesDeFiltro()
        {
            cboTipoDeFiltro.Items.Clear();
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Cliente", "1"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Período de vencimento", "2"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Descrição", "3"));
        }

        private void ExibaTelaInicial()
        {
            Control controle1 = pnlFiltro;
            UtilidadesWeb.LimparComponente(ref controle1);

            Control controle2 = rdkItensFinanceirosDeRecebimento;
            UtilidadesWeb.LimparComponente(ref controle2);

            CarregaOpcoesDeFiltro();
            EscondaTodosOsPanelsDeFiltro();
            pnlCliente.Visible = true;
            cboTipoDeFiltro.SelectedValue = "1";

            ctrlOperacaoFiltro1.Inicializa();
            ctrlCliente1.Inicializa();
            
            ctrlOperacaoFiltro1.Codigo = OperacaoDeFiltro.EmQualquerParte.ID.ToString();

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroItemFinanceiroRecebimentoSemFiltro>();
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensFinanceiros.PageSize, 0);

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnGerarContaAReceberColetivo")).Visible = false;
        }

        private string ObtenhaURLDeCadastroDeLancamentoFinanceiroDeRecebimento()
        {
            return String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/cdLancamentoFinanceiroRecebimento.aspx");
        }

        protected void btnNovo_Click()
        {
            var URL = ObtenhaURLDeCadastroDeLancamentoFinanceiroDeRecebimento();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(URL, "Novo lançamento financeiro de recebimento", 800, 550, "cdLancamentoFinanceiroRecebimento_aspx"), false);
        }

        private void Recarregue()
        {
            MostraItens(FiltroAplicado, grdItensFinanceiros.PageSize, 0);
        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {
                case "btnNovo":
                    btnNovo_Click();
                    break;
                case "btnRecarregar":
                    Recarregue();
                    break;
                case "btnGerarContaAReceberColetivo":
                    PreparaEmissaoDeContaAReceberColetivamente();
                    break;
                case "btnLimpar":
                    ExibaTelaInicial();
                    break;
                case "btnRelatorio":
                    GerarRelatorio();
                    break;
            }
        }

        private void PreparaEmissaoDeContaAReceberColetivamente()
        {
            var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmGerarContaAReceber.aspx",
                                           "?ItensFinanceiros=", obtenhaIdsDosItensSelecionados());
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(url,
                                                                               "Gerar conta a receber coletivamente",
                                                                               800, 550, "frmGerarContaAReceber_aspx"), false);
        }

        private string obtenhaIdsDosItensSelecionados()
        {
            var ids = new StringBuilder();

            foreach (GridDataItem dataItem in grdItensFinanceiros.MasterTableView.Items)
            {
                if ((dataItem.FindControl("CheckBox1") as CheckBox).Checked)
                    ids.Append(dataItem.Cells[NUMERO_CELULA_ID_ITEM_FINANCEIRO].Text + "|");

            }

            return ids.ToString().Remove(ids.ToString().LastIndexOf("|"));
        }

        protected void cboTipoDeFiltro_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            EscondaTodosOsPanelsDeFiltro();

            switch (cboTipoDeFiltro.SelectedValue)
            {
                case "1":
                    pnlCliente.Visible = true;
                    break;
                case "2":
                    pnlPeriodoDeVencimento.Visible = true;
                    break;
                case "3":
                    pnlDescricao.Visible = true;
                    break;
            }
        }

        private bool OpcaoDeOperacaodeFiltroEstaSelecionada()
        {
            return !string.IsNullOrEmpty(ctrlOperacaoFiltro1.Codigo);
        }

        private void ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro()
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione uma opção de filtro."), false);
        }

        protected void btnPesquisarPorCliente_OnClick_(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }

            var operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));

            if (operacao.Equals(OperacaoDeFiltro.Intervalo))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Para o filtro selecionado essa opção de filtro não está disponível."), false);
                return;
            }

            if (ctrlCliente1.ClienteSelecionado == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione um cliente."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroItemFinanceiroRecebimentoPorCliente>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlCliente1.ClienteSelecionado.Pessoa.ID.Value.ToString();
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensFinanceiros.PageSize, 0);
        }

        protected void grdItensFinanceiros_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                var offSet = 0;

                if (e.NewPageIndex > 0)
                    offSet = e.NewPageIndex * grdItensFinanceiros.PageSize;

                MostraItens(FiltroAplicado, grdItensFinanceiros.PageSize, offSet);

            }
        }


        protected void grdItensFinanceiros_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize" && e.CommandName != "ExpandCollapse")
                id = Convert.ToInt64((e.Item.Cells[NUMERO_CELULA_ID_ITEM_FINANCEIRO].Text));

            switch (e.CommandName)
            {
                //case "Cancelar":

                //    try
                //    {
                //        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
                //        {
                //            var itemLancamento = servico.Obtenha(id);
                //            itemLancamento.Situacao = Situacao.Cancelada;
                //            servico.Modifique(itemLancamento);
                //        }

                //        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                //                                                UtilidadesWeb.MostraMensagemDeInformacao(
                //                                                    "Item de lançamento financeiro cancelado com sucesso."), false);
                //        ExibaTelaInicial();
                //    }
                //    catch (BussinesException ex)
                //    {
                //        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                //                                                UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                //    }

                //    break;
                case "Modificar":
                    var url = String.Concat(ObtenhaURLDeCadastroDeLancamentoFinanceiroDeRecebimento(),
                                            "?Id=", id);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url,
                                                                                       "Modificar lançamento financeiro de recebimento",
                                                                                       800, 550, "cdLancamentoFinanceiroRecebimento_aspx"), false);
                    break;
                case "GerarContaAReceber":

                    var url2 = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmGerarContaAReceber.aspx",
                                           "?ItensFinanceiros=", id);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url2,
                                                                                       "Gerar conta a receber",
                                                                                       640, 480, "FN/frmGerarContaAReceber_aspx"), false);

                    break;
            }
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.FN.007";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        protected void btnPesquisarPorPeriodoDeVencimento_OnClick_(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }

            var operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));

            if (!operacao.Equals(OperacaoDeFiltro.Intervalo))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Para o filtro selecionado a única opção de filtro disponível é Intervalo."), false);
                return;
            }


            if (!txtPeriodo1.SelectedDate.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione o primeiro período da data de vencimento."), false);
                return;
            }

            if (!txtPeriodo2.SelectedDate.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione o segundo período da data de vencimento."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroItemFinanceiroRecebimentoPorIntervaloDAtaDeVencimento>();

            filtro.Operacao = operacao;
            filtro.AdicioneValoresDoFiltroParaEntre(txtPeriodo1.SelectedDate.Value.ToString("yyyyMMdd"),
                                                    txtPeriodo2.SelectedDate.Value.ToString("yyyyMMdd"));
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensFinanceiros.PageSize, 0);

        }

        protected void btnPesquisarPorDescricao_OnClick_(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }

            var operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));

            if (operacao.Equals(OperacaoDeFiltro.Intervalo))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Para o filtro selecionado essa opção de filtro não está disponível."), false);
                return;
            }

            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Informe uma descrição."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroItemFinanceiroRecebimentoPorDescricao>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = txtDescricao.Text;
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensFinanceiros.PageSize, 0);
        }

        protected void ToggleRowSelection(object sender, EventArgs e)
        {
            ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
            bool checkHeader = true;
            var quantidadeDeItensSelecionado = 0;

            foreach (GridDataItem dataItem in grdItensFinanceiros.MasterTableView.Items)
            {
                if (!(dataItem.FindControl("CheckBox1") as CheckBox).Checked)
                    checkHeader = false;

                if ((dataItem.FindControl("CheckBox1") as CheckBox).Checked)
                    quantidadeDeItensSelecionado += 1;
            }

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnGerarContaAReceberColetivo")).Visible = quantidadeDeItensSelecionado > 0;
            GridHeaderItem headerItem = grdItensFinanceiros.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            (headerItem.FindControl("headerChkbox") as CheckBox).Checked = checkHeader;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var headerCheckBox = (sender as CheckBox);
            var mostrarBotaoBoletoColetivo = headerCheckBox.Checked;
            
            foreach (GridDataItem dataItem in grdItensFinanceiros.MasterTableView.Items)
            {
                (dataItem.FindControl("CheckBox1") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;

             }

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnGerarContaAReceberColetivo")).Visible = mostrarBotaoBoletoColetivo;
        }

        private void GerarRelatorio()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
            {
                var itensDeLancamento = servico.ObtenhaItensFinanceiros(FiltroAplicado, int.MaxValue, 0);
                var geradorDeRelatorioGeral = new GeradorDeRelatorioDeContasAReceber(itensDeLancamento);
                var nomeDoArquivo = geradorDeRelatorioGeral.GereRelatorio();
                var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" + nomeDoArquivo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);
            }
        }
    }
}