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
    public partial class cdCadastroDeProcuradores : SuperPagina
    {
        private const string ID_OBJETO = "ID_OBJETO_CD_PROCURADOR";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_PROCURADOR";
        private const string MSG_CADASTRO_SUCESSO = "Procurador cadastrado com sucesso.";
        private const string MSG_MODIFICADO_SUCESSO = "Procurador modificado com sucesso.";
        private const string MSG_EXCLUIDO_SUCESSO = "Procurador excluído com sucesso.";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlProcuradores.ProcuradorFoiSelecionado += MostreProcurador;

            if (!IsPostBack)
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

            Control controlePanel = PanelDadosDoProcurador;

            UtilidadesWeb.LimparComponente(ref controlePanel);
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            PanelDadosDoProcurador.Visible = false;
            ctrlProcuradores.Visible = true;
            ctrlPessoa.Visible = false;

            ctrlProcuradores.Inicializa();
            ctrlProcuradores.EnableLoadOnDemand = true;
            ctrlProcuradores.ShowDropDownOnTextboxClick = true;
            ctrlProcuradores.AutoPostBack = true;
            ctrlProcuradores.EhObrigatorio = false;

            ViewState[CHAVE_ESTADO] = Estado.Inicial;
            ViewState[ID_OBJETO] = null;
        }

        private void MostreProcurador(IProcurador procurador)
        {
            ViewState[ID_OBJETO] = procurador.Pessoa.ID;

            ctrlProcuradores.Nome = procurador.Pessoa.Nome;
            ctrlProcuradores.IdProcurador = procurador.Pessoa.ID;

            txtMatriculaAPI.Text = procurador.MatriculaAPI;
            txtSiglaOrgao.Text = procurador.SiglaOrgaoProfissional;
            txtNumeroRegistro.Text = procurador.NumeroRegistroProfissional;
            txtDataRegistro.Text = procurador.DataRegistroProfissional.ToString();
            txtContato.Text = procurador.ObservacaoContato;

            ExibaTelaModificar();
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

            Control controlePanel = this.PanelDadosDoProcurador;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ViewState[CHAVE_ESTADO] = Estado.Novo;

            PanelDadosDoProcurador.Visible = true;
            ctrlProcuradores.Visible = false;
            ctrlPessoa.Visible = true; 

            ctrlProcuradores.Inicializa();
            ctrlProcuradores.EnableLoadOnDemand = false;
            ctrlProcuradores.ShowDropDownOnTextboxClick = false;
            ctrlProcuradores.AutoPostBack = false;
            ctrlProcuradores.EhObrigatorio = true;
            ctrlProcuradores.TextoItemVazio = string.Empty;
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

            Control controlePanel = this.PanelDadosDoProcurador;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ViewState[CHAVE_ESTADO] = Estado.Modifica;

            PanelDadosDoProcurador.Visible = true;
            ctrlProcuradores.Visible = false;
            ctrlPessoa.Visible = true;

            ctrlProcuradores.EnableLoadOnDemand = false;
            ctrlProcuradores.ShowDropDownOnTextboxClick = false;
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

            Control controlePanel = PanelDadosDoProcurador;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            PanelDadosDoProcurador.Visible = false;
        }

        protected void btnCancela_Click()
        {
            ExibaTelaInicial();
        }

        private IProcurador MontaObjetoProcurador()
        {
            var procurador = FabricaGenerica.GetInstancia().CrieObjeto<IProcurador>();
            DateTime? dataRegistro;

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
            {
                procurador.Pessoa.ID = Convert.ToInt64(ViewState[ID_OBJETO]);
            }

            procurador.MatriculaAPI = txtMatriculaAPI.Text;
            procurador.SiglaOrgaoProfissional = txtSiglaOrgao.Text;
            procurador.NumeroRegistroProfissional = txtNumeroRegistro.Text;
            procurador.DataRegistroProfissional = txtDataRegistro.SelectedDate;
            procurador.ObservacaoContato = txtContato.Text;

            return procurador;
        }

        private void btnSalva_Click()
        {
            string mensagem;
            var procurador = MontaObjetoProcurador();

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcurador>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        servico.Inserir(procurador);
                        mensagem = MSG_CADASTRO_SUCESSO;
                    }
                    else
                    {
                        servico.Atualizar(procurador);
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
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcurador>())
                    servico.Remover(Convert.ToInt64(ViewState[ID_OBJETO]));

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
            return "FUN.MP.005";
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