using System;
using System.Collections.Generic;
using System.Web.UI;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Client.Relatorios;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;
using MP.Interfaces.Negocio.Filtros.Patentes;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmRelatorioDeManutencoes : Page
    {
        private const string CHAVE_CLIENTES = "CHAVE_CLIENTES";

        private IList<ICliente> ListaDeClientes
        {
            get
            {
                if (ViewState[CHAVE_CLIENTES] == null)
                    ViewState[CHAVE_CLIENTES] = new List<ICliente>();

                return (IList<ICliente>)ViewState[CHAVE_CLIENTES];
            }
            set { ViewState[CHAVE_CLIENTES] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Limpar();
        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {
                case "btnRelatorio":
                    GereRelatorioDeManutencoes();
                    break;

                case "btnLimpar":
                    Limpar();
                    break;
            }
        }

        private void GereRelatorioDeManutencoes()
        {
            if(!PodeGerarRelatorio())
                return;
            
            IList<IProcessoDePatente> processoDePatentes = null;
            IList<IProcessoDeMarca> processoDeMarcas = null;

            if (chkPatentes.Checked)
                using (var servicoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
                    processoDePatentes = servicoDePatente.ObtenhaProcessosDePatentes(ObtenhaFiltroDePatentes(), int.MaxValue, 0);

            if(chkMarcas.Checked)
                using (var servicoDeMarca = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                    processoDeMarcas = servicoDeMarca.ObtenhaProcessosDeMarcas(ObtenhaFiltroDeMarcas(), int.MaxValue, 0, chkConsiderarNaoAtivas.Checked);

            if(processoDePatentes == null && processoDeMarcas == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                    UtilidadesWeb.MostraMensagemDeInformacao("Nenhuma manutenção foi encontrada."), false);
                return;
            }

            var geradorDeRelatorioDeManutencoes = new GeradorDeRelatorioDeManutencoes(processoDePatentes, processoDeMarcas);
            var nomeDoArquivo = geradorDeRelatorioDeManutencoes.GereRelatorio();
            var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" + nomeDoArquivo;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);

        }

        private IFiltroRelatorioDeManutencoesDaMarca ObtenhaFiltroDeMarcas()
        {
            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroRelatorioDeManutencoesDaMarca>();

            if (ViewState[CHAVE_CLIENTES] != null)
                filtro.ClientesSelecionados = (IList<ICliente>) ViewState[CHAVE_CLIENTES];

            filtro.DataDeInicio = rdpPeriodoInicio.SelectedDate;
            filtro.DataTermino = rdpPeriodoTermino.SelectedDate;

            return filtro;
        }

        private IFiltroRelatorioDeManutencoesDaPatente ObtenhaFiltroDePatentes()
        {
            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroRelatorioDeManutencoesDaPatente>();

            if (ViewState[CHAVE_CLIENTES] != null)
                filtro.ClientesSelecionados = (IList<ICliente>)ViewState[CHAVE_CLIENTES];

            filtro.DataDeInicio = rdpPeriodoInicio.SelectedDate;
            filtro.DataTermino = rdpPeriodoTermino.SelectedDate;

            return filtro;
        }

        private bool PodeGerarRelatorio()
        {
            if (!chkMarcas.Checked && !chkPatentes.Checked)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                    UtilidadesWeb.MostraMensagemDeInformacao("É necessário selecionar uma opção (Marcas/Patentes)."), false);
                return false;
            }

            if (!rdpPeriodoInicio.SelectedDate.HasValue && rdpPeriodoTermino.SelectedDate.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                    UtilidadesWeb.MostraMensagemDeInformacao("Informe a data de início."), false);
                return false;
            }

            if (rdpPeriodoInicio.SelectedDate.HasValue && !rdpPeriodoTermino.SelectedDate.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                    UtilidadesWeb.MostraMensagemDeInformacao("Informe a data de término."), false);
                return false;
            }

            if (rdpPeriodoInicio.SelectedDate.HasValue && rdpPeriodoTermino.SelectedDate.HasValue &&
                rdpPeriodoInicio.SelectedDate.Value > rdpPeriodoTermino.SelectedDate.Value)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                    UtilidadesWeb.MostraMensagemDeInformacao("A data de início deve ser maior do que a data de término."), false);
                return false;
            }

            return true;
        }

        protected void btnAdicionarCliente_ButtonClick(object sender, EventArgs e)
        {
            if (!PodeAdicionarCliente())
                return;

            ListaDeClientes.Add(ctrlCliente.ClienteSelecionado);
            CarregueGridDeClientes();
        }

        private bool PodeAdicionarCliente()
        {
            if (ctrlCliente.ClienteSelecionado == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                    UtilidadesWeb.MostraMensagemDeInformacao("Selecione um cliente."), false);

                return false;
            }

            if (ListaDeClientes.Contains(ctrlCliente.ClienteSelecionado))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                    UtilidadesWeb.MostraMensagemDeInformacao("O cliente selecionado já foi adicionado."), false);

                return false;
            }

            return true;
        }

        protected void grdClientes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                IndiceSelecionado = e.Item.ItemIndex;

            if (e.CommandName == "Excluir")
            {
                ListaDeClientes.RemoveAt(IndiceSelecionado);
                CarregueGridDeClientes();
            }
        }

        private void CarregueGridDeClientes()
        {
            grdClientes.DataSource = ListaDeClientes;
            grdClientes.DataBind();
        }

        protected void grdClientes_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdClientes.MasterTableView.RenderColumns)
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void grdClientes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdClientes, ViewState[CHAVE_CLIENTES], e);
        }

        private void Limpar()
        {
            ctrlCliente.Inicializa();
            ctrlCliente.BotaoNovoEhVisivel = true;
            ListaDeClientes = new List<ICliente>();
            CarregueGridDeClientes();
            chkMarcas.Checked = true;
            chkPatentes.Checked = true;
            rdpPeriodoInicio.DbSelectedDate = null;
            rdpPeriodoTermino.DbSelectedDate = null;
            chkConsiderarNaoAtivas.Checked = false;

        }
    }
}