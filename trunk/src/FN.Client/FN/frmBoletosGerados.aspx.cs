using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using FN.Client.FN.Relatorios;
using FN.Interfaces.Negocio;
using FN.Interfaces.Negocio.Filtros.BoletosGerados;
using FN.Interfaces.Servicos;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class frmBoletosGerados : SuperPagina
    {
        private const string CHAVE_FILTRO_APLICADO = "CHAVE_FILTRO_APLICADO_BOLETOS_GERADOS";
        private const string CHAVE_BOLETOS_GERADOS = "CHAVE_BOLETOS_GERADOS";

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

        private void ExibaTelaInicial()
        {
            Control controle1 = pnlFiltro;
            UtilidadesWeb.LimparComponente(ref controle1);

            CarregaOpcoesDeFiltro();
            EscondaTodosOsPanelsDeFiltro();

            ctrlOperacaoFiltro1.Inicializa();
            ctrlCliente1.Inicializa();
            ctrlCedente1.Inicializa();

            pnlNossoNumero.Visible = true;
            cboTipoDeFiltro.SelectedValue = "1";

            pnlOpcaoDeFiltro.Visible = true;

            ctrlOperacaoFiltro1.Codigo = OperacaoDeFiltro.EmQualquerParte.ID.ToString();

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroBoletosGeradosSemFiltro>();
            FiltroAplicado = filtro;

            CarregaBoletosGerados(FiltroAplicado, grdBoletosGerados.PageSize, 0);
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnRelatorio")).Visible = BoletosGerados.Count > 0;
        }

        private void CarregaBoletosGerados(IFiltro filtro, int quantidadeDeBoletos, int offset)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
            {
                var listaDeBoletosGerados = servico.obtenhaBoletosGerados(filtro, quantidadeDeBoletos, offset);

                BoletosGerados = listaDeBoletosGerados;
                ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnRelatorio")).Visible = BoletosGerados.Count > 0;

                if (listaDeBoletosGerados.Count > 0)
                {
                    grdBoletosGerados.VirtualItemCount = servico.ObtenhaQuantidadeDeBoletos(filtro);
                    grdBoletosGerados.DataSource = ConvertaBoletosGeradosParaDTO(listaDeBoletosGerados);
                    grdBoletosGerados.DataBind();
                    return;
                }

                var controleGrid = grdBoletosGerados as Control;
                UtilidadesWeb.LimparComponente(ref controleGrid);
                grdBoletosGerados.DataSource = new List<DTOBoletosGerados>();
                grdBoletosGerados.DataBind();
            }
        }

        private void CarregaOpcoesDeFiltro()
        {
            cboTipoDeFiltro.Items.Clear();
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("NossoNumero", "1"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Cliente", "2"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Data de geracao", "3"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Data de vencimento", "4"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Cedente", "5"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Vencidos", "6"));
        }

        private IList<DTOBoletosGerados> ConvertaBoletosGeradosParaDTO(IList<IBoletosGerados> listaDeBoletosGerados)
        {
            var listaDeBoletos = new List<DTOBoletosGerados>();

            foreach (var boletosGerado in listaDeBoletosGerados)
            {
                var dto = new DTOBoletosGerados();

                if (boletosGerado.DataGeracao.HasValue)
                    dto.DataGeracao = boletosGerado.DataGeracao.Value.ToString("dd/MM/yyyy");

                if (boletosGerado.DataVencimento.HasValue)
                    dto.DataVencimento = boletosGerado.DataVencimento.Value.ToString("dd/MM/yyyy");

                if (boletosGerado.ID.HasValue)
                    dto.ID = boletosGerado.ID.Value.ToString();

                if (boletosGerado.NossoNumero.HasValue)
                    dto.NossoNumero = boletosGerado.NossoNumero.Value.ToString();

                dto.NumeroBoleto = boletosGerado.NumeroBoleto;

                dto.Valor = boletosGerado.Valor > 0 ? boletosGerado.Valor.ToString("N", CultureInfo.CreateSpecificCulture("es-ES")) : "0";

                if (boletosGerado.Cliente != null && boletosGerado.Cliente.Pessoa != null)
                    dto.Cliente = boletosGerado.Cliente.Pessoa.Nome;

                dto.Observacao = !string.IsNullOrEmpty(boletosGerado.Observacao) ? boletosGerado.Observacao : null;

                dto.Cedente = boletosGerado.Cedente != null && boletosGerado.Cedente.Pessoa != null ?
                    boletosGerado.Cedente.Pessoa.Nome : null;

                dto.Instrucoes = !string.IsNullOrEmpty(boletosGerado.Instrucoes) ? boletosGerado.Instrucoes : null;

                dto.StatusBoleto = !string.IsNullOrEmpty(boletosGerado.StatusBoleto) ? boletosGerado.StatusBoleto : "Aberto";

                dto.EhBoletoAvulso = boletosGerado.EhBoletoAvulso ? "SIM" : string.Empty;

                listaDeBoletos.Add(dto);
            }

            return listaDeBoletos;
        }

        protected void grdBoletosGerados_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                var offSet = UtilidadesWeb.ObtenhaOffSet(e, grdBoletosGerados.PageSize);

                CarregaBoletosGerados(FiltroAplicado, grdBoletosGerados.PageSize, offSet);

            }
        }

        protected void grdBoletosGerados_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                id = Convert.ToInt64((e.Item.Cells[4].Text));

            switch (e.CommandName)
            {
                case "Excluir":

                    try
                    {
                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                        {
                            servico.Excluir(id);
                        }

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInformacao(
                                                                    "Boleto excluído com sucesso."), false);

                        var grid = sender as RadGrid;

                        var offset = UtilidadesWeb.ObtenhaOffSet(grid.CurrentPageIndex,  grid.PageSize, grid.VirtualItemCount - 1);
                        CarregaBoletosGerados(FiltroAplicado, UtilidadesWeb.ObtenhaQuantidadeDeItensDaPagina(grid.Items.Count -1, grid.PageSize), offset);
                        
                    }
                    catch (BussinesException ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                    }

                    break;
                case "Modificar":
                    var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmBoletoAvulso.aspx",
                                            "?Id=", id);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url,
                                                                                       "Reimprimir boleto",
                                                                                       800, 550, "FN_frmBoletoAvulso_aspx"), false);
                    break;
            }
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.FN.005";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
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

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroBoletosGeradosPorCliente>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlCliente1.ClienteSelecionado.Pessoa.ID.Value.ToString();
            FiltroAplicado = filtro;

            CarregaBoletosGerados(FiltroAplicado, grdBoletosGerados.PageSize, 0);
        }

        protected void btnPesquisarPorDataDeGeracao_OnClick(object sender, ImageClickEventArgs e)
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

            if (!txtDataDeGeracaoIni.SelectedDate.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione o primeiro período da data de geração."), false);
                return;
            }

            if (!txtDataDeGeracaoFim.SelectedDate.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione o segundo período da data de geração."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroBoletosGeradosPorIntervaloDeDataDeGeracao>();

            filtro.Operacao = operacao;
            filtro.AdicioneValoresDoFiltroParaEntre(txtDataDeGeracaoIni.SelectedDate.Value.ToString("yyyyMMdd"),
                                                    txtDataDeGeracaoFim.SelectedDate.Value.ToString("yyyyMMdd"));
            FiltroAplicado = filtro;

            CarregaBoletosGerados(FiltroAplicado, grdBoletosGerados.PageSize, 0);
        }

        protected void btnPesquisarPorDataDeVencimento_OnClick(object sender, ImageClickEventArgs e)
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

            if (!txtDataDeVencimentoIni.SelectedDate.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione o primeiro período da data de vencimento."), false);
                return;
            }

            if (!txtDataDeVencimentoFim.SelectedDate.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione o segundo período da data de vencimento."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroBoletosGeradosPorIntervaloDeDataDeVencimento>();

            filtro.Operacao = operacao;
            filtro.AdicioneValoresDoFiltroParaEntre(txtDataDeVencimentoIni.SelectedDate.Value.ToString("yyyyMMdd"),
                                                    txtDataDeVencimentoFim.SelectedDate.Value.ToString("yyyyMMdd"));
            FiltroAplicado = filtro;

            CarregaBoletosGerados(FiltroAplicado, grdBoletosGerados.PageSize, 0);
        }

        protected void btnPesquisarPorNossoNumero_OnClick(object sender, ImageClickEventArgs e)
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
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Para o filtro selecionado a esta opção não é válida."), false);
                return;
            }

            if (string.IsNullOrEmpty(txtNossoNumero.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                   UtilidadesWeb.MostraMensagemDeInconsitencia("Informe o nosso número."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroBoletosGeradosPoNossoNumero>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = txtNossoNumero.Text;
            FiltroAplicado = filtro;

            CarregaBoletosGerados(FiltroAplicado, grdBoletosGerados.PageSize, 0);
        }

        protected void cboTipoDeFiltro_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            EscondaTodosOsPanelsDeFiltro();

            pnlOpcaoDeFiltro.Visible = true;

            switch (cboTipoDeFiltro.SelectedValue)
            {
                case "1":
                    pnlNossoNumero.Visible = true;
                    break;
                case "2":
                    pnlCliente.Visible = true;
                    break;
                case "3":
                    pnlDataDeGeracao.Visible = true;
                    break;
                case "4":
                    pnlDataDeVencimento.Visible = true;
                    break;
                case "5":
                    pnlCedente.Visible = true;
                    break;
                case "6":
                    pnlVencidos.Visible = true;
                    pnlOpcaoDeFiltro.Visible = false;
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

        private void EscondaTodosOsPanelsDeFiltro()
        {
            pnlNossoNumero.Visible = false;
            pnlCliente.Visible = false;
            pnlDataDeGeracao.Visible = false;
            pnlDataDeVencimento.Visible = false;
            pnlCedente.Visible = false;
            pnlVencidos.Visible = false;
        }

        protected void btnPesquisarPorCedente_OnClick_(object sender, ImageClickEventArgs e)
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

            if (ctrlCedente1.CedenteSelecionado == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione um cedente."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroBoletosGeradosPorCedente>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlCedente1.CedenteSelecionado.Pessoa.ID.Value.ToString();
            FiltroAplicado = filtro;

            CarregaBoletosGerados(FiltroAplicado, grdBoletosGerados.PageSize, 0);
        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {
                case "btnRecarregar":
                    Recarregue();
                    break;

                case "btnLimpar":
                    ExibaTelaInicial();
                    break;

                case "btnRelatorio":
                    GerarRelatorio();
                    break;
            }
        }

        private void Recarregue()
        {
            CarregaBoletosGerados(FiltroAplicado, grdBoletosGerados.PageSize, 0);
        }

        private void GerarRelatorio()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
            {
                var listaDeBoletosGerados = servico.obtenhaBoletosGerados(FiltroAplicado, int.MaxValue, 0);
                var geradorDeRelatorioGeral = new GeradorDeRelatorioDeBoletosGerados(listaDeBoletosGerados);
                var nomeDoArquivo = geradorDeRelatorioGeral.GereRelatorio();
                var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" + nomeDoArquivo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);
            }
        }

        private IList<IBoletosGerados> BoletosGerados
        {
            get
            {
                if (ViewState[CHAVE_BOLETOS_GERADOS] == null)
                    ViewState[CHAVE_BOLETOS_GERADOS] = new List<IBoletosGerados>();

                return (IList<IBoletosGerados>)ViewState[CHAVE_BOLETOS_GERADOS];
            }

            set { ViewState[CHAVE_BOLETOS_GERADOS] = value; }
        }

        protected void btnVencidos_OnClick(object sender, ImageClickEventArgs e)
        {
            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroBoletosGeradosVencidos>();
            FiltroAplicado = filtro;
            CarregaBoletosGerados(FiltroAplicado, grdBoletosGerados.PageSize, 0);
        }
    }
}