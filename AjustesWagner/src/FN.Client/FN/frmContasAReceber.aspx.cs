using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.FN.Negocio;
using Compartilhados.Interfaces.FN.Servicos;
using FN.Client.FN.Relatorios;
using FN.Interfaces;
using FN.Interfaces.Negocio.Filtros.ContasAReceber;
using FN.Interfaces.Servicos;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class frmContasAReceber : SuperPagina
    {
        private const string CHAVE_FILTRO_APLICADO = "CHAVE_FILTRO_APLICADO_CONTAS_A_RECEBER";
        private const int NUMERO_CELULA_FORMA_RECEBIMENTO = 16;
        private const int NUMERO_CELULA_SITUACAO = 17;
        private const int NUMERO_CELULA_NUMERO_BOLETO = 19;
        private const int NUMERO_CELULA_ID_CLIENTE = 10;
        private const int NUMERO_CELULA_ID_ITEM_FINANCEIRO = 8;
        private const int NUMERO_CELULA_GERAR_BOLETO = 5;
        private const int NUMERO_CELULA_CANCELAR = 6;
        private const int NUMERO_CELULA_RECEBER = 7;
        private const string CHAVE_ID_ITEM_SELECIONADO = "CHAVE_ID_ITEM_SELECIONADO";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlDataDePagamentoContaAReceber1.UsuarioPediuParaGravar += RecebaContaComADataInformada;
            ctrlDataDePagamentoContaAReceber2.UsuarioPediuParaGravar += RecebaContasColetivamente;

            if (!IsPostBack)
                ExibaTelaInicial();
        }

        private void RecebaContaComADataInformada()
        {
            try
            {
                divJanelaParaConfirmarData.Visible = false;
                var dataInformada = ctrlDataDePagamentoContaAReceber1.DataInformada();
                RecebaLancamento((long)ViewState[CHAVE_ID_ITEM_SELECIONADO], dataInformada.Value);

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                   UtilidadesWeb.MostraMensagemDeInformacao(
                                                                      "O Item de lançamento de conta a receber foi recebido com sucesso."), false);

                Recarregue();
            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
            }

           
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
                grdItensDeContasAReceber.VirtualItemCount = servico.ObtenhaQuantidadeDeItensFinanceiros(filtro);
                grdItensDeContasAReceber.DataSource = servico.ObtenhaItensFinanceiros(filtro, quantidadeDeProcessos, offSet);
                grdItensDeContasAReceber.DataBind();
                ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnRelatorio")).Visible = grdItensDeContasAReceber.VirtualItemCount > 0;
            }
        }

        private void EscondaTodosOsPanelsDeFiltro()
        {
            pnlCliente.Visible = false;
            pnlPeriodoDeVencimento.Visible = false;
            pnlSituacao.Visible = false;
            pnlDescricao.Visible = false;
            pnlFormaDeRecebimento.Visible = false;
            pnlVencidos.Visible = false;
            pnlNumeroDoBoleto.Visible = false;
            pnlTipoDeLancamentoFinanceiroRecebimento.Visible = false;
        }

        private void CarregaOpcoesDeFiltro()
        {
            cboTipoDeFiltro.Items.Clear();
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Situacao", "1"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Cliente", "2"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Período de vencimento", "3"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Descrição", "4"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Forma de recebimento", "5"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Vencidos", "6"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Número do boleto", "7"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Tipo de lançamento", "8"));
        }

        private void ExibaTelaInicial()
        {
            Control controle1 = pnlFiltro;
            UtilidadesWeb.LimparComponente(ref controle1);

            Control controle2 = rdkContasAReceber;
            UtilidadesWeb.LimparComponente(ref controle2);

            CarregaOpcoesDeFiltro();
            EscondaTodosOsPanelsDeFiltro();
            pnlSituacao.Visible = true;
            cboTipoDeFiltro.SelectedValue = "1";

            pnlOpcaoDeFiltro.Visible = true;

            ctrlOperacaoFiltro1.Inicializa();
            ctrlCliente1.Inicializa();
            var situacoesADesconsiderar = new List<Situacao>();

            situacoesADesconsiderar.Add(Situacao.AguardandoCobranca);
            ctrlSituacao.Inicializa(situacoesADesconsiderar);

            ctrlFormaRecebimento.Inicializa();
            ctrlOperacaoFiltro1.Codigo = OperacaoDeFiltro.IgualA.ID.ToString();

            ctrlSituacao.Codigo = Situacao.CobrancaEmAberto.ID.ToString();

            divJanelaParaConfirmarData.Visible = false;
            divJanelaParaConfirmarDataColetivo.Visible = false;

            ctrlTipoLacamentoFinanceiroRecebimento.Inicializa(new List<TipoLacamentoFinanceiroRecebimento>());

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroContaAReceberSemFiltro>();
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensDeContasAReceber.PageSize, 0);

            ViewState[CHAVE_ID_ITEM_SELECIONADO] = null;
            EscondaBotoesColetivo();

        }

        private string ObtenhaURLDeContaAReceber()
        {
            return String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/cdContaAReceber.aspx");
        }

        protected void btnNovo_Click()
        {
            var URL = ObtenhaURLDeContaAReceber();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(URL, "Nova conta a receber", 800, 550, "FN_cdContaAReceber_aspx"), false);
        }

        private void Recarregue()
        {
            EscondaBotoesColetivo();
            MostraItens(FiltroAplicado, grdItensDeContasAReceber.PageSize, 0);
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
                case "btnGerarBoletoColetivo":
                    PreparaEmissaoDeBoletoColetivamente();
                    break;
                case "btnLimpar":
                    ExibaTelaInicial();
                    break;
                case "btnRelatorio":
                    GerarRelatorio();
                    break;
                case "btnReceberContaColetivo":
                    divJanelaParaConfirmarDataColetivo.Visible = true;
                    break;

            }
        }

        private void RecebaContasColetivamente()
        {
            divJanelaParaConfirmarDataColetivo.Visible = false;
            try
            {
                foreach (GridDataItem dataItem in grdItensDeContasAReceber.MasterTableView.Items)
                    if ((dataItem.FindControl("CheckBox1") as CheckBox).Checked)
                        RecebaLancamento(Convert.ToInt64(dataItem.Cells[NUMERO_CELULA_ID_ITEM_FINANCEIRO].Text), ctrlDataDePagamentoContaAReceber2.DataInformada().Value);

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                             UtilidadesWeb.MostraMensagemDeInformacao(
                                                                 "Os Itens de lançamento de conta a receber foram recebidos com sucesso."), false);

                Recarregue();
            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
            }

        }

        private void PreparaEmissaoDeBoletoColetivamente()
        {
            var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmBoletoAvulso.aspx",
                                           "?ItensFinanceiros=", obtenhaIdsDosItensSelecionados());
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(url,
                                                                               "Gerar boleto coletivamente",
                                                                               800, 550, "FN_frmBoletoAvulso_aspx"), false);

            EscondaBotoesColetivo();

        }

        private string obtenhaIdsDosItensSelecionados()
        {
            var ids = new StringBuilder();

            foreach (GridDataItem dataItem in grdItensDeContasAReceber.MasterTableView.Items)
            {
                if ((dataItem.FindControl("CheckBox1") as CheckBox).Checked)
                    ids.Append(dataItem.Cells[NUMERO_CELULA_ID_ITEM_FINANCEIRO].Text + "|");

            }

            return ids.ToString().Remove(ids.ToString().LastIndexOf("|"));
        }

        protected void cboTipoDeFiltro_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            EscondaTodosOsPanelsDeFiltro();

            pnlOpcaoDeFiltro.Visible = true;

            switch (cboTipoDeFiltro.SelectedValue)
            {
                case "1":
                    pnlSituacao.Visible = true;
                    break;
                case "2":
                    pnlCliente.Visible = true;
                    break;
                case "3":
                    pnlPeriodoDeVencimento.Visible = true;
                    pnlOpcaoDeFiltro.Visible = true;
                    break;
                case "4":
                    pnlDescricao.Visible = true;
                    break;
                case "5":
                    pnlFormaDeRecebimento.Visible = true;
                    break;
                case "6":
                    pnlVencidos.Visible = true;
                    pnlOpcaoDeFiltro.Visible = false;
                    break;
                case "7":
                    pnlNumeroDoBoleto.Visible = true;
                    break;
                case "8":
                    pnlTipoDeLancamentoFinanceiroRecebimento.Visible = true;
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

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroContaAReceberPorCliente>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlCliente1.ClienteSelecionado.Pessoa.ID.Value.ToString();
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensDeContasAReceber.PageSize, 0);
            EscondaBotoesColetivo();
        }

        protected void grdItensDeContasAReceber_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                var offSet = UtilidadesWeb.ObtenhaOffSet(e, grdItensDeContasAReceber.PageSize);
                MostraItens(FiltroAplicado, grdItensDeContasAReceber.PageSize, offSet);

            }
        }


        protected void grdItensDeContasAReceber_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize" && e.CommandName != "ExpandCollapse")
                id = Convert.ToInt64((e.Item.Cells[NUMERO_CELULA_ID_ITEM_FINANCEIRO].Text));

            switch (e.CommandName)
            {
                case "Cancelar":

                    try
                    {
                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
                        {
                            var itemLancamento = servico.Obtenha(id);
                            itemLancamento.Situacao = Situacao.Cancelada;
                            servico.Modifique(itemLancamento);
                        }

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInformacao(
                                                                    "Item de lançamento de conta a receber cancelado com sucesso."), false);

                        var grid = sender as RadGrid;

                        var offset = UtilidadesWeb.ObtenhaOffSet(grid.CurrentPageIndex, grid.PageSize, grid.VirtualItemCount - 1);
                        MostraItens(FiltroAplicado, UtilidadesWeb.ObtenhaQuantidadeDeItensDaPagina(grid.Items.Count - 1, grid.PageSize), offset);
                        EscondaBotoesColetivo();

                    }
                    catch (BussinesException ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                    }

                    break;
                case "Modificar":
                    var url = String.Concat(ObtenhaURLDeContaAReceber(),
                                            "?Id=", id);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url,
                                                                                       "Modificar conta a receber",
                                                                                       800, 550, "FN_cdContaAReceber_aspx"), false);
                    break;
                case "GerarBoleto":

                    var url2 = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmBoletoAvulso.aspx",
                                           "?ItensFinanceiros=", id);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url2,
                                                                                       "Gerar boleto",
                                                                                       800, 550, "FN_frmBoletoAvulso_aspx"), false);

                    break;
                case "Receber":
                    divJanelaParaConfirmarData.Visible = true;
                    ViewState[CHAVE_ID_ITEM_SELECIONADO] = id;
                    break;
            }
        }

        private void RecebaLancamento(long idDoLancamento, DateTime dataDoRecebimento)
        {

            IItemLancamentoFinanceiroRecebimento lancamento;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
            {
                lancamento = servico.Obtenha(idDoLancamento);

                lancamento.Situacao = Situacao.Paga;
                lancamento.DataDoRecebimento = dataDoRecebimento;
                servico.Modifique(lancamento);
            }


            if (lancamento.FormaDeRecebimentoEhBoleto())
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                    servico.AtualizarStatusDoBoletoGerado(Convert.ToInt64(lancamento.NumeroBoletoGerado), StatusBoleto.Status.Pago.ToString());

        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.FN.001";
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

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroContaAReceberPorIntervaloDeDataDeVencimento>();

            filtro.Operacao = operacao;
            filtro.AdicioneValoresDoFiltroParaEntre(txtPeriodo1.SelectedDate.Value.ToString("yyyyMMdd"),
                                                    txtPeriodo2.SelectedDate.Value.ToString("yyyyMMdd"));
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensDeContasAReceber.PageSize, 0);
            EscondaBotoesColetivo();

        }

        protected void btnPesquisarPorSituacao_OnClick_(object sender, ImageClickEventArgs e)
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

            if (ctrlSituacao.SituacaoSelecionada == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione uma situação."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroContaAReceberPorSituacao>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlSituacao.Codigo;
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensDeContasAReceber.PageSize, 0);
            EscondaBotoesColetivo();
        }

        protected void btnPesquisarPorFormaDeRecebimento_OnClick_(object sender, ImageClickEventArgs e)
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

            if (ctrlFormaRecebimento.FormaDeRecebimentoSelecionada == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione uma forma de recebimento."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroContaAReceberPorFormaDeRecebimento>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlFormaRecebimento.Codigo;
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensDeContasAReceber.PageSize, 0);
            EscondaBotoesColetivo();
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

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroContaAReceberPorDescricao>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = txtDescricao.Text;
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensDeContasAReceber.PageSize, 0);
            EscondaBotoesColetivo();
        }

        protected void ToggleRowSelection(object sender, EventArgs e)
        {
            ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
            bool checkHeader = true;
            var idsDeClientePorQuantidade = new Dictionary<string, int>();
            var idsDeClientePorFormaDeRecebimento = new Dictionary<string, int>();
            var idsDeClientePorSituacao = new Dictionary<string, int>();
            var idsDeClientesPorQntNumeroDeBoletoNaoGerado = new Dictionary<string, int>();
            var idsDeClientesPorNumerosDeBoletos = new Dictionary<string, Dictionary<string, int>>();

            int quantidadeDeItensSelecionados = 0;

            foreach (GridDataItem dataItem in grdItensDeContasAReceber.MasterTableView.Items)
            {
                if (!(dataItem.FindControl("CheckBox1") as CheckBox).Checked)
                {
                    checkHeader = false;
                }

                if ((dataItem.FindControl("CheckBox1") as CheckBox).Checked)
                {
                    if (!idsDeClientePorFormaDeRecebimento.ContainsKey(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text))
                        idsDeClientePorFormaDeRecebimento.Add(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text, 0);

                    if (!idsDeClientePorQuantidade.ContainsKey(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text))
                        idsDeClientePorQuantidade.Add(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text, 0);

                    if (!idsDeClientePorSituacao.ContainsKey(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text))
                        idsDeClientePorSituacao.Add(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text, 0);

                    if (!idsDeClientesPorQntNumeroDeBoletoNaoGerado.ContainsKey(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text))
                        idsDeClientesPorQntNumeroDeBoletoNaoGerado.Add(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text, 0);

                    if (!idsDeClientesPorNumerosDeBoletos.ContainsKey(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text))
                        idsDeClientesPorNumerosDeBoletos.Add(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text, new Dictionary<string, int>());

                    idsDeClientePorQuantidade[dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text] += 1;

                    if (dataItem.Cells[NUMERO_CELULA_FORMA_RECEBIMENTO].Text.Equals(FormaDeRecebimento.Boleto.Descricao, StringComparison.InvariantCultureIgnoreCase))
                        idsDeClientePorFormaDeRecebimento[dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text] += 1;

                    if (dataItem.Cells[NUMERO_CELULA_SITUACAO].Text.Equals(Situacao.CobrancaEmAberto.Descricao, StringComparison.InvariantCultureIgnoreCase) ||
                        dataItem.Cells[NUMERO_CELULA_SITUACAO].Text.Equals(Situacao.CobrancaGerada.Descricao, StringComparison.InvariantCultureIgnoreCase))
                        idsDeClientePorSituacao[dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text] += 1;

                    if (string.IsNullOrEmpty(dataItem.Cells[NUMERO_CELULA_NUMERO_BOLETO].Text))
                        idsDeClientesPorQntNumeroDeBoletoNaoGerado[dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text] += 1;


                    if (!idsDeClientesPorNumerosDeBoletos[dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text].ContainsKey(dataItem.Cells[NUMERO_CELULA_NUMERO_BOLETO].Text))
                        idsDeClientesPorNumerosDeBoletos[dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text].Add(dataItem.Cells[NUMERO_CELULA_NUMERO_BOLETO].Text, 0);

                    idsDeClientesPorNumerosDeBoletos[dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text][
                        dataItem.Cells[NUMERO_CELULA_NUMERO_BOLETO].Text] += 1;


                    quantidadeDeItensSelecionados += 1;
                }
            }

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnGerarBoletoColetivo")).Visible =
                idsDeClientePorQuantidade.Count() == 1 && idsDeClientePorQuantidade.ElementAt(0).Value > 1 &&
                idsDeClientePorFormaDeRecebimento.ElementAt(0).Value == idsDeClientePorQuantidade.ElementAt(0).Value &&
                idsDeClientePorSituacao.ElementAt(0).Value == idsDeClientePorQuantidade.ElementAt(0).Value &&
                (idsDeClientesPorQntNumeroDeBoletoNaoGerado.ElementAt(0).Value == 0 &&
                idsDeClientesPorNumerosDeBoletos.ElementAt(0).Value.Count == 1);

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnReceberContaColetivo")).Visible =
                quantidadeDeItensSelecionados > 1;

            GridHeaderItem headerItem = grdItensDeContasAReceber.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            (headerItem.FindControl("headerChkbox") as CheckBox).Checked = checkHeader;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var headerCheckBox = (sender as CheckBox);
            var mostrarBotaoBoletoColetivo = true;
            string idDoClienteDaPrimeiraLinha = null;
            string formaDeRecebimentoDaPrimeiraLinha = null;
            string numeroDoBoletoPrimeiraLinha = null;


            foreach (GridDataItem dataItem in grdItensDeContasAReceber.MasterTableView.Items)
            {
                (dataItem.FindControl("CheckBox1") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;

                if (idDoClienteDaPrimeiraLinha == null)
                    idDoClienteDaPrimeiraLinha = dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text;

                if (formaDeRecebimentoDaPrimeiraLinha == null)
                    formaDeRecebimentoDaPrimeiraLinha = dataItem.Cells[NUMERO_CELULA_FORMA_RECEBIMENTO].Text;

                if (numeroDoBoletoPrimeiraLinha == null)
                    numeroDoBoletoPrimeiraLinha = dataItem.Cells[NUMERO_CELULA_NUMERO_BOLETO].Text;

                if (!idDoClienteDaPrimeiraLinha.Equals(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text) ||
                    !formaDeRecebimentoDaPrimeiraLinha.Equals(dataItem.Cells[NUMERO_CELULA_FORMA_RECEBIMENTO].Text) ||
                    dataItem.Cells[NUMERO_CELULA_SITUACAO].Text.Equals(Situacao.Cancelada.Descricao) ||
                    dataItem.Cells[NUMERO_CELULA_SITUACAO].Text.Equals(Situacao.Paga.Descricao) ||
                    !numeroDoBoletoPrimeiraLinha.Equals(dataItem.Cells[NUMERO_CELULA_NUMERO_BOLETO].Text))
                    mostrarBotaoBoletoColetivo = false;
            }

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnGerarBoletoColetivo")).Visible =
                mostrarBotaoBoletoColetivo && headerCheckBox.Checked;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnReceberContaColetivo")).Visible =
                headerCheckBox.Checked;

        }

        private void EscondaBotoesColetivo()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnGerarBoletoColetivo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnReceberContaColetivo")).Visible = false;
        }

        private void GerarRelatorio()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
            {
                var itensDeLancamento = servico.ObtenhaItensFinanceiros(FiltroAplicado, int.MaxValue, 0);
                var geradorDeRelatorioGeral = new GeradorDeRelatorioDeContasAReceber(itensDeLancamento, true);
                var nomeDoArquivo = geradorDeRelatorioGeral.GereRelatorio();
                var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" + nomeDoArquivo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);
            }
        }

        protected void grdItensDeContasAReceber_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var gridItem = (GridDataItem)e.Item;

                var lancamento = ((IItemLancamentoFinanceiro)(((gridItem)).DataItem));

                if (lancamento.EstaVencido())
                {
                    ((gridItem)).BackColor = Color.Red;
                    ((gridItem)).ForeColor = Color.White;
                }

                gridItem.Cells[NUMERO_CELULA_GERAR_BOLETO].Controls[0].Visible = (lancamento as IItemLancamentoFinanceiroRecebimento).FormaDeRecebimentoEhBoleto() && !lancamento.LacamentoFoiCanceladoOuPago() && !lancamento.BoletoFoiGeradoColetivamente;
                gridItem.Cells[NUMERO_CELULA_CANCELAR].Controls[0].Visible = !lancamento.LacamentoFoiCanceladoOuPago();
                gridItem.Cells[NUMERO_CELULA_RECEBER].Controls[0].Visible = !lancamento.LacamentoFoiCanceladoOuPago();

            }
        }

        protected void btnVencidos_OnClick(object sender, ImageClickEventArgs e)
        {
            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroContaAReceberVencidos>();
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensDeContasAReceber.PageSize, 0);
            EscondaBotoesColetivo();
        }

        protected void btnPesquisarPorNumeroDoBoleto_OnClick(object sender, ImageClickEventArgs e)
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

            if (string.IsNullOrEmpty(txtNumeroDoBoleto.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Informe o número do boleto."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPorNumeroDoBoleto>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = txtNumeroDoBoleto.Text;
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensDeContasAReceber.PageSize, 0);
            EscondaBotoesColetivo();
        }

        protected void btnPesquisarPorTipoLacamentoFinanceiroRecebimento_OnClick(object sender, ImageClickEventArgs e)
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

            if (ctrlTipoLacamentoFinanceiroRecebimento.TipoLacamentoSelecionado == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione um tipo de lançamento."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroTipoLacamentoFinanceiroRecebimento>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlTipoLacamentoFinanceiroRecebimento.Codigo;
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensDeContasAReceber.PageSize, 0);
            EscondaBotoesColetivo();
        }
    }
}