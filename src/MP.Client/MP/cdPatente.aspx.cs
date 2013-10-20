using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;
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
            ctrlTipoDePatente.Inicializa();
            ctrlCliente.Inicializa();
            ctrlCliente.BotaoNovoEhVisivel = true;
            ctrlInventor.Inicializa();
            ctrlInventor.BotaoNovoEhVisivel = true;

            RadTabStrip1.Tabs[0].Selected = true;
            rpvDadosPatentes.Selected = true;
        }

        protected void grdClientes_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void grdClientes_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdClientes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        protected void grdInventores_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void grdInventores_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdInventores_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        protected void grdPrioridadeUnionista_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdPrioridadeUnionista_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void grdPrioridadeUnionista_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
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
            
        }

        private void btnCancelar_Click()
        {
            ExibaTelaInicial();
        }

        private void btnSalvar_Click()
        {
            
        }

        private void btnExcluir_Click()
        {
            ExibaTelaExcluir();
        }

        private void btnModificar_Click()
        {
            ExibaTelaModificar();
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

        protected void grvClassificacaoPatente_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void grvClassificacaoPatente_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grvClassificacaoPatente_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        protected void grvObrigacoes_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void grvObrigacoes_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grvObrigacoes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        private void ExibaPatenteSelecionada(IPatente patente)
        {
        }

        private void ExibaTelaModificar()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = false;
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
    }
}