using System;
using System.Collections.Generic;
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
using FN.Interfaces.Negocio.Filtros.ContasAReceber;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class frmContasAReceber : SuperPagina
    {
        private const string CHAVE_FILTRO_APLICADO = "CHAVE_FILTRO_APLICADO_CONTAS_A_RECEBER";
        private const int NUMERO_CELULA_ID_CLIENTE = 9;
        private const int NUMERO_CELULA_ID_ITEM_FINANCEIRO = 7;
        private const string CHAVE_ITENS_LANCAMENTO = "CHAVE_ITENS_LANCAMENTO";

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
                grdItensDeContasAReceber.VirtualItemCount = servico.ObtenhaQuantidadeDeItensFinanceiros(filtro);
                ItensLancamento = servico.ObtenhaItensFinanceiros(filtro, quantidadeDeProcessos, offSet);
                grdItensDeContasAReceber.DataSource = ItensLancamento;
                grdItensDeContasAReceber.DataBind();
                ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnRelatorio")).Visible = ItensLancamento.Count > 0;
            }
        }

        private void EscondaTodosOsPanelsDeFiltro()
        {
            pnlCliente.Visible = false;
            pnlPeriodoDeVencimento.Visible = false;
            pnlSituacao.Visible = false;
            pnlDescricao.Visible = false;
            pnlFormaDeRecebimento.Visible = false;
        }

        private void CarregaOpcoesDeFiltro()
        {
            cboTipoDeFiltro.Items.Clear();
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Situacao", "1"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Cliente", "2"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Período de vencimento", "3"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Descrição", "4"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Forma de recebimento", "5"));
        }

        private void ExibaTelaInicial()
        {
            Control controle1 = pnlFiltro;
            UtilidadesWeb.LimparComponente(ref controle1);

            Control controle2 = rdkProcessosDeMarcas;
            UtilidadesWeb.LimparComponente(ref controle2);

            CarregaOpcoesDeFiltro();
            EscondaTodosOsPanelsDeFiltro();
            pnlCliente.Visible = true;
            cboTipoDeFiltro.SelectedValue = "2";

            ctrlOperacaoFiltro1.Inicializa();
            ctrlCliente1.Inicializa();
            ctrlSituacao.Inicializa();
            ctrlFormaRecebimento.Inicializa();

            ctrlOperacaoFiltro1.Codigo = OperacaoDeFiltro.EmQualquerParte.ID.ToString();

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroContaAReceberSemFiltro>();
            FiltroAplicado = filtro;
            MostraItens(filtro, grdItensDeContasAReceber.PageSize, 0);

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnGerarBoletoColetivo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnRelatorio")).Visible = ItensLancamento.Count > 0;
        }

        private string ObtenhaURLDeContaAReceber()
        {
            return String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/cdContaAReceber.aspx");
        }

        protected void btnNovo_Click()
        {
            var URL = ObtenhaURLDeContaAReceber();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(URL, "Nova conta a receber", 800, 550, "cdContaAReceber_aspx"), false);
        }

        private void Recarregue()
        {
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
            }
        }

        private void PreparaEmissaoDeBoletoColetivamente()
        {
            var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmBoletoAvulso.aspx",
                                           "?ItensFinanceiros=", obtenhaIdsDosItensSelecionados());
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(url,
                                                                               "Gerar boleto coletivamente",
                                                                               800, 550, "frmBoletoAvulso_aspx"), false);
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
                    break;
                case "4":
                    pnlDescricao.Visible = true;
                    break;
                case "5":
                    pnlFormaDeRecebimento.Visible = true;
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
        }

        protected void grdItensDeContasAReceber_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                var offSet = 0;

                if (e.NewPageIndex > 0)
                    offSet = e.NewPageIndex * grdItensDeContasAReceber.PageSize;

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
                                                                    "Item de lançcamento de conta a receber cancelado com sucesso."), false);
                        ExibaTelaInicial();
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
                                                                                       800, 550, "cdContaAReceber_aspx"), false);
                    break;
                case "GerarBoleto":

                    var url2 = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmBoletoAvulso.aspx",
                                           "?ItensFinanceiros=", id);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url2,
                                                                                       "Gerar boleto",
                                                                                       800, 550, "frmBoletoAvulso_aspx"), false);

                    break;
            }
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
        }

        protected void ToggleRowSelection(object sender, EventArgs e)
        {
            ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
            bool checkHeader = true;
            var idsDeCliente = new Dictionary<string, int>();

            foreach (GridDataItem dataItem in grdItensDeContasAReceber.MasterTableView.Items)
            {
                if (!(dataItem.FindControl("CheckBox1") as CheckBox).Checked)
                {
                    checkHeader = false;
                }

                if ((dataItem.FindControl("CheckBox1") as CheckBox).Checked)
                {
                    if (!idsDeCliente.ContainsKey(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text))
                        idsDeCliente.Add(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text, 0);

                    idsDeCliente[dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text] += 1;
                }
            }

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnGerarBoletoColetivo")).Visible =
                idsDeCliente.Count() == 1 && idsDeCliente.ElementAt(0).Value > 1;
            GridHeaderItem headerItem = grdItensDeContasAReceber.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            (headerItem.FindControl("headerChkbox") as CheckBox).Checked = checkHeader;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var headerCheckBox = (sender as CheckBox);
            var mostrarBotaoBoletoColetivo = true;
            string idDoClienteDaPrimeiraLinha = null;
            foreach (GridDataItem dataItem in grdItensDeContasAReceber.MasterTableView.Items)
            {
                (dataItem.FindControl("CheckBox1") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;

                if (idDoClienteDaPrimeiraLinha == null)
                    idDoClienteDaPrimeiraLinha = dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text;

                if (!idDoClienteDaPrimeiraLinha.Equals(dataItem.Cells[NUMERO_CELULA_ID_CLIENTE].Text))
                    mostrarBotaoBoletoColetivo = false;

            }

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnGerarBoletoColetivo")).Visible = mostrarBotaoBoletoColetivo;
        }

        private void GerarRelatorio()
        {
            var geradorDeRelatorioGeral = new GeradorDeRelatorioDeContasAReceber(ItensLancamento);
            var nomeDoArquivo = geradorDeRelatorioGeral.GereRelatorio();
            var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" + nomeDoArquivo;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);
        }

        private IList<IItemLancamentoFinanceiroRecebimento> ItensLancamento
        {
            get
            {
                if(ViewState[CHAVE_ITENS_LANCAMENTO] == null)
                    return new List<IItemLancamentoFinanceiroRecebimento>();

                return (IList<IItemLancamentoFinanceiroRecebimento>) ViewState[CHAVE_ITENS_LANCAMENTO];
            }
            set { ViewState[CHAVE_ITENS_LANCAMENTO] = value; }
        }
    }
}