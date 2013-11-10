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
    public partial class cdPastas : SuperPagina
    {
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_PASTA";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlPasta.PastaFoiSelecionada += Mostre;
            
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

            Control controlePanel = this.pnlDadosDaPasta;

            UtilidadesWeb.LimparComponente(ref controlePanel);
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ctrlPasta.Inicializa();
            ctrlPasta.BotaoNovoEhVisivel = false;
            ctrlPasta.EnableLoadOnDemand = true;
            ctrlPasta.ShowDropDownOnTextboxClick = true;
            ctrlPasta.AutoPostBack = true;
            ViewState[CHAVE_ESTADO] = Estado.Inicial;
            ctrlPasta.TextoItemVazio = "Selecione uma pasta";
            
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

            ViewState[CHAVE_ESTADO] = Estado.Novo;

            Control controlePanel = pnlDadosDaPasta;
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);

            ctrlPasta.EnableLoadOnDemand = false;
            ctrlPasta.ShowDropDownOnTextboxClick = false;
            ctrlPasta.AutoPostBack = false;
            ctrlPasta.TextoItemVazio = "";
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

            ViewState[CHAVE_ESTADO] = Estado.Modifica;

            Control controlePanel = pnlDadosDaPasta;
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);

            ctrlPasta.EnableLoadOnDemand = false;
            ctrlPasta.ShowDropDownOnTextboxClick = false;
            ctrlPasta.AutoPostBack = false;
            ctrlPasta.TextoItemVazio = "";
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

            Control controlePanel = pnlDadosDaPasta;
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);
            ctrlPasta.TextoItemVazio = "";
        }

        protected void btnCancela_Click()
        {
            ExibaTelaInicial();
        }

        private IPasta MontaObjeto()
        {
            IPasta pasta;

            if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                pasta = FabricaGenerica.GetInstancia().CrieObjeto<IPasta>();
            else
                pasta = ctrlPasta.PastaSelecionada;

            pasta.Nome = ctrlPasta.Nome;
            return pasta;
        }

        private void btnSalva_Click()
        {
            string mensagem;
            var pasta = MontaObjeto();

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePasta>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        servico.Inserir(pasta);
                        mensagem = "Pasta cadastrada com sucesso.";
                    }
                    else
                    {
                        servico.Modificar(pasta);
                        mensagem = "Pasta modificada com sucesso.";
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
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePasta>())
                {
                    servico.Excluir(ctrlPasta.PastaSelecionada.ID.Value);
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(
                                                            "Pasta excluída com sucesso."), false);
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

        private void Mostre(IPasta pasta)
        {
           ExibaTelaConsultar();
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.011";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        private enum Estado : byte
        {
            Inicial = 1,
            Novo,
            Modifica,
            Remove
        }

       

        private void ExibaTelaConsultar()
        {
            Control controlePanel = pnlDadosDaPasta;
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;
        }
    }
}