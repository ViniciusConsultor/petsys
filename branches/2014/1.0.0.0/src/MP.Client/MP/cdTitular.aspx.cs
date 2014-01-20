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
    public partial class cdTitular : SuperPagina
    {
        private string CHAVE_ESTADO_CD_TITULAR = "CHAVE_ESTADO_CD_TITULAR";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlPessoa1.PessoaFoiSelecionada += ObtenhaTitular;

            if (!IsPostBack)
                ExibaTelaInicial();
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }


        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.014";
        }

        private void ExibaTelaInicial()
        {
            Control controle = pnlDadosDoTitular;

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;
            UtilidadesWeb.LimparComponente(ref controle);
            UtilidadesWeb.HabilitaComponentes(ref controle, false);
            ctrlPessoa1.Inicializa();
            ctrlPessoa1.BotaoDetalharEhVisivel = false;
            ctrlPessoa1.BotaoNovoEhVisivel = true;
            ViewState[CHAVE_ESTADO_CD_TITULAR] = Estado.Inicial;
        }

        protected void btnNovo_Click()
        {
            ExibaTelaNovo();
        }

        private void ExibaTelaNovo()
        {
            Control controle = pnlDadosDoTitular;

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;
            ViewState[CHAVE_ESTADO_CD_TITULAR] = Estado.Novo;
            UtilidadesWeb.HabilitaComponentes(ref controle, true);
            txtDataDoCadastro.SelectedDate = DateTime.Now;
            txtDataDoCadastro.Enabled = false;
        }

        private void ExibaTelaModificar()
        {
            Control controle = pnlDadosDoTitular;

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;
            ViewState[CHAVE_ESTADO_CD_TITULAR] = Estado.Modifica;
            UtilidadesWeb.HabilitaComponentes(ref controle, true);
            txtDataDoCadastro.Enabled = false;
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
            ViewState[CHAVE_ESTADO_CD_TITULAR] = Estado.Remove;
        }

        private void ExibaTelaConsultar()
        {
            Control controle = pnlDadosDoTitular;

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;
            UtilidadesWeb.HabilitaComponentes(ref controle, false);
        }

        protected void btnCancela_Click()
        {
            ExibaTelaInicial();
        }

        private ITitular MontaObjeto()
        {
            var titular = FabricaGenerica.GetInstancia().CrieObjeto<ITitular>(new object[] { ctrlPessoa1.PessoaSelecionada });

            titular.DataDoCadastro = txtDataDoCadastro.SelectedDate;
            titular.InformacoesAdicionais = txtInformacoesAdicionais.Text;

            return titular;
        }

        private void btnSalva_Click()
        {
            var titular = MontaObjeto();
            string mensagem = null;

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTitular>())
                {
                    if (ViewState[CHAVE_ESTADO_CD_TITULAR].Equals(Estado.Novo))
                    {
                        servico.Inserir(titular);
                        mensagem = "Titular inserido com sucesso.";
                    }
                    else
                    {
                        servico.Atualizar(titular);
                        mensagem = "Titular atualizado com sucesso.";
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),UtilidadesWeb.MostraMensagemDeInformacao(mensagem), false);
                ExibaTelaInicial();
            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
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
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTitular>())
                {
                    servico.Remover(ctrlPessoa1.PessoaSelecionada.ID.Value);
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                    UtilidadesWeb.MostraMensagemDeInformacao("Titular excluído com sucesso."), false);
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

        private void ObtenhaTitular(IPessoa pessoa)
        {
            ITitular titular;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTitular>())
            {
                titular = servico.Obtenha(pessoa);
            }

            if (titular == null)
            {
                ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = true;
                return;

            }
            MostreTitular(titular);
            ExibaTelaConsultar();
        }

        private void MostreTitular(ITitular titular)
        {
            ctrlPessoa1.PessoaSelecionada = titular.Pessoa;
            txtDataDoCadastro.SelectedDate = titular.DataDoCadastro;
            txtInformacoesAdicionais.Text = titular.InformacoesAdicionais;
            ctrlPessoa1.BotaoDetalharEhVisivel = true;
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