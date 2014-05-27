using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Interfaces.Core.Negocio;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class frmRelatorioDeManutencoes : System.Web.UI.Page
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
            if(ctrlCliente.ClienteSelecionado == null)
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
            
        }
    }
}