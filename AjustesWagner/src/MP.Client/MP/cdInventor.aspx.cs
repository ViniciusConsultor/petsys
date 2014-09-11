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
using Compartilhados.Interfaces.Core.Servicos;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class cdInventor : SuperPagina
    {
        private string CHAVE_ESTADO_CD_INVENTOR = "CHAVE_ESTADO_CD_INVENTOR";

        private enum Estado : byte
        {
            Inicial = 1,
            Novo,
            Consulta,
            Modifica,
            Remove
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlPessoa1.PessoaFoiSelecionada += ObtenhaInventor;

            if (!IsPostBack)
            {
                ExibaTelaInicial();
            }
        }

        protected override Telerik.Web.UI.RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }


        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.002";
        }

        private void ExibaTelaInicial()
        {
            Control controle = pnlDadosDoInventor;

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
            ViewState[CHAVE_ESTADO_CD_INVENTOR] = Estado.Inicial;
        }

        protected void btnNovo_Click()
        {
            ExibaTelaNovo();
        }

        private void ExibaTelaNovo()
        {
            Control controle = pnlDadosDoInventor;

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;
            ViewState[CHAVE_ESTADO_CD_INVENTOR] = Estado.Novo;
            UtilidadesWeb.HabilitaComponentes(ref controle, true);
            txtDataDoCadastro.SelectedDate = DateTime.Now;
            txtDataDoCadastro.Enabled = false;
        }

        private void ExibaTelaModificar()
        {
            Control controle = pnlDadosDoInventor;

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;
            ViewState[CHAVE_ESTADO_CD_INVENTOR] = Estado.Modifica;
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
            ViewState[CHAVE_ESTADO_CD_INVENTOR] = Estado.Remove;
        }

        private void ExibaTelaConsultar()
        {
            Control controle = pnlDadosDoInventor;

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

        private IInventor MontaObjeto()
        {
            var inventor = FabricaGenerica.GetInstancia().CrieObjeto<IInventor>(new object[] {ctrlPessoa1.PessoaSelecionada});

            inventor.DataDoCadastro = txtDataDoCadastro.SelectedDate;
            inventor.InformacoesAdicionais = txtInformacoesAdicionais.Text;

            return inventor;
        }

        private void btnSalva_Click()
        {
            var inventor = MontaObjeto();
            string mensagem =  null;

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeInventor>())
                {
                    if (ViewState[CHAVE_ESTADO_CD_INVENTOR].Equals(Estado.Novo))
                    {
                        servico.Inserir(inventor);
                        mensagem = "Inventor inserido com sucesso.";
                    }
                    else
                    {
                        servico.Atualizar(inventor);
                        mensagem = "Inventor atualizado com sucesso.";
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(
                                                            mensagem), false);
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
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeInventor>())
                {
                    servico.Remover(ctrlPessoa1.PessoaSelecionada.ID.Value);
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(
                                                            "Inventor excluído com sucesso."), false);
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

        private void ObtenhaInventor(IPessoa pessoa)
        {
            IInventor inventor;

            txtInformacoesAdicionais.Text = string.Empty;
            txtDataDoCadastro.SelectedDate = DateTime.Now;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeInventor>())
            {
                inventor = servico.Obtenha(pessoa);
            }

            if (inventor == null)
            {
                ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = true;
                return;

            }
            MostreInventor(inventor);
            ExibaTelaConsultar();
        }

        private void MostreInventor(IInventor inventor)
        {
            ctrlPessoa1.PessoaSelecionada = inventor.Pessoa;
            txtDataDoCadastro.SelectedDate = inventor.DataDoCadastro;
            txtInformacoesAdicionais.Text = inventor.InformacoesAdicionais;
            ctrlPessoa1.BotaoDetalharEhVisivel = true;
        }
    }
}