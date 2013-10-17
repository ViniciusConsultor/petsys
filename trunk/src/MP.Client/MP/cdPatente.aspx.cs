using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using MP.Interfaces.Negocio;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class cdPatente : System.Web.UI.Page
    {
        private const string ID_OBJETO = "ID_OBJETO_CD_PATENTES";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_PATENTES";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlPatente.PatenteFoiSelecionada += ExibaPatenteSelecionada;

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

            var controleGridClientes = grdClientes as Control;
            UtilidadesWeb.LimparComponente(ref controleGridClientes);

            var controleGridInventores = grdInventores as Control;
            UtilidadesWeb.LimparComponente(ref controleGridInventores);

            var controleGridPrioridadeUnionista = grdPrioridadeUnionista as Control;
            UtilidadesWeb.LimparComponente(ref controleGridPrioridadeUnionista);

            var controleGridAnuidades = grdAnuidades as Control;
            UtilidadesWeb.LimparComponente(ref controleGridAnuidades);

            var controleGridClassificacaoPatente = grdClassificacaoPatente as Control;
            UtilidadesWeb.LimparComponente(ref controleGridClassificacaoPatente);

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