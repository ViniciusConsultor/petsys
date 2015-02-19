﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;
using MP.Client.Relatorios;

namespace MP.Client.MP
{
    public partial class frmProcessosDeMarcas : SuperPagina
    {
        private const string CHAVE_FILTRO_APLICADO = "CHAVE_FILTRO_APLICADO_PROCESSO_DE_MARCA";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ExibaTelaInicial();

        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.007";
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

            Control controle2 = rdkProcessosDeMarcas;
            UtilidadesWeb.LimparComponente(ref controle2);

            CarregaOpcoesDeFiltro();
            EscondaTodosOsPanelsDeFiltro();
            pnlMarca.Visible = true;
            cboTipoDeFiltro.SelectedValue = "4";
            
            ctrlApresentacao1.Inicializa();
            ctrlNCL1.Inicializa();
            ctrlNatureza1.Inicializa();
            ctrlOperacaoFiltro1.Inicializa();
            ctrlCliente1.Inicializa();
            ctrlDespacho.Inicializa();

            ctrlOperacaoFiltro1.Codigo = OperacaoDeFiltro.EmQualquerParte.ID.ToString();

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaSemFiltro>();
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
            chkConsiderarNaoAtivas.Checked = false;
        }

        private void MostraProcessos(IFiltro filtro, int quantidadeDeProcessos, int offSet)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
            {
                grdProcessosDeMarcas.VirtualItemCount = servico.ObtenhaQuantidadeDeProcessosCadastrados(filtro, chkConsiderarNaoAtivas.Checked);
                grdProcessosDeMarcas.DataSource = DTOProcessoDeMarca.ConvertaProcessoParaDTO(servico.ObtenhaProcessosDeMarcas(filtro, quantidadeDeProcessos, offSet, chkConsiderarNaoAtivas.Checked));
                grdProcessosDeMarcas.DataBind();
            }

        }
        
        private void CarregaOpcoesDeFiltro()
        {
            cboTipoDeFiltro.Items.Clear();
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Apresentação", "1"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Cliente", "2"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Data de cadastro", "3"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Marca", "4"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Natureza", "5"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("NCL", "6"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Processo", "7"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Despacho", "8"));
        }


        protected void cboTipoDeFiltro_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            EscondaTodosOsPanelsDeFiltro();

            switch (cboTipoDeFiltro.SelectedValue)
            {
                case "1":
                    pnlApresentacao.Visible = true;
                    break;
                case "2":
                    pnlCliente.Visible = true;
                    break;
                case "3":
                    pnlDataDeCadastro.Visible = true;
                    break;
                case "4":
                    pnlMarca.Visible = true;
                    break;
                case "5":
                    pnlNatureza.Visible = true;
                    break;
                case "6":
                    pnlNCL.Visible = true;
                    break;
                case "7":
                    pnlProcesso.Visible = true;
                    break;
                case "8":
                    pnlDespacho.Visible = true;
                    break;
            }
        }


        private void EscondaTodosOsPanelsDeFiltro()
        {
            pnlApresentacao.Visible = false;
            pnlCliente.Visible = false;
            pnlDataDeCadastro.Visible = false;
            pnlMarca.Visible = false;
            pnlNatureza.Visible = false;
            pnlNCL.Visible = false;
            pnlProcesso.Visible = false;
            pnlDespacho.Visible = false;
        }


        protected void btnPesquisarPorApresentacao_OnClick(object sender, ImageClickEventArgs e)
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

            if (String.IsNullOrEmpty(ctrlApresentacao1.Codigo))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione uma apresentação."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaPorApresentacao>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlApresentacao1.Codigo;
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);

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

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaPorCliente>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlCliente1.ClienteSelecionado.Pessoa.ID.Value.ToString();
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
           
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

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaPorDataDeCadastro>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = txtDataDeCadastro.SelectedDate.Value.ToString("yyyyMMdd");
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
        }

        protected void btnPesquisarPorMarca_OnClick(object sender, ImageClickEventArgs e)
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

            if (ctrlMarcas1.MarcaSelecionada == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione uma marca."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaPorMarca>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlMarcas1.MarcaSelecionada.IdMarca.Value.ToString();
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
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

            if (string.IsNullOrEmpty(ctrlNatureza1.Codigo))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione uma natureza."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaPorNatureza>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlNatureza1.Codigo;
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
        }

        protected void btnPesquisarPorNCL_OnClick(object sender, ImageClickEventArgs e)
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

            if (ctrlNCL1.NCLSelecionado == null )
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione uma NCL."), false);
                return;
            }


            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaPorNCL>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlNCL1.NCLSelecionado.Codigo;
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
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

            if (!txtProcesso.Value.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Informe um número de processo."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaPorProcesso>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = txtProcesso.Text;
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
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
            var URL = ObtenhaURLDeCadastroDeMarca();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(URL, "Novo processo de marca", 800, 550, "MP_cdProcessoDeMarca_aspx"), false);
        }

        private string ObtenhaURLDeCadastroDeMarca()
        {
            return String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/cdProcessoDeMarca.aspx");
        }

        private string ObtenhaURLLeituraDeRevistaDeMarca()
        {
            return String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/frmLeituraRevistaMarca.aspx");
        }

        private string ObtenhaURLDeCadastroDeCliente()
        {
            return String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "Nucleo/cdCliente.aspx");
        }

        private void AbraTelaDeLeituraDaRevista()
        {
            var URL = ObtenhaURLLeituraDeRevistaDeMarca();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(URL, "Leitura da revista de marca", 800, 550, "MP_frmLeituraRevistaMarca_aspx"), false);
        }
        
        private void Recarregue()
        {
            MostraProcessos(FiltroAplicado, grdProcessosDeMarcas.PageSize, 0);
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
                    AbraTelaDeLeituraDaRevista();
                    break;
                case "btnLimpar":
                    ExibaTelaInicial();
                    break;
                case "btnGerarRelatorio":
                    GerarRelatorio();
                    break;
            }
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

        protected void grdProcessosDeMarcas_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;
            
            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                id = Convert.ToInt64((e.Item.Cells[5].Text));

            switch (e.CommandName)
            {
                case "Excluir":
                    
                    try
                    {
                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                        {
                            var processo = servico.Obtenha(id);
                            servico.Excluir(processo);
                        }

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInformacao(
                                                                    "Processo de marca excluído com sucesso."), false);
                        ExibaTelaInicial();
                    }
                    catch (BussinesException ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                    }

                    break;
                case "Modificar":
                    var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/cdProcessoDeMarca.aspx",
                                            "?Id=", id);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url,
                                                                                       "Modificar processo de marca",
                                                                                       800, 550, "MP_cdProcessoDeMarca_aspx"), false);
                    break;

                case "Email" :
                    var url2 = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/frmEnviaEmail.aspx",
                                            "?Id=", id, "&Tipo=M");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url2,
                                                                                       "Enviar e-mail",
                                                                                       800, 550, "MP_frmEnviaEmail_aspx"), false);
                    break;
                case "AbrirCliente" :
                    var url3 = String.Concat(ObtenhaURLDeCadastroDeCliente(),"?Id=", e.CommandArgument );
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url3,
                                                                                       "Cadastro de cliente",
                                                                                       800, 550, "Nucleo_cdCliente_aspx"), false);
                    break;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            var principal = FabricaDeContexto.GetInstancia().GetContextoAtual();

            if (grdProcessosDeMarcas.Columns[0].Visible)
                grdProcessosDeMarcas.Columns[0].Visible = principal.EstaAutorizado("OPE.MP.007.0002");

            if (grdProcessosDeMarcas.Columns[1].Visible)
                grdProcessosDeMarcas.Columns[1].Visible = principal.EstaAutorizado("OPE.MP.007.0003");

            if (grdProcessosDeMarcas.Columns[2].Visible)
                grdProcessosDeMarcas.Columns[2].Visible = principal.EstaAutorizado("OPE.MP.007.0005");

            base.OnPreRender(e);
        }

        protected void grdProcessosDeMarcas_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {

                var item = (GridDataItem)e.Item;

                
                ((System.Web.UI.WebControls.LinkButton) (item["cliente"].Controls[0])).CommandArgument = item["idCliente"].Text;
                ((System.Web.UI.WebControls.LinkButton)(item["cliente"].Controls[0])).CssClass = "hidelink";
            }
        }

        private void GerarRelatorio()
        {
            IList<IProcessoDeMarca> processoDeMarcas = new List<IProcessoDeMarca>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
            {
                int quantidadeMaxima = servico.ObtenhaQuantidadeDeProcessosCadastrados(FiltroAplicado, chkConsiderarNaoAtivas.Checked);
                processoDeMarcas = servico.ObtenhaProcessosDeMarcas(FiltroAplicado, quantidadeMaxima, 0, chkConsiderarNaoAtivas.Checked);

            }

            if (processoDeMarcas.Count == 0)
                return;

            var geradorDeRelatorioDeMarcas = new GeradorDeRelatorioDeMarcas(processoDeMarcas);
            var nomeDoArquivo = geradorDeRelatorioDeMarcas.GereRelatorio();
            var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" + nomeDoArquivo;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);
        }

        protected void btnPesquisarPorDespacho_OnClick(object sender, ImageClickEventArgs e)
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

            if (ctrlDespacho.DespachoDeMarcasSelecionada == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione um despacho."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaPorDespacho>();
            filtro.Operacao = operacao;
            filtro.ValorDoFiltro = ctrlDespacho.DespachoDeMarcasSelecionada.IdDespacho.Value.ToString();
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
        }
    }
}