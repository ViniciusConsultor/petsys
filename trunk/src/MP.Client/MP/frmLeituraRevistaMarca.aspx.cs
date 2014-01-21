using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using MP.Client.Relatorios;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;
using MP.Interfaces.Servicos;
using MP.Interfaces.Utilidades;
using Telerik.Web.UI;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace MP.Client.MP
{
    public partial class frmLeituraRevistaMarca : SuperPagina
    {
        private const string CHAVE_REVISTAS_A_PROCESSAR = "CHAVE_REVISTAS_A_PROCESSAR";
        private const string CHAVE_REVISTAS_PROCESSADAS = "CHAVE_REVISTAS_PROCESSADAS";
        private const string CHAVE_PROCESSOS_DA_REVISTA = "CHAVE_PROCESSOS_DA_REVISTA";
        private const string CHAVE_REVISTA_SELECIONADA = "CHAVE_REVISTA_SELECIONADA";
        public const string CHAVE_PROCESSOS_REUSLTADO_FILTRO = "CHAVE_PROCESSOS_REUSLTADO_FILTRO";
        public const string CHAVE_MARCAS_CLIENTES_COM_RADICAL = "CHAVE_MARCAS_CLIENTES_COM_RADICAL";
        public const string CHAVE_MARCAS_COLIDENTES = "CHAVE_MARCAS_COLIDENTES";
        public const string CHAVE_RADICAIS_CLIENTES = "CHAVE_RADICAIS_CLIENTES";
        public const string CHAVE_MARCAS_COLIDENTES_PAGINA_ATUAL_GRID = "CHAVE_MARCAS_COLIDENTES_PAGINA_ATUAL_GRID";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ExibaTelaInicial();
        }

        private void ExibaTelaInicial()
        {
            LimpaCampos();
            CarregueGridRevistasAProcessar();
            CarregueGridRevistasJaProcessadas();
            CarregaGridComProcessosExistentesNaBase(new List<IRevistaDeMarcas>());
            DesabilitaAbaFiltros();
            DesabilitaAbaRadicais();
        }

        private void DesabilitaAbaRadicais()
        {
            this.RadTabStrip1.Tabs[2].Enabled = false;
            //this.pnlRadicais.Visible = false;
        }

        private void HabilitaAbaRadicais()
        {
            this.RadTabStrip1.Tabs[2].Enabled = true;
            //this.pnlRadicais.Visible = true;
        }
        private void LimpaCampos()
        {
            var controle1 = pnlRevistaPrincipal as Control;
            UtilidadesWeb.LimparComponente(ref controle1);
            var controle2 = pnlFiltro as Control;
            UtilidadesWeb.LimparComponente(ref controle2);
            var controle3 = grdFiltros as Control;
            UtilidadesWeb.LimparComponente(ref controle3);
            var controle4 = pnlDadosDaRevistaProcesso as Control;
            UtilidadesWeb.LimparComponente(ref controle4);
            var controle5 = listRadical as Control;
            UtilidadesWeb.LimparComponente(ref controle5);
            var controle6 = grdMarcasClientes as Control;
            UtilidadesWeb.LimparComponente(ref controle6);
            var controle7 = grdMarcasColidentes as Control;
            UtilidadesWeb.LimparComponente(ref controle7);
        }

        private void LimpaRadicais()
        {
            var controle = listRadical as Control;
            UtilidadesWeb.LimparComponente(ref controle);

            listRadical.ClearEditItems();
            listRadical.ClearSelectedItems();
            listRadical.Items.Clear();
            listRadical.DataSource = new List<ILeituraRevistaDeMarcas>();
            listRadical.DataBind();

            var panel = pnlRadicais as Control;
            UtilidadesWeb.LimparComponente(ref panel);

            CarregaGridMarcasCliente(new List<ILeituraRevistaDeMarcas>());
            CarregaGridMarcasColidentes(new List<ILeituraRevistaDeMarcas>());

            ViewState[CHAVE_RADICAIS_CLIENTES] = null;

            ViewState[CHAVE_MARCAS_CLIENTES_COM_RADICAL] = null;

            ViewState[CHAVE_MARCAS_COLIDENTES] = null;
        }

        private void DesabilitaAbaFiltros()
        {
            this.RadTabStrip1.Tabs[1].Enabled = false;
        }

        private void HabilitaAbaFiltros()
        {
            this.RadTabStrip1.Tabs[1].Enabled = true;
            ctrlUF.Inicializa();
            ctrlProcurador.Inicializa();
            ctrlDespachoDeMarcas.Inicializa();

            var controleGrid = this.grdFiltros as Control;
            UtilidadesWeb.LimparComponente(ref controleGrid);
            CarregaGridFiltros(new List<ILeituraRevistaDeMarcas>());
        }

        private void CarregueGridRevistasJaProcessadas()
        {
            IList<IRevistaDeMarcas> listaDeRevistas = new List<IRevistaDeMarcas>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                listaDeRevistas = servico.ObtenhaRevistasJaProcessadas(int.MaxValue);

            if (listaDeRevistas.Count > 0)
                MostraListaRevistasJaProcessadas(listaDeRevistas);
            else
            {
                var controleGrid = this.grdRevistasJaProcessadas as Control;
                UtilidadesWeb.LimparComponente(ref controleGrid);
                MostraListaRevistasJaProcessadas(new List<IRevistaDeMarcas>());
            }
        }

        private void CarregueGridRevistasAProcessar()
        {
            IList<IRevistaDeMarcas> listaDeRevistas = new List<IRevistaDeMarcas>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                listaDeRevistas = servico.ObtenhaRevistasAProcessar(int.MaxValue);

            if (listaDeRevistas.Count > 0)
                MostraListaRevistasAProcessar(listaDeRevistas);
            else
            {
                var controleGrid = this.grdRevistasAProcessar as Control;
                UtilidadesWeb.LimparComponente(ref controleGrid);
                MostraListaRevistasAProcessar(new List<IRevistaDeMarcas>());
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

                    UtilidadesWeb.CrieDiretorio(pastaDeDestino);

                    ExtrairArquivoZip(arquivo, pastaDeDestino);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstancia().Erro("Erro ao carregar revista, exceção: ", ex);
                throw;
            }
        }

        private void ExtrairArquivoZip(UploadedFile arquivo, string pastaDeDestino)
        {
            IList<IRevistaDeMarcas> listaRevistasAProcessar = new List<IRevistaDeMarcas>();
            var revistaDeMarcas = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDeMarcas>();
            var numeroRevista = arquivo.GetNameWithoutExtension();

            if (numeroRevista.Substring(0, 2).Equals("rm"))
            {
                revistaDeMarcas.NumeroRevistaMarcas = Convert.ToInt32(numeroRevista.Substring(2, 4));
            }
            else
            {
                revistaDeMarcas.NumeroRevistaMarcas = Convert.ToInt32(numeroRevista);
            }

            var pastaDeDestinoTemp = Server.MapPath(UtilidadesWeb.URL_REVISTA_MARCA + "/temp/");

            Directory.CreateDirectory(pastaDeDestinoTemp);

            var caminhoArquivoZip = Path.Combine(pastaDeDestinoTemp, revistaDeMarcas.NumeroRevistaMarcas + arquivo.GetExtension());

            arquivo.SaveAs(caminhoArquivoZip, true);

            UtilidadesWeb.DescompacteArquivoZip(caminhoArquivoZip, pastaDeDestinoTemp);

            File.Delete(caminhoArquivoZip);

            var dirInfo = new DirectoryInfo(pastaDeDestinoTemp);

            FileInfo[] arquivos = dirInfo.GetFiles();

            foreach (var arquivoDaPasta in arquivos)
            {
                var caminhoArquivoAntigo = Path.Combine(pastaDeDestinoTemp, arquivoDaPasta.Name);

                if (arquivoDaPasta.Name.Equals("rm" + revistaDeMarcas.NumeroRevistaMarcas + arquivoDaPasta.Extension))
                {
                    var arquivoNovo = arquivoDaPasta.Name.Replace("rm" + revistaDeMarcas.NumeroRevistaMarcas + arquivoDaPasta.Extension,
                                                revistaDeMarcas.NumeroRevistaMarcas.ToString() + arquivoDaPasta.Extension);

                    var caminhoArquivoNovo = Path.Combine(pastaDeDestino, arquivoNovo);

                    File.Delete(caminhoArquivoNovo);
                    File.Move(caminhoArquivoAntigo, caminhoArquivoNovo);
                    File.Delete(caminhoArquivoAntigo);

                    revistaDeMarcas.ExtensaoArquivo = arquivoDaPasta.Extension;
                    listaRevistasAProcessar.Add(revistaDeMarcas);

                    MostraListaRevistasAProcessar(listaRevistasAProcessar);
                    return;
                }
                if (arquivoDaPasta.Name.Replace(arquivoDaPasta.Extension, "").Equals(revistaDeMarcas.NumeroRevistaMarcas.ToString()))
                {
                    var arquivoNovo = revistaDeMarcas.NumeroRevistaMarcas.ToString() + arquivoDaPasta.Extension;

                    var caminhoArquivoNovo = Path.Combine(pastaDeDestino, arquivoNovo);

                    File.Delete(caminhoArquivoNovo);
                    File.Move(caminhoArquivoAntigo, caminhoArquivoNovo);
                    File.Delete(caminhoArquivoAntigo);

                    revistaDeMarcas.ExtensaoArquivo = arquivoDaPasta.Extension;
                    listaRevistasAProcessar.Add(revistaDeMarcas);

                    MostraListaRevistasAProcessar(listaRevistasAProcessar);
                    return;
                }
            }

            revistaDeMarcas.ExtensaoArquivo = arquivo.GetExtension();
            listaRevistasAProcessar.Add(revistaDeMarcas);

            MostraListaRevistasAProcessar(listaRevistasAProcessar);
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

        private void MontaXMLParaProcessamentoDaRevistaAtravesDoTXT(IRevistaDeMarcas revistaDeMarcas)
        {
            var pastaDeDestino = Server.MapPath(UtilidadesWeb.URL_REVISTA_MARCA);

            UtilidadesWeb.CrieDiretorio(pastaDeDestino);

            var caminhoArquivoTxt = Path.Combine(pastaDeDestino, revistaDeMarcas.NumeroRevistaMarcas +
                revistaDeMarcas.ExtensaoArquivo);

            using(var arquivo = new StreamReader(caminhoArquivoTxt))
            {
                TradutorDeRevistaTxtParaRevistaXml.TraduzaRevistaDeMarcas(DateTime.Now, revistaDeMarcas.NumeroRevistaMarcas.ToString(),
                arquivo, pastaDeDestino);
                arquivo.Close();
            }
                
        }

        private XmlDocument MontaXmlParaProcessamentoDaRevista(IRevistaDeMarcas revistaDeMarcas)
        {
            var pastaDeDestino = Server.MapPath(UtilidadesWeb.URL_REVISTA_MARCA);

            UtilidadesWeb.CrieDiretorio(pastaDeDestino);

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
                IndiceSelecionado = e.Item.ItemIndex;

            if (e.CommandName == "Excluir")
            {
                var listaRevistasAProcessar = (IList<IRevistaDeMarcas>)ViewState[CHAVE_REVISTAS_A_PROCESSAR];
                listaRevistasAProcessar.RemoveAt(IndiceSelecionado);
                MostraListaRevistasAProcessar(listaRevistasAProcessar);
            }

            else if (e.CommandName == "ProcessarRevista")
            {
                var listaRevistasAProcessar = (IList<IRevistaDeMarcas>)ViewState[CHAVE_REVISTAS_A_PROCESSAR];

                try
                {
                    if (listaRevistasAProcessar[IndiceSelecionado].ExtensaoArquivo.ToUpper().Equals(".TXT"))
                    {
                        // para arquivo .txt
                        MontaXMLParaProcessamentoDaRevistaAtravesDoTXT(listaRevistasAProcessar[IndiceSelecionado]);
                        listaRevistasAProcessar[IndiceSelecionado].ExtensaoArquivo = ".xml";
                    }

                    // leitura .xml

                    var xmlRevista = MontaXmlParaProcessamentoDaRevista(listaRevistasAProcessar[IndiceSelecionado]);
                    listaRevistasAProcessar[IndiceSelecionado].Processada = true;

                    // lista de processos existentes na base, de acordo com a revista que está sendo processada.
                    IList<IRevistaDeMarcas> listaDeProcessosExistentes = new List<IRevistaDeMarcas>();

                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                        listaDeProcessosExistentes = servico.ObtenhaProcessosExistentesDeAcordoComARevistaXml(listaRevistasAProcessar[IndiceSelecionado], xmlRevista);

                    if (listaDeProcessosExistentes.Count > 0)
                    {
                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                            servico.Inserir(listaDeProcessosExistentes);

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInformacao("Processamento da revista realizado com sucesso."),
                                                    false);

                        this.ExibaTelaInicial();
                        RadPageView1.Selected = true;
                        RadTabStrip1.Tabs[0].Selected = true;
                        CarregaGridComProcessosExistentesNaBase(listaDeProcessosExistentes);
                        //CarregueGridRevistasAProcessar();
                        //CarregueGridRevistasJaProcessadas();
                        txtPublicacoesProprias.Text = listaDeProcessosExistentes.Count.ToString();
                        txtQuantdadeDeProcessos.Text = xmlRevista.GetElementsByTagName("processo").Count.ToString();
                        HabilitaAbaFiltros();
                        HabilitaAbaRadicais();

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInformacao("Não existe publicações próprias na revista processada."),
                                                    false);

                        var revistaDeMarcasParaHistorico =
                            FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDeMarcas>();

                        revistaDeMarcasParaHistorico.NumeroRevistaMarcas =
                            listaRevistasAProcessar[IndiceSelecionado].NumeroRevistaMarcas;
                        revistaDeMarcasParaHistorico.Processada = true;
                        revistaDeMarcasParaHistorico.ExtensaoArquivo =
                            listaRevistasAProcessar[IndiceSelecionado].ExtensaoArquivo;

                        listaDeProcessosExistentes.Add(revistaDeMarcasParaHistorico);

                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                            servico.Inserir(listaDeProcessosExistentes);

                        this.ExibaTelaInicial();
                        RadPageView1.Selected = true;
                        RadTabStrip1.Tabs[0].Selected = true;
                        CarregueGridRevistasAProcessar();
                        CarregueGridRevistasJaProcessadas();
                        txtPublicacoesProprias.Text = "0";
                        txtQuantdadeDeProcessos.Text = xmlRevista.GetElementsByTagName("processo").Count.ToString();
                        HabilitaAbaFiltros();
                        HabilitaAbaRadicais();
                    }
                }
                catch (BussinesException ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                }
            }
        }

        private void CarregaGridComProcessosExistentesNaBase(IList<IRevistaDeMarcas> listaDeProcessosExistentes)
        {
            IList<IProcessoDeMarca> listaDeProcessos = new List<IProcessoDeMarca>();

            if (listaDeProcessosExistentes.Count > 0)
            {
                foreach (var processo in listaDeProcessosExistentes)
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                        listaDeProcessos.Add(servico.ObtenhaProcessoDeMarcaPeloNumero(processo.NumeroProcessoDeMarca));

                MostraProcessosDaRevista(listaDeProcessos);

                pnlRelatorios.Visible = true;
            }
            else
            {
                var controleGrid = gridRevistaProcessos as Control;
                UtilidadesWeb.LimparComponente(ref controleGrid);
                MostraProcessosDaRevista(new List<IProcessoDeMarca>());
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
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void grdRevistasJaProcessadas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                var IndiceSelecionado = 0;

                if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                    IndiceSelecionado = e.Item.ItemIndex;

                if (e.CommandName == "Excluir")
                {
                    Logger.GetInstancia().Info("Excluindo revista de marcas já processada.");

                    var listaRevistasJaProcessadas = (IList<IRevistaDeMarcas>)ViewState[CHAVE_REVISTAS_PROCESSADAS];

                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                    {
                        servico.Excluir(listaRevistasJaProcessadas[IndiceSelecionado].NumeroRevistaMarcas);
                    }

                    listaRevistasJaProcessadas.RemoveAt(IndiceSelecionado);
                    MostraListaRevistasJaProcessadas(listaRevistasJaProcessadas);

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInformacao("Revista removida com sucesso."),
                                                    false);
                }

                if (e.CommandName == "ReprocessarRevista")
                {
                    var listaRevistasProcessadas = (IList<IRevistaDeMarcas>)ViewState[CHAVE_REVISTAS_PROCESSADAS];


                    if (listaRevistasProcessadas[IndiceSelecionado].ExtensaoArquivo.ToUpper().Equals(".TXT"))
                    {
                        // para arquivo .txt
                        MontaXMLParaProcessamentoDaRevistaAtravesDoTXT(listaRevistasProcessadas[IndiceSelecionado]);
                        listaRevistasProcessadas[IndiceSelecionado].ExtensaoArquivo = ".xml";
                    }

                    // leitura .xml
                    var xmlRevista = MontaXmlParaProcessamentoDaRevista(listaRevistasProcessadas[IndiceSelecionado]);
                    listaRevistasProcessadas[IndiceSelecionado].Processada = true;
                    // lista de processos existentes na base, de acordo com a revista que está sendo processada.
                    IList<IRevistaDeMarcas> listaDeProcessosExistentes = new List<IRevistaDeMarcas>();

                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                        listaDeProcessosExistentes = servico.ObtenhaProcessosExistentesDeAcordoComARevistaXml(listaRevistasProcessadas[IndiceSelecionado], xmlRevista);

                    if (listaDeProcessosExistentes.Count > 0)
                    {
                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                            servico.Inserir(listaDeProcessosExistentes);

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInformacao("Processamento da revista realizado com sucesso."),
                                                    false);
                        this.ExibaTelaInicial();
                        RadPageView1.Selected = true;
                        RadTabStrip1.Tabs[0].Selected = true;
                        HabilitaAbaRadicais();
                        HabilitaAbaFiltros();
                        CarregaGridComProcessosExistentesNaBase(listaDeProcessosExistentes);
                        txtPublicacoesProprias.Text = listaDeProcessosExistentes.Count.ToString();
                        txtQuantdadeDeProcessos.Text = xmlRevista.GetElementsByTagName("processo").Count.ToString();
                        RadTabStrip1.Tabs[0].SelectParents();
                        RadTabStrip1.Tabs[0].Selected = true;

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                     UtilidadesWeb.MostraMensagemDeInformacao("Não existe processos próprios na revista processada."),
                                                     false);

                        this.ExibaTelaInicial();
                        RadPageView1.Selected = true;
                        HabilitaAbaRadicais();
                        HabilitaAbaFiltros();
                        txtPublicacoesProprias.Text = "0";
                        txtQuantdadeDeProcessos.Text = xmlRevista.GetElementsByTagName("processo").Count.ToString();
                        RadTabStrip1.Tabs[0].SelectParents();
                        RadTabStrip1.Tabs[0].Selected = true;

                    }
                }
            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
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
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
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
                var revistaSelecionada = (IRevistaDeMarcas)ViewState[CHAVE_REVISTA_SELECIONADA];
                var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroLeituraDeRevistaDeMarcas>();

                filtro.NumeroDoProcesso = txtProcesso.Text;
                filtro.UF = ctrlUF.Sigla != null ? ctrlUF.Sigla.Sigla : null;
                filtro.Procurador = ctrlProcurador.ProcuradorSelecionado;
                filtro.Despacho = ctrlDespachoDeMarcas.DespachoDeMarcasSelecionada;

                // leitura .xml
                var xmlRevista = MontaXmlParaProcessamentoDaRevista(revistaSelecionada);

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                    listaDeProcessosDaRevista = servico.ObtenhaResultadoDaConsultaPorFiltroXML(xmlRevista, filtro);

                if (listaDeProcessosDaRevista.Count > 0)
                {
                    // adicionar viewstate para filtro CHAVE_PROCESSOS_REUSLTADO_FILTRO
                    CarregaGridFiltros(listaDeProcessosDaRevista);
                    txtQuantdadeDeProcessos.Text = listaDeProcessosDaRevista.Count.ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                         UtilidadesWeb.MostraMensagemDeInformacao("Não existe resultados para o filtro informado."),
                                                         false);
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

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                id = Convert.ToInt64((e.Item.Cells[3].Text));

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
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
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
                IndiceSelecionado = e.Item.ItemIndex;

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
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void grdFiltros_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdFiltros, Session[CHAVE_PROCESSOS_REUSLTADO_FILTRO], e);
        }

        protected void listRadical_OnPageIndexChanged(object sender, RadListViewPageChangedEventArgs e)
        {
            if (ViewState[CHAVE_RADICAIS_CLIENTES] != null)
            {
                listRadical.CurrentPageIndex = e.NewPageIndex;
                listRadical.DataSource = ViewState[CHAVE_RADICAIS_CLIENTES];
                listRadical.DataBind();

                var idLeitura = ((ILeituraRevistaDeMarcas)listRadical.Items[0].DataItem).IdLeitura.Value;

                IDictionary<long, IList<ILeituraRevistaDeMarcas>> dicionarioDeMarcasDeClientes =
                    new Dictionary<long, IList<ILeituraRevistaDeMarcas>>();

                IList<ILeituraRevistaDeMarcas> listaDeMarcasDeClientes = new List<ILeituraRevistaDeMarcas>();

                dicionarioDeMarcasDeClientes = (IDictionary<long, IList<ILeituraRevistaDeMarcas>>)ViewState[CHAVE_MARCAS_CLIENTES_COM_RADICAL];

                listaDeMarcasDeClientes = dicionarioDeMarcasDeClientes[idLeitura];

                CarregaGridMarcasCliente(listaDeMarcasDeClientes);

                IDictionary<long, IList<ILeituraRevistaDeMarcas>> dicionarioDeMarcasColidentes =
                    new Dictionary<long, IList<ILeituraRevistaDeMarcas>>();

                IList<ILeituraRevistaDeMarcas> listaDeMarcasColidentes = new List<ILeituraRevistaDeMarcas>();

                dicionarioDeMarcasColidentes =
                    (IDictionary<long, IList<ILeituraRevistaDeMarcas>>)ViewState[CHAVE_MARCAS_COLIDENTES];

                listaDeMarcasColidentes = dicionarioDeMarcasColidentes[idLeitura];

                CarregaGridMarcasColidentes(listaDeMarcasColidentes);
            }
        }

        protected void RadTabStrip1_OnTabClick(object sender, RadTabStripEventArgs e)
        {
            if (e.Tab.Text.Equals("Radicais"))
            {
                Logger.GetInstancia().Debug("Iniciando busca de radicais colidentes");

                e.Tab.PostBack = true;
                this.pnlRadicais.Visible = true;

                var revistaSelecionada = (IRevistaDeMarcas)ViewState[CHAVE_REVISTA_SELECIONADA];

                var xmlRevista = MontaXmlParaProcessamentoDaRevista(revistaSelecionada);

                IList<ILeituraRevistaDeMarcas> listaDeTodosProcessosDaRevistaXML = new List<ILeituraRevistaDeMarcas>();
                IList<IProcessoDeMarca> listaDeProcessosDeMarcasComRadicalCadastrado = new List<IProcessoDeMarca>();
                IList<IProcessoDeMarca> listaDeProcessosDeMarcasComRadicalAdiconadoAMarca = new List<IProcessoDeMarca>();
                IList<ILeituraRevistaDeMarcas> listaDeProcessosDaRevistaComMarcaExistente = new List<ILeituraRevistaDeMarcas>();

                Logger.GetInstancia().Debug("Buscando todos os processos da revista de marcas xml");

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                    listaDeTodosProcessosDaRevistaXML = servico.obtenhaTodosOsProcessosDaRevistaDeMarcasXML(xmlRevista);

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                {
                    Logger.GetInstancia().Debug("Buscando todos os processos com marcas que contem radical cadastrado");
                    listaDeProcessosDeMarcasComRadicalCadastrado = servico.obtenhaProcessosComMarcaQueContemRadicalDadastrado();

                    // código feito para adicionar a lista de radicais na marca, pois a marca não faz relação com
                    // o objeto IRadicalMarcas
                    Logger.GetInstancia().Debug("Buscando processos com radical adicionado na marca");
                    listaDeProcessosDeMarcasComRadicalAdiconadoAMarca = servico.ObtenhaProcessoComRadicailAdicionadoNaMarca(listaDeProcessosDeMarcasComRadicalCadastrado);
                }

                if (listaDeProcessosDeMarcasComRadicalAdiconadoAMarca.Count > 0)
                {
                    foreach (var processoDaRevista in listaDeTodosProcessosDaRevistaXML.Where(processoDaRevista => !string.IsNullOrEmpty(processoDaRevista.Marca)))
                        listaDeProcessosDaRevistaComMarcaExistente.Add(processoDaRevista);

                    // obtendo informações para o preenchimento das grids, com as marcas de clientes e as marcas colidentes da revista
                    IDictionary<IList<ILeituraRevistaDeMarcas>, IList<ILeituraRevistaDeMarcas>> dicionarioDeMarcasColidentesEClientes;

                    Logger.GetInstancia().Debug("Obtendo lista de marcas colidentes");
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDeMarcas>())
                        dicionarioDeMarcasColidentesEClientes = servico.obtenhaListaDasMarcasColidentesEClientes(listaDeProcessosDaRevistaComMarcaExistente, listaDeProcessosDeMarcasComRadicalAdiconadoAMarca);

                    if (dicionarioDeMarcasColidentesEClientes.Count > 0 )
                        CarregaListaDeRadicais(dicionarioDeMarcasColidentesEClientes);
                    else
                    {
                        // não existe marcas colidentes
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                         UtilidadesWeb.MostraMensagemDeInformacao("Não existe marcas colidentes."),
                                                         false);

                        LimpaRadicais();
                    }
                        
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao("Não existe marcas colidentes."),
                                                        false);

                    LimpaRadicais();
                }
                   

                Logger.GetInstancia().Debug("Fim da busca de radicais colidentes");
            }
        }

        private void CarregaListaDeRadicais(IDictionary<IList<ILeituraRevistaDeMarcas>, IList<ILeituraRevistaDeMarcas>> dicionarioDeMarcasColidentesEClientes)
        {
            IList<ILeituraRevistaDeMarcas> listaDeRadicaisDeClientes = new List<ILeituraRevistaDeMarcas>();

            IDictionary<long, IList<ILeituraRevistaDeMarcas>> dicionarioDeMarcasDeClientes =
                new Dictionary<long, IList<ILeituraRevistaDeMarcas>>();

            IDictionary<long, IList<ILeituraRevistaDeMarcas>> dicionarioDeMarcasDeColidentes =
                new Dictionary<long, IList<ILeituraRevistaDeMarcas>>();

            bool marcaDeClienteCarregadaPrimeiraVez = false;

            foreach (IList<ILeituraRevistaDeMarcas> listaObjetoLeitura in dicionarioDeMarcasColidentesEClientes.Values)
            {
                listaDeRadicaisDeClientes = listaObjetoLeitura.OrderBy(radical => radical.Radical).ToList();

                foreach (var objetoLeitura in listaDeRadicaisDeClientes)
                {
                    IList<ILeituraRevistaDeMarcas> listaDeMarcasDeClientes = new List<ILeituraRevistaDeMarcas>();
                    listaDeMarcasDeClientes.Add(objetoLeitura);
                    dicionarioDeMarcasDeClientes.Add(objetoLeitura.IdLeitura.Value, listaDeMarcasDeClientes);

                    if (!marcaDeClienteCarregadaPrimeiraVez)
                    {
                        CarregaGridMarcasCliente(listaDeMarcasDeClientes);
                        marcaDeClienteCarregadaPrimeiraVez = true;
                    }
                }
            }

            foreach (var listaMarcasColidentes in dicionarioDeMarcasColidentesEClientes.Keys)
            {
                foreach (var marcaColidente in listaMarcasColidentes)
                {
                    IList<ILeituraRevistaDeMarcas> listaDeMarcasColidentes = new List<ILeituraRevistaDeMarcas>();
                    listaDeMarcasColidentes.Add(marcaColidente);

                    if (dicionarioDeMarcasDeColidentes.ContainsKey(marcaColidente.IdLeitura.Value))
                    {
                        dicionarioDeMarcasDeColidentes[marcaColidente.IdLeitura.Value].Add(marcaColidente);
                    }
                    else
                    {
                        dicionarioDeMarcasDeColidentes.Add(marcaColidente.IdLeitura.Value, listaDeMarcasColidentes);
                    }
                }
            }

            if (listaDeRadicaisDeClientes.Count > 0)
            {
                listRadical.ClearEditItems();
                listRadical.ClearSelectedItems();
                listRadical.Items.Clear();
                listRadical.DataSource = listaDeRadicaisDeClientes;
                listRadical.DataBind();
                ViewState.Add(CHAVE_RADICAIS_CLIENTES, listaDeRadicaisDeClientes);

                ViewState.Add(CHAVE_MARCAS_CLIENTES_COM_RADICAL, dicionarioDeMarcasDeClientes);

                ViewState.Add(CHAVE_MARCAS_COLIDENTES, dicionarioDeMarcasDeColidentes);

                var idLeitura = ((ILeituraRevistaDeMarcas)listRadical.Items[0].DataItem).IdLeitura;

                if (idLeitura != null)
                    CarregaGridMarcasColidentes(dicionarioDeMarcasDeColidentes[idLeitura.Value]);
            }
            else
            {
                // não existe marcas colidentes
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                 UtilidadesWeb.MostraMensagemDeInformacao("Não existe marcas colidentes."),
                                                 false);

                LimpaRadicais();
            }
        }

        private void CarregaGridMarcasCliente(IList<ILeituraRevistaDeMarcas> listaDeMarcasDeClientes)
        {
            grdMarcasClientes.MasterTableView.DataSource = listaDeMarcasDeClientes;
            grdMarcasClientes.DataBind();
        }

        private void CarregaGridMarcasColidentes(IList<ILeituraRevistaDeMarcas> listaDeMarcasColidentes)
        {
            grdMarcasColidentes.MasterTableView.DataSource = listaDeMarcasColidentes;
            grdMarcasColidentes.DataBind();
            ViewState.Add(CHAVE_MARCAS_COLIDENTES_PAGINA_ATUAL_GRID, listaDeMarcasColidentes);
        }

        protected void grdMarcasClientes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                id = Convert.ToInt64((e.Item.Cells[2].Text));
        }

        protected void grdMarcasClientes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdMarcasClientes, ViewState[CHAVE_MARCAS_CLIENTES_COM_RADICAL], e);
        }

        protected void grdMarcasClientes_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdMarcasClientes.MasterTableView.RenderColumns)
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void grdMarcasColidentes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                id = Convert.ToInt64((e.Item.Cells[2].Text));
        }

        protected void grdMarcasColidentes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            //UtilidadesWeb.PaginacaoDataGrid(ref grdMarcasColidentes, ViewState[CHAVE_MARCAS_COLIDENTES], e);
            UtilidadesWeb.PaginacaoDataGrid(ref grdMarcasColidentes, ViewState[CHAVE_MARCAS_COLIDENTES_PAGINA_ATUAL_GRID], e);
        }

        protected void grdMarcasColidentes_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdMarcasColidentes.MasterTableView.RenderColumns)
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void btnRelPublicPropriasAnalitico_OnClick(object sender, ImageClickEventArgs e)
        {
            if (ViewState[CHAVE_PROCESSOS_DA_REVISTA] != null)
            {
                IList<IProcessoDeMarca> listaDeProcessos = new List<IProcessoDeMarca>();

                var numeroDaRevistaSelecionada = ((IRevistaDeMarcas)ViewState[CHAVE_REVISTA_SELECIONADA]).NumeroRevistaMarcas.ToString();

                listaDeProcessos = ((IList<IProcessoDeMarca>)ViewState[CHAVE_PROCESSOS_DA_REVISTA]);

                var gerador = new GeradorDeRelatorioDeProcessosDeMarcasPublicacoesProprias(listaDeProcessos);
                var nomeDoArquivo = gerador.GereRelatorioAnalitico(numeroDaRevistaSelecionada);

                var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" +
                      nomeDoArquivo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);
            }
        }

        protected void btnRelPublicPropriasSintetico_OnClick(object sender, ImageClickEventArgs e)
        {
            if (ViewState[CHAVE_PROCESSOS_DA_REVISTA] != null)
            {
                IList<IProcessoDeMarca> listaDeProcessos = new List<IProcessoDeMarca>();

                listaDeProcessos = ((IList<IProcessoDeMarca>)ViewState[CHAVE_PROCESSOS_DA_REVISTA]);

                var numeroDaRevistaSelecionada = ((IRevistaDeMarcas)ViewState[CHAVE_REVISTA_SELECIONADA]).NumeroRevistaMarcas.ToString();

                var gerador = new GeradorDeRelatorioDeProcessosDeMarcasPublicacoesProprias(listaDeProcessos);
                var nomeDoArquivo = gerador.GereRelatorioSintetico(numeroDaRevistaSelecionada);

                var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" +
                      nomeDoArquivo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);
            }
        }

        public override void  VerifyRenderingInServerForm(Control control)
        {
        }

    }
}