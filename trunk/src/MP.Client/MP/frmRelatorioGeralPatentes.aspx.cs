using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Client.Relatorios.Patentes;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Patentes;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmRelatorioGeralPatentes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlCliente1.BotaoNovoEhVisivel = false;
            ctrlTitular1.BotaoNovoEhVisivel = false;
            ctrlInventor1.BotaoNovoEhVisivel = false;
        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {
                case "btnImprimir":
                    CarregueDadosEGereRelatorio();
                    break;
            }
        }

        private void CarregueDadosEGereRelatorio()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
            {
                var processosDePatentes = servico.ObtenhaProcessosDePatentes(ObtenhaFiltroRelatorio(), int.MaxValue, 0, true);

                if(processosDePatentes == null || processosDePatentes.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                         UtilidadesWeb.MostraMensagemDeInformacao("Não foi encontrado nehuma dados com os parâmetros informados."),
                                                         false);
                    return;
                }

                var geradorDeRelatorioGeral = new GeradorDeRelatorioGeralDePatentes(processosDePatentes);
                var nomeDoArquivo = geradorDeRelatorioGeral.GereRelatorio(ObtenhaOrdenacao());
                var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" + nomeDoArquivo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);
            }
        }

        private IFiltroRelatorioGeralPatente ObtenhaFiltroRelatorio()
        {
            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroRelatorioGeralPatente>();

            filtro.TipoNatureza = rblTiposDePatente.SelectedIndex == 0 ? "PATENTE" : "DESENHO";
            filtro.ClassificacaoPatente = ObtenhaClassificacao();
            filtro.TipoDeOrigem = ObtenhaTipoDeOrigem();
            filtro.StatusDoProcesso = ObtenhaStatusDoProcesos();
            filtro.Cliente = ctrlCliente1.ClienteSelecionado;
            filtro.Titular = ctrlTitular1.TitularSelecionado;
            filtro.Inventor = ctrlInventor1.InventorSelecionado;

            return filtro;
        }

        private TipoClassificacaoPatente ObtenhaClassificacao()
        {
            if (rblOrigemDeProcessos.SelectedIndex == 0)
                return null;

            if (rblOrigemDeProcessos.SelectedIndex == 1)
                return TipoClassificacaoPatente.Nacional;

            return TipoClassificacaoPatente.Internacional;
        }

        private string ObtenhaTipoDeOrigem()
        {
            if (rdlTipoDeOrigem.SelectedIndex == 0)
                return null;

            if (rdlTipoDeOrigem.SelectedIndex == 1)
                return "PROPRIO";

            return "TERCEIROS";
        }

        private string ObtenhaStatusDoProcesos()
        {
            if (rdlStatusDoProcesso.SelectedIndex == 0)
                return null;

            if (rdlStatusDoProcesso.SelectedIndex == 1)
                return "ATIVOS";

            return "INATIVOS";
        }

        private OrdenacaoRelatorioGeralPatente ObtenhaOrdenacao()
        {
            return rdlOrdenacao.SelectedValue.ToUpper().Equals("CLIENTE") ? OrdenacaoRelatorioGeralPatente.Cliente :
            OrdenacaoRelatorioGeralPatente.Patente;
        }
    }
}