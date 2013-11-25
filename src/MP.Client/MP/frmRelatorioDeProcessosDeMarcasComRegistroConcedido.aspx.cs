using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmRelatorioDeProcessosDeMarcasComRegistroConcedido : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                AjusteControles();
        }

        private void AjusteControles()
        {
            var controle = pnlRegistroConcedido as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            ctrlDespacho.Inicializa();
            ctrlDespacho.BotaoNovoEhVisivel = false;
            DespachosSelecionados = new List<IDespachoDeMarcas>();
            MostraDespachosSelecionados();
        }

        private void MostraDespachosSelecionados()
        {
            grdDespachos.DataSource = DespachosSelecionados;
            grdDespachos.DataBind();
        }

        private IList<IDespachoDeMarcas> DespachosSelecionados
        {
            get { return (IList<IDespachoDeMarcas>)ViewState["DESPACHOS_SELECIONADOS"]; }
            set { ViewState["DESPACHOS_SELECIONADOS"] = value; }
        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {
                case "btnImprimir":
                    PesquiseEGereRelatorio();
                    break;
            }
        }

        protected void btnAdicionarDespacho_OnClick(object sender, ImageClickEventArgs e)
        {
            var despachoSelecionado = ctrlDespacho.DespachoDeMarcasSelecionada;

            if (!DespachosSelecionados.Contains(despachoSelecionado))
            {
                DespachosSelecionados.Add(despachoSelecionado);
                MostraDespachosSelecionados();
                ctrlDespacho.Inicializa();
                return;
            }

            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("O despacho já foi adicionado na lista"),
                                                       false);
        }

        protected void grdDespachos_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdDespachos, DespachosSelecionados, e);
        }

        protected void grdDespachos_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int indiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
            {
                indiceSelecionado = e.Item.ItemIndex;
            }

            switch (e.CommandName)
            {
                case "Excluir":
                    DespachosSelecionados.RemoveAt(indiceSelecionado);
                    MostraDespachosSelecionados();
                    break;
            }
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.013";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        private void PesquiseEGereRelatorio()
        {
            if(txtDataInicialDeConcessao.SelectedDate.HasValue && txtDataFimConcessao.SelectedDate.HasValue)
            {
                IList<string> IDsDosDespachos = null;

                var dataInicial = this.txtDataInicialDeConcessao.SelectedDate;
                var dataFinal = this.txtDataFimConcessao.SelectedDate;

                if (DespachosSelecionados.Count > 0)
                    IDsDosDespachos = DespachosSelecionados.Select(despacho => despacho.IdDespacho.Value.ToString()).ToList();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                {
                    // obtenha processos de marcas de acordo com o período da data de concessão e despachos.
                    var processos = servico.ObtenhaProcessosDeMarcasComRegistroConcedido(dataInicial, dataFinal, IDsDosDespachos);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione as datas para geração do relatório."),
                                                       false);
            }
        }
    }
}