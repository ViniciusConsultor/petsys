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
using MP.Client.Relatorios.Patentes;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;
using MP.Interfaces.Negocio.Filtros.Patentes;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmProcessosDePatentes : SuperPagina
    {
      
        private const string CHAVE_FILTRO_APLICADO = "CHAVE_FILTRO_APLICADO_PROCESSO_DE_PATENTE";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ExibaTelaInicial();

        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.009";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
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

            Control controle2 = rdkProcessosDePatentes;
            UtilidadesWeb.LimparComponente(ref controle2);

            CarregaOpcoesDeFiltro();
            EscondaTodosOsPanelsDeFiltro();
            pnlProcesso.Visible = true;
            cboTipoDeFiltro.SelectedValue = "1";
            ctrlOperacaoFiltro1.Inicializa();
            ctrlNaturezaPatente1.Inicializa();
            ctrlCliente1.Inicializa();
            ctrlInventor.Inicializa();
            ctrlTitular.Inicializa();

            ctrlOperacaoFiltro1.Codigo = OperacaoDeFiltro.EmQualquerParte.ID.ToString();

            chkConsiderarNaoAtivas.Checked = false;

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatenteSemFiltro>();
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDePatentes.PageSize, 0);
        }

        private void MostraProcessos(IFiltro filtro, int quantidadeDeProcessos, int offSet)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
            {
                grdProcessosDePatentes.VirtualItemCount = servico.ObtenhaQuantidadeDeProcessosCadastrados(filtro, chkConsiderarNaoAtivas.Checked);
                grdProcessosDePatentes.DataSource = servico.ObtenhaProcessosDePatentes(filtro, quantidadeDeProcessos, offSet, chkConsiderarNaoAtivas.Checked);
                grdProcessosDePatentes.DataBind();
            }
        }

        protected void btnPesquisarPorTituloDaPatente_OnClick(object sender, ImageClickEventArgs e)
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

            if (string.IsNullOrEmpty(txtTituloPatente.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Informe o título da patente."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatentePorTituloDaPatente>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = txtTituloPatente.Text;
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDePatentes.PageSize, 0);
        }

        protected void btnPesquisarPorNatureza_OnClick(object sender, ImageClickEventArgs e)
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

            if (ctrlNaturezaPatente1.NaturezaPatenteSelecionada == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione uma natureza."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatentePorNatureza>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlNaturezaPatente1.NaturezaPatenteSelecionada.IdNaturezaPatente.ToString();
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDePatentes.PageSize, 0);
        }

        private void CarregaOpcoesDeFiltro()
        {
            cboTipoDeFiltro.Items.Clear();
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Processo", "1"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Data de cadastro", "2"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Título da patente", "3"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Natureza", "4"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Cliente","5"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Inventor", "6"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Titular", "7"));
        }


        protected void cboTipoDeFiltro_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            EscondaTodosOsPanelsDeFiltro();

            switch (cboTipoDeFiltro.SelectedValue)
            {
                case "1":
                    pnlProcesso.Visible = true;
                    break;
                case "2":
                    pnlDataDeCadastro.Visible = true;
                    break;
                case "3":
                    pnlTituloPatente.Visible = true;
                    break;
                case "4":
                    pnlNatureza.Visible = true;
                    break;
                case "5":
                    pnlCliente.Visible = true;
                    break;
                case "6":
                    pnlInventor.Visible = true;
                    break;
                case "7":
                    pnlTitular.Visible = true;
                    break;
            }
        }


        private void EscondaTodosOsPanelsDeFiltro()
        {
            pnlProcesso.Visible = false;
            pnlDataDeCadastro.Visible = false;
            pnlTituloPatente.Visible = false;
            pnlNatureza.Visible = false;
            pnlCliente.Visible = false;
            pnlInventor.Visible = false;
            pnlTitular.Visible = false;
        }
        
        protected void btnPesquisarPorDataDeCadastro_OnClick(object sender, ImageClickEventArgs e)
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

            if (!txtDataDeCadastro.SelectedDate.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Informe uma data."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatentePorDataDeCadastro>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = txtDataDeCadastro.SelectedDate.Value.ToString("yyyyMMdd");
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDePatentes.PageSize, 0);
        }

        
        protected void btnPesquisarPorProcesso_OnClick(object sender, ImageClickEventArgs e)
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

            if (string.IsNullOrEmpty(txtProcesso.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Informe um número de processo."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatentePorProcesso>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = txtProcesso.Text;
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDePatentes.PageSize, 0);
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

        protected void btnNovo_Click()
        {
            var URL = ObtenhaURLCadastrodePatente();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(URL, "Novo processo de patente", 800, 550, "MP_cdProcessoDePatente_aspx"), false);
        }



        private string ObtenhaURLCadastrodePatente()
        {
            return String.Concat(UtilidadesWeb.ObtenhaURLCorrente(), "cdProcessoDePatente.aspx");
        }

        private string ObtenhaURLLeituraDeRevistaDePatente()
        {
            return String.Concat(UtilidadesWeb.ObtenhaURLCorrente(), "frmLeituraRevistaPatente.aspx");
        }

        private void Recarregue()
        {
            MostraProcessos(FiltroAplicado, grdProcessosDePatentes.PageSize, 0);
        }

        private void AbraLeituraDeRevista()
        {

            var URL = ObtenhaURLLeituraDeRevistaDePatente();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(URL, "Leitura da revista de patente", 800, 550, "MP_frmLeituraRevistaPatente_aspx"), false);
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
                case "btnLerRevista":
                    AbraLeituraDeRevista();
                    break;
                case "btnLimpar":
                    ExibaTelaInicial();
                    break;
                case "btnGerarRelatorio":
                    GerarRelatorio();
                    break;
            }
        }

        protected void grdProcessosDePatentes_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                var offSet = 0;

                if (e.NewPageIndex > 0)
                    offSet = e.NewPageIndex * grdProcessosDePatentes.PageSize;

                MostraProcessos(FiltroAplicado, grdProcessosDePatentes.PageSize, offSet);

            }
        }

        protected void grdProcessosDePatentes_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;
            
            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
               id = Convert.ToInt64((e.Item.Cells[5].Text));

            switch (e.CommandName)
            {
                case "Excluir":

                    try
                    {
                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
                        {
                            var processo = servico.Obtenha(id);
                            servico.Excluir(processo);
                        }
                        
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInformacao(
                                                                    "Processo de patente excluído com sucesso."), false);
                        ExibaTelaInicial();
                    }
                    catch (BussinesException ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                    }

                    break;
                case "Modificar":
                    var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/cdProcessoDePatente.aspx",
                                            "?Id=", id);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url,
                                                                                       "Modificar processo de patente",
                                                                                       800, 550, "MP_cdProcessoDePatente_aspx"), false);
                    break;

                case "Email":
                    var url2 = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/frmEnviaEmail.aspx",
                                            "?Id=", id, "&Tipo=P");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url2,
                                                                                       "Enviar e-mail",
                                                                                       800, 550, "MP_frmEnviaEmail_aspx"), false);
                    break;
            }
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
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Informe um cliente."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatentePorCliente>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlCliente1.ClienteSelecionado.Pessoa.ID.Value.ToString();
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDePatentes.PageSize, 0);
        }

        protected void btnPesquisarPorTitular_OnClick_(object sender, ImageClickEventArgs e)
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

            if (ctrlTitular.TitularSelecionado == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Informe um titular."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatentePorTitular>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlTitular.TitularSelecionado.Pessoa.ID.Value.ToString();
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDePatentes.PageSize, 0);
        }

        protected void btnPesquisarPorInventor_OnClick_(object sender, ImageClickEventArgs e)
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

            if (ctrlInventor.InventorSelecionado == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Informe um inventor."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatentePorInventor>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlInventor.InventorSelecionado.Pessoa.ID.Value.ToString();
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDePatentes.PageSize, 0);
        }

        protected override void OnPreRender(EventArgs e)
        {
            var principal = FabricaDeContexto.GetInstancia().GetContextoAtual();

            if (grdProcessosDePatentes.Columns[0].Visible)
                grdProcessosDePatentes.Columns[0].Visible = principal.EstaAutorizado("OPE.MP.009.0002");

            if (grdProcessosDePatentes.Columns[1].Visible)
                grdProcessosDePatentes.Columns[1].Visible = principal.EstaAutorizado("OPE.MP.009.0003");

            if (grdProcessosDePatentes.Columns[2].Visible)
                grdProcessosDePatentes.Columns[2].Visible = principal.EstaAutorizado("OPE.MP.009.0005");

            base.OnPreRender(e);
        }

        private void GerarRelatorio()
        {
            IList<IProcessoDePatente> processosDePatentes = new List<IProcessoDePatente>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
            {
                int quantidadeMaxima = servico.ObtenhaQuantidadeDeProcessosCadastrados(FiltroAplicado, chkConsiderarNaoAtivas.Checked);
                processosDePatentes = servico.ObtenhaProcessosDePatentes(FiltroAplicado, quantidadeMaxima, 0, chkConsiderarNaoAtivas.Checked);

            }

            if(processosDePatentes.Count == 0)
                return;

            var geradorDeRelatorioGeral = new GeradorDeRelatorioDePatentes(processosDePatentes);
            var nomeDoArquivo = geradorDeRelatorioGeral.GereRelatorio();
            var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" + nomeDoArquivo;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);
        }
    }
}