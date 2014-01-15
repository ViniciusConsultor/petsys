using System;
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
            if(!IsPostBack)
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
                var revistasAProcessar = (IList<IRevistaDePatente>) ViewState[CHAVE_REVISTAS_A_PROCESSAR];

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

                        CarreguePaginaInicial();
                        RadPageView1.Selected = true;
                        txtPublicacoesProprias.Text = "0";
                        txtQuantdadeDeProcessos.Text = xmlRevista.GetElementsByTagName("processo").Count.ToString();
                        RadTabStrip1.Tabs[0].SelectParents();
                        RadTabStrip1.Tabs[0].Selected = true;
                        HabilteAbaFiltro(false);
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
                        HabilteAbaFiltro(false);
                    }
                }
                catch (BussinesException ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                }
            }
        }

        protected void grdRevistasJaProcessadas_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdRevistasJaProcessadas_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        protected void gridRevistaProcessos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                id = Convert.ToInt64((e.Item.Cells[3].Text));

            if (e.CommandName == "Modificar")
            {
                var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/cdProcessoDePatente.aspx",
                                            "?Id=", id);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.ExibeJanela(url, "Modificar processo de patente", 800, 550), false);
            }
        }

        protected void gridRevistaProcessos_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void gridRevistaProcessos_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
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
                                                    UtilidadesWeb.ExibeJanela(url, "Detalhes do processo da revista patentes",800, 550), false);
            }
        }

        protected void grdFiltros_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdFiltros_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        protected void uplRevistaPatente_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                if(uplRevistaPatente.UploadedFiles.Count > 0)
                {
                    var arquivo = uplRevistaPatente.UploadedFiles[0];
                    var pastaDeDestino = Server.MapPath(UtilidadesWeb.URL_REVISTA_PATENTE);

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
            if (string.IsNullOrEmpty(txtProcesso.Text) && string.IsNullOrEmpty(txtDepositante.Text) &&
                string.IsNullOrEmpty(txtTitular.Text) && ctrlProcurador.ProcuradorSelecionado == null)
            {
                // Nenhum filtro informado
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                         UtilidadesWeb.MostraMensagemDeInformacao("Informe um filtro para consultar as informações."),
                                                         false);
            }
            else
            {
                IList<IRevistaDePatente> listaDeProcessosDaRevista = new List<IRevistaDePatente>();
                var revistaSelecionada = (IRevistaDePatente)ViewState[CHAVE_REVISTA_SELECIONADA];
                var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroLeituraDeRevistaDePatentes>();

                filtro.NumeroDoProcesso = txtProcesso.Text;
                filtro.Depositante = txtDepositante.Text;
                filtro.Titular = txtTitular.Text;
                filtro.Procurador = ctrlProcurador.ProcuradorSelecionado;

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
            Session.Add(CHAVE_PROCESSOS_REUSLTADO_FILTRO, listaDeProcessosDaRevista);
        }

        protected void btnLimpar_ButtonClick(object sender, EventArgs e)
        {
            txtProcesso.Text = string.Empty;
            txtDepositante.Text = string.Empty;
            txtTitular.Text = string.Empty;
            ctrlProcurador.Inicializa();
        }

        protected override string ObtenhaIdFuncao()
        {
            return "";
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
            var pastaDeDestino = Server.MapPath(UtilidadesWeb.URL_REVISTA_PATENTE);

            UtilidadesWeb.CrieDiretorio(pastaDeDestino);

            var caminhoArquivoTxt = Path.Combine(pastaDeDestino, revistaDePatente.NumeroRevistaPatente + revistaDePatente.ExtensaoArquivo);
            var arquivo = new StreamReader(caminhoArquivoTxt);

            TradutorDeRevistaPatenteTXTParaRevistaPatenteXML.TraduzaRevistaDePatente(DateTime.Now, revistaDePatente.NumeroRevistaPatente.ToString(), arquivo, pastaDeDestino);
        }

        private XmlDocument MontaXmlParaProcessamentoDaRevista(IRevistaDePatente revistaDePatente)
        {
            var pastaDeDestino = Server.MapPath(UtilidadesWeb.URL_REVISTA_PATENTE);

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
                        listaDeProcessos.Add(servico.ObtenhaPeloNumeroDoProcesso(processo.NumeroDoProcesso));

                MostraProcessosDaRevista(listaDeProcessos);
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

            var pastaDeDestinoTemp = Server.MapPath(UtilidadesWeb.URL_REVISTA_PATENTE + "/temp/");

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

                if(!((IRadicalPatente)listRadical.Items[0].DataItem).IdRadicalPatente.HasValue)
                    return;

                var radical = ((IRadicalPatente)listRadical.Items[0].DataItem).IdRadicalPatente.Value;

                IDictionary<long, IList<IRevistaDePatente>> dicionarioDePatentesDeClientes = new Dictionary<long, IList<IRevistaDePatente>>();

                IList<IRevistaDePatente> listaDePatentesDeClientes = new List<IRevistaDePatente>();

                dicionarioDePatentesDeClientes = (IDictionary<long, IList<IRevistaDePatente>>)ViewState[CHAVE_PATENTES_COLIDENTES];

                listaDePatentesDeClientes = dicionarioDePatentesDeClientes[radical];

                CarregaGridPatentesCliente(listaDePatentesDeClientes);

                IDictionary<long, IList<IRevistaDePatente>> dicionarioDePatentesColidentes = new Dictionary<long, IList<IRevistaDePatente>>();

                IList<IRevistaDePatente> listaDePatentesColidentes = new List<IRevistaDePatente>();

                dicionarioDePatentesColidentes = (IDictionary<long, IList<IRevistaDePatente>>)ViewState[CHAVE_PATENTES_COLIDENTES];

                listaDePatentesColidentes = dicionarioDePatentesColidentes[radical];

                CarregaGridPatentesColidentes(listaDePatentesColidentes);
            }
        }

        protected void grdPatenteClientes_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void grdPatenteClientes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        protected void grdPatenteClientes_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdPatentesColidentes_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void grdPatentesColidentes_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdPatentesColidentes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
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
                                                         UtilidadesWeb.MostraMensagemDeInformacao("Não existe revista com títulos das patentes."),
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

            CarregaGridPatentesCliente(new List<IRevistaDePatente>());
            CarregaGridPatentesColidentes(new List<IRevistaDePatente>());

            ViewState[CHAVE_PATENTES_CLIENTES_COM_RADICAL] = null;
            ViewState[CHAVE_PATENTES_COLIDENTES] = null;
            ViewState[CHAVE_RADICAIS_CLIENTES] = null;
        }

        private void CarregaGridPatentesCliente(IList<IRevistaDePatente> listaDeRevistaDeClientes)
        {
            grdPatenteClientes.MasterTableView.DataSource = listaDeRevistaDeClientes;
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
            IDictionary<long, IList<IRevistaDePatente>> dicionarioDePatentesDeClientes = new Dictionary<long, IList<IRevistaDePatente>>();
            IDictionary<long, IList<IRevistaDePatente>> dicionarioDePatentesDeColidentes = new Dictionary<long, IList<IRevistaDePatente>>();

            bool patenteDeClienteCarregadaPrimeiraVez = false;

            foreach (IProcessoDePatente processoDePatente in processoDePatentes)
                foreach (IRadicalPatente radicalPatente in processoDePatente.Patente.Radicais)
                    if(!listaDeRadicaisDeClientes.Contains(radicalPatente))
                        listaDeRadicaisDeClientes.Add(radicalPatente);

            foreach (IRadicalPatente radical in listaDeRadicaisDeClientes)
            {
                IList<IRevistaDePatente> listaDePatentesColidentes = new List<IRevistaDePatente>();
                listaDePatentesColidentes = revistaDePatentes.ToList().FindAll(revista => revista.Titulo.Contains(radical.Colidencia));

                if (radical.IdRadicalPatente.HasValue)
                    dicionarioDePatentesDeColidentes.Add(radical.IdRadicalPatente.Value, listaDePatentesColidentes);

                if (!patenteDeClienteCarregadaPrimeiraVez)
                {
                    CarregaGridPatentesCliente(listaDePatentesColidentes);
                    patenteDeClienteCarregadaPrimeiraVez = true;
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
                ViewState.Add(CHAVE_PATENTES_COLIDENTES, dicionarioDePatentesDeColidentes);
                if(((IRadicalPatente)listRadical.Items[0].DataItem).IdRadicalPatente.HasValue)
                {
                    var idRadical = ((IRadicalPatente)listRadical.Items[0].DataItem).IdRadicalPatente.Value;
                    CarregaGridPatentesColidentes(dicionarioDePatentesDeColidentes[idRadical]);
                }
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
    }
}
