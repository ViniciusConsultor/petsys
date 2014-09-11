using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class cdProcessoDePatente : SuperPagina
    {
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_PROCESSO_DE_PATENTE";
        private const string CHAVE_ID_PROCESSO_DE_PATENTE = "CHAVE_ID_PROCESSO_DE_PATENTE";

        private const string CHAVE_ID_PATENTE = "CHAVE_ID_PATENTE";
        private const string CHAVE_CLIENTES = "CHAVE_CLIENTES";
        private const string CHAVE_INVENTORES = "CHAVE_INVENTORES";
        private const string CHAVE_PRIORIDADE_UNIONISTA = "CHAVE_PRIORIDADE_UNIONISTA";
        private const string CHAVE_CLASSIFICACAO_PATENTE = "CHAVE_CLASSIFICACAO_PATENTE";
        private const string CHAVE_ANUIDADE_PATENTE = "CHAVE_ANUIDADE_PATENTE";
        private const string CHAVE_RADICAIS = "CHAVE_RADICAIS";
        private const string CHAVE_TITULARES = "CHAVE_TITULARES";
        private const string CHAVE_INDICE_BAIXA_ANUIDADE = "CHAVE_INDICE_BAIXA_ANUIDADE";

        private IList<ICliente> ListaDeClientes
        {
            get { return (IList<ICliente>)ViewState[CHAVE_CLIENTES]; }
            set { ViewState[CHAVE_CLIENTES] = value; }
        }

        private IList<IInventor> ListaDeInventores
        {
            get { return (IList<IInventor>)ViewState[CHAVE_INVENTORES]; }
            set { ViewState[CHAVE_INVENTORES] = value; }
        }

        private IList<IPrioridadeUnionistaPatente> ListaDePrioridadeUnionista
        {
            get { return (IList<IPrioridadeUnionistaPatente>)ViewState[CHAVE_PRIORIDADE_UNIONISTA]; }
            set { ViewState[CHAVE_PRIORIDADE_UNIONISTA] = value; }
        }

        private IList<IClassificacaoPatente> ListaDeClassificacaoDePatente
        {
            get { return (IList<IClassificacaoPatente>)ViewState[CHAVE_CLASSIFICACAO_PATENTE]; }
            set { ViewState[CHAVE_CLASSIFICACAO_PATENTE] = value; }
        }

        private IList<IAnuidadePatente> ListaDeAnuidadeDaPatente
        {
            get { return (IList<IAnuidadePatente>)ViewState[CHAVE_ANUIDADE_PATENTE]; }
            set { ViewState[CHAVE_ANUIDADE_PATENTE] = value; }
        }

        private IList<ITitular> ListaDeTitulares
        {
            get { return (IList<ITitular>)ViewState[CHAVE_TITULARES]; }
            set { ViewState[CHAVE_TITULARES] = value; }
        }

        private int IndiceBaixaAnuidade
        {
            get { return (int)ViewState[CHAVE_INDICE_BAIXA_ANUIDADE]; }
            set { ViewState[CHAVE_INDICE_BAIXA_ANUIDADE] = value; }
        }

        private enum Estado : byte
        {
            Novo,
            Modifica,
        }

        private void VerificaSeNaturezaEhDeDesenhoIndustrial(INaturezaPatente natureza)
        {
            ExibaTabDeImagemDeDesenhoIndustrial(natureza.EhNaturezaDeDesenhoIndustrial());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlNaturezaPatente.NaturezaPatenteFoiSelecionada += VerificaSeNaturezaEhDeDesenhoIndustrial;
            ctrlDespachoDePatentes.DespachoDePatentesFoiSelecionada += MostreDespacho;

            if (IsPostBack) return;

            Nullable<long> id = null;

            if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
                id = Convert.ToInt64(Request.QueryString["Id"]);

            if (id == null)
                ExibaTelaNovo();
            else
                ExibaTelaDetalhes(id.Value);
        }

        private void ExibaTelaNovo()
        {
            ViewState[CHAVE_ESTADO] = Estado.Novo;
            LimpaTela();
            txtDataDeCadastro.SelectedDate = DateTime.Now;
        }

        private void ExibaTelaDetalhes(long id)
        {
            ViewState[CHAVE_ESTADO] = Estado.Modifica;
            LimpaTela();

            txtDataDoDeposito.Enabled = FabricaDeContexto.GetInstancia().GetContextoAtual().EstaAutorizado("OPE.MP.009.0004");

            IProcessoDePatente processoDePatente = null;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
                processoDePatente = servico.Obtenha(id);

            if (processoDePatente != null)
            {
                if (processoDePatente.Patente != null && processoDePatente.Patente.Manutencao != null)
                {
                    if (processoDePatente.Patente.Manutencao.ManutencaoEstaVencida())
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInformacao("Processo possui manutenção vencida."), false);
                }

                MostreProcessoDePatente(processoDePatente);
            }
        }

        private void ExibaTelaModificar()
        {
            ViewState[CHAVE_ESTADO] = Estado.Modifica;
        }


        private void MostreProcessoDePatente(IProcessoDePatente processoDePatente)
        {
            ViewState[CHAVE_ID_PROCESSO_DE_PATENTE] = processoDePatente.IdProcessoDePatente;
            txtProcesso.Text = processoDePatente.Processo;
            txtDataDeCadastro.SelectedDate = processoDePatente.DataDoCadastro != DateTime.MinValue ? processoDePatente.DataDoCadastro : (DateTime?) null;
            txtDataDeConcessao.SelectedDate = processoDePatente.DataDaConcessao != DateTime.MinValue ? processoDePatente.DataDaConcessao : null;
            txtDataDePublicacao.SelectedDate = processoDePatente.DataDaPublicacao != DateTime.MinValue ? processoDePatente.DataDaPublicacao : null;
            txtDataDoDeposito.SelectedDate = processoDePatente.DataDoDeposito != DateTime.MinValue ? processoDePatente.DataDoDeposito : null;
            txtDataDaVigencia.SelectedDate = processoDePatente.DataDaVigencia != DateTime.MinValue ? processoDePatente.DataDaVigencia : null;
            txtDataDoExame.SelectedDate = processoDePatente.DataDoExame;
            rblProcessoEhDeTerceiro.SelectedValue = processoDePatente.ProcessoEhDeTerceiro ? "1" : "0";

            if (processoDePatente.Procurador != null)
            {
                ctrlProcurador.ProcuradorSelecionado = processoDePatente.Procurador;
                ctrlProcurador.Nome = processoDePatente.Procurador.Pessoa.Nome;
            }

            rblProcessoEhEstrangeiro.SelectedValue = processoDePatente.ProcessoEhEstrangeiro ? "1" : "0";
            rblEstaAtivo.SelectedValue = processoDePatente.Ativo ? "1" : "0";

            if (processoDePatente.Pasta != null)
            {
                ctrlPasta.PastaSelecionada = processoDePatente.Pasta;
                ctrlPasta.Codigo = processoDePatente.Pasta.Codigo;
            }

            if (processoDePatente.Despacho != null)
                MostreDespacho(processoDePatente.Despacho);

            if (processoDePatente.PCT != null)
            {
                rblEHPCT.SelectedValue = "1";
                txtNumeroPCT.Text = processoDePatente.PCT.Numero;
                txtNumeroPCTWO.Text = processoDePatente.PCT.NumeroWO;
                txtDataDaPublicacaoPCT.SelectedDate = processoDePatente.PCT.DataDaPublicacao;
                txtDataDoDepositoPCT.SelectedDate = processoDePatente.PCT.DataDoDeposito;
            }

            ctrlPaisProcesso.PaisSelecionado = processoDePatente.Pais;
            ctrlPaisProcesso.CarreguePaisSelecionado();
            ExibaPatenteSelecionada(processoDePatente.Patente);
        }

        private void LimpaTela()
        {
            ViewState[CHAVE_ID_PATENTE] = null;
            ViewState[CHAVE_ID_PROCESSO_DE_PATENTE] = null;

            var controle = pnlDadosPatente as Control;
            UtilidadesWeb.LimparComponente(ref controle);

            var controle1 = pnlComplemento as Control;
            UtilidadesWeb.LimparComponente(ref controle1);

            var controle2 = pnlAnuidades as Control;
            UtilidadesWeb.LimparComponente(ref controle2);

            var controle3 = pnlPrioridadeUnionista as Control;
            UtilidadesWeb.LimparComponente(ref controle3);

            var controle4 = pnlClassificacao as Control;
            UtilidadesWeb.LimparComponente(ref controle4);

            var controle5 = pnlRadicais as Control;
            UtilidadesWeb.LimparComponente(ref controle5);

            ctrlProcurador.Inicializa();
            ctrlDespachoDePatentes.Inicializa();
            ctrlPasta.Inicializa();

            ctrlProcurador.BotaoNovoEhVisivel = true;
            ctrlDespachoDePatentes.BotaoNovoEhVisivel = true;
            ctrlPasta.BotaoNovoEhVisivel = true;

            rblProcessoEhDeTerceiro.Items.Clear();
            rblProcessoEhDeTerceiro.Items.Add(new ListItem("Não", "0"));
            rblProcessoEhDeTerceiro.Items.Add(new ListItem("Sim", "1"));
            rblProcessoEhDeTerceiro.SelectedValue = "0";

            rblProcessoEhEstrangeiro.Items.Clear();
            rblProcessoEhEstrangeiro.Items.Add(new ListItem("Não", "0"));
            rblProcessoEhEstrangeiro.Items.Add(new ListItem("Sim", "1"));
            rblProcessoEhEstrangeiro.SelectedValue = "0";


            rblEstaAtivo.Items.Clear();
            rblEstaAtivo.Items.Add(new ListItem("Não", "0"));
            rblEstaAtivo.Items.Add(new ListItem("Sim", "1"));
            rblEstaAtivo.SelectedValue = "1";

            rblEstaAtivo.Items.Clear();
            rblEstaAtivo.Items.Add(new ListItem("Não", "0"));
            rblEstaAtivo.Items.Add(new ListItem("Sim", "1"));
            rblEstaAtivo.SelectedValue = "1";

            rblEHPCT.Items.Clear();
            rblEHPCT.Items.Add(new ListItem("Não", "0"));
            rblEHPCT.Items.Add(new ListItem("Sim", "1"));
            rblEHPCT.SelectedValue = "0";
            MostraPCT(false);

            txtDataDeCadastro.Enabled = false;
            txtDataDoDeposito.Enabled = true;

            grdClientes.DataSource = new List<ICliente>();
            grdClientes.DataBind();

            grdInventores.DataSource = new List<IInventor>();
            grdInventores.DataBind();

            grdPrioridadeUnionista.DataSource = new List<IPrioridadeUnionistaPatente>();
            grdPrioridadeUnionista.DataBind();

            grdClassificacaoPatente.DataSource = new List<IClassificacaoPatente>();
            grdClassificacaoPatente.DataBind();

            grdAnuidades.DataSource = new List<IAnuidadePatente>();
            grdAnuidades.DataBind();

            grvRadicais.DataSource = new List<IRadicalPatente>();
            grvRadicais.DataBind();

            grdTitulares.DataSource = new List<ITitular>();
            grdTitulares.DataBind();

            ctrlNaturezaPatente.Inicializa();
            ctrlNaturezaPatente.BotaoNovoEhVisivel = true;
            ctrlCliente.Inicializa();
            ctrlCliente.BotaoNovoEhVisivel = true;
            ctrlInventor.Inicializa();
            ctrlInventor.BotaoNovoEhVisivel = true;

            tabPatente.Tabs[0].Selected = true;
            rpvDadosPatentes.Selected = true;
            btnGerarTodas.Visible = false;

            ctrlPeriodo.Inicializa();
            
            rblPagaManutencao.Items.Clear();
            rblPagaManutencao.Items.Add(new ListItem("  Sim  ", "1"));
            rblPagaManutencao.Items.Add(new ListItem("  Não", "0"));
            rblPagaManutencao.SelectedValue = "0";

            pnlDadosDaManutencao.Visible = false;
            rblFormaDeCobranca.Items.Clear();
            
            foreach (var formaCobrança in FormaCobrancaManutencao.ObtenhaTodas())
                rblFormaDeCobranca.Items.Add(new ListItem(formaCobrança.Descricao  ,formaCobrança.Codigo));

            rblFormaDeCobranca.SelectedValue = FormaCobrancaManutencao.ValorFixo.Codigo;
            FormataValorManutencao(FormaCobrancaManutencao.ValorFixo);
            txtDataDaPrimeiraManutencao.Clear();
            ctrlPaisProcesso.LimparControle();

            //Tab da imagem do desenho industrial
            ExibaTabDeImagemDeDesenhoIndustrial(false);
            imgImagem.ImageUrl = UtilMP.URL_IMAGEM_SEM_FOTO_PATENTE;

            LimpeCamposClassificacaoDePatentes();
        }

        private void MostraPCT(bool mostra)
        {
            pnlPCT.Visible = mostra;
        }

        private IProcessoDePatente MontaObjeto()
        {
            var processoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDePatente>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                processoDePatente.IdProcessoDePatente = Convert.ToInt64(ViewState[CHAVE_ID_PROCESSO_DE_PATENTE]);

            processoDePatente.Patente = MonteObjetoPatente();
            processoDePatente.Processo = txtProcesso.Text;
            processoDePatente.DataDoCadastro = txtDataDeCadastro.SelectedDate.Value;
            processoDePatente.DataDaConcessao = txtDataDeConcessao.SelectedDate;
            processoDePatente.DataDaPublicacao = txtDataDePublicacao.SelectedDate;
            processoDePatente.DataDoDeposito = txtDataDoDeposito.SelectedDate;
            processoDePatente.DataDaVigencia = txtDataDaVigencia.SelectedDate;
            processoDePatente.DataDoExame = txtDataDoExame.SelectedDate;
            processoDePatente.ProcessoEhDeTerceiro = rblProcessoEhDeTerceiro.SelectedValue != "0";
            processoDePatente.Procurador = ctrlProcurador.ProcuradorSelecionado;
            processoDePatente.ProcessoEhEstrangeiro = rblProcessoEhEstrangeiro.SelectedValue != "0";
            processoDePatente.Ativo = rblEstaAtivo.SelectedValue != "0";
            processoDePatente.Despacho = ctrlDespachoDePatentes.DespachoDePatentesSelecionada;
            processoDePatente.Pasta = ctrlPasta.PastaSelecionada;
            processoDePatente.Pais = ctrlPaisProcesso.PaisSelecionado;

            if (rblEHPCT.SelectedValue != "0")
            {
                var pct = FabricaGenerica.GetInstancia().CrieObjeto<IPCT>();
                pct.Numero = txtNumeroPCT.Text;
                pct.NumeroWO = txtNumeroPCTWO.Text;
                pct.DataDaPublicacao = txtDataDaPublicacaoPCT.SelectedDate;
                pct.DataDoDeposito = txtDataDoDepositoPCT.SelectedDate;
            }

            return processoDePatente;
        }

        private IList<string> VerifiqueFormatoDosCampos() 
        {

            if (!string.IsNullOrEmpty(txtProcesso.Text) && !VerifiqueFormatacaoDoNumeroDoProcesso())
            {
                var inconsitencias = new List<string>();

                inconsitencias.Add("Só é permitido números para a formação do número do processo. Ex: ");
                inconsitencias.Add("1234567-8 -> Correto: 12345678");
                inconsitencias.Add("BR 01 2000 123456-7 -> Correto: 20001234567");

                return inconsitencias;
            }

            return new List<string>();
        }

        private IList<string> VerifiqueCamposObrigatorios()
        {
            var inconsitencias = new List<string>();

            if (ctrlNaturezaPatente.NaturezaPatenteSelecionada == null)
                inconsitencias.Add("É necessário informar a natureza da patente.");

            if (ListaDeClientes == null || (ListaDeClientes != null && ListaDeClientes.Count == 0))
                inconsitencias.Add("É necessário informar pelo menos um cliente.");

            if (ListaDeInventores == null || (ListaDeInventores != null && ListaDeInventores.Count == 0))
                inconsitencias.Add("É necessário informar pelo menos um inventor.");

            if (string.IsNullOrEmpty(txtProcesso.Text)) inconsitencias.Add("É necessário informar o número do processo da patente.");
            
            if (!txtDataDeCadastro.SelectedDate.HasValue) inconsitencias.Add("É necessário informar a data de cadastro.");

            if (rblProcessoEhDeTerceiro.SelectedValue == "0" && ctrlProcurador.ProcuradorSelecionado == null) inconsitencias.Add("É necessário informar o procurador.");

            if (ctrlPaisProcesso.PaisSelecionado == null) inconsitencias.Add("Informe o país no qual o processo está vinculado.");

            if (rblPagaManutencao.SelectedValue == "1")
            {
                if (!txtDataDaPrimeiraManutencao.SelectedDate.HasValue)
                    inconsitencias.Add("É necessário informar a data da primeira manutenção.");

                if (string.IsNullOrEmpty(rblFormaDeCobranca.SelectedValue))
                    inconsitencias.Add("É necessário informar a forma de cobrança.");

                if (ctrlPeriodo.PeriodoSelecionado == null)
                    inconsitencias.Add("É necessário informar o período de cobrança.");

                if (!txtValor.Value.HasValue)
                    inconsitencias.Add("É necessário informar o valor de cobrança.");

            }

            return inconsitencias;
        }

        protected void btnSalvar_Click()
        {
            var inconsitencias = VerifiqueCamposObrigatorios();

            if (inconsitencias.Count != 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsistencias(inconsitencias),
                                                        false);
                return;
            }


            var inconsistenciasDeFormato = VerifiqueFormatoDosCampos();

            if (inconsistenciasDeFormato.Count != 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsistencias(inconsistenciasDeFormato),
                                                        false);
                return;
            }


            var processoDePatente = MontaObjeto();
            string mensagem;

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        servico.Inserir(processoDePatente);
                        mensagem = "Processo de patente cadastrado com sucesso.";
                    }
                    else
                    {
                        servico.Modificar(processoDePatente);
                        mensagem = "Processo de patente modificado com sucesso.";
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(mensagem), false);
                ExibaTelaModificar();

            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
            }

        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {
                case "btnSalvar":
                    btnSalvar_Click();
                    break;
            }
        }

        protected void rblEHPCT_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            MostraPCT((sender as RadioButtonList).SelectedValue == "1");
        }

        protected override string ObtenhaIdFuncao()
        {
            return "";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        protected void grdClientes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                IndiceSelecionado = e.Item.ItemIndex;

            if (e.CommandName == "Excluir")
            {
                ListaDeClientes.RemoveAt(IndiceSelecionado);
                MostrarListasDeClientes();
            }
        }

        protected void grdClientes_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdClientes.MasterTableView.RenderColumns)
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void grdClientes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdClientes, ViewState[CHAVE_CLIENTES], e);
        }

        protected void grdInventores_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                IndiceSelecionado = e.Item.ItemIndex;

            if (e.CommandName == "Excluir")
            {
                ListaDeInventores.RemoveAt(IndiceSelecionado);
                MostrarListasDeInventores();
            }
        }

        protected void grdInventores_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdInventores.MasterTableView.RenderColumns)
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void grdInventores_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdInventores, ViewState[CHAVE_INVENTORES], e);
        }

        protected void grdPrioridadeUnionista_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdPrioridadeUnionista.MasterTableView.RenderColumns)
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void grdPrioridadeUnionista_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                IndiceSelecionado = e.Item.ItemIndex;

            if (e.CommandName == "Excluir")
            {
                ListaDePrioridadeUnionista.RemoveAt(IndiceSelecionado);
                MostrarListasDePrioridadesUnionistas();
            }
        }

        protected void grdPrioridadeUnionista_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdPrioridadeUnionista, ViewState[CHAVE_PRIORIDADE_UNIONISTA], e);
        }

        protected void grvClassificacaoPatente_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                IndiceSelecionado = e.Item.ItemIndex;

            if (e.CommandName == "Excluir")
            {
                ListaDeClassificacaoDePatente.RemoveAt(IndiceSelecionado);
                MostrarListasDeClassificacaoDePatentes();
            }
        }

        protected void grvClassificacaoPatente_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdClassificacaoPatente.MasterTableView.RenderColumns)
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void grvClassificacaoPatente_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdClassificacaoPatente, ViewState[CHAVE_CLASSIFICACAO_PATENTE], e);
        }

        protected void grvObrigacoes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                IndiceSelecionado = e.Item.ItemIndex;

            if (e.CommandName == "Excluir")
            {
                ListaDeAnuidadeDaPatente.RemoveAt(IndiceSelecionado);
                MostrarListaDeAnuidadeDaPatente();
            }

            if (e.CommandName == "Baixar")
            {
                var grid = (RadGrid) sender;
                IndiceBaixaAnuidade = (grid.CurrentPageIndex * 10) + IndiceSelecionado;
                BaixarAnuidade(ListaDeAnuidadeDaPatente[IndiceBaixaAnuidade]);
                grdAnuidades.MasterTableView.DataSource = ListaDeAnuidadeDaPatente;
                grdAnuidades.DataBind();
            }
        }

        protected void grvObrigacoes_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdAnuidades.MasterTableView.RenderColumns)
                {
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;

                    if (column.UniqueName == "colunaAnuidadePaga")
                        gridItem[column.UniqueName].Text = gridItem[column.UniqueName].Text.Equals("False") ? "Sim" : "Não";
                }

            }
        }

        protected void grvObrigacoes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdAnuidades, ViewState[CHAVE_ANUIDADE_PATENTE], e);
        }

        private void ExibaPatenteSelecionada(IPatente patente)
        {
            ViewState[CHAVE_ID_PATENTE] = patente.Identificador;
            txtTituloPatente.Text = patente.TituloPatente;
            ctrlNaturezaPatente.Inicializa();
            ctrlNaturezaPatente.NaturezaPatenteSelecionada = patente.NaturezaPatente;
            ctrlNaturezaPatente.DescricaoNaturezaPatente = patente.NaturezaPatente.DescricaoNaturezaPatente;
            ctrlNaturezaPatente.SiglaTipo = patente.NaturezaPatente.SiglaNatureza;
            ListaDeClientes = patente.Clientes;
            MostrarListasDeClientes();
            ListaDeInventores = patente.Inventores;
            MostrarListasDeInventores();
            ListaDePrioridadeUnionista = patente.PrioridadesUnionista;
            MostrarListasDePrioridadesUnionistas();
            txtResumoDaPatente.Text = patente.Resumo;
            txtObservacoes.Text = patente.Observacao;
            ListaDeClassificacaoDePatente = patente.Classificacoes;
            MostrarListasDeClassificacaoDePatentes();
            txtReivindicacoes.Text = patente.QuantidadeReivindicacao.ToString();
            ListaDeAnuidadeDaPatente = patente.Anuidades;
            btnGerarTodas.Visible = true;
            MostrarListaDeAnuidadeDaPatente();
            Radicais = patente.Radicais;
            CarregueGridDeRadicais();
            ListaDeTitulares = patente.Titulares;
            MostrarTitulares();

            rblPagaManutencao.SelectedValue = patente.Manutencao != null ? "1" : "0";

            if (patente.Manutencao != null)
            {
                pnlDadosDaManutencao.Visible = true;
                txtDataDaPrimeiraManutencao.SelectedDate = patente.Manutencao.DataDaProximaManutencao;
                ctrlPeriodo.Codigo = patente.Manutencao.Periodo.Codigo.ToString();
                ctrlPeriodo.PeriodoSelecionado = patente.Manutencao.Periodo;

                rblFormaDeCobranca.SelectedValue = patente.Manutencao.FormaDeCobranca.Codigo;
                txtValor.Value = patente.Manutencao.ValorDeCobranca;
                FormataValorManutencao(patente.Manutencao.FormaDeCobranca);
            }

            if (patente.PatenteEhDeDesenhoIndutrial())
            {
                ExibaTabDeImagemDeDesenhoIndustrial(true);
                
                if (string.IsNullOrEmpty(patente.Imagem))
                    imgImagem.ImageUrl = UtilMP.URL_IMAGEM_SEM_FOTO_PATENTE;
                else
                    imgImagem.ImageUrl = patente.Imagem;
            }
                
        }

        private void ExibaTabDeImagemDeDesenhoIndustrial( bool exiba)
        {
            tabPatente.Tabs[6].Visible = exiba;
        }


        protected void btnAdicionarCliente_ButtonClick(object sender, EventArgs e)
        {
            if (ctrlCliente.ClienteSelecionado == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione o cliente que deseja adicionar."), false);
                return;
            }

            if (ListaDeClientes == null)
                ListaDeClientes = new List<ICliente>();

            if (ListaDeClientes.Contains(ctrlCliente.ClienteSelecionado))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia("Cliente já foi adicionado."), false);
                return;
            }

            ListaDeClientes.Add(ctrlCliente.ClienteSelecionado);
            MostrarListasDeClientes();
            ctrlCliente.Inicializa();
        }

        protected void btnAdicionarInventor_ButtonClick(object sender, EventArgs e)
        {
            if (ctrlInventor.InventorSelecionado == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione o inventor que deseja adicionar."), false);
                return;
            }

            if (ListaDeInventores == null)
                ListaDeInventores = new List<IInventor>();

            if (ListaDeInventores.Contains(ctrlInventor.InventorSelecionado))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia("Inventor já foi adicionado."), false);
                return;
            }

            ListaDeInventores.Add(ctrlInventor.InventorSelecionado);
            MostrarListasDeInventores();
            ctrlInventor.Inicializa();
        }

        private void MostrarListasDeClientes()
        {
            grdClientes.MasterTableView.DataSource = ListaDeClientes;
            grdClientes.DataBind();
        }

        private void MostrarListasDeInventores()
        {
            grdInventores.MasterTableView.DataSource = ListaDeInventores;
            grdInventores.DataBind();
        }

        protected void btnAdicionarPrioridadeUnionista_ButtonClick(object sender, EventArgs e)
        {
            if (!PodeAdicionarPrioridadeUnionista())
                return;

            var prioridadeUnionista = FabricaGenerica.GetInstancia().CrieObjeto<IPrioridadeUnionistaPatente>();

            prioridadeUnionista.DataPrioridade = txtDataPrioridade.SelectedDate;
            prioridadeUnionista.Pais = ctrlPais.PaisSelecionado;
            prioridadeUnionista.NumeroPrioridade = txtNumeroPrioridade.Text;

            if (ListaDePrioridadeUnionista == null)
                ListaDePrioridadeUnionista = new List<IPrioridadeUnionistaPatente>();

            ListaDePrioridadeUnionista.Add(prioridadeUnionista);
            MostrarListasDePrioridadesUnionistas();
        }

        private bool PodeAdicionarPrioridadeUnionista()
        {
            if (txtDataPrioridade.SelectedDate == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Informe a data da prioridade."), false);
                return false;
            }

            if (ctrlPais.PaisSelecionado == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione o país de origem."), false);
                return false;
            }

            if (string.IsNullOrEmpty(txtNumeroPrioridade.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Informe o número da prioridade."), false);
                return false;
            }

            return true;
        }

        private void MostrarListasDePrioridadesUnionistas()
        {
            grdPrioridadeUnionista.MasterTableView.DataSource = ListaDePrioridadeUnionista;
            grdPrioridadeUnionista.DataBind();
            LimpeCamposPrioridadeUnionista();
        }

        private void LimpeCamposPrioridadeUnionista()
        {
            txtDataPrioridade.Clear();
            ctrlPais.PaisSelecionado = null;
            txtNumeroPrioridade.Text = string.Empty;
        }

        protected void btnAdicionarClassificacao_ButtonClick(object sender, EventArgs e)
        {
            if (!PodeAdicionarClassficicaoPatente())
                return;

            var classficacaoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IClassificacaoPatente>();

            classficacaoDePatente.Classificacao = txtClassificacao.Text;
            classficacaoDePatente.DescricaoClassificacao = txtDescricaoClassificacao.Text;
            classficacaoDePatente.TipoClassificacao = TipoClassificacaoPatente.ObtenhaPorCodigo(int.Parse(cboTipoDeClassificacao.SelectedItem.Value));

            if (ListaDeClassificacaoDePatente == null)
                ListaDeClassificacaoDePatente = new List<IClassificacaoPatente>();

            ListaDeClassificacaoDePatente.Add(classficacaoDePatente);
            MostrarListasDeClassificacaoDePatentes();
        }

        private bool PodeAdicionarClassficicaoPatente()
        {
            if (string.IsNullOrEmpty(txtDescricaoClassificacao.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Informe a descrição classificação."), false);
                return false;
            }

            if (string.IsNullOrEmpty(cboTipoDeClassificacao.SelectedValue))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione o tipo de classificação."), false);
                return false;
            }

            return true;
        }

        private void CarregueComboTipoDeClassificacao()
        {
            var itemInternacional = new RadComboBoxItem(TipoClassificacaoPatente.Internacional.Descricao, TipoClassificacaoPatente.Internacional.Codigo.ToString());
            var itemNacional = new RadComboBoxItem(TipoClassificacaoPatente.Nacional.Descricao, TipoClassificacaoPatente.Nacional.Codigo.ToString());

            if (cboTipoDeClassificacao.Items.Count == 0)
            {
                cboTipoDeClassificacao.Items.Add(itemInternacional);
                cboTipoDeClassificacao.Items.Add(itemNacional);

                itemInternacional.DataBind();
                itemNacional.DataBind();
            }
        }

        private void MostrarListasDeClassificacaoDePatentes()
        {
            grdClassificacaoPatente.MasterTableView.DataSource = ListaDeClassificacaoDePatente;
            grdClassificacaoPatente.DataBind();
            LimpeCamposClassificacaoDePatentes();
        }

        private void LimpeCamposClassificacaoDePatentes()
        {
            txtClassificacao.Text = string.Empty;
            txtDescricaoClassificacao.Text = string.Empty;
            CarregueComboTipoDeClassificacao();
        }

        private void MostrarListaDeAnuidadeDaPatente()
        {
            grdAnuidades.MasterTableView.DataSource = ListaDeAnuidadeDaPatente;
            grdAnuidades.DataBind();
            LimpeCamposAnuidadeDePatentes();
        }

        private void LimpeCamposAnuidadeDePatentes()
        {
            txtDescricaoDaAnuidade.Text = string.Empty;
            txtInicioPrazoPagamento.Clear();
            txtPagamentoSemMulta.Clear();
            txtPagamentoComMulta.Clear();
            txtDataPagamento.Clear();
            txtValorPagamento.Text = string.Empty;
        }

        private void BaixarAnuidade(IAnuidadePatente anuidadePatente)
        {
            txtDescricaoDaAnuidade.Text = anuidadePatente.DescricaoAnuidade;
            txtInicioPrazoPagamento.SelectedDate = anuidadePatente.DataLancamento;
            txtPagamentoSemMulta.SelectedDate = anuidadePatente.DataVencimentoSemMulta;
            txtPagamentoComMulta.SelectedDate = anuidadePatente.DataVencimentoComMulta;
            txtDataPagamento.SelectedDate = anuidadePatente.DataPagamento;
            txtValorPagamento.Text = anuidadePatente.ValorPagamento.ToString();
            VisibilidadeBaixar(false);
        }

        private void VisibilidadeBaixar(bool visibilidade)
        {
            btnBaixar.Visible = !visibilidade;
            btnCancelarBaixaAnuidade.Visible = !visibilidade;
            txtDescricaoDaAnuidade.Enabled = visibilidade;
            txtInicioPrazoPagamento.Enabled = visibilidade;
            txtPagamentoSemMulta.Enabled = visibilidade;
            txtPagamentoComMulta.Enabled = visibilidade;
        }

        protected void btnBaixar_ButtonClick(object sender, EventArgs e)
        {
            if (!PodeBaixarAnuidadeDaPatente())
                return;

            var anuidadeDaPatente = FabricaGenerica.GetInstancia().CrieObjeto<IAnuidadePatente>();

            anuidadeDaPatente.DescricaoAnuidade = txtDescricaoDaAnuidade.Text;
            anuidadeDaPatente.DataLancamento = txtInicioPrazoPagamento.SelectedDate;
            anuidadeDaPatente.DataVencimentoSemMulta = txtPagamentoSemMulta.SelectedDate;
            anuidadeDaPatente.DataVencimentoComMulta = txtPagamentoComMulta.SelectedDate;
            anuidadeDaPatente.DataPagamento = txtDataPagamento.SelectedDate;
            anuidadeDaPatente.AnuidadePaga = true;

            if (!string.IsNullOrEmpty(txtValorPagamento.Text))
                anuidadeDaPatente.ValorPagamento = double.Parse(txtValorPagamento.Text.Replace(".", ","));

            if (ListaDeAnuidadeDaPatente == null)
                ListaDeAnuidadeDaPatente = new List<IAnuidadePatente>();

            ListaDeAnuidadeDaPatente.RemoveAt(IndiceBaixaAnuidade);
            ListaDeAnuidadeDaPatente.Insert(IndiceBaixaAnuidade, anuidadeDaPatente);
            MostrarListaDeAnuidadeDaPatente();
            VisibilidadeBaixar(true);

            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Anuidade baixada com sucesso."), false);
        }

        private bool PodeBaixarAnuidadeDaPatente()
        {
            if (txtDataPagamento.SelectedDate == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Informe a data do pagamento."), false);
                return false;
            }

            if (string.IsNullOrEmpty(txtValorPagamento.Text) || txtValorPagamento.Text.Equals("0"))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Informe o valor do pagamento."), false);
                return false;
            }

            return true;
        }

        protected void btnGerarTodas_ButtonClick(object sender, EventArgs e)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
            {
                var processo = servico.Obtenha((long)ViewState[CHAVE_ID_PROCESSO_DE_PATENTE]);

                if (processo.DataDoDeposito == null || txtDataDoDeposito.SelectedDate == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                    UtilidadesWeb.MostraMensagemDeInconsitencia("É necessário informar a data de depósito do processo."), false);
                    return;
                }

                if (processo.Patente.PatenteEhDeDesenhoIndutrial())
                    CalculeAnuidadesPatentesDeNaturezaDI(processo.DataDoDeposito.Value);
                else
                    CalculeAnuidadesPatentesDeNatureza(processo.DataDoDeposito.Value);
            }
        }


        private IPatente MonteObjetoPatente()
        {
            var patente = FabricaGenerica.GetInstancia().CrieObjeto<IPatente>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                patente.Identificador = Convert.ToInt64(ViewState[CHAVE_ID_PATENTE]);

            patente.TituloPatente = txtTituloPatente.Text;
            patente.NaturezaPatente = ctrlNaturezaPatente.NaturezaPatenteSelecionada;
            patente.Resumo = txtResumoDaPatente.Text;
            patente.Observacao = txtObservacoes.Text;

            if (!string.IsNullOrEmpty(txtReivindicacoes.Text))
                patente.QuantidadeReivindicacao = int.Parse(txtReivindicacoes.Text);

            patente.DataCadastro = DateTime.Now;

            if (ListaDeAnuidadeDaPatente != null && ListaDeAnuidadeDaPatente.Count > 0)
                patente.Anuidades = ListaDeAnuidadeDaPatente;

            if (ListaDeClassificacaoDePatente != null && ListaDeClassificacaoDePatente.Count > 0)
                patente.Classificacoes = ListaDeClassificacaoDePatente;

            if (ListaDePrioridadeUnionista != null && ListaDePrioridadeUnionista.Count > 0)
                patente.PrioridadesUnionista = ListaDePrioridadeUnionista;

            if (ListaDeClientes != null && ListaDeClientes.Count > 0)
                patente.Clientes = ListaDeClientes;

            if (ListaDeInventores != null && ListaDeInventores.Count > 0)
                patente.Inventores = ListaDeInventores;

            if (Radicais != null && Radicais.Count > 0)
                patente.Radicais = Radicais;

            if (ListaDeTitulares != null && ListaDeTitulares.Count > 0)
                patente.Titulares = ListaDeTitulares;


            if (rblPagaManutencao.SelectedValue.Equals("1"))
            {
                var manutencao = FabricaGenerica.GetInstancia().CrieObjeto<IManutencao>();

                manutencao.DataDaProximaManutencao = txtDataDaPrimeiraManutencao.SelectedDate;

                manutencao.Periodo = ctrlPeriodo.PeriodoSelecionado;
                manutencao.FormaDeCobranca = FormaCobrancaManutencao.ObtenhaPorCodigo(rblFormaDeCobranca.SelectedValue);
                manutencao.ValorDeCobranca = txtValor.Value.Value;
                patente.Manutencao = manutencao;
            }

            return patente;
        }

        protected void grvObrigacoes_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                ((GridDataItem)e.Item)["colunaAnuidadePaga"].Text = ((GridDataItem)e.Item)["colunaAnuidadePaga"].Text.Equals("True") ? "Sim" : "Não";
        }

        private void CalculeAnuidadesPatentesDeNatureza(DateTime dataDeDeposito)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
                ListaDeAnuidadeDaPatente = servico.CalculeAnuidadesPatentesDeNaturezaPIeMU(dataDeDeposito);

            MostrarListaDeAnuidadeDaPatente();
        }

        private void CalculeAnuidadesPatentesDeNaturezaDI(DateTime dataDeDeposito)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
                ListaDeAnuidadeDaPatente = servico.CalculeAnuidadesPatentesDeNaturezaDI(dataDeDeposito);

            MostrarListaDeAnuidadeDaPatente();
        }

        protected void btnAdicionarRadical_ButtonClick(object sender, EventArgs e)
        {
            string radical = txtRadical.Text;

            if (string.IsNullOrEmpty(radical))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                    UtilidadesWeb.MostraMensagemDeInformacao("Informe o radical a ser adicionado."), false);
                return;
            }

            if (Radicais.Any(radicalPatente => radicalPatente.Colidencia == radical))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao("Radical informado já foi adicionado."), false);
                return;
            }

            var radicalDaPatente = FabricaGenerica.GetInstancia().CrieObjeto<IRadicalPatente>();

            radicalDaPatente.Colidencia = radical;

            Radicais.Add(radicalDaPatente);
            CarregueGridDeRadicais();
        }

        private void CarregueGridDeRadicais()
        {
            grvRadicais.DataSource = Radicais;
            grvRadicais.DataBind();
            LimpeCamposRadicais();
        }

        private void LimpeCamposRadicais()
        {
            txtRadical.Text = string.Empty;
        }

        private IList<IRadicalPatente> Radicais
        {
            get
            {
                if (ViewState[CHAVE_RADICAIS] == null)
                    ViewState[CHAVE_RADICAIS] = new List<IRadicalPatente>();

                return (List<IRadicalPatente>)ViewState[CHAVE_RADICAIS];
            }

            set { ViewState[CHAVE_RADICAIS] = value; }
        }

        protected void grvRadicais_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                IndiceSelecionado = e.Item.ItemIndex;

            if (e.CommandName == "Excluir")
            {
                Radicais.RemoveAt(IndiceSelecionado);
                CarregueGridDeRadicais();
            }
        }

        protected void grvRadicais_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grvRadicais_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        protected void grdTitulares_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                IndiceSelecionado = e.Item.ItemIndex;

            if (e.CommandName == "Excluir")
            {
                ListaDeTitulares.RemoveAt(IndiceSelecionado);
                MostrarTitulares();
            }
        }

        protected void grdTitulares_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdTitulares.MasterTableView.RenderColumns)
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void grdTitulares_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdTitulares, ViewState[CHAVE_INVENTORES], e);
        }

        protected void btnAdicionarTitular_ButtonClick(object sender, EventArgs e)
        {

            if (ctrlTitular.TitularSelecionado == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione o titular que deseja adicionar."), false);
                return;
            }

            if (ListaDeTitulares == null)
                ListaDeTitulares = new List<ITitular>();

            if (ListaDeTitulares.Contains(ctrlTitular.TitularSelecionado))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia("Titular já foi adicionado."), false);
                return;
            }

            ListaDeTitulares.Add(ctrlTitular.TitularSelecionado);
            MostrarTitulares();
            ctrlTitular.Inicializa();
        }

        private void MostrarTitulares()
        {
            grdTitulares.MasterTableView.DataSource = ListaDeTitulares;
            grdTitulares.DataBind();
        }

        protected void rblPagaManutencao_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var rblManutencao = sender as RadioButtonList;

            if (rblManutencao != null && rblManutencao.SelectedValue == "1")
                pnlDadosDaManutencao.Visible = true;
            else
            {
                pnlDadosDaManutencao.Visible = false;
                rblFormaDeCobranca.ClearSelection();
                txtValor.Text = "";
                txtValor.Value = null;
                ctrlPeriodo.Inicializa();
                txtDataDaPrimeiraManutencao.Clear();
            }
        }

        protected void btnCancelarBaixaAnuidade_ButtonClick(object sender, EventArgs e)
        {
            var anuidadeDaPatente = FabricaGenerica.GetInstancia().CrieObjeto<IAnuidadePatente>();

            anuidadeDaPatente.DescricaoAnuidade = txtDescricaoDaAnuidade.Text;
            anuidadeDaPatente.DataLancamento = txtInicioPrazoPagamento.SelectedDate;
            anuidadeDaPatente.DataVencimentoSemMulta = txtPagamentoSemMulta.SelectedDate;
            anuidadeDaPatente.DataVencimentoComMulta = txtPagamentoComMulta.SelectedDate;
            anuidadeDaPatente.DataPagamento = txtDataPagamento.SelectedDate;

            if (!string.IsNullOrEmpty(txtValorPagamento.Text))
                anuidadeDaPatente.ValorPagamento = double.Parse(txtValorPagamento.Text.Replace(".", ","));

            if (ListaDeAnuidadeDaPatente == null)
                ListaDeAnuidadeDaPatente = new List<IAnuidadePatente>();

            MostrarListaDeAnuidadeDaPatente();
            VisibilidadeBaixar(true);
        }

        protected void uplImagem_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                if (uplImagem.UploadedFiles.Count > 0)
                {
                    var arquivo = uplImagem.UploadedFiles[0];
                    var pastaDeDestino = Server.MapPath(UtilMP.URL_IMAGEM_PATENTE);

                    Util.CrieDiretorio(pastaDeDestino);

                    var caminhoArquivo = Path.Combine(pastaDeDestino, arquivo.GetNameWithoutExtension() + arquivo.GetExtension());

                    arquivo.SaveAs(caminhoArquivo);
                    imgImagem.ImageUrl = string.Concat(UtilMP.URL_IMAGEM_PATENTE, "/", arquivo.GetNameWithoutExtension() + arquivo.GetExtension());
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstancia().Erro("Erro ao carregar imagem, exceção: ", ex);
            }
        }

        private void FormataValorManutencao(FormaCobrancaManutencao formaCobranca)
        {
            if (formaCobranca.Equals(FormaCobrancaManutencao.ValorFixo))
            {
                txtValor.Type = NumericType.Currency;
                return;
            }


            txtValor.Type = NumericType.Percent;
        }

        protected void rblFormaDeCobranca_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FormataValorManutencao(FormaCobrancaManutencao.ObtenhaPorCodigo(rblFormaDeCobranca.SelectedValue));
        }

        private bool VerifiqueFormatacaoDoNumeroDoProcesso()
        {
            var expressao = new Regex(@"^\d*[0-9](|.\d*[0-9])?$");
            return expressao.IsMatch(txtProcesso.Text);
        }

        private void MostreDespacho(IDespachoDePatentes despacho)
        {
            ctrlDespachoDePatentes.DespachoDePatentesSelecionada = despacho;
            ctrlDespachoDePatentes.CodigoDespachoDePatentes = despacho.Codigo;

            txtProvidencia.Text = despacho.TipoProvidencia;
            txtPrazoParaProvidencia.Text = despacho.PrazoProvidencia.ToString();
            txtSituacaoDoProcesso.Text = despacho.Situacao;
            txtDescricaoDoDespacho.Text = despacho.Descricao;
        }
    }
}