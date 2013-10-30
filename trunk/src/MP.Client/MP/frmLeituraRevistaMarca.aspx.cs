﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmLeituraRevistaMarca : SuperPagina
    {
        private const string ID_OBJETO = "ID_OBJETO_FRM_LEITURA_REVISTA_MARCA";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_FRM_LEITURA_REVISTA_MARCA";
        private const string CHAVE_REVISTAS_A_PROCESSAR = "CHAVE_REVISTAS_A_PROCESSAR";
        private const string CHAVE_REVISTAS_PROCESSADAS = "CHAVE_REVISTAS_PROCESSADAS";
        private const string CHAVE_PROCESSOS_DA_REVISTA = "CHAVE_PROCESSOS_DA_REVISTA";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ExibaTelaInicial();
            }
        }

        private void ExibaTelaInicial()
        {
            CarregueGridRevistasAProcessar();
            CarregueGridRevistasJaProcessadas();
        }

        private void CarregueGridRevistasJaProcessadas()
        {
            try
            {
                IList<IRevistaDeMarcas> listaDeRevistas = new List<IRevistaDeMarcas>();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                {
                    listaDeRevistas = servico.ObtenhaRevistasJaProcessadas(grdRevistasJaProcessadas.PageSize);
                }

                if(listaDeRevistas.Count > 0)
                {
                    MostraListaRevistasJaProcessadas(listaDeRevistas);
                }
                else
                {
                    var controleGrid = this.grdRevistasJaProcessadas as Control;
                    UtilidadesWeb.LimparComponente(ref controleGrid);

                    MostraListaRevistasJaProcessadas(new List<IRevistaDeMarcas>());
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private void CarregueGridRevistasAProcessar()
        {
            try
            {
                IList<IRevistaDeMarcas> listaDeRevistas = new List<IRevistaDeMarcas>();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                {
                    listaDeRevistas = servico.ObtenhaRevistasAProcessar(grdRevistasJaProcessadas.PageSize);
                }

                if (listaDeRevistas.Count > 0)
                {
                    MostraListaRevistasAProcessar(listaDeRevistas);
                }
                else
                {
                    var controleGrid = this.grdRevistasAProcessar as Control;
                    UtilidadesWeb.LimparComponente(ref controleGrid);

                    MostraListaRevistasAProcessar(new List<IRevistaDeMarcas>());
                }
            }
            catch (Exception ex)
            {

                throw;
            }
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

                    Directory.CreateDirectory(pastaDeDestino);

                    arquivo.SaveAs(caminhoArquivo,true);
                    
                    IList<IRevistaDeMarcas> listaRevistasAProcessar = new List<IRevistaDeMarcas>();

                    var revistaDeMarcas = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDeMarcas>();

                    var numeroRevista = arquivo.GetNameWithoutExtension();

                    revistaDeMarcas.ExtensaoArquivo = arquivo.GetExtension();

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
                throw;
            }
        }

        private void MostraListaRevistasAProcessar(IList<IRevistaDeMarcas> listaRevistasAProcessar)
        {
            grdRevistasAProcessar.MasterTableView.DataSource = listaRevistasAProcessar;
            grdRevistasAProcessar.DataBind();
            ViewState.Add(CHAVE_REVISTAS_A_PROCESSAR, listaRevistasAProcessar);
        }

        private void MostraListaRevistasJaProcessadas(IList<IRevistaDeMarcas> listaRevistasJaProcessadas)
        {
            grdRevistasJaProcessadas.MasterTableView.DataSource = listaRevistasJaProcessadas;
            grdRevistasJaProcessadas.DataBind();
            ViewState.Add(CHAVE_REVISTAS_PROCESSADAS, listaRevistasJaProcessadas);
        }

        private void MostraProcessosCadastradosDaRevista(IList<IProcessoDeMarca> listaDeProcessosDaRevista)
        {
            gridRevistaProcessos.MasterTableView.DataSource = listaDeProcessosDaRevista;
            gridRevistaProcessos.DataBind();
            ViewState.Add(CHAVE_PROCESSOS_DA_REVISTA, listaDeProcessosDaRevista);
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

            else if(e.CommandName == "ProcessarRevista")
            {
                var listaRevistasAProcessar = (IList<IRevistaDeMarcas>)ViewState[CHAVE_REVISTAS_A_PROCESSAR];

                try
                {
                    var pastaDeDestino = Server.MapPath(UtilidadesWeb.URL_REVISTA_MARCA);

                    CaminhoArquivo = Path.Combine(pastaDeDestino, listaRevistasAProcessar[IndiceSelecionado].NumeroRevistaMarcas + 
                        listaRevistasAProcessar[IndiceSelecionado].ExtensaoArquivo);

                    if (listaRevistasAProcessar[IndiceSelecionado].ExtensaoArquivo.Equals(".txt"))
                    {
                        // leitura .txt
                    }
                    else if (listaRevistasAProcessar[IndiceSelecionado].ExtensaoArquivo.Equals(".xml"))
                    {
                        var xmlRevista = new XmlDocument();

                        xmlRevista.Load(CaminhoArquivo);

                        listaRevistasAProcessar[IndiceSelecionado].Processada = true;

                        // lista de processos existentes na base, de acordo com a revista que está sendo processada.
                        IList<IRevistaDeMarcas> listaDeProcessosExistentes = new List<IRevistaDeMarcas>();
                        
                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                        {
                            listaDeProcessosExistentes = servico.ObtenhaProcessosExistentesDeAcordoComARevistaXml(listaRevistasAProcessar[IndiceSelecionado], xmlRevista);
                        }

                        if (listaDeProcessosExistentes.Count > 0)
                        {
                            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                            {
                                servico.Inserir(listaDeProcessosExistentes);
                            }

                            CarregaGridComProcessosExistentesNaBase(listaDeProcessosExistentes);
                            //carregar grid com as informações dos processos da revista
                            //carrega grid Já processadas
                        }
                        else
                        {
                            // tratar
                        }

                    }
                    
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
            }
        }

        private void CarregaGridComProcessosExistentesNaBase(IList<IRevistaDeMarcas> listaDeProcessosExistentes)
        {
            IList<IProcessoDeMarca> listaDeProcessos = new List<IProcessoDeMarca>();

            foreach (var processo in listaDeProcessosExistentes)
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                {
                    listaDeProcessos.Add(servico.ObtenhaProcessoDeMarcaPeloNumero(processo.NumeroProcessoDeMarca));
                }
            }

            MostraProcessosCadastradosDaRevista(listaDeProcessos);
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
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
            {
                IndiceSelecionado = e.Item.ItemIndex;
            }

            if (e.CommandName == "ProcessarRevista")
            {
                //var listaRevistasAProcessar = (IList<IRevistaDeMarcas>)ViewState[CHAVE_REVISTAS_PROCESSADAS];

                //try
                //{
                //    var pastaDeDestino = Server.MapPath(UtilidadesWeb.URL_REVISTA_MARCA);

                //    CaminhoArquivo = Path.Combine(pastaDeDestino, listaRevistasAProcessar[IndiceSelecionado].NumeroRevistaMarcas +
                //        listaRevistasAProcessar[IndiceSelecionado].ExtensaoArquivo);

                //    if (listaRevistasAProcessar[IndiceSelecionado].ExtensaoArquivo.Equals(".txt"))
                //    {
                //        // leitura .txt
                //    }
                //    else if (listaRevistasAProcessar[IndiceSelecionado].ExtensaoArquivo.Equals(".xml"))
                //    {
                //        var xmlRevista = new XmlDocument();

                //        xmlRevista.Load(CaminhoArquivo);

                //        listaRevistasAProcessar[IndiceSelecionado].Processada = true;
                //    }

                //}
                //catch (Exception ex)
                //{

                //    throw;
                //}
            }
        }

        protected void grdRevistasJaProcessadas_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdRevistasJaProcessadas, ViewState[CHAVE_REVISTAS_PROCESSADAS], e);
        }

        protected void grdRevistasJaProcessadas_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdRevistasJaProcessadas.MasterTableView.RenderColumns)
                {
                    if ((column is GridButtonColumn))
                    {
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
                    }
                }
            }
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
             var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
            {
                IndiceSelecionado = e.Item.ItemIndex;
            }

            if (e.CommandName == "DetalharProcesso")
            {

            }
        }

        protected void gridRevistaProcessos_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref gridRevistaProcessos, ViewState[CHAVE_PROCESSOS_DA_REVISTA], e);
        }

        protected void gridRevistaProcessos_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in gridRevistaProcessos.MasterTableView.RenderColumns)
                {
                    if ((column is GridButtonColumn))
                    {
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
                    }
                }
            }
        }

        protected override string ObtenhaIdFuncao()
        {
            return "";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
        }
    }
}