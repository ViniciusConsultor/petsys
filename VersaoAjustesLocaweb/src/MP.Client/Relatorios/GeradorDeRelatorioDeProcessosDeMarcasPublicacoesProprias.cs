using System;
using System.Collections.Generic;
using System.Drawing;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using Color = iTextSharp.text.Color;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

namespace MP.Client.Relatorios
{
    public class GeradorDeRelatorioDeProcessosDeMarcasPublicacoesProprias
    {
        private IList<IProcessoDeMarca> _processos;
        private Document _documento;
        private Font _Fonte1;
        private Font _Fonte2;
        private Font _Fonte3;
        private Font _Fonte4;
        private IEmpresa empresa;
        private string _NumeroDaRevistaSelecionada;

        public GeradorDeRelatorioDeProcessosDeMarcasPublicacoesProprias(IList<IProcessoDeMarca> processos)
        {
            _processos = processos;
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
            var tabela = new Table(6);

            tabela.Widths = new Single[] { 60, 100, 50, 100, 60, 100 };

            tabela.Padding = 0;
            tabela.Spacing = 0;
            tabela.Width = 100;
            tabela.AutoFillEmptyCells = true;

            tabela.EndHeaders();

            foreach (var processo in _processos)
            {
                var labelNumeroProcesso =
                    new Cell(new Phrase("Número do processo: ", _Fonte2));

                labelNumeroProcesso.DisableBorderSide(0);

                tabela.AddCell(labelNumeroProcesso);

                var valorNumeroProcesso =
                    new Cell(new Phrase(processo.Processo.ToString(), _Fonte1));

                valorNumeroProcesso.DisableBorderSide(0);

                tabela.AddCell(valorNumeroProcesso);

                var labelDataDoCadastro = new Cell(new Phrase("Data do cadastro: ", _Fonte2));

                labelDataDoCadastro.DisableBorderSide(0);

                tabela.AddCell(labelDataDoCadastro);

                var valorDataDoCadastro = new Cell(new Phrase(processo.DataDoCadastro.ToString("dd/MM/yyyy"), _Fonte1));

                valorDataDoCadastro.DisableBorderSide(0);

                tabela.AddCell(valorDataDoCadastro);

                var labelDespacho = new Cell(new Phrase("Despacho: ", _Fonte2));

                labelDespacho.DisableBorderSide(0);

                tabela.AddCell(labelDespacho);

                var valorDespacho = processo.Despacho != null ? new Cell(new Phrase(processo.Despacho.CodigoDespacho, _Fonte1)) :
                   new Cell(new Phrase(string.Empty, _Fonte1));

                valorDespacho.DisableBorderSide(0);

                tabela.AddCell(valorDespacho);


                var labelApresentacao = new Cell(new Phrase("Apresentação: ", _Fonte2));

                labelApresentacao.DisableBorderSide(0);

                tabela.AddCell(labelApresentacao);

                Cell valorApresentacao;

                if(processo.Marca != null && processo.Marca.Apresentacao != null)
                     valorApresentacao = new Cell(new Phrase(processo.Marca.Apresentacao.Nome, _Fonte1));
                else
                     valorApresentacao = new Cell(new Phrase(string.Empty, _Fonte1));

                valorApresentacao.DisableBorderSide(0);

                tabela.AddCell(valorApresentacao);


                var labelNatureza = new Cell(new Phrase("Natureza: ", _Fonte2));

                labelNatureza.DisableBorderSide(0);

                tabela.AddCell(labelNatureza);

                Cell valorNatureza;

                if (processo.Marca != null && processo.Marca.Natureza != null)
                    valorNatureza = new Cell(new Phrase(processo.Marca.Natureza.Nome, _Fonte1));
                else
                    valorNatureza = new Cell(new Phrase(string.Empty, _Fonte1));

                valorNatureza.DisableBorderSide(0);

                tabela.AddCell(valorNatureza);


                var labelNCL = new Cell(new Phrase("NCL: ", _Fonte2));

                labelNCL.DisableBorderSide(0);

                tabela.AddCell(labelNCL);

                Cell valorNCL;

                if (processo.Marca != null && processo.Marca.NCL != null)
                    valorNCL = new Cell(new Phrase(processo.Marca.NCL.Codigo, _Fonte1));
                else
                    valorNCL = new Cell(new Phrase(string.Empty, _Fonte1));

                valorNCL.DisableBorderSide(0);

                tabela.AddCell(valorNCL);

                var labelCliente = new Cell(new Phrase("Cliente: ", _Fonte2));

                labelCliente.DisableBorderSide(0);

                tabela.AddCell(labelCliente);

                var valorCliente = new Cell(new Phrase(processo.Marca.Cliente.Pessoa.Nome, _Fonte1)) { Colspan = 5 };

                valorCliente.DisableBorderSide(0);

                tabela.AddCell(valorCliente);


                var labelMarca = new Cell(new Phrase("Marca: ", _Fonte2));

                labelMarca.DisableBorderSide(0);

                tabela.AddCell(labelMarca);

                Cell valorMarca;

                if (processo.Marca != null && !string.IsNullOrEmpty(processo.Marca.DescricaoDaMarca))
                    valorMarca = new Cell(new Phrase(processo.Marca.DescricaoDaMarca, _Fonte1));
                else
                    valorMarca = new Cell(new Phrase(string.Empty, _Fonte1));

                valorMarca.Colspan = 5;
                valorMarca.DisableBorderSide(0);

                tabela.AddCell(valorMarca);



                var labelApostila = new Cell(new Phrase("Apostila: ", _Fonte2));

                labelApostila.DisableBorderSide(0);

                tabela.AddCell(labelApostila);

                Cell valorApostila;

                if (!string.IsNullOrEmpty(processo.Apostila))
                    valorApostila = new Cell(new Phrase(processo.Apostila, _Fonte1));
                else
                    valorApostila = new Cell(new Phrase(string.Empty, _Fonte1));

                valorApostila.Colspan = 5;
                valorApostila.DisableBorderSide(0);

                tabela.AddCell(valorApostila);


                var labelTextoDespacho = new Cell(new Phrase("Texto do Despacho: ", _Fonte2));

                labelTextoDespacho.DisableBorderSide(0);

                tabela.AddCell(labelTextoDespacho);

                Cell valorTextoDespacho;

                if (!string.IsNullOrEmpty(processo.TextoComplementarDoDespacho))
                    valorTextoDespacho = new Cell(new Phrase(processo.Apostila, _Fonte1));
                else
                    valorTextoDespacho = new Cell(new Phrase(string.Empty, _Fonte1));

                valorTextoDespacho.Colspan = 5;
                valorTextoDespacho.DisableBorderSide(0);

                tabela.AddCell(valorTextoDespacho);


                var labelProcurador = new Cell(new Phrase("Procurador: ", _Fonte2));

                labelProcurador.DisableBorderSide(0);

                tabela.AddCell(labelProcurador);

                Cell valorProcurador;

                if (processo.Procurador != null && processo.Procurador.Pessoa != null &&
                    !string.IsNullOrEmpty(processo.Procurador.Pessoa.Nome))
                    valorProcurador = new Cell(new Phrase(processo.Procurador.Pessoa.Nome, _Fonte1));
                else
                    valorProcurador = new Cell(new Phrase(string.Empty, _Fonte1));

                valorProcurador.Colspan = 5;
                valorProcurador.DisableBorderSide(0);

                tabela.AddCell(valorProcurador);


                var linhaVazia = new Cell(new Phrase("\n", _Fonte1));
                linhaVazia.Colspan = 6;
                linhaVazia.DisableBorderSide(1);

                tabela.AddCell(linhaVazia);
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
            var escritor = PdfWriter.GetInstance(_documento,
                                                       new FileStream(Path.Combine(caminho, nomeDoArquivoDeSaida),
                                                                      FileMode.Create));
            escritor.PageEvent = new Ouvinte(_Fonte1, _Fonte2, _Fonte3, _Fonte4, empresa, _NumeroDaRevistaSelecionada);
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
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Marca", _Fonte2, Cell.ALIGN_LEFT, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Despacho", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Cliente", _Fonte2, Cell.ALIGN_LEFT, 0, corBackgroudHeader, true));

            tabela.EndHeaders();

            foreach (var processo in _processos)
            {
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Processo.ToString(), _Fonte1, Cell.ALIGN_CENTER, 0, false));

                if (processo.Marca != null && !string.IsNullOrEmpty(processo.Marca.DescricaoDaMarca))
                    tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Marca.DescricaoDaMarca, _Fonte1, Cell.ALIGN_LEFT, 0, false));
                else
                    tabela.AddCell(iTextSharpUtilidades.CrieCelula(string.Empty, _Fonte1, Cell.ALIGN_LEFT, 0, false));

                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Despacho != null ? processo.Despacho.CodigoDespacho : "", _Fonte1, Cell.ALIGN_CENTER, 0, false));

                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Marca.Cliente.Pessoa.Nome, _Fonte1, Cell.ALIGN_LEFT, 0, false));
            }

            _documento.Add(tabela);
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


                // Adicionando linha que informa o número da revista 

                var tabelaNumeroDaRevista = new Table(1);
                tabelaNumeroDaRevista.Border = 0;
                tabelaNumeroDaRevista.Width = 100;

                var celulaNumeroDaRevista = new Cell(new Phrase("Processos de clientes publicados na revista de marcas: " + _numeroDaRevistaSelecionada, font3));
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
                //if (document.PageNumber > 1)
                //    document.Header = null;

                HeaderFooter rodape;
                StringBuilder texto = new StringBuilder();

                texto.AppendLine(String.Concat("Impressão em: ", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));

                rodape = new HeaderFooter(new Phrase(texto.ToString() + " página :" + (document.PageNumber + 1 ), font4), false);
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