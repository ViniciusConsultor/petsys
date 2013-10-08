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
    public partial class cdLayoutRevistaPatente : SuperPagina
    {
        private const string ID_OBJETO = "ID_OBJETO_CD_PROCURADOR";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_PROCURADOR";
        private const string MSG_CADASTRO_SUCESSO = "Layout cadastrado com sucesso.";
        private const string MSG_MODIFICADO_SUCESSO = "Layout modificado com sucesso.";
        private const string MSG_EXCLUIDO_SUCESSO = "Layout excluído com sucesso.";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlLayoutRevistaPatente.LayoutPatenteFoiSelecionado += MostreLayoutRevistaPatente;

            if(!IsPostBack)
                ExibaTelaInicial();
        }

        private void MostreLayoutRevistaPatente(ILayoutRevistaPatente layoutRevistaPatente)
        {
            
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

            Control controlePanel = PanelDadosDoLayout;

            UtilidadesWeb.LimparComponente(ref controlePanel);
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);
            ctrlLayoutRevistaPatente.Inicializa();
            ctrlLayoutRevistaPatente.Visible = true;
            PanelDadosDoLayout.Visible = false;

            ViewState[CHAVE_ESTADO] = Estado.Inicial;
            ViewState[ID_OBJETO] = null;
            CarregueCombosLayout();
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

            Control controlePanel = PanelDadosDoLayout;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ViewState[CHAVE_ESTADO] = Estado.Novo;
            ctrlLayoutRevistaPatente.Visible = false;

            PanelDadosDoLayout.Visible = true;
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

            Control controlePanel = this.PanelDadosDoLayout;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ViewState[CHAVE_ESTADO] = Estado.Modifica;

            PanelDadosDoLayout.Visible = true;
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

            Control controlePanel = PanelDadosDoLayout;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            PanelDadosDoLayout.Visible = false;
        }

        protected void btnCancela_Click()
        {
            ExibaTelaInicial();
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.006";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        private void btnSalvar_Click()
        {
            //if (!PodeSalvarOuModificar())
            //    return;

            string mensagem;
            var layoutPatente = MonteObjetoLayoutPatente();

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeLayoutRevistaPatente>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        servico.Inserir(layoutPatente);
                        mensagem = MSG_CADASTRO_SUCESSO;
                    }
                    else
                    {
                        servico.Modificar(layoutPatente);
                        mensagem = MSG_MODIFICADO_SUCESSO;
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
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeLayoutRevistaPatente>())
                    servico.Excluir(Convert.ToInt64(ViewState[ID_OBJETO]));

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraMensagemDeInformacao(MSG_EXCLUIDO_SUCESSO), false);
                ExibaTelaInicial();
            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
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
                    btnSalvar_Click();
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

        private ILayoutRevistaPatente MonteObjetoLayoutPatente()
        {
            var layoutPatente = FabricaGenerica.GetInstancia().CrieObjeto<ILayoutRevistaPatente>();

            layoutPatente.NomeDoCampo = txtNomeDoCampo.Text;
            layoutPatente.DescricaoResumida = txtDescricaoResumida.Text;
            layoutPatente.DescricaoDoCampo = txtDescricaoDoCampo.Text;
            layoutPatente.TamanhoDoCampo = int.Parse(txtTamanhoDoCampo.Text);
            layoutPatente.CampoDelimitadorDoRegistro = cboCampoDelimitadorDoRegistro.SelectedValue.Equals("Sim");
            layoutPatente.CampoIdentificadorDeColidencia = cboCampoIdentificadorDeColidencia.SelectedValue.Equals("Sim");
            layoutPatente.CampoIdentificadorDoProcesso = cboCampoIdentificadorDoProcesso.SelectedValue.Equals("Sim");

            return layoutPatente;
        }

        private enum Estado : byte
        {
            Inicial = 1,
            Novo,
            Modifica,
            Remove
        }

        private void CarregueCombosLayout()
        {
            IList<string> valoresDaCombo = new List<string>();

            valoresDaCombo.Add("Sim"); 
            valoresDaCombo.Add("Não");

            cboCampoDelimitadorDoRegistro.DataSource = valoresDaCombo;
            cboCampoDelimitadorDoRegistro.DataBind();

            cboCampoIdentificadorDoProcesso.DataSource = valoresDaCombo;
            cboCampoIdentificadorDoProcesso.DataBind();
            
            cboCampoIdentificadorDeColidencia.DataSource = valoresDaCombo;
            cboCampoIdentificadorDeColidencia.DataBind();
        }
    }
}