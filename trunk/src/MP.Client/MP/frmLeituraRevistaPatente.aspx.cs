using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Client.Relatorios.Patentes;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Patentes;
using MP.Interfaces.Servicos;
using MP.Interfaces.Utilidades;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmLeituraRevistaPatente : SuperPagina
    {
        private const string CHAVE_REVISTAS_A_PROCESSAR = "CHAVE_REVISTAS_A_PROCESSAR";
        private const string CHAVE_REVISTAS_PROCESSADAS = "CHAVE_REVISTAS_PROCESSADAS";
        private const string CHAVE_REVISTA_SELECIONADA = "CHAVE_REVISTA_SELECIONADA";
        private const string CHAVE_PROCESSOS_DA_REVISTA = "CHAVE_PROCESSOS_DA_REVISTA";
        private const string CHAVE_PROCESSOS_REUSLTADO_FILTRO = "CHAVE_PROCESSOS_REUSLTADO_FILTRO";
        public const string CHAVE_PATENTES_CLIENTES_COM_RADICAL = "CHAVE_PATENTES_CLIENTES_COM_RADICAL";
        public const string CHAVE_PATENTES_COLIDENTES = "CHAVE_PATENTES_COLIDENTES";
        public const string CHAVE_RADICAIS_CLIENTES = "CHAVE_RADICAIS_CLIENTES";

        public string CaminhoArquivo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarreguePaginaInicial();
        }

        private void CarreguePaginaInicial()
        {
            CarregueRevistasAProcessar();
            CarregueRevistasProcessadas();
            grdFiltros.DataSource = new List<IRevistaDePatente>();
            gridRevistaProcessos.DataSource = new List<IRevistaDePatente>();
            HabilteAbaFiltro(false);
        }

        private void HabilteAbaFiltro(bool habilite)
        {
            RadTabStrip1.Tabs[1].Enabled = habilite;
            RadTabStrip1.Tabs[2].Enabled = habilite;

            if (habilite)
                ctrlFitroRevistaPatente1.Inicializa();
        }

        private void CarregueRevistasAProcessar()
        {
            IList<IRevistaDePatente> listaDeRevistas = new List<IRevistaDePatente>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDePatente>())
                listaDeRevistas = servico.ObtenhaRevistasAProcessar(int.MaxValue);

            if (listaDeRevistas.Count > 0)
                MostraListaRevistasAProcessar(listaDeRevistas);
            else
            {
                var controleGrid = this.grdRevistasAProcessar as Control;
                UtilidadesWeb.LimparComponente(ref controleGrid);
                MostraListaRevistasAProcessar(new List<IRevistaDePatente>());
            }
        }

        private void CarregueRevistasProcessadas()
        {
            IList<IRevistaDePatente> listaDeRevistas = new List<IRevistaDePatente>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDePatente>())
                listaDeRevistas = servico.ObtenhaRevistasJaProcessadas(int.MaxValue);

            if (listaDeRevistas.Count > 0)
                MostraListaRevistasJaProcessadas(listaDeRevistas);
            else
            {
                var controleGrid = this.grdRevistasJaProcessadas as Control;
                UtilidadesWeb.LimparComponente(ref controleGrid);
                MostraListaRevistasJaProcessadas(new List<IRevistaDePatente>());
            }
        }

        protected void grdRevistasAProcessar_ItemCommand(object sender, GridCommandEventArgs e)
        {
            int indiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                indiceSelecionado = e.Item.ItemIndex;

            if (e.CommandName == "ProcessarRevista")
            {
                var revistasAProcessar = (IList<IRevistaDePatente>)ViewState[CHAVE_REVISTAS_A_PROCESSAR];

                try
                {
                    // Caso o arquivo tenha extensão .txt
                    if (revistasAProcessar[indiceSelecionado].ExtensaoArquivo.ToUpper().Equals(".TXT"))
                    {
                        MontaXMLParaProcessamentoDaRevistaAtravesDoTXT(revistasAProcessar[indiceSelecionado]);
                        revistasAProcessar[indiceSelecionado].ExtensaoArquivo = ".xml";
                    }

                    var xmlRevista = MontaXmlParaProcessamentoDaRevista(revistasAProcessar[indiceSelecionado]);
                    revistasAProcessar[indiceSelecionado].Processada = true;

                    // lista de processos existentes na base, de acordo com a revista que está sendo processada.
                    IList<IRevistaDePatente> listaDeProcessosExistentes = new List<IRevistaDePatente>();

                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDePatente>())
                        listaDeProcessosExistentes = servico.ObtenhaProcessosExistentesDeAcordoComARevistaXml(revistasAProcessar[indiceSelecionado], xmlRevista);

                    if (listaDeProcessosExistentes.Count > 0)
                    {
                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDePatente>())
                            servico.Inserir(listaDeProcessosExistentes);

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInformacao("Processamento da revista realizado com sucesso."), false);

                        CarreguePaginaInicial();
                        RadPageView1.Selected = true;
                        RadTabStrip1.Tabs[0].Selected = true;
                        CarregaGridComProcessosExistentesNaBase(listaDeProcessosExistentes);
                        txtPublicacoesProprias.Text = listaDeProcessosExistentes.Count.ToString();
                        txtQuantdadeDeProcessos.Text = xmlRevista.GetElementsByTagName("processo").Count.ToString();
                        RadTabStrip1.Tabs[0].SelectParents();
                        RadTabStrip1.Tabs[0].Selected = true;
                        HabilteAbaFiltro(true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInformacao("Não existe publicações próprias na revista processada."), false);

                        var revistaDePatenteParaHistorico = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDePatente>();

                        revistaDePatenteParaHistorico.NumeroRevistaPatente =
                            revistasAProcessar[indiceSelecionado].NumeroRevistaPatente;
                        revistaDePatenteParaHistorico.Processada = true;
                        revistaDePatenteParaHistorico.ExtensaoArquivo = revistasAProcessar[indiceSelecionado].ExtensaoArquivo;
                        revistaDePatenteParaHistorico.NumeroDoProcesso =
                            revistasAProcessar[indiceSelecionado].NumeroDoProcesso;
                        revistaDePatenteParaHistorico.NumeroProcessoDaPatente = revistasAProcessar[indiceSelecionado].NumeroProcessoDaPatente;

                        listaDeProcessosExistentes.Add(revistaDePatenteParaHistorico);

                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDePatente>())
                            servico.Inserir(listaDeProcessosExistentes);

                        CarregueRevistasProcessadas();
                        CarregueRevistasAProcessar();
                        RadPageView1.Selected = true;
                        txtPublicacoesProprias.Text = "0";
                        txtQuantdadeDeProcessos.Text = xmlRevista.GetElementsByTagName("processo").Count.ToString();
                        RadTabStrip1.Tabs[0].SelectParents();
                        RadTabStrip1.Tabs[0].Selected = true;
                        HabilteAbaFiltro(true);
                    }
                }
                catch (BussinesException ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                }
            }

            if (e.CommandName == "Excluir")
            {
                var revistasAProcessar = (IList<IRevistaDePatente>)ViewState[CHAVE_REVISTAS_A_PROCESSAR];
                revistasAProcessar.RemoveAt(indiceSelecionado);
                MostraListaRevistasAProcessar(revistasAProcessar);
            }
        }

        protected void grdRevistasAProcessar_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdRevistasAProcessar_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdRevistasAProcessar, ViewState[CHAVE_REVISTAS_A_PROCESSAR], e);
        }

        protected void grdRevistasJaProcessadas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            int indiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                indiceSelecionado = e.Item.ItemIndex;

            if (e.CommandName == "ReprocessarRevista")
            {
                var revistasProcessadas = (IList<IRevistaDePatente>)ViewState[CHAVE_REVISTAS_PROCESSADAS];

                try
                {
                    // Caso o arquivo tenha extensão .txt
                    if (revistasProcessadas[indiceSelecionado].ExtensaoArquivo.ToUpper().Equals(".TXT"))
                    {
                        MontaXMLParaProcessamentoDaRevistaAtravesDoTXT(revistasProcessadas[indiceSelecionado]);
                        revistasProcessadas[indiceSelecionado].ExtensaoArquivo = ".xml";
                    }

                    var xmlRevista = MontaXmlParaProcessamentoDaRevista(revistasProcessadas[indiceSelecionado]);
                    revistasProcessadas[indiceSelecionado].Processada = true;

                    // lista de processos existentes na base, de acordo com a revista que está sendo processada.
                    IList<IRevistaDePatente> listaDeProcessosExistentes = new List<IRevistaDePatente>();

                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDePatente>())
                        listaDeProcessosExistentes = servico.ObtenhaProcessosExistentesDeAcordoComARevistaXml(revistasProcessadas[indiceSelecionado], xmlRevista);

                    if (listaDeProcessosExistentes.Count > 0)
                    {
                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDePatente>())
                            servico.Inserir(listaDeProcessosExistentes);

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInformacao("Processamento da revista realizado com sucesso."), false);

                        CarreguePaginaInicial();
                        RadPageView1.Selected = true;
                        RadTabStrip1.Tabs[0].Selected = true;
                        CarregaGridComProcessosExistentesNaBase(listaDeProcessosExistentes);
                        txtPublicacoesProprias.Text = listaDeProcessosExistentes.Count.ToString();
                        txtQuantdadeDeProcessos.Text = xmlRevista.GetElementsByTagName("processo").Count.ToString();
                        RadTabStrip1.Tabs[0].SelectParents();
                        RadTabStrip1.Tabs[0].Selected = true;
                        HabilteAbaFiltro(true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInformacao("Não existe publicações próprias na revista processada."), false);

                        CarreguePaginaInicial();
                        RadPageView1.Selected = true;
                        txtPublicacoesProprias.Text = "0";
                        txtQuantdadeDeProcessos.Text = xmlRevista.GetElementsByTagName("processo").Count.ToString();
                        RadTabStrip1.Tabs[0].SelectParents();
                        RadTabStrip1.Tabs[0].Selected = true;
                        HabilteAbaFiltro(true);
                    }
                }
                catch (BussinesException ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                }
            }

            if (e.CommandName == "Excluir")
            {
                Logger.GetInstancia().Info("Excluindo revista de patentes já processada.");

                var listaRevistasJaProcessadas = (IList<IRevistaDePatente>)ViewState[CHAVE_REVISTAS_PROCESSADAS];

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDePatente>())
                {
                    servico.Excluir(Convert.ToInt32(listaRevistasJaProcessadas[indiceSelecionado].NumeroRevistaPatente));
                }

                listaRevistasJaProcessadas.RemoveAt(indiceSelecionado);
                MostraListaRevistasJaProcessadas(listaRevistasJaProcessadas);

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.MostraMensagemDeInformacao("Revista removida com sucesso."),
                                                false);
            }
        }

        protected void grdRevistasJaProcessadas_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdRevistasJaProcessadas_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdRevistasJaProcessadas, ViewState[CHAVE_REVISTAS_PROCESSADAS], e);
        }

        protected void gridRevistaProcessos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                id = Convert.ToInt64((e.Item.Cells[4].Text));

            switch (e.CommandName)
            {
                case "Modificar":

                    var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(),
                                            "MP/cdProcessoDePatente.aspx",
                                            "?Id=", id);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url, "Modificar processo de patente",
                                                                                  800, 550, "cdProcessoDePatente_aspx"),
                                                        false);
                    break;

                case "Email":
                    var url2 = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/frmEnviaEmail.aspx",
                                             "?Id=", id, "&Tipo=P","&Despacho=1");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url2,
                                                                                  "Enviar e-mail",
                                                                                  800, 550, "frmEnviaEmail_aspx"), false);
                    break;
            }
        }

        protected void gridRevistaProcessos_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void gridRevistaProcessos_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref gridRevistaProcessos, ViewState[CHAVE_PROCESSOS_DA_REVISTA], e);
        }

        protected void grdFiltros_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                IndiceSelecionado = e.Item.ItemIndex;

            if (e.CommandName == "DetalharProcesso")
            {
                var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/cdProcessoDePatente.aspx",
                                            "?Id=", IndiceSelecionado);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.ExibeJanela(url, "Detalhes do processo da revista patentes", 800, 550, "cdProcessoDePatente_aspx"), false);
            }
        }

        protected void grdFiltros_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdFiltros_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdFiltros, ViewState[CHAVE_PROCESSOS_REUSLTADO_FILTRO], e);
        }

        protected void uplRevistaPatente_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                if (uplRevistaPatente.UploadedFiles.Count > 0)
                {
                    var arquivo = uplRevistaPatente.UploadedFiles[0];
                    var pastaDeDestino = Server.MapPath(Util.URL_REVISTA_PATENTE);

                    UtilidadesWeb.CrieDiretorio(pastaDeDestino);

                    IList<IRevistaDePatente> listaRevistasAProcessar = new List<IRevistaDePatente>();
                    var revistaDePatentes = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDePatente>();
                    var numeroRevista = arquivo.GetNameWithoutExtension().Remove(0, 1);
                    revistaDePatentes.ExtensaoArquivo = arquivo.GetExtension();
                    revistaDePatentes.NumeroRevistaPatente = Convert.ToInt32(numeroRevista);
                    listaRevistasAProcessar.Add(revistaDePatentes);

                    UtilidadesWeb.CrieDiretorio(pastaDeDestino);
                    ExtrairArquivoZip(arquivo, pastaDeDestino);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstancia().Erro("Erro ao carregar revista da patente, exceção: ", ex);
                throw;
            }
        }

        protected void btnFiltrar_ButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ctrlFitroRevistaPatente1.Codigo))
            {
                // Nenhum filtro informado
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                         UtilidadesWeb.MostraMensagemDeInformacao("Selecionado o campo que deseja filtrar."),
                                                         false);
            }
            else if (string.IsNullOrEmpty(txtValor.Text))
            {
                // Nenhum filtro informado
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                         UtilidadesWeb.MostraMensagemDeInformacao("Informe o valor do filtro a ser pesquisado."),
                                                         false);
            }
            else
            {
                IList<IRevistaDePatente> listaDeProcessosDaRevista = new List<IRevistaDePatente>();
                var revistaSelecionada = (IRevistaDePatente)ViewState[CHAVE_REVISTA_SELECIONADA];
                var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroLeituraDeRevistaDePatentes>();

                filtro.EnumeradorFiltro = EnumeradorFiltroPatente.Obtenha(int.Parse(ctrlFitroRevistaPatente1.Codigo));
                filtro.ValorFiltro = txtValor.Text;

                // leitura .xml
                var xmlRevista = MontaXmlParaProcessamentoDaRevista(revistaSelecionada);

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDePatente>())
                    listaDeProcessosDaRevista = servico.ObtenhaTodosOsProcessosDaRevistaXML(xmlRevista, filtro);

                if (listaDeProcessosDaRevista.Count > 0)
                {
                    // adicionar viewstate para filtro CHAVE_PROCESSOS_REUSLTADO_FILTRO
                    CarregaGridFiltros(listaDeProcessosDaRevista);
                    txtQuantdadeDeProcessos.Text = listaDeProcessosDaRevista.Count.ToString();
                }
                else
                {
                    CarregaGridFiltros(new List<IRevistaDePatente>());
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                         UtilidadesWeb.MostraMensagemDeInformacao("Não existe resultados para o filtro informado."),
                                                         false);
                }
            }
        }

        private void CarregaGridFiltros(IList<IRevistaDePatente> listaDeProcessosDaRevista)
        {
            grdFiltros.MasterTableView.DataSource = listaDeProcessosDaRevista;
            grdFiltros.DataBind();
            ViewState.Add(CHAVE_PROCESSOS_REUSLTADO_FILTRO, listaDeProcessosDaRevista);
        }

        protected void btnLimpar_ButtonClick(object sender, EventArgs e)
        {
            ctrlFitroRevistaPatente1.Inicializa();
            txtValor.Text = string.Empty;
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.016";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
        }

        private void ExibaRevistasDePatentesAProcessar(IList<IRevistaDePatente> revistaDePatentes)
        {
            grdRevistasAProcessar.MasterTableView.DataSource = revistaDePatentes;
            grdRevistasAProcessar.DataBind();
            ViewState.Add(CHAVE_REVISTAS_A_PROCESSAR, revistaDePatentes);
        }

        private void MontaXMLParaProcessamentoDaRevistaAtravesDoTXT(IRevistaDePatente revistaDePatente)
        {
            var pastaDeDestino = Server.MapPath(Util.URL_REVISTA_PATENTE);

            UtilidadesWeb.CrieDiretorio(pastaDeDestino);

            var caminhoArquivoTxt = Path.Combine(pastaDeDestino, revistaDePatente.NumeroRevistaPatente + revistaDePatente.ExtensaoArquivo);
            using (var arquivo = new StreamReader(caminhoArquivoTxt))
            {
                TradutorDeRevistaPatenteTXTParaRevistaPatenteXML.TraduzaRevistaDePatente(DateTime.Now, revistaDePatente.NumeroRevistaPatente.ToString(), arquivo, pastaDeDestino);
                arquivo.Close();
            }

        }

        private XmlDocument MontaXmlParaProcessamentoDaRevista(IRevistaDePatente revistaDePatente)
        {
            var pastaDeDestino = Server.MapPath(Util.URL_REVISTA_PATENTE);

            UtilidadesWeb.CrieDiretorio(pastaDeDestino);

            CaminhoArquivo = Path.Combine(pastaDeDestino, revistaDePatente.NumeroRevistaPatente + revistaDePatente.ExtensaoArquivo);
            AdicioneNumeroDaRevistaSelecionada(revistaDePatente);
            var xmlRevista = new XmlDocument();
            xmlRevista.Load(CaminhoArquivo);
            return xmlRevista;
        }

        private void AdicioneNumeroDaRevistaSelecionada(IRevistaDePatente revistaSelecionada)
        {
            ViewState.Add(CHAVE_REVISTA_SELECIONADA, revistaSelecionada);
        }

        private void MostraProcessosDaRevista(IList<IProcessoDePatente> listaDeProcessosDaRevista)
        {
            gridRevistaProcessos.MasterTableView.DataSource = listaDeProcessosDaRevista;
            gridRevistaProcessos.DataBind();
            ViewState.Add(CHAVE_PROCESSOS_DA_REVISTA, listaDeProcessosDaRevista);
        }

        private void MostraListaRevistasAProcessar(IList<IRevistaDePatente> listaRevistasAProcessar)
        {
            grdRevistasAProcessar.MasterTableView.DataSource = listaRevistasAProcessar;
            grdRevistasAProcessar.DataBind();
            ViewState.Add(CHAVE_REVISTAS_A_PROCESSAR, listaRevistasAProcessar);
        }

        private void MostraListaRevistasJaProcessadas(IList<IRevistaDePatente> listaRevistasJaProcessadas)
        {
            grdRevistasJaProcessadas.MasterTableView.DataSource = listaRevistasJaProcessadas;
            grdRevistasJaProcessadas.DataBind();
            ViewState.Add(CHAVE_REVISTAS_PROCESSADAS, listaRevistasJaProcessadas);
        }

        private void CarregaGridComProcessosExistentesNaBase(IList<IRevistaDePatente> listaDeProcessosExistentes)
        {
            IList<IProcessoDePatente> listaDeProcessos = new List<IProcessoDePatente>();

            if (listaDeProcessosExistentes.Count > 0)
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
                    foreach (var processo in listaDeProcessosExistentes)
                        listaDeProcessos.Add(processo.NumeroDoProcesso.Length == 15 ? servico.ObtenhaPeloNumeroDoProcesso(processo.NumeroDoProcesso.Remove(0, 4)) :
                                                                                      servico.ObtenhaPeloNumeroDoProcesso(processo.NumeroDoProcesso));
                MostraProcessosDaRevista(listaDeProcessos);
                pnlRelatorios.Visible = true;
            }
            else
            {
                var controleGrid = gridRevistaProcessos as Control;
                UtilidadesWeb.LimparComponente(ref controleGrid);
                MostraProcessosDaRevista(new List<IProcessoDePatente>());
            }
        }

        private void ExtrairArquivoZip(UploadedFile arquivo, string pastaDeDestino)
        {
            IList<IRevistaDePatente> listaRevistasAProcessar = new List<IRevistaDePatente>();
            var revistaDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDePatente>();
            var numeroRevista = arquivo.GetNameWithoutExtension();

            revistaDePatente.NumeroRevistaPatente = Convert.ToInt32(numeroRevista.Substring(0, 1).ToUpper().Equals("P") ? numeroRevista.Substring(1, 4) : numeroRevista);

            var pastaDeDestinoTemp = Server.MapPath(Util.URL_REVISTA_PATENTE + "/temp/");

            Directory.CreateDirectory(pastaDeDestinoTemp);

            var caminhoArquivoZip = Path.Combine(pastaDeDestinoTemp, revistaDePatente.NumeroRevistaPatente + arquivo.GetExtension());

            arquivo.SaveAs(caminhoArquivoZip, true);

            UtilidadesWeb.DescompacteArquivoZip(caminhoArquivoZip, pastaDeDestinoTemp);

            File.Delete(caminhoArquivoZip);

            var dirInfo = new DirectoryInfo(pastaDeDestinoTemp);

            FileInfo[] arquivos = dirInfo.GetFiles();

            foreach (var arquivoDaPasta in arquivos)
            {
                var caminhoArquivoAntigo = Path.Combine(pastaDeDestinoTemp, arquivoDaPasta.Name);

                if (arquivoDaPasta.Name.Equals("P" + revistaDePatente.NumeroRevistaPatente + arquivoDaPasta.Extension))
                {
                    var arquivoNovo = arquivoDaPasta.Name.Replace("P" + revistaDePatente.NumeroRevistaPatente + arquivoDaPasta.Extension,
                                                revistaDePatente.NumeroRevistaPatente.ToString() + arquivoDaPasta.Extension);

                    var caminhoArquivoNovo = Path.Combine(pastaDeDestino, arquivoNovo);

                    File.Delete(caminhoArquivoNovo);
                    File.Move(caminhoArquivoAntigo, caminhoArquivoNovo);
                    File.Delete(caminhoArquivoAntigo);

                    revistaDePatente.ExtensaoArquivo = arquivoDaPasta.Extension;
                    listaRevistasAProcessar.Add(revistaDePatente);

                    MostraListaRevistasAProcessar(listaRevistasAProcessar);
                    return;
                }
                if (arquivoDaPasta.Name.Replace(arquivoDaPasta.Extension, "").Equals(revistaDePatente.NumeroRevistaPatente.ToString()))
                {
                    var arquivoNovo = revistaDePatente.NumeroRevistaPatente.ToString() + arquivoDaPasta.Extension;

                    var caminhoArquivoNovo = Path.Combine(pastaDeDestino, arquivoNovo);

                    File.Delete(caminhoArquivoNovo);
                    File.Move(caminhoArquivoAntigo, caminhoArquivoNovo);
                    File.Delete(caminhoArquivoAntigo);

                    revistaDePatente.ExtensaoArquivo = arquivoDaPasta.Extension;
                    listaRevistasAProcessar.Add(revistaDePatente);

                    MostraListaRevistasAProcessar(listaRevistasAProcessar);
                    return;
                }
            }

            revistaDePatente.ExtensaoArquivo = arquivo.GetExtension();
            listaRevistasAProcessar.Add(revistaDePatente);

            MostraListaRevistasAProcessar(listaRevistasAProcessar);
        }

        protected void listRadical_OnPageIndexChanged(object sender, RadListViewPageChangedEventArgs e)
        {
            if (ViewState[CHAVE_RADICAIS_CLIENTES] != null)
            {
                listRadical.CurrentPageIndex = e.NewPageIndex;
                listRadical.DataSource = ViewState[CHAVE_RADICAIS_CLIENTES];
                listRadical.DataBind();

                if (listRadical.Items.Count > 0 && !((IRadicalPatente)listRadical.Items[0].DataItem).IdRadicalPatente.HasValue)
                    return;

                var radical = ((IRadicalPatente)listRadical.Items[0].DataItem).IdRadicalPatente.Value;

                IDictionary<long, IList<IProcessoDePatente>> dicionarioDePatentesDeClientes = new Dictionary<long, IList<IProcessoDePatente>>();

                IList<IProcessoDePatente> listaDePatentesDeClientes = new List<IProcessoDePatente>();

                dicionarioDePatentesDeClientes = (IDictionary<long, IList<IProcessoDePatente>>)ViewState[CHAVE_PATENTES_CLIENTES_COM_RADICAL];

                if (dicionarioDePatentesDeClientes.ContainsKey(radical))
                    listaDePatentesDeClientes = dicionarioDePatentesDeClientes[radical];

                CarregaGridPatentesCliente(listaDePatentesDeClientes);

                IDictionary<long, IList<IRevistaDePatente>> dicionarioDePatentesColidentes = new Dictionary<long, IList<IRevistaDePatente>>();

                IList<IRevistaDePatente> listaDePatentesColidentes = new List<IRevistaDePatente>();

                dicionarioDePatentesColidentes = (IDictionary<long, IList<IRevistaDePatente>>)ViewState[CHAVE_PATENTES_COLIDENTES];

                if (dicionarioDePatentesColidentes.ContainsKey(radical))
                    listaDePatentesColidentes = dicionarioDePatentesColidentes[radical];

                CarregaGridPatentesColidentes(listaDePatentesColidentes);
            }
        }

        protected void grdPatenteClientes_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void grdPatenteClientes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            if (ViewState[CHAVE_RADICAIS_CLIENTES] != null)
            {
                listRadical.DataSource = ViewState[CHAVE_RADICAIS_CLIENTES];
                listRadical.DataBind();

                var dicionarioDePatentesDeClientes = (IDictionary<long, IList<IRevistaDePatente>>)ViewState[CHAVE_PATENTES_CLIENTES_COM_RADICAL];

                if (!((IRadicalPatente)listRadical.Items[0].DataItem).IdRadicalPatente.HasValue)
                    return;

                var radical = ((IRadicalPatente)listRadical.Items[0].DataItem).IdRadicalPatente.Value;
                UtilidadesWeb.PaginacaoDataGrid(ref grdPatenteClientes, dicionarioDePatentesDeClientes[radical], e);
            }
        }

        protected void grdPatenteClientes_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdPatenteClientes.MasterTableView.RenderColumns)
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void grdPatentesColidentes_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void grdPatentesColidentes_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdPatentesColidentes.MasterTableView.RenderColumns)
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void grdPatentesColidentes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            if (ViewState[CHAVE_RADICAIS_CLIENTES] != null)
            {
                listRadical.DataSource = ViewState[CHAVE_RADICAIS_CLIENTES];
                listRadical.DataBind();

                var dicionarioDePatentesDeColidentes = (IDictionary<long, IList<IRevistaDePatente>>)ViewState[CHAVE_PATENTES_COLIDENTES];

                if (!((IRadicalPatente)listRadical.Items[0].DataItem).IdRadicalPatente.HasValue)
                    return;

                var radical = ((IRadicalPatente)listRadical.Items[0].DataItem).IdRadicalPatente.Value;
                UtilidadesWeb.PaginacaoDataGrid(ref grdPatentesColidentes, dicionarioDePatentesDeColidentes[radical], e);
            }
        }

        protected void RadTabStrip1_OnTabClick(object sender, RadTabStripEventArgs e)
        {
            if (e.Tab.Text.Equals("Radicais"))
            {
                Logger.GetInstancia().Debug("Iniciando busca de radicais colidentes");

                e.Tab.PostBack = true;
                this.pnlRadicais.Visible = true;

                var revistaSelecionada = (IRevistaDePatente)ViewState[CHAVE_REVISTA_SELECIONADA];

                var xmlRevista = MontaXmlParaProcessamentoDaRevista(revistaSelecionada);

                IList<IRevistaDePatente> listaDeTodosProcessosDaRevistaXML = new List<IRevistaDePatente>();
                IList<IProcessoDePatente> listaDeProcessosDePatentesComRadicalCadastrado = new List<IProcessoDePatente>();
                IList<IRevistaDePatente> listaDeProcessosDaRevistaComPatenteExistente = new List<IRevistaDePatente>();

                Logger.GetInstancia().Debug("Buscando todos os processos da revista de patente xml");

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDePatente>())
                    listaDeTodosProcessosDaRevistaXML = servico.CarregueDadosDeTodaRevistaXML(xmlRevista);

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
                {
                    Logger.GetInstancia().Debug("Buscando todos os processos com patentes que contem radical cadastrado");
                    listaDeProcessosDePatentesComRadicalCadastrado = servico.obtenhaProcessosComPatenteQueContemRadicalCadastrado();
                }

                if (listaDeProcessosDePatentesComRadicalCadastrado.Count > 0)
                {
                    pnlRelatoriosRadical.Visible = true;
                    foreach (var processoDaRevista in listaDeTodosProcessosDaRevistaXML.Where(processoDaRevista => !string.IsNullOrEmpty(processoDaRevista.Titulo)))
                        listaDeProcessosDaRevistaComPatenteExistente.Add(processoDaRevista);

                    // obtendo informações para o preenchimento das grids, com as patentes de clientes e as patentes colidentes da revista
                    IList<IRevistaDePatente> revistaDePatentes = new List<IRevistaDePatente>();

                    Logger.GetInstancia().Debug("Obtendo lista de patentes colidentes");

                    if (listaDeProcessosDaRevistaComPatenteExistente.Count > 0)
                        CarregaListaDeRadicais(listaDeProcessosDaRevistaComPatenteExistente, listaDeProcessosDePatentesComRadicalCadastrado);
                    else
                    {
                        // não existe patentes colidentes
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                         UtilidadesWeb.MostraMensagemDeInformacao("Não existe patentes colidentes."),
                                                         false);

                        LimpaRadicais();
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao("Não existe patentes colidentes."),
                                                        false);

                    LimpaRadicais();
                }


                Logger.GetInstancia().Debug("Fim da busca de radicais colidentes");
            }
        }

        private void LimpaRadicais()
        {
            var controle = listRadical as Control;
            UtilidadesWeb.LimparComponente(ref controle);

            listRadical.ClearEditItems();
            listRadical.ClearSelectedItems();
            listRadical.Items.Clear();
            listRadical.DataSource = new List<IRevistaDePatente>();
            listRadical.DataBind();

            var panel = pnlRadicais as Control;
            UtilidadesWeb.LimparComponente(ref panel);

            CarregaGridPatentesCliente(new List<IProcessoDePatente>());
            CarregaGridPatentesColidentes(new List<IRevistaDePatente>());

            ViewState[CHAVE_PATENTES_CLIENTES_COM_RADICAL] = null;
            ViewState[CHAVE_PATENTES_COLIDENTES] = null;
            ViewState[CHAVE_RADICAIS_CLIENTES] = null;
        }

        private void CarregaGridPatentesCliente(IList<IProcessoDePatente> listaDeProcessoDeClientes)
        {
            grdPatenteClientes.MasterTableView.DataSource = listaDeProcessoDeClientes;
            grdPatenteClientes.DataBind();
        }

        private void CarregaGridPatentesColidentes(IList<IRevistaDePatente> listaDeRevistaDeClientes)
        {
            grdPatentesColidentes.MasterTableView.DataSource = listaDeRevistaDeClientes;
            grdPatentesColidentes.DataBind();
        }

        private void CarregaListaDeRadicais(IList<IRevistaDePatente> revistaDePatentes, IList<IProcessoDePatente> processoDePatentes)
        {
            IList<IRadicalPatente> listaDeRadicaisDeClientes = new List<IRadicalPatente>();
            IList<IRadicalPatente> listaDeRadicaisASeremRemovidos = new List<IRadicalPatente>();
            IDictionary<long, IList<IProcessoDePatente>> dicionarioDePatentesDeClientes = new Dictionary<long, IList<IProcessoDePatente>>();
            IDictionary<long, IList<IRevistaDePatente>> dicionarioDePatentesDeColidentes = new Dictionary<long, IList<IRevistaDePatente>>();

            bool patenteDeClienteCarregadaPrimeiraVez = false;

            foreach (IProcessoDePatente processoDePatente in processoDePatentes)
            {
                if (processoDePatente.Patente.Radicais != null && processoDePatente.Patente.Radicais.Count > 0)
                    foreach (IRadicalPatente radicalPatente in processoDePatente.Patente.Radicais)
                        if (!listaDeRadicaisDeClientes.Contains(radicalPatente) && !string.IsNullOrEmpty(radicalPatente.Colidencia))
                            listaDeRadicaisDeClientes.Add(radicalPatente);

                if (processoDePatente.Patente.Classificacoes != null && processoDePatente.Patente.Classificacoes.Count > 0)
                {
                    int indice = 0;

                    foreach (IClassificacaoPatente classificacaoPatente in processoDePatente.Patente.Classificacoes)
                    {
                        var radicalComClassificacao = FabricaGenerica.GetInstancia().CrieObjeto<IRadicalPatente>();
                        radicalComClassificacao.Classificacao = classificacaoPatente.Classificacao;
                        radicalComClassificacao.IdRadicalPatente = processoDePatente.IdProcessoDePatente + indice;
                        radicalComClassificacao.IdPatente = processoDePatente.Patente.Identificador;

                        if (!listaDeRadicaisDeClientes.Contains(radicalComClassificacao) && !string.IsNullOrEmpty(radicalComClassificacao.Classificacao))
                        {
                            listaDeRadicaisDeClientes.Add(radicalComClassificacao);
                            indice++;
                        }
                    }
                }
            }

            foreach (IRadicalPatente radical in listaDeRadicaisDeClientes)
            {
                IList<IRevistaDePatente> listaDePatentesColidentes = new List<IRevistaDePatente>();
                IList<IProcessoDePatente> listaDeProcessosDeClientes = new List<IProcessoDePatente>();

                if (!string.IsNullOrEmpty(radical.Colidencia))
                {
                    listaDePatentesColidentes = revistaDePatentes.ToList().FindAll(revista => revista.Titulo.Contains(radical.Colidencia));
                    listaDeProcessosDeClientes = processoDePatentes.ToList().FindAll(processo => processo.Patente.Radicais != null &&
                        processo.Patente.Radicais.Contains(radical));

                    if (radical.IdRadicalPatente.HasValue && listaDePatentesColidentes.Count > 0)
                    {
                        dicionarioDePatentesDeColidentes.Add(radical.IdRadicalPatente.Value, listaDePatentesColidentes);
                        dicionarioDePatentesDeClientes.Add(radical.IdRadicalPatente.Value, listaDeProcessosDeClientes);
                    }

                    if (!patenteDeClienteCarregadaPrimeiraVez)
                    {
                        CarregaGridPatentesCliente(listaDeProcessosDeClientes);
                        CarregaGridPatentesColidentes(listaDePatentesColidentes);
                        patenteDeClienteCarregadaPrimeiraVez = true;
                    }
                }
                else if (!string.IsNullOrEmpty(radical.Classificacao))
                {
                    listaDePatentesColidentes = revistaDePatentes.ToList().FindAll(revista =>
                        (!string.IsNullOrEmpty(revista.ClassificacaoInternacional) && revista.ClassificacaoInternacional.Contains(radical.Classificacao)) ||
                        (!string.IsNullOrEmpty(revista.ClassificacaoNacional) && revista.ClassificacaoNacional.Contains(radical.Classificacao)));

                    foreach (IProcessoDePatente processoDePatente in processoDePatentes)
                        foreach (IClassificacaoPatente classificacaoPatente in processoDePatente.Patente.Classificacoes)
                            if (radical.Classificacao.Contains(classificacaoPatente.Classificacao) && listaDePatentesColidentes.Count > 0)
                                listaDeProcessosDeClientes.Add(processoDePatente);

                    if (listaDePatentesColidentes.Count == 0)
                        listaDeRadicaisASeremRemovidos.Add(radical);

                    if (radical.IdRadicalPatente.HasValue && listaDePatentesColidentes.Count > 0)
                    {
                        dicionarioDePatentesDeColidentes.Add(radical.IdRadicalPatente.Value, listaDePatentesColidentes);
                        dicionarioDePatentesDeClientes.Add(radical.IdRadicalPatente.Value, listaDeProcessosDeClientes);
                    }

                    if (!patenteDeClienteCarregadaPrimeiraVez)
                    {
                        CarregaGridPatentesCliente(listaDeProcessosDeClientes);
                        CarregaGridPatentesColidentes(listaDePatentesColidentes);
                        patenteDeClienteCarregadaPrimeiraVez = true;
                    }
                }
            }

            foreach (IRadicalPatente radicalPatente in listaDeRadicaisASeremRemovidos)
                listaDeRadicaisDeClientes.Remove(radicalPatente);

            if (listaDeRadicaisDeClientes.Count > 0)
            {
                listRadical.ClearEditItems();
                listRadical.ClearSelectedItems();
                listRadical.Items.Clear();
                listRadical.DataSource = listaDeRadicaisDeClientes;
                listRadical.DataBind();
                ViewState.Add(CHAVE_RADICAIS_CLIENTES, listaDeRadicaisDeClientes);
                ViewState.Add(CHAVE_PATENTES_CLIENTES_COM_RADICAL, dicionarioDePatentesDeClientes);
                ViewState.Add(CHAVE_PATENTES_COLIDENTES, dicionarioDePatentesDeColidentes);
            }
            else
            {
                // não existe patentes colidentes
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                 UtilidadesWeb.MostraMensagemDeInformacao("Não existe patentes colidentes."),
                                                 false);

                LimpaRadicais();
            }
        }

        protected void btnRelPublicPropriasAnalitico_OnClick(object sender, ImageClickEventArgs e)
        {
            if (ViewState[CHAVE_PROCESSOS_DA_REVISTA] != null)
            {
                IList<IProcessoDePatente> listaDeProcessos = new List<IProcessoDePatente>();
                IList<IRevistaDePatente> revistaDePatentes = new List<IRevistaDePatente>();
                var numeroDaRevistaSelecionada = ((IRevistaDePatente)ViewState[CHAVE_REVISTA_SELECIONADA]).NumeroRevistaPatente.ToString();

                listaDeProcessos = ((IList<IProcessoDePatente>)ViewState[CHAVE_PROCESSOS_DA_REVISTA]);

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDePatente>())
                    revistaDePatentes = servico.ObtenhaRevistasProcessadas(int.Parse(numeroDaRevistaSelecionada));

                var gerador = new GeradorDeRelatorioDePublicacoesDasPatentes(listaDeProcessos, revistaDePatentes);
                var nomeDoArquivo = gerador.GereRelatorioAnalitico(numeroDaRevistaSelecionada);

                var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" + nomeDoArquivo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);
            }
        }

        protected void btnRelPublicPropriasSintetico_OnClick(object sender, ImageClickEventArgs e)
        {
            if (ViewState[CHAVE_PROCESSOS_DA_REVISTA] != null)
            {
                IList<IProcessoDePatente> listaDeProcessos = new List<IProcessoDePatente>();
                listaDeProcessos = ((IList<IProcessoDePatente>)ViewState[CHAVE_PROCESSOS_DA_REVISTA]);
                IList<IRevistaDePatente> revistaDePatentes = new List<IRevistaDePatente>();

                var numeroDaRevistaSelecionada = ((IRevistaDePatente)ViewState[CHAVE_REVISTA_SELECIONADA]).NumeroRevistaPatente.ToString();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRevistaDePatente>())
                    revistaDePatentes = servico.ObtenhaRevistasProcessadas(int.Parse(numeroDaRevistaSelecionada));

                var gerador = new GeradorDeRelatorioDePublicacoesDasPatentes(listaDeProcessos, revistaDePatentes);
                var nomeDoArquivo = gerador.GereRelatorioSintetico(numeroDaRevistaSelecionada);
                var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" + nomeDoArquivo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);
            }
        }

        protected void btnRelatorioPorClassificacao_OnClick(object sender, ImageClickEventArgs e)
        {
            if (ViewState[CHAVE_RADICAIS_CLIENTES] != null && ViewState[CHAVE_PATENTES_COLIDENTES] != null)
            {
                var dicionarioDeRevistaColidentes = (IDictionary<long, IList<IRevistaDePatente>>)ViewState[CHAVE_PATENTES_COLIDENTES];
                var radicais = ((IList<IRadicalPatente>)ViewState[CHAVE_RADICAIS_CLIENTES]);
                var numeroDaRevistaSelecionada = ((IRevistaDePatente)ViewState[CHAVE_REVISTA_SELECIONADA]).NumeroRevistaPatente.ToString();
                string dataDaPublicacao = ((IRevistaDePatente)ViewState[CHAVE_REVISTA_SELECIONADA]).DataPublicacao.HasValue ?
                    ((IRevistaDePatente)ViewState[CHAVE_REVISTA_SELECIONADA]).DataPublicacao.Value.ToString("dd/MM/yyyy") : string.Empty;
                bool verifiqueSeExisteClassificacoes = false;

                foreach (KeyValuePair<long, IList<IRevistaDePatente>> item in dicionarioDeRevistaColidentes)
                {
                    var classificacoes = radicais.ToList().Find(itemLista => itemLista.IdRadicalPatente.Equals(item.Key));

                    if (classificacoes != null && !string.IsNullOrEmpty(classificacoes.Classificacao))
                    {
                        verifiqueSeExisteClassificacoes = true;
                        break;
                    }

                }

                if (!verifiqueSeExisteClassificacoes)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                         UtilidadesWeb.MostraMensagemDeInformacao("Não existe nenhuma classificação encontrada."),
                                                         false);
                    return;
                }

                var gerador = new GeradorDeRelatorioPorClassificacao(dicionarioDeRevistaColidentes, radicais);
                var nomeDoArquivo = gerador.GereRelatorio(numeroDaRevistaSelecionada, dataDaPublicacao);
                var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" + nomeDoArquivo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);
            }
        }

        protected void btnGerarRelatorioPorColidencia_OnClick(object sender, ImageClickEventArgs e)
        {
            if (ViewState[CHAVE_RADICAIS_CLIENTES] != null && ViewState[CHAVE_PATENTES_COLIDENTES] != null)
            {
                var dicionarioDeRevistaColidentes = (IDictionary<long, IList<IRevistaDePatente>>)ViewState[CHAVE_PATENTES_COLIDENTES];
                var radicais = ((IList<IRadicalPatente>)ViewState[CHAVE_RADICAIS_CLIENTES]);
                var numeroDaRevistaSelecionada = ((IRevistaDePatente)ViewState[CHAVE_REVISTA_SELECIONADA]).NumeroRevistaPatente.ToString();
                string dataDaPublicacao = ((IRevistaDePatente)ViewState[CHAVE_REVISTA_SELECIONADA]).DataPublicacao.HasValue ?
                    ((IRevistaDePatente)ViewState[CHAVE_REVISTA_SELECIONADA]).DataPublicacao.Value.ToString("dd/MM/yyyy") : string.Empty;
                bool verifiqueSeExisteColidencias = false;

                foreach (KeyValuePair<long, IList<IRevistaDePatente>> item in dicionarioDeRevistaColidentes)
                {
                    var radical = radicais.ToList().Find(itemLista => itemLista.IdRadicalPatente.Equals(item.Key));

                    if (radical != null && !string.IsNullOrEmpty(radical.Colidencia))
                    {
                        verifiqueSeExisteColidencias = true;
                        break;
                    }

                }

                if (!verifiqueSeExisteColidencias)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                         UtilidadesWeb.MostraMensagemDeInformacao("Não existe nenhuma colidência encontrada."),
                                                         false);
                    return;
                }

                var gerador = new GeradorDeRelatorioPorColidencia(dicionarioDeRevistaColidentes, radicais);
                var nomeDoArquivo = gerador.GereRelatorio(numeroDaRevistaSelecionada, dataDaPublicacao);
                var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" + nomeDoArquivo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);
            }
        }
    }
}
