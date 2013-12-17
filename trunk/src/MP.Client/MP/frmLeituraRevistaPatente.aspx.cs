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
        }

        private void CarregueRevistasAProcessar()
        {
            grdRevistasAProcessar.DataSource = new List<IRevistaDePatente>();
        }

        private void CarregueRevistasProcessadas()
        {
            grdRevistasJaProcessadas.DataSource = new List<IRevistaDePatente>();
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
                    }
                }
                catch (BussinesException ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                }
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
        }

        protected void grdRevistasJaProcessadas_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void grdRevistasJaProcessadas_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        protected void gridRevistaProcessos_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void gridRevistaProcessos_ItemCreated(object sender, GridItemEventArgs e)
        {
        }

        protected void gridRevistaProcessos_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
        }

        protected void grdFiltros_ItemCommand(object sender, GridCommandEventArgs e)
        {
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

                    var caminhoArquivo = Path.Combine(pastaDeDestino, revistaDePatentes.NumeroRevistaPatente + arquivo.GetExtension());

                    UtilidadesWeb.CrieDiretorio(pastaDeDestino);
                    ExtrairArquivoZip(arquivo, pastaDeDestino);
                    ExibaRevistasDePatentesAProcessar(listaRevistasAProcessar);
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
        }

        protected void btnLimpar_ButtonClick(object sender, EventArgs e)
        {
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

            CaminhoArquivo = Path.Combine(pastaDeDestino, revistaDePatente.NumeroRevistaPatente + ".xml");
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
                        listaDeProcessos.Add(servico.Obtenha(Convert.ToInt64(processo.NumeroProcessoDaPatente)));

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

                if (arquivoDaPasta.Name.Equals("rm" + revistaDePatente.NumeroRevistaPatente + arquivoDaPasta.Extension))
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
    }
}