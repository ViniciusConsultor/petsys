using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Servicos;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace MP.Client.Relatorios.Patentes
{
    public class GeradorDeRelatorioDePublicacoesDasPatentes
    {
        private IList<IProcessoDePatente> _processosPatentes;
        private Document _documento;
        private Font _Fonte1;
        private Font _Fonte2;
        private Font _Fonte3;
        private Font _Fonte4;
        private IEmpresa empresa;
        private string _NumeroDaRevistaSelecionada;
        private IList<IRevistaDePatente> _revistasPatentes;

        public GeradorDeRelatorioDePublicacoesDasPatentes(IList<IProcessoDePatente> processosPatentes, IList<IRevistaDePatente> revistasPatentes)
        {
            _processosPatentes = processosPatentes;
            _revistasPatentes = revistasPatentes;
            _Fonte1 = new Font(Font.TIMES_ROMAN, 10);
            _Fonte2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
            _Fonte3 = new Font(Font.TIMES_ROMAN, 14, Font.BOLDITALIC);
            _Fonte4 = new Font(Font.TIMES_ROMAN, 10, Font.BOLDITALIC);
        }

        public string GereRelatorioAnalitico(string numeroDaRevistaSelecionada)
        {
            _NumeroDaRevistaSelecionada = numeroDaRevistaSelecionada;

            var nomeDoArquivoDeSaida = GereInformacoesUteisParaOArquivoDeSaidaDoRelatorio();

            _documento.Open();
            EscrevaProcessosNoDocumentoAnalitico();
            _documento.Close();
            return nomeDoArquivoDeSaida;
        }

        private void EscrevaProcessosNoDocumentoAnalitico()
        {
            var tabela = new Table(1);

            tabela.Widths = new Single[] { 218 };

            tabela.Padding = 0;
            tabela.Spacing = 0;
            tabela.Width = 100;
            tabela.AutoFillEmptyCells = true;

            tabela.EndHeaders();

            foreach (var revistaDePatente in _revistasPatentes)
            {
                var tabela1 = new Table(1);
                tabela1.Widths = new Single[] { 100 };
                tabela1.Padding = 0;
                tabela1.Spacing = 0;
                tabela1.Width = 100;
                tabela1.AutoFillEmptyCells = true;
                tabela1.Border = 0;
                tabela1.EndHeaders();

                var tabelaProcesso = new Cell(ObtenhaTabelaCelulaProcesso(revistaDePatente));
                tabela1.AddCell(tabelaProcesso);

                var tabelaRevistas = new Cell(ObtenhaTabelaInformacoesRevista(revistaDePatente));
                tabela1.AddCell(tabelaRevistas);

                tabela.AddCell(new Cell(tabela1));
            }

            _documento.Add(tabela);
        }

        private string GereInformacoesUteisParaOArquivoDeSaidaDoRelatorio()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeEmpresa>())
                empresa = servico.Obtenha(FabricaDeContexto.GetInstancia().GetContextoAtual().EmpresaLogada.ID);

            var nomeDoArquivoDeSaida = String.Concat(DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf");
            var caminho = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_LOADS);
            _documento = new Document();
            _documento.SetPageSize(PageSize.A4.Rotate());
            var escritor = PdfWriter.GetInstance(_documento, new FileStream(Path.Combine(caminho, nomeDoArquivoDeSaida), FileMode.Create));
            escritor.PageEvent = new GeradorDeRelatorioDePublicacoesDasPatentes.Ouvinte(_Fonte1, _Fonte2, _Fonte3, _Fonte4, empresa, _NumeroDaRevistaSelecionada);
            escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE);
            escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE);
            return nomeDoArquivoDeSaida;
        }

        public string GereRelatorioSintetico(string numeroDaRevistaSelecionada)
        {
            _NumeroDaRevistaSelecionada = numeroDaRevistaSelecionada;

            var nomeDoArquivoDeSaida = GereInformacoesUteisParaOArquivoDeSaidaDoRelatorio();

            _documento.Open();
            EscrevaProcessosNoDocumentoSintetico();
            _documento.Close();
            return nomeDoArquivoDeSaida;
        }

        private void EscrevaProcessosNoDocumentoSintetico()
        {
            var tabela = new Table(4);

            tabela.Widths = new Single[] { 60, 100, 60, 100 };

            tabela.Padding = 1;
            tabela.Spacing = 0;
            tabela.Width = 100;
            tabela.AutoFillEmptyCells = true;

            var corBackgroudHeader = new Color(211, 211, 211);

            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Número do processo", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Patente", _Fonte2, Cell.ALIGN_LEFT, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Despacho", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Cliente", _Fonte2, Cell.ALIGN_LEFT, 0, corBackgroudHeader, true));

            tabela.EndHeaders();

            foreach (var processo in _processosPatentes)
            {
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Processo.ToString(), _Fonte1, Cell.ALIGN_CENTER, 0, false));

                if (processo.Patente != null && !string.IsNullOrEmpty(processo.Patente.TituloPatente))
                    tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Patente.TituloPatente, _Fonte1, Cell.ALIGN_LEFT, 0, false));
                else
                    tabela.AddCell(iTextSharpUtilidades.CrieCelula(string.Empty, _Fonte1, Cell.ALIGN_LEFT, 0, false));

                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Despacho != null ? processo.Despacho.Codigo : "", _Fonte1, Cell.ALIGN_CENTER, 0, false));
                
                string clientes = string.Empty;

                if (processo.Patente != null)
                {
                    clientes = processo.Patente.Clientes.Aggregate(clientes, (current, cliente) => current + (cliente.Pessoa.Nome + " - "));
                    if (!string.IsNullOrEmpty(clientes))
                        clientes = clientes.Substring(0, clientes.Length - 3);
                }

                tabela.AddCell(iTextSharpUtilidades.CrieCelula(clientes, _Fonte1, Cell.ALIGN_LEFT, 0, false));
            }

            _documento.Add(tabela);
        }

        private Cell ObtenhaCelulaVazia()
        {
            var celulaVazia = new Cell(new Phrase("\n", _Fonte1));
            celulaVazia.Colspan = 2;
            celulaVazia.DisableBorderSide(0);
            return celulaVazia;
        }

        private Table ObtenhaTabelaInformacoesRevista(IRevistaDePatente revista)
        {
            var tabela = new Table(1);
            tabela.Widths = new Single[] { 100 };
            tabela.Padding = 0;
            tabela.Spacing = 0;
            tabela.Width = 100;
            tabela.AutoFillEmptyCells = true;
            tabela.Border = 0;
            tabela.EndHeaders();

            if(revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataDeDeposito.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout22.IdentificadorCampo + " " + revista.DataDeDeposito.Value.ToString("dd/MM/yyyy"), _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.NumeroProcessoDaPatente))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout11.IdentificadorCampo + " " + revista.NumeroProcessoDaPatente, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.NumeroDoPedido))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout21.IdentificadorCampo + " " + revista.NumeroDoPedido, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataDaPublicacaoDoPedido.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataDaPublicacaoDoPedido.Value.ToString("dd/MM/yyyy"), _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataDeConcessao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout45.IdentificadorCampo + " " + revista.DataDeConcessao.Value.ToString("dd/MM/yyyy"), _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.PrioridadeUnionista))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.ClassificacaoInternacional))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout51.IdentificadorCampo + " " + revista.ClassificacaoInternacional, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Titulo))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout54.IdentificadorCampo + " " + revista.Titulo, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Resumo))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout57.IdentificadorCampo + " " + revista.Resumo, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.DadosDoPedidoDaPatente))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout61.IdentificadorCampo + " " + revista.DadosDoPedidoDaPatente, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.DadosDoPedidoOriginal))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout62.IdentificadorCampo + " " + revista.DadosDoPedidoOriginal, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.PrioridadeInterna))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout66.IdentificadorCampo + " " + revista.PrioridadeInterna, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Depositante))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout71.IdentificadorCampo + " " + revista.Depositante, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Inventor))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout72.IdentificadorCampo + " " + revista.Inventor, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Titular))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout73.IdentificadorCampo + " " + revista.Titular + " " + revista.UFTitular + " " + revista.PaisTitular, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Procurador))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout74.IdentificadorCampo + " " + revista.Procurador, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.PaisesDesignados))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout81.IdentificadorCampo + " " + revista.PaisesDesignados, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataInicioFaseNacional.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout85.IdentificadorCampo + " " + revista.DataInicioFaseNacional.Value.ToString("dd/MM/yyyy"), _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.DadosDepositoInternacional))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout86.IdentificadorCampo + " " + revista.DadosDepositoInternacional, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.DadosPublicacaoInternacional))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout87.IdentificadorCampo + " " + revista.DadosPublicacaoInternacional, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.CodigoDoDespacho))
            {
                var despacho = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDePatentes>();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDePatentes>())
                    despacho = servico.ObtenhaDespachoPeloCodigo(revista.CodigoDoDespacho, 1);

                if(despacho != null)
                {
                    var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutCD.IdentificadorCampo + " " + despacho.Codigo + " - " + despacho.Titulo, _Fonte1));
                    celula.DisableBorderSide(0);
                    tabela.AddCell(celula);
                }
            }

            if (!string.IsNullOrEmpty(revista.ResponsavelPagamentoImpostoDeRenda))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutRP.IdentificadorCampo + " " + revista.ResponsavelPagamentoImpostoDeRenda, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Complemento))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutCO.IdentificadorCampo + " " + revista.Complemento, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Decisao))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutDE.IdentificadorCampo + " " + revista.Decisao, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Recorrente))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutRE.IdentificadorCampo + " " + revista.Recorrente, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.NumeroDoProcesso))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutNP.IdentificadorCampo + " " + revista.NumeroDoProcesso, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Cedente))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutCE.IdentificadorCampo + " " + revista.Cedente, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Cessionaria))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutCS.IdentificadorCampo + " " + revista.Cessionaria, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.UltimaInformacao))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutUI.IdentificadorCampo + " " + revista.UltimaInformacao, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.CertificadoDeAverbacao))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutCA.IdentificadorCampo + " " + revista.CertificadoDeAverbacao, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.PaisCedente))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutPE.IdentificadorCampo + " " + revista.PaisCedente, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.PaisDaCessionaria))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutPS.IdentificadorCampo + " " + revista.PaisDaCessionaria, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Setor))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutSE.IdentificadorCampo + " " + revista.Setor, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.EnderecoDaCessionaria))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutES.IdentificadorCampo + " " + revista.EnderecoDaCessionaria, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.NaturezaDoDocumento))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutND.IdentificadorCampo + " " + revista.NaturezaDoDocumento, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.MoedaDePagamento))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutMO.IdentificadorCampo + " " + revista.MoedaDePagamento, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Valor))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutVA.IdentificadorCampo + " " + revista.Valor, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Pagamento))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutPG.IdentificadorCampo + " " + revista.Pagamento, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Prazo))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutPZ.IdentificadorCampo + " " + revista.Prazo, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.ServicosIsentosDeAverbacao))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutIA.IdentificadorCampo + " " + revista.ServicosIsentosDeAverbacao, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Criador))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutCR.IdentificadorCampo + " " + revista.Criador, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Linguagem))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutLG.IdentificadorCampo + " " + revista.Linguagem, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.CampoDeAplicacao))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutCP.IdentificadorCampo + " " + revista.CampoDeAplicacao, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.TipoDePrograma))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutTP.IdentificadorCampo + " " + revista.TipoDePrograma, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataDaCriacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutDL.IdentificadorCampo + " " + revista.DataDaCriacao.Value.ToString("dd/MM/yyyy"), _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.RegimeDeGuarda))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutRG.IdentificadorCampo + " " + revista.RegimeDeGuarda, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Requerente))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutRQ.IdentificadorCampo + " " + revista.Requerente, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Redacao))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutRD.IdentificadorCampo + " " + revista.Redacao, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Criador))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutCR.IdentificadorCampo + " " + revista.Criador, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Criador))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutCR.IdentificadorCampo + " " + revista.Criador, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (!string.IsNullOrEmpty(revista.Criador))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.LayoutCR.IdentificadorCampo + " " + revista.Criador, _Fonte1));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            return tabela;
        }

        private Table ObtenhaTabelaCelulaProcesso(IRevistaDePatente revista)
        {
            var tabela = new Table(1);
            tabela.Widths = new Single[] { 100 };
            tabela.Padding = 0;
            tabela.Spacing = 0;
            tabela.Width = 100;
            tabela.AutoFillEmptyCells = true;
            tabela.Border = 0;
            tabela.EndHeaders();

            var celula1 = new Cell(new Phrase("Processo  " + revista.NumeroDoProcesso, _Fonte2));
            celula1.DisableBorderSide(0);
            tabela.AddCell(celula1);

            return tabela;
        }

        private class Ouvinte : IPdfPageEvent
        {
            private Font font1;
            private Font font2;
            private Font font3;
            private Font font4;
            private IEmpresa empresa;
            private string _numeroDaRevistaSelecionada;

            public Ouvinte(Font font1, Font font2, Font font3, Font font4, IEmpresa empresa, string numeroDaRevistaSelecionada)
            {
                this.font1 = font1;
                this.font2 = font2;
                this.font3 = font3;
                this.font4 = font4;
                this.empresa = empresa;
                _numeroDaRevistaSelecionada = numeroDaRevistaSelecionada;
            }

            public void OnOpenDocument(PdfWriter writer, Document document)
            {
            }

            public void OnStartPage(PdfWriter writer, Document document)
            {
                var pessoaJuridica = empresa.Pessoa as IPessoaJuridica;

                Chunk imagem;

                if (!string.IsNullOrEmpty(pessoaJuridica.Logomarca))
                {
                    var imghead = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(pessoaJuridica.Logomarca));
                    imagem = new Chunk(imghead, 0, 0);
                }
                else
                    imagem = new Chunk("");

                var dadosEmpresa = new Phrase();

                dadosEmpresa.Add(pessoaJuridica.NomeFantasia + Environment.NewLine);

                if (pessoaJuridica.Enderecos.Count > 0)
                {
                    var endereco = pessoaJuridica.Enderecos[0];
                    dadosEmpresa.Add(endereco.ToString());
                }


                if (pessoaJuridica.Telefones.Count > 0)
                {
                    var telefone = pessoaJuridica.Telefones[0];
                    dadosEmpresa.Add("Telefone " + telefone);
                }

                var tabelaHeader = new Table(2);
                tabelaHeader.Border = 0;
                tabelaHeader.Width = 100;

                var cell = new Cell(new Phrase(imagem));
                cell.Border = 0;
                cell.Width = 30;

                tabelaHeader.AddCell(cell);

                var cell1 = new Cell(dadosEmpresa);
                cell1.Border = 0;
                cell1.Width = 70;
                tabelaHeader.AddCell(cell1);

                var linhaVazia = new Cell(new Phrase("\n", font2));
                linhaVazia.Colspan = 2;
                linhaVazia.DisableBorderSide(1);

                tabelaHeader.AddCell(linhaVazia);

                document.Add(tabelaHeader);


                 //Adicionando linha que informa o número da revista 
                var tabelaNumeroDaRevista = new Table(1);
                tabelaNumeroDaRevista.Border = 0;
                tabelaNumeroDaRevista.Width = 100;

                var celulaNumeroDaRevista = new Cell(new Phrase("Processos de clientes publicados na revista de patentes: " + _numeroDaRevistaSelecionada, font3));
                celulaNumeroDaRevista.Border = 0;
                celulaNumeroDaRevista.Width = 70;
                celulaNumeroDaRevista.Colspan = 1;

                tabelaNumeroDaRevista.AddCell(celulaNumeroDaRevista);

                var linhaVaziaNumeroRevista = new Cell(new Phrase("\n", font2));
                linhaVaziaNumeroRevista.Border = 0;
                linhaVaziaNumeroRevista.Width = 70;
                linhaVaziaNumeroRevista.Colspan = 1;
                linhaVaziaNumeroRevista.DisableBorderSide(1);

                tabelaNumeroDaRevista.AddCell(linhaVaziaNumeroRevista);

                document.Add(tabelaNumeroDaRevista);
            }

            public void OnEndPage(PdfWriter writer, Document document)
            {
                HeaderFooter rodape;
                StringBuilder texto = new StringBuilder();

                texto.AppendLine(String.Concat("Impressão em: ", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));

                rodape = new HeaderFooter(new Phrase(texto.ToString() + " página :" + (document.PageNumber + 1), font4), false);
                rodape.Border = HeaderFooter.NO_BORDER;
                rodape.Alignment = HeaderFooter.ALIGN_RIGHT;

                document.Footer = rodape;
            }

            public void OnCloseDocument(PdfWriter writer, Document document)
            {

            }

            public void OnParagraph(PdfWriter writer, Document document, float paragraphPosition)
            {

            }

            public void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition)
            {

            }

            public void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title)
            {

            }

            public void OnChapterEnd(PdfWriter writer, Document document, float paragraphPosition)
            {

            }

            public void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title)
            {

            }

            public void OnSectionEnd(PdfWriter writer, Document document, float paragraphPosition)
            {

            }

            public void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text)
            {

            }
        }
    }
}