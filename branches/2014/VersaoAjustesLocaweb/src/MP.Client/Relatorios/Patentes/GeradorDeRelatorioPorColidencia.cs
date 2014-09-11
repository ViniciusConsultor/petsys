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
    public class GeradorDeRelatorioPorColidencia
    {
        private Document _documento;
        private Font _Fonte1;
        private Font _Fonte2;
        private Font _Fonte3;
        private Font _Fonte4;
        private IEmpresa empresa;
        private string _NumeroDaRevistaSelecionada;
        private string _DataPublicacao;
        private IDictionary<long, IList<IRevistaDePatente>> _dicionarioDePatentesDeColidentes;
        private IList<IRadicalPatente> _radicais;

        public GeradorDeRelatorioPorColidencia(IDictionary<long, IList<IRevistaDePatente>> dicionarioDePatentesDeColidentes, IList<IRadicalPatente> radicais)
        {
            _dicionarioDePatentesDeColidentes = dicionarioDePatentesDeColidentes;
            _radicais = radicais;
            _Fonte1 = new Font(Font.TIMES_ROMAN, 10);
            _Fonte2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
            _Fonte3 = new Font(Font.TIMES_ROMAN, 14, Font.BOLDITALIC);
            _Fonte4 = new Font(Font.TIMES_ROMAN, 10, Font.BOLDITALIC);
        }

        public string GereRelatorio(string numeroDaRevistaSelecionada, string dataPublicacao)
        {
            _NumeroDaRevistaSelecionada = numeroDaRevistaSelecionada;
            _DataPublicacao = dataPublicacao;
            var nomeDoArquivoDeSaida = GereInformacoesUteisParaOArquivoDeSaidaDoRelatorio();

            _documento.Open();
            MonteDocumentoDoRelatorio();
            _documento.Close();
            return nomeDoArquivoDeSaida;
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
            escritor.PageEvent = new GeradorDeRelatorioPorColidencia.Ouvinte(_Fonte1, _Fonte2, _Fonte3, _Fonte4, empresa, _NumeroDaRevistaSelecionada, _DataPublicacao);
            escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE);
            escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE);
            return nomeDoArquivoDeSaida;
        }

        private void MonteDocumentoDoRelatorio()
        {
            var corBackgroudHeader = new Color(211, 211, 211);

            var tabela = new PdfPTable(1);
            tabela.WidthPercentage = 100;
            tabela.DefaultCell.Border = Rectangle.NO_BORDER;

            foreach (IRadicalPatente radical in _radicais)
            {
                if(string.IsNullOrEmpty(radical.Colidencia))
                    continue;

                var tabela1 = new PdfPTable(1);

                var celulaColidencia = new PdfPCell(new Phrase(radical.Colidencia, _Fonte3));
                celulaColidencia.HorizontalAlignment = Cell.ALIGN_LEFT;
                celulaColidencia.Border = 0;
                celulaColidencia.BackgroundColor = corBackgroudHeader;
                tabela1.AddCell(celulaColidencia);
                tabela.AddCell(tabela1);

                if(radical.IdRadicalPatente.HasValue && _dicionarioDePatentesDeColidentes.ContainsKey(radical.IdRadicalPatente.Value))
                {
                    IList<IRevistaDePatente> revistaDePatentes = _dicionarioDePatentesDeColidentes[radical.IdRadicalPatente.Value];

                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDePatentes>())
                    {
                        foreach (IRevistaDePatente revistaDePatente in revistaDePatentes)
                        {
                            var despacho = servico.ObtenhaDespachoPeloCodigo(revistaDePatente.CodigoDoDespacho);

                            var tabela2 = new PdfPTable(3);

                            var celulaProcesso = new PdfPCell(new Phrase("Processo: " + revistaDePatente.NumeroDoProcessoFormatado, _Fonte1));
                            celulaProcesso.HorizontalAlignment = Cell.ALIGN_LEFT;
                            celulaProcesso.Border = 0;
                            tabela2.AddCell(celulaProcesso);

                            var celulaDespacho = new PdfPCell(new Phrase("Despacho: " + despacho.Codigo + " - " + despacho.Titulo, _Fonte1));
                            celulaDespacho.HorizontalAlignment = Cell.ALIGN_LEFT;
                            celulaDespacho.Border = 0;
                            tabela2.AddCell(celulaDespacho);

                            var celulaTitular = new PdfPCell(new Phrase("Titular: " + revistaDePatente.Titular + " " + revistaDePatente.PaisTitular + " " +
                                                                            revistaDePatente.UFTitular, _Fonte1));
                            celulaTitular.HorizontalAlignment = Cell.ALIGN_LEFT;
                            celulaTitular.Border = 0;;
                            tabela2.AddCell(celulaTitular);

                            var celulaTitulo = new PdfPCell(new Phrase("Título: " + revistaDePatente.Titulo, _Fonte1));
                            celulaTitulo.Colspan = 3;
                            celulaTitulo.HorizontalAlignment = Cell.ALIGN_LEFT;
                            celulaTitulo.Border = 0;
                            tabela2.AddCell(celulaTitulo);

                            tabela.AddCell(tabela2);
                        }
                    }
                }
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
            private string _dataPublicacao;

            public Ouvinte(Font font1, Font font2, Font font3, Font font4, IEmpresa empresa, string numeroDaRevistaSelecionada, string dataPublicacao)
            {
                this.font1 = font1;
                this.font2 = font2;
                this.font3 = font3;
                this.font4 = font4;
                this.empresa = empresa;
                _numeroDaRevistaSelecionada = numeroDaRevistaSelecionada;
                _dataPublicacao = dataPublicacao;
            }

            public void OnOpenDocument(PdfWriter writer, Document document) { }

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

                var celulaNumeroDaRevista = new Cell(new Phrase("Relatório de Colidências Por Radicais: " + _numeroDaRevistaSelecionada + " " + _dataPublicacao, font3));
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

            public void OnCloseDocument(PdfWriter writer, Document document) { }
            public void OnParagraph(PdfWriter writer, Document document, float paragraphPosition) { }
            public void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition) { }
            public void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title) { }
            public void OnChapterEnd(PdfWriter writer, Document document, float paragraphPosition) { }
            public void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title) { }
            public void OnSectionEnd(PdfWriter writer, Document document, float paragraphPosition) { }
            public void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text) { }
        }
    }
}