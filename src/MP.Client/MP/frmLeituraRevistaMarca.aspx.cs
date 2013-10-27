using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmLeituraRevistaMarca : System.Web.UI.Page
    {
        private const string ID_OBJETO = "ID_OBJETO_FRM_LEITURA_REVISTA_MARCA";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_FRM_LEITURA_REVISTA_MARCA";
        private const string CHAVE_REVISTAS_A_PROCESSAR = "CHAVE_REVISTAS_A_PROCESSAR";
        private const string CHAVE_REVISTAS_PROCESSADAS = "CHAVE_REVISTAS_PROCESSADAS";

        protected void Page_Load(object sender, EventArgs e)
        {
            // carregua grids já processadas.
        }

        public string CaminhoArquivo { get; set; }

        public string ExtensaoDoArquivo { get; set; }

        protected void uplRevistaMarca_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                if (uplRevistaMarca.UploadedFiles.Count > 0)
                {
                    var arquivo = uplRevistaMarca.UploadedFiles[0];
                    var pastaDeDestino = Server.MapPath(UtilidadesWeb.URL_REVISTA_MARCA);
                    var caminhoArquivo = Path.Combine(pastaDeDestino, arquivo.GetNameWithoutExtension() + arquivo.GetExtension());
                    arquivo.SaveAs(caminhoArquivo);

                    ExtensaoDoArquivo = arquivo.GetExtension();
                    CaminhoArquivo = string.Concat(UtilidadesWeb.URL_REVISTA_MARCA, "/", arquivo.GetNameWithoutExtension() + arquivo.GetExtension());

                    IList<IRevistaDeMarcas> listaRevistasAProcessar = new List<IRevistaDeMarcas>();

                    var revistaDeMarcas = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDeMarcas>();

                    var numeroRevista = arquivo.GetNameWithoutExtension();

                    if(numeroRevista.Substring(0,2).Equals("rm"))
                    {
                        revistaDeMarcas.NumeroRevistaMarcas = Convert.ToInt32(numeroRevista.Substring(2, 4));
                        listaRevistasAProcessar.Add(revistaDeMarcas);
                    }
                    else
                    {
                        revistaDeMarcas.NumeroRevistaMarcas = Convert.ToInt32(numeroRevista);
                        listaRevistasAProcessar.Add(revistaDeMarcas);
                    }

                    MostraListaRevistasAProcessar(listaRevistasAProcessar);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstancia().Erro("Erro ao carregar revista, exceção: ", ex);
            }
        }

        private void MostraListaRevistasAProcessar(IList<IRevistaDeMarcas> listaRevistasAProcessar)
        {
            grdRevistasAProcessar.MasterTableView.DataSource = listaRevistasAProcessar;
            grdRevistasAProcessar.DataBind();
            ViewState.Add(CHAVE_REVISTAS_A_PROCESSAR, listaRevistasAProcessar);
        }

        protected void grdRevistasAProcessar_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
            {
                IndiceSelecionado = e.Item.ItemIndex;
            }

            if (e.CommandName == "Excluir")
            {
                var listaRevistasAProcessar = (IList<IRevistaDeMarcas>)ViewState[CHAVE_REVISTAS_A_PROCESSAR];

                listaRevistasAProcessar.RemoveAt(IndiceSelecionado);
                MostraListaRevistasAProcessar(listaRevistasAProcessar);
            }

            if(e.CommandName == "ProcessarRevista")
            {
                
            }
        }

        protected void grdRevistasAProcessar_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdRevistasAProcessar, ViewState[CHAVE_REVISTAS_A_PROCESSAR], e);
        }

        protected void grdRevistasAProcessar_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdRevistasAProcessar.MasterTableView.RenderColumns)
                {
                    if ((column is GridButtonColumn))
                    {
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
                    }
                }
            }
        }

        protected void grdRevistasJaProcessadas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void grdRevistasJaProcessadas_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void grdRevistasJaProcessadas_ItemCreated(object sender, GridItemEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnFiltrar_ButtonClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnLimpar_ButtonClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void gridRevistaProcessos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void gridRevistaProcessos_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void gridRevistaProcessos_ItemCreated(object sender, GridItemEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}