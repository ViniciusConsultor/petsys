using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class cdDespachoDePatentes : SuperPagina
    {
        private const string ID_OBJETO = "ID_OBJETO_CD_DESPACHODEPATENTES";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_DESPACHODEPATENTES";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlDespachoDePatentes.DespachoDePatentesFoiSelecionada += MostreDespachoDePatentes;

            if (!IsPostBack)
            {
                this.ExibaTelaInicial();
            }
        }

        private void CarregueComponentes()
        {
            rblDesativaProcesso.Items.Clear();
            rblDesativaProcesso.Items.Add(new ListItem("Sim", "1"));
            rblDesativaProcesso.Items.Add(new ListItem("Não", "0"));
            rblDesativaProcesso.SelectedValue = "0";

            rblAgendaPagamento.Items.Clear();
            rblAgendaPagamento.Items.Add(new ListItem("Sim", "1"));
            rblAgendaPagamento.Items.Add(new ListItem("Não", "0"));
            rblAgendaPagamento.SelectedValue = "0";
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

            var controlePanel = this.PanelCdDespachoDePatentes as Control;

            UtilidadesWeb.LimparComponente(ref controlePanel);
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            pnlDespacho.Visible = true;
            ctrlDespachoDePatentes.Inicializa();
            ctrlDespachoDePatentes.EnableLoadOnDemand = true;
            ctrlDespachoDePatentes.ShowDropDownOnTextboxClick = true;
            ctrlDespachoDePatentes.AutoPostBack = true;
            ViewState[CHAVE_ESTADO] = Estado.Inicial;
            ViewState[ID_OBJETO] = null;
            CarregueComponentes();
        }

        private void ExibaTelaConsultar()
        {
            var controle = PanelCdDespachoDePatentes as Control;

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;
            UtilidadesWeb.HabilitaComponentes(ref controle, false);
        }

        protected void btnNovo_Click()
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

            var controlePanel = this.PanelCdDespachoDePatentes as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            //UtilidadesWeb.LimparComponente(ref controlePanel);
            ViewState[CHAVE_ESTADO] = Estado.Novo;

            ctrlDespachoDePatentes.Inicializa();
            ctrlDespachoDePatentes.EnableLoadOnDemand = false;
            ctrlDespachoDePatentes.ShowDropDownOnTextboxClick = false;
            ctrlDespachoDePatentes.AutoPostBack = false;
            ctrlDespachoDePatentes.TextoItemVazio = string.Empty;
        }

        private void ExibaTelaModificar()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            var controlePanel = this.PanelCdDespachoDePatentes as Control;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ViewState[CHAVE_ESTADO] = Estado.Modifica;

            ctrlDespachoDePatentes.EnableLoadOnDemand = false;
            ctrlDespachoDePatentes.ShowDropDownOnTextboxClick = false;
            ctrlDespachoDePatentes.AutoPostBack = false;
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

            var controlePanel = this.PanelCdDespachoDePatentes as Control;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

        }

        protected void btnCancela_Click()
        {
            ExibaTelaInicial();
        }

        private IDespachoDePatentes MontaObjetoDespachoDePatentes()
        {
            var despachoDePatentes = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDePatentes>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                despachoDePatentes.IdDespachoDePatente = Convert.ToInt64(ViewState[ID_OBJETO]);


            despachoDePatentes.AgendarPagamento = rblAgendaPagamento.SelectedValue != "0";
            despachoDePatentes.Codigo = ctrlDespachoDePatentes.CodigoDespachoDePatentes;
            despachoDePatentes.DesativaProcesso = rblDesativaProcesso.SelectedValue != "0";
            despachoDePatentes.Descricao = txtDescricao.Text;

            if (txtPrazoProvidencia.Value.HasValue)
                despachoDePatentes.PrazoProvidencia = Convert.ToInt32(txtPrazoProvidencia.Text);

            despachoDePatentes.Situacao = txtSituacao.Text;
            despachoDePatentes.TipoProvidencia = txtProvidencia.Text;
            despachoDePatentes.Titulo = txtTitulo.Text;

            return despachoDePatentes;
        }

        private IList<string> validaErrosDePreenchimento()
        {
            var mensagem = new List<string>();

            if (string.IsNullOrEmpty(ctrlDespachoDePatentes.CodigoDespachoDePatentes))
                 mensagem .Add("O código do despacho deve ser informado.");
            
            if (string.IsNullOrEmpty(txtTitulo.Text))
                mensagem.Add("O título do despacho deve ser informado.");


            return mensagem;
        }

        private void btnSalva_Click()
        {
            var mensagem = string.Empty;

            try
            {
                var erros = validaErrosDePreenchimento();

                if (erros.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsistencias(erros), false);
                    return;
                }
                
                var despachoDePatentes = MontaObjetoDespachoDePatentes();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDePatentes>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        IList<IDespachoDePatentes> listaDeDespachosCadastrados = new List<IDespachoDePatentes>();

                        listaDeDespachosCadastrados = verificaSeJaExisteDespachoCadastrado(despachoDePatentes.Codigo, servico);

                        if (listaDeDespachosCadastrados.Count > 0)
                        {
                            mensagem = "Já existe um despacho de patente cadastrado com este código";

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(mensagem), false);
                            return;
                        }

                        servico.Inserir(despachoDePatentes);
                        mensagem = "Despacho de patente cadastrado com sucesso.";
                    }
                    else
                    {
                        servico.Modificar(despachoDePatentes);
                        mensagem = "Despacho de patente modificado com sucesso.";
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(mensagem), false);
                ExibaTelaInicial();

            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
            }
        }

        private IList<IDespachoDePatentes> verificaSeJaExisteDespachoCadastrado(string codigoDespachoDePatente, IServicoDeDespachoDePatentes servico)
        {
            var listaDeDespachos = servico.ObtenhaPorCodigoDoDespachoComoFiltro(codigoDespachoDePatente, int.MaxValue);

            return listaDeDespachos;
        }

        private void btnModificar_Click()
        {
            ExibaTelaModificar();
        }

        private void btnExclui_Click()
        {
            ExibaTelaExcluir();
        }

        private void btnNao_Click()
        {
            ExibaTelaInicial();
        }

        private void btnSim_Click()
        {
            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDePatentes>())
                    servico.Excluir(Convert.ToInt64(ViewState[ID_OBJETO]));
                
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(
                                                            "Despacho de patente excluído com sucesso."), false);
                ExibaTelaInicial();
            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
            }
        }

        private void MostreDespachoDePatentes(IDespachoDePatentes despachodepatentes)
        {
            ViewState[ID_OBJETO] = despachodepatentes.IdDespachoDePatente.Value.ToString();

            ctrlDespachoDePatentes.DespachoDePatentesSelecionada = despachodepatentes;
            ctrlDespachoDePatentes.CodigoDespachoDePatentes = despachodepatentes.Codigo;
            txtDescricao.Text = despachodepatentes.Descricao;
            txtProvidencia.Text = despachodepatentes.TipoProvidencia;
            txtPrazoProvidencia.Value = despachodepatentes.PrazoProvidencia;
            txtSituacao.Text = despachodepatentes.Situacao;
            txtTitulo.Text = despachodepatentes.Titulo;

            rblAgendaPagamento.SelectedValue = despachodepatentes.AgendarPagamento ? "1" : "0";
            rblDesativaProcesso.SelectedValue = despachodepatentes.DesativaProcesso ? "1" : "0";

            ExibaTelaConsultar();
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
                    btnExclui_Click();
                    break;
                case "btnSalvar":
                    btnSalva_Click();
                    break;
                case "btnCancelar":
                    btnCancela_Click();
                    break;
                case "btnSim":
                    btnSim_Click();
                    break;
                case "btnNao":
                    btnNao_Click();
                    break;
            }
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.010";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        private enum Estado : byte
        {
            Inicial = 1,
            Novo,
            Consulta,
            Modifica,
            Remove
        }
    }
}