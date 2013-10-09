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
    public partial class cdTipoDeProcedimentoInterno : SuperPagina
    {
        private const string ID_OBJETO = "ID_OBJETO_CD_PROCEDIMENTOINTERNO";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_PROCEDIMENTOINTERNO";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlTipoProcedimentoInterno.ProcedimentosInternosFoiSelecionado += MostreProcedimentoInterno;

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

            Control controlePanel = this.PanelCdProcedimentosInternos;

            UtilidadesWeb.LimparComponente(ref controlePanel);
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            PanelCdProcedimentosInternos.Visible = false;
            ctrlTipoProcedimentoInterno.Visible = true;

            ctrlTipoProcedimentoInterno.Inicializa();
            ctrlTipoProcedimentoInterno.EnableLoadOnDemand = true;
            ctrlTipoProcedimentoInterno.ShowDropDownOnTextboxClick = true;
            ctrlTipoProcedimentoInterno.AutoPostBack = true;
            ctrlTipoProcedimentoInterno.EhObrigatorio = false;

            ViewState[CHAVE_ESTADO] = Estado.Inicial;
            ViewState[ID_OBJETO] = null;
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

            Control controlePanel = this.PanelCdProcedimentosInternos;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ViewState[CHAVE_ESTADO] = Estado.Novo;

            PanelCdProcedimentosInternos.Visible = true;
            ctrlTipoProcedimentoInterno.Visible = false;

            ctrlTipoProcedimentoInterno.Inicializa();
            ctrlTipoProcedimentoInterno.EnableLoadOnDemand = false;
            ctrlTipoProcedimentoInterno.ShowDropDownOnTextboxClick = false;
            ctrlTipoProcedimentoInterno.AutoPostBack = false;
            ctrlTipoProcedimentoInterno.EhObrigatorio = true;
            ctrlTipoProcedimentoInterno.TextoItemVazio = string.Empty;
        }

        private void ExibaTelaModificar()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            Control controlePanel = this.PanelCdProcedimentosInternos;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ViewState[CHAVE_ESTADO] = Estado.Modifica;

            PanelCdProcedimentosInternos.Visible = true;
            ctrlTipoProcedimentoInterno.Visible = false;

            ctrlTipoProcedimentoInterno.EnableLoadOnDemand = false;
            ctrlTipoProcedimentoInterno.ShowDropDownOnTextboxClick = false;
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

            Control controlePanel = this.PanelCdProcedimentosInternos;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            PanelCdProcedimentosInternos.Visible = false;
        }

        protected void btnCancela_Click()
        {
            ExibaTelaInicial();
        }

        private ITipoDeProcedimentoInterno MontaObjetoProcedimentosInternos()
        {
            var procedimentosInternos = FabricaGenerica.GetInstancia().CrieObjeto<ITipoDeProcedimentoInterno>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
            {
                procedimentosInternos.Id = Convert.ToInt64(ViewState[ID_OBJETO]);
            }

            procedimentosInternos.Descricao = this.txtDescricaoTipo.Text;

            return procedimentosInternos;
        }

        private void btnSalva_Click()
        {
            string mensagem;
            var procedimentoInterno = MontaObjetoProcedimentosInternos();

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTipoDeProcedimentoInterno>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        servico.Inserir(procedimentoInterno);
                        mensagem = "Tipo de procedimento interno cadastrado com sucesso.";
                    }
                    else
                    {
                        servico.Modificar(procedimentoInterno);
                        mensagem = "Tipo de procedimento interno modificado com sucesso.";
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
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTipoDeProcedimentoInterno>())
                {
                    servico.Excluir(Convert.ToInt64(ViewState[ID_OBJETO]));
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(
                                                            "Tipo de procedimento interno excluído com sucesso."), false);
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

        private void MostreProcedimentoInterno(ITipoDeProcedimentoInterno procedimentoInterno)
        {
            ViewState[ID_OBJETO] = procedimentoInterno.Id.Value.ToString();

            ctrlTipoProcedimentoInterno.DescricaoTipo = procedimentoInterno.Descricao;

            this.txtDescricaoTipo.Text = procedimentoInterno.Descricao;

            ExibaTelaModificar();
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.003";
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