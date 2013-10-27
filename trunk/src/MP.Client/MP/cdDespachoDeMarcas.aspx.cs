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
    public partial class cdDespachoDeMarcas : SuperPagina
    {
        private const string ID_OBJETO = "ID_OBJETO_CD_DESPACHODEMARCAS";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_DESPACHODEMARCAS";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlDespachoDeMarcas.DespachoDeMarcasFoiSelecionada += MostreDespachoDeMarcas;

            if (!IsPostBack)
            {
                this.ExibaTelaInicial();
            }
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

            Control controlePanel = this.PanelCdDespachoDeMarcas;

            UtilidadesWeb.LimparComponente(ref controlePanel);
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            pnlDespacho.Visible = true;
            ctrlDespachoDeMarcas.Inicializa();
            ctrlDespachoDeMarcas.EnableLoadOnDemand = true;
            ctrlDespachoDeMarcas.ShowDropDownOnTextboxClick = true;
            ctrlDespachoDeMarcas.AutoPostBack = true;


            ViewState[CHAVE_ESTADO] = Estado.Inicial;
            ViewState[ID_OBJETO] = null;

            CarregueComponentes();
        }

        private void ExibaTelaConsultar()
        {
            Control controle = PanelCdDespachoDeMarcas;

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;
            UtilidadesWeb.HabilitaComponentes(ref controle, false);
        }

        private void CarregueComponentes()
        {
            rblDesativaProcesso.Items.Clear();
            rblDesativaProcesso.Items.Add(new ListItem("Sim", "1"));
            rblDesativaProcesso.Items.Add(new ListItem("Não", "0"));
            rblDesativaProcesso.SelectedValue = "0";

            rblDesativaPesquisa.Items.Clear();
            rblDesativaPesquisa.Items.Add(new ListItem("Sim", "1"));
            rblDesativaPesquisa.Items.Add(new ListItem("Não", "0"));
            rblDesativaPesquisa.SelectedValue = "0";
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

            Control controlePanel = this.PanelCdDespachoDeMarcas;
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            UtilidadesWeb.LimparComponente(ref controlePanel);
            ViewState[CHAVE_ESTADO] = Estado.Novo;

            pnlDespacho.Visible = false;
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

            Control controlePanel = this.PanelCdDespachoDeMarcas;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ViewState[CHAVE_ESTADO] = Estado.Modifica;

            pnlDespacho.Visible = false;
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

            Control controlePanel = this.PanelCdDespachoDeMarcas;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

        }

        protected void btnCancela_Click()
        {
            ExibaTelaInicial();
        }

        private IDespachoDeMarcas MontaObjetoDespachoDeMarcas()
        {
            var despachoDeMarcas = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDeMarcas>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
            {
                despachoDeMarcas.IdDespacho = Convert.ToInt64(ViewState[ID_OBJETO]);
            }

            despachoDeMarcas.CodigoDespacho = txtCodigo.Text;
            despachoDeMarcas.DescricaoDespacho = txtDescricao.Text;
            despachoDeMarcas.PrazoParaProvidenciaEmDias = Convert.ToInt32(txtPrazoProvidencia.Value);
            despachoDeMarcas.Providencia = txtProvidencia.Text;
            despachoDeMarcas.SituacaoProcesso = txtSituacao.Text;
            despachoDeMarcas.DesativaProcesso = rblDesativaProcesso.SelectedValue != "0";
            despachoDeMarcas.DesativaPesquisaDeColidencia = rblDesativaPesquisa.SelectedValue != "0";

            return despachoDeMarcas;
        }

        private IList<string> VerifiqueCamposObrigatorios()
        {
            var inconsistencias = new List<string>();

            if (string.IsNullOrEmpty(txtCodigo.Text))
                inconsistencias.Add("O código do despacho deve ser informado.");

            if (string.IsNullOrEmpty(txtDescricao.Text))
                inconsistencias.Add("A descrição do despacho deve ser informada.");

            if (!txtPrazoProvidencia.Value.HasValue)
                inconsistencias.Add("O prazo para a providência deve ser informado.");

            return inconsistencias;
        }

        private void btnSalva_Click()
        {
            string mensagem = string.Empty;
            var inconsistencias = VerifiqueCamposObrigatorios();


            if (inconsistencias.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsistencias(inconsistencias), false);
                return;
            }


            var despachoDeMarcas = MontaObjetoDespachoDeMarcas();

            try
            {

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDeMarcas>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        IList<IDespachoDeMarcas> listaDeDespachosCadastrados = new List<IDespachoDeMarcas>();

                        listaDeDespachosCadastrados = verificaSeJaExisteDespachoCadastrado(despachoDeMarcas.CodigoDespacho, servico);

                        if (listaDeDespachosCadastrados.Count > 0)
                        {
                            mensagem = "Já existe um despacho de marcas cadastrado com este código";

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(mensagem), false);
                            return;
                        }

                        servico.Inserir(despachoDeMarcas);
                        mensagem = "Despacho de marcas cadastrado com sucesso.";
                    }
                    else
                    {
                        servico.Modificar(despachoDeMarcas);
                        mensagem = "Despacho de marcas modificado com sucesso.";
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

        private IList<IDespachoDeMarcas> verificaSeJaExisteDespachoCadastrado(string codigoDespacho, IServicoDeDespachoDeMarcas servico)
        {
            var listaDeDespachos = servico.ObtenhaPorCodigoDoDespachoComoFiltro(codigoDespacho, int.MaxValue);

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
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDeMarcas>())
                {
                    servico.Excluir(Convert.ToInt64(ViewState[ID_OBJETO]));
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(
                                                            "Despacho de marcas excluído com sucesso."), false);
                ExibaTelaInicial();
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

        private void MostreDespachoDeMarcas(IDespachoDeMarcas despachoDeMarcas)
        {
            ViewState[ID_OBJETO] = despachoDeMarcas.IdDespacho.Value.ToString();

            txtCodigo.Text = despachoDeMarcas.CodigoDespacho;
            txtDescricao.Text = despachoDeMarcas.DescricaoDespacho;
            txtPrazoProvidencia.Text = despachoDeMarcas.PrazoParaProvidenciaEmDias.ToString();
            txtProvidencia.Text = despachoDeMarcas.Providencia;
            txtSituacao.Text = despachoDeMarcas.SituacaoProcesso;
            rblDesativaPesquisa.SelectedValue = despachoDeMarcas.DesativaPesquisaDeColidencia ? "1": "0";
            rblDesativaProcesso.SelectedValue = despachoDeMarcas.DesativaProcesso ? "1" : "0";
            ExibaTelaConsultar();
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.004";
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