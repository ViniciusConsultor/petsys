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
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class cdPatente : SuperPagina
    {
        private const string ID_OBJETO = "ID_OBJETO_CD_PATENTES";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_PATENTES";
        private const string CHAVE_CLIENTES = "CHAVE_CLIENTES";
        private const string CHAVE_INVENTORES = "CHAVE_INVENTORES";
        private const string CHAVE_PRIORIDADE_UNIONISTA = "CHAVE_PRIORIDADE_UNIONISTA";
        private const string CHAVE_CLASSIFICACAO_PATENTE = "CHAVE_CLASSIFICACAO_PATENTE";
        private const string CHAVE_ANUIDADE_PATENTE = "CHAVE_ANUIDADE_PATENTE";

        private IList<ICliente> ListaDeClientes
        {
            get { return (IList<ICliente>) ViewState[CHAVE_CLIENTES]; }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlPatente.PatenteFoiSelecionada += ExibaPatenteSelecionada;
            CarregueComboTipoDeClassificacao();

            if(!IsPostBack)
                ExibaTelaInicial();
            
        }

        private void ExibaTelaInicial()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            var controlePanelPatente = pnlDadosPatente as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelPatente, true);

            UtilidadesWeb.LimparComponente(ref controlePanelPatente);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelPatente, false);

            var controlePanelComplemento = pnlComplemento as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, true);

            UtilidadesWeb.LimparComponente(ref controlePanelComplemento);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, false);

            var controlePanelAnuidades = pnlAnuidades as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelAnuidades, true);

            UtilidadesWeb.LimparComponente(ref controlePanelComplemento);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelAnuidades, false);

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
            
            ViewState[CHAVE_ESTADO] = Estado.Inicial;
            ViewState[ID_OBJETO] = null;

            ctrlPatente.Inicializa();
            ctrlPatente.EnableLoadOnDemand = true;
            ctrlPatente.ShowDropDownOnTextboxClick = true;
            ctrlPatente.AutoPostBack = true;
            ctrlNaturezaPatente.Inicializa();
            ctrlNaturezaPatente.BotaoNovoEhVisivel = true;
            ctrlCliente.Inicializa();
            ctrlCliente.BotaoNovoEhVisivel = true;
            ctrlInventor.Inicializa();
            ctrlInventor.BotaoNovoEhVisivel = true;

            RadTabStrip1.Tabs[0].Selected = true;
            rpvDadosPatentes.Selected = true;
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
                BaixarAnuidade(ListaDeAnuidadeDaPatente[IndiceSelecionado]);
                ListaDeAnuidadeDaPatente.RemoveAt(IndiceSelecionado);
            }
        }

        protected void grvObrigacoes_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdAnuidades.MasterTableView.RenderColumns)
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void grvObrigacoes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdAnuidades, ViewState[CHAVE_ANUIDADE_PATENTE], e);
        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {
                case "btnNovo":
                    btnNovo_Click();
                    break;
                case "btnModificar":
                    btnModificar_Click();
                    break;
                case "btnExcluir":
                    btnExcluir_Click();
                    break;
                case "btnSalvar":
                    btnSalvar_Click();
                    break;
                case "btnCancelar":
                    btnCancelar_Click();
                    break;
                case "btnSim":
                    btnSim_Click();
                    break;
                case "btnNao":
                    btnNao_Click();
                    break;
            }
        }

        private void btnNao_Click()
        {
            ExibaTelaInicial();
        }

        private void btnSim_Click()
        {
            try
            {
                var patente = MonteObjetoPatente();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
                    servico.Exluir(patente.Identificador);

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraMensagemDeInformacao("Patente excluída com sucesso."), false);
                ExibaTelaInicial();
            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
            }
        }

        private void btnCancelar_Click()
        {
            ExibaTelaInicial();
        }

        private void btnSalvar_Click()
        {
            GravePatente();
        }

        private void btnExcluir_Click()
        {
            ExibaTelaExcluir();
        }

        private void btnModificar_Click()
        {
            GravePatente();
        }

        private void btnNovo_Click()
        {
            ExibaTelaNovo();
        }

        private void ExibaTelaNovo()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            ViewState[CHAVE_ESTADO] = Estado.Novo;

            var controlePanelPatente = pnlDadosPatente as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelPatente, true);

            UtilidadesWeb.LimparComponente(ref controlePanelPatente);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelPatente, true);

            var controlePanelComplemento = pnlComplemento as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, true);

            UtilidadesWeb.LimparComponente(ref controlePanelComplemento);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, true);

            var controlePanelAnuidades = pnlAnuidades as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelAnuidades, true);

            UtilidadesWeb.LimparComponente(ref controlePanelComplemento);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelAnuidades, true);

            ctrlPatente.Inicializa();
            ctrlPatente.EnableLoadOnDemand = false;
            ctrlPatente.ShowDropDownOnTextboxClick = false;
            ctrlPatente.AutoPostBack = false;
        }

        private void ExibaPatenteSelecionada(IPatente patente)
        {
            ExibaTelaModificar();

            txtTituloPatente.Text = patente.TituloPatente;
            ctrlNaturezaPatente.Inicializa();
            ctrlNaturezaPatente.NaturezaPatenteSelecionada = patente.NaturezaPatente;
            ctrlNaturezaPatente.DescricaoNaturezaPatente = patente.NaturezaPatente.DescricaoNaturezaPatente;
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
            MostrarListaDeAnuidadeDaPatente();
        }

        private void ExibaTelaModificar()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            ctrlPatente.EnableLoadOnDemand = false;
            ctrlPatente.ShowDropDownOnTextboxClick = false;

            var controlePanelPatente = pnlDadosPatente as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelPatente, true);

            UtilidadesWeb.LimparComponente(ref controlePanelPatente);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelPatente, true);

            var controlePanelComplemento = pnlComplemento as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, true);

            UtilidadesWeb.LimparComponente(ref controlePanelComplemento);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, true);

            var controlePanelAnuidades = pnlAnuidades as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelAnuidades, true);

            UtilidadesWeb.LimparComponente(ref controlePanelComplemento);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelAnuidades, true);

            ViewState[CHAVE_ESTADO] = Estado.Modifica;
        }

        private void ExibaTelaExcluir()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = true;

            ViewState[CHAVE_ESTADO] = Estado.Remove;

            var controlePanelPatente = pnlDadosPatente as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelPatente, false);

            UtilidadesWeb.LimparComponente(ref controlePanelPatente);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelPatente, false);

            var controlePanelComplemento = pnlComplemento as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, false);

            UtilidadesWeb.LimparComponente(ref controlePanelComplemento);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, false);

            var controlePanelAnuidades = pnlAnuidades as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelAnuidades, false);

            UtilidadesWeb.LimparComponente(ref controlePanelComplemento);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelAnuidades, false);
        }

        protected void btnAdicionarCliente_ButtonClick(object sender, EventArgs e)
        {
            if(ctrlCliente.ClienteSelecionado == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione o cliente que deseja adicionar."), false);
                return;
            }

            if(ListaDeClientes == null)
                ListaDeClientes = new List<ICliente>();

            if(ListaDeClientes.Contains(ctrlCliente.ClienteSelecionado))
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

        private enum Estado : byte
        {
            Inicial = 1,
            Novo,
            Consulta,
            Modifica,
            Remove
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.006";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        protected void btnAdicionarPrioridadeUnionista_ButtonClick(object sender, EventArgs e)
        {
            if(!PodeAdicionarPrioridadeUnionista())
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
            if(txtDataPrioridade.SelectedDate == null)
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
            if(!PodeAdicionarClassficicaoPatente())
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
            if(string.IsNullOrEmpty(txtClassificacao.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Informe a classificação."), false);
                return false;
            }

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
            IList<TipoClassificacaoPatente> tiposDeClassficacaoPatente = new List<TipoClassificacaoPatente>();

            tiposDeClassficacaoPatente.Add(TipoClassificacaoPatente.Internacional);
            tiposDeClassficacaoPatente.Add(TipoClassificacaoPatente.Nacional);

            cboTipoDeClassificacao.DataValueField = "Codigo";
            cboTipoDeClassificacao.DataTextField = "Descricao";
            cboTipoDeClassificacao.DataSource = tiposDeClassficacaoPatente;
            cboTipoDeClassificacao.DataBind();
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

        protected void btnNovaAnuidade_ButtonClick(object sender, EventArgs e)
        {
            if(!PodeAdicionarAnuidadeDaPatente())
                return;

            var anuidadeDaPatente = FabricaGenerica.GetInstancia().CrieObjeto<IAnuidadePatente>();

            anuidadeDaPatente.DescricaoAnuidade = txtDescricaoDaAnuidade.Text;
            anuidadeDaPatente.DataLancamento = txtInicioPrazoPagamento.SelectedDate;
            anuidadeDaPatente.DataVencimentoSemMulta = txtPagamentoSemMulta.SelectedDate;
            anuidadeDaPatente.DataVencimentoComMulta = txtPagamentoComMulta.SelectedDate;
            anuidadeDaPatente.DataPagamento = txtDataPagamento.SelectedDate;

            if (!string.IsNullOrEmpty(txtValorPagamento.Text))
                anuidadeDaPatente.ValorPagamento = double.Parse(txtValorPagamento.Text);

            if (ListaDeAnuidadeDaPatente == null)
                ListaDeAnuidadeDaPatente = new List<IAnuidadePatente>();

            ListaDeAnuidadeDaPatente.Add(anuidadeDaPatente);
            MostrarListaDeAnuidadeDaPatente();
        }

        private bool PodeAdicionarAnuidadeDaPatente()
        {
            if (string.IsNullOrEmpty(txtDescricaoDaAnuidade.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Informe a descrição da anuidade."), false);
                return false;
            }

            if (txtInicioPrazoPagamento.SelectedDate == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Informe a data de início."), false);
                return false;
            }

            return true;
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
            btnNovaAnuidade.Visible = visibilidade;
            btnBaixar.Visible = !visibilidade;
        }

        protected void btnBaixar_ButtonClick(object sender, EventArgs e)
        {
            if(!PodeBaixarAnuidadeDaPatente())
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

            ListaDeAnuidadeDaPatente.Add(anuidadeDaPatente);
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
        }

        private void GravePatente()
        {
            if(!PodeGravarPatente())
                return;

            string mensagem;

            try
            {
                var patente = MonteObjetoPatente();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        servico.Insira(patente);
                        mensagem = "Patente cadastrada com sucesso.";
                    }
                    else
                    {
                        servico.Modificar(patente);
                        mensagem = "Patente modificada com sucesso.";
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraMensagemDeInformacao(mensagem), false);
                ExibaTelaInicial();    
            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
            }
        }

        private bool PodeGravarPatente()
        {
            if (ctrlNaturezaPatente.NaturezaPatenteSelecionada == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione o tipo da patente."), false);
                return false;
            }

            if (ListaDeClientes == null || (ListaDeClientes != null && ListaDeClientes.Count == 0))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Informe os clientes da patente."), false);
                return false;
            }

            if (ListaDeInventores == null || (ListaDeInventores != null && ListaDeInventores.Count == 0))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Informe os inventores da patente."), false);
                return false;
            }

            return true;
        }

        private IPatente MonteObjetoPatente()
        {
            var patente = ViewState[CHAVE_ESTADO].Equals(Estado.Novo) ? FabricaGenerica.GetInstancia().CrieObjeto<IPatente>() : ctrlPatente.PatenteSelecionada;

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

            return patente;
        }
    }
}