﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MP.Interfaces.Negocio.Filtros.Marcas;
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
        private const string CHAVE_REVISTA_SELECIONADA = "CHAVE_REVISTA_SELECIONADA";
        public const string CHAVE_PROCESSOS_REUSLTADO_FILTRO = "CHAVE_PROCESSOS_REUSLTADO_FILTRO";

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
            CarregaGridComProcessosExistentesNaBase(new List<IRevistaDeMarcas>());
            EscondaPanelDeFiltro();
        }

        private void EscondaPanelDeFiltro()
        {
            pnlFiltro.Visible = false;
        }

        private void ExibaPanelDeFiltro()
        {
            pnlFiltro.Visible = true;
            ctrlUF.Inicializa();
            ctrlProcurador.Inicializa();
            ctrlDespachoDeMarcas.Inicializa();
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
                
                throw ex;
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

                throw ex;
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

        private void MostraProcessosDaRevista(IList<IProcessoDeMarca> listaDeProcessosDaRevista)
        {
            gridRevistaProcessos.MasterTableView.DataSource = listaDeProcessosDaRevista;
            gridRevistaProcessos.DataBind();
            ViewState.Add(CHAVE_PROCESSOS_DA_REVISTA, listaDeProcessosDaRevista);
        }

        private void AdicioneNumeroDaRevistaSelecionada(IRevistaDeMarcas revistaSelecionada)
        {
            ViewState.Add(CHAVE_REVISTA_SELECIONADA, revistaSelecionada);
        }

        private XmlDocument MontaXmlParaProcessamentoDaRevista(IRevistaDeMarcas revistaDeMarcas )
        {
            var pastaDeDestino = Server.MapPath(UtilidadesWeb.URL_REVISTA_MARCA);

            CaminhoArquivo = Path.Combine(pastaDeDestino, revistaDeMarcas.NumeroRevistaMarcas +
                revistaDeMarcas.ExtensaoArquivo);

            AdicioneNumeroDaRevistaSelecionada(revistaDeMarcas);

            var xmlRevista = new XmlDocument();

            xmlRevista.Load(CaminhoArquivo);

            return xmlRevista;
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
                    if (listaRevistasAProcessar[IndiceSelecionado].ExtensaoArquivo.ToUpper().Equals(".TXT"))
                    {
                        // leitura .txt
                    }
                    else
                    {
                        // leitura .xml

                        var xmlRevista = MontaXmlParaProcessamentoDaRevista(listaRevistasAProcessar[IndiceSelecionado]);

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

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao("Processamento da revista realizado com sucesso."), 
                                                        false);
                            
                            CarregaGridComProcessosExistentesNaBase(listaDeProcessosExistentes);
                            CarregueGridRevistasAProcessar();
                            CarregueGridRevistasJaProcessadas();

                            txtPublicacoesProprias.Text = listaDeProcessosExistentes.Count.ToString();
                            txtQuantdadeDeProcessos.Text = xmlRevista.GetElementsByTagName("processo").Count.ToString();

                            ExibaPanelDeFiltro();

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao("Não existe processos próprios na revista em processada."),
                                                        false);
                        }

                    }
                    
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                }
            }
        }

        private void CarregaGridComProcessosExistentesNaBase(IList<IRevistaDeMarcas> listaDeProcessosExistentes)
        {
            try
            {
                IList<IProcessoDeMarca> listaDeProcessos = new List<IProcessoDeMarca>();

                if (listaDeProcessosExistentes.Count > 0)
                {
                    foreach (var processo in listaDeProcessosExistentes)
                    {
                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                        {
                            listaDeProcessos.Add(servico.ObtenhaProcessoDeMarcaPeloNumero(processo.NumeroProcessoDeMarca));
                        }
                    }

                    MostraProcessosDaRevista(listaDeProcessos);
                }
                else
                {
                    var controleGrid = this.gridRevistaProcessos as Control;
                    UtilidadesWeb.LimparComponente(ref controleGrid);

                    MostraProcessosDaRevista(new List<IProcessoDeMarca>());
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
            {
                IndiceSelecionado = e.Item.ItemIndex;
            }

            if (e.CommandName == "ReprocessarRevista")
            {
                var listaRevistasProcessadas = (IList<IRevistaDeMarcas>)ViewState[CHAVE_REVISTAS_PROCESSADAS];

                try
                {
                    if (listaRevistasProcessadas[IndiceSelecionado].ExtensaoArquivo.ToUpper().Equals(".TXT"))
                    {
                        // leitura .txt
                    }
                    else
                    {
                        // leitura .xml

                        var xmlRevista = MontaXmlParaProcessamentoDaRevista(listaRevistasProcessadas[IndiceSelecionado]);

                        listaRevistasProcessadas[IndiceSelecionado].Processada = true;

                        // lista de processos existentes na base, de acordo com a revista que está sendo processada.
                        IList<IRevistaDeMarcas> listaDeProcessosExistentes = new List<IRevistaDeMarcas>();

                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                        {
                            listaDeProcessosExistentes = servico.ObtenhaProcessosExistentesDeAcordoComARevistaXml(listaRevistasProcessadas[IndiceSelecionado], xmlRevista);
                        }

                        if (listaDeProcessosExistentes.Count > 0)
                        {
                            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                            {
                                servico.Inserir(listaDeProcessosExistentes);
                            }

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao("Processamento da revista realizado com sucesso."),
                                                        false);

                            CarregaGridComProcessosExistentesNaBase(listaDeProcessosExistentes);

                            txtPublicacoesProprias.Text = listaDeProcessosExistentes.Count.ToString();

                            ExibaPanelDeFiltro();

                            txtQuantdadeDeProcessos.Text = xmlRevista.GetElementsByTagName("processo").Count.ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                         UtilidadesWeb.MostraMensagemDeInformacao("Não existe processos próprios na revista em processada."),
                                                         false);
                        }

                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                }
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
            if (string.IsNullOrEmpty(txtProcesso.Text) && string.IsNullOrEmpty(ctrlUF.Codigo) &&
                ctrlProcurador.ProcuradorSelecionado == null && ctrlDespachoDeMarcas.DespachoDeMarcasSelecionada == null)
            {
                // Nenhum filtro

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                         UtilidadesWeb.MostraMensagemDeInformacao("Não existe filtro informado para realizar a consulta das informações."),
                                                         false);
            }
            else
            {
                IList<ILeituraRevistaDeMarcas> listaDeProcessosDaRevista = new List<ILeituraRevistaDeMarcas>();

                var revistaSelecionada = (IRevistaDeMarcas) ViewState[CHAVE_REVISTA_SELECIONADA];

                var filtro =
                            FabricaGenerica.GetInstancia().CrieObjeto<IFiltroLeituraDeRevistaDeMarcas>();

                

                filtro.NumeroDoProcesso = txtProcesso.Text;
                filtro.UF = ctrlUF.Sigla != null ? ctrlUF.Sigla.Sigla : null;
                filtro.Procurador = ctrlProcurador.ProcuradorSelecionado;
                filtro.Despacho = ctrlDespachoDeMarcas.DespachoDeMarcasSelecionada;

                if (revistaSelecionada.ExtensaoArquivo.ToUpper().Equals(".TXT"))
                {
                    // leitura .txt
                }
                else
                {
                    // leitura .xml

                    var xmlRevista = MontaXmlParaProcessamentoDaRevista(revistaSelecionada);

                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                    {
                        listaDeProcessosDaRevista = servico.ObtenhaResultadoDaConsultaPorFiltroXML(xmlRevista, filtro);
                    }

                    if (listaDeProcessosDaRevista.Count > 0)
                    {
                        // adicionar viewstate para filtro CHAVE_PROCESSOS_REUSLTADO_FILTRO
                        CarregaGridFiltros(listaDeProcessosDaRevista); 
                        txtQuantdadeDeProcessos.Text = listaDeProcessosDaRevista.Count.ToString();
                    }
                }
            }
        }

        protected void btnLimpar_ButtonClick(object sender, EventArgs e)
        {
            txtProcesso.Text = string.Empty;
            ctrlUF.Inicializa();
            ctrlProcurador.Inicializa();
            ctrlDespachoDeMarcas.Inicializa();
        }

        protected void gridRevistaProcessos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;
             var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
            {
                id = Convert.ToInt64((e.Item.Cells[3].Text));
                IndiceSelecionado = e.Item.ItemIndex;
            }

            if (e.CommandName == "Modificar")
            {
                var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/cdProcessoDeMarca.aspx",
                                            "?Id=", id);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.ExibeJanela(url,
                                                                                   "Modificar processo de marca",
                                                                                   800, 550), false);
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

        private void CarregaGridFiltros(IList<ILeituraRevistaDeMarcas> listaDeProcessosDaRevista)
        {
            grdFiltros.MasterTableView.DataSource = listaDeProcessosDaRevista;
            grdFiltros.DataBind();
            Session.Add(CHAVE_PROCESSOS_REUSLTADO_FILTRO, listaDeProcessosDaRevista);
        }

        protected void grdFiltros_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
            {
                IndiceSelecionado = e.Item.ItemIndex;
            }

            if (e.CommandName == "DetalharProcesso")
            {
                var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/frmDetalhesLeituraDaRevista.aspx",
                                            "?Indice=", IndiceSelecionado);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.ExibeJanela(url,
                                                                                   "Detalhes do processo da revista marcas",
                                                                                   800, 550), false);
            }
        }

        protected void grdFiltros_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdFiltros.MasterTableView.RenderColumns)
                {
                    if ((column is GridButtonColumn))
                    {
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
                    }
                }
            }
        }

        protected void grdFiltros_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdFiltros, Session[CHAVE_PROCESSOS_REUSLTADO_FILTRO], e);
        }
    }
}