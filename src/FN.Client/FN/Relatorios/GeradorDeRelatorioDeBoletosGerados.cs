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
using Compartilhados.Interfaces.FN.Negocio;
using FN.Interfaces.Negocio;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FN.Client.FN.Relatorios
{
    public class GeradorDeRelatorioDeBoletosGerados
    {
        private Document _documento;
        private Font _Fonte1;
        private Font _Fonte2;
        private Font _Fonte3;
        private Font _Fonte4;
        private IEmpresa empresa;
        private IList<IBoletosGerados> _boletosGerados;

        public GeradorDeRelatorioDeBoletosGerados(IList<IBoletosGerados> boletosGerados)
        {
            _boletosGerados = boletosGerados;
            _Fonte1 = new Font(Font.TIMES_ROMAN, 10);
            _Fonte2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
            _Fonte3 = new Font(Font.TIMES_ROMAN, 14, Font.BOLDITALIC);
            _Fonte4 = new Font(Font.TIMES_ROMAN, 10, Font.BOLDITALIC);
        }

        public string GereRelatorio()
        {
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
            escritor.PageEvent = new GeradorDeRelatorioDeBoletosGerados.Ouvinte(_Fonte1, _Fonte2, _Fonte3, _Fonte4, empresa);
            escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE);
            escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE);
            return nomeDoArquivoDeSaida;
        }

        private void MonteDocumentoDoRelatorio()
        {
            var corBackgroudHeader = Color.GRAY;
            var tabela = new PdfPTable(1);
            var dicionarioDeBoletosGerados = new Dictionary<long, List<IBoletosGerados>>();

            tabela.WidthPercentage = 100;
            tabela.DefaultCell.Border = Rectangle.NO_BORDER;

            foreach (IBoletosGerados boletoGerado in _boletosGerados)
            {
                var cliente = boletoGerado.Cliente;

                if(cliente.Pessoa != null && cliente.Pessoa.ID.HasValue && !dicionarioDeBoletosGerados.ContainsKey(cliente.Pessoa.ID.Value))
                    dicionarioDeBoletosGerados.Add(cliente.Pessoa.ID.Value, new List<IBoletosGerados>());
                    
                if(cliente.Pessoa != null && cliente.Pessoa.ID.HasValue)
                    dicionarioDeBoletosGerados[cliente.Pessoa.ID.Value].Add(boletoGerado);
            }

            foreach (long chave in dicionarioDeBoletosGerados.Keys)
            {
                string nomeDoCliente = dicionarioDeBoletosGerados[chave][0].Cliente.Pessoa.Nome;
                var tabela1 = new PdfPTable(1);
                tabela1.DefaultCell.Border = Rectangle.NO_BORDER;
                    
                var celulaCliente = new PdfPCell(new Phrase(nomeDoCliente, _Fonte3));
                celulaCliente.HorizontalAlignment = Cell.ALIGN_LEFT;
                celulaCliente.Border = 0;
                celulaCliente.BackgroundColor = corBackgroudHeader;
                tabela1.AddCell(celulaCliente);
                tabela.AddCell(tabela1);
                tabela.AddCell(ObtenhaTabelaTituloColuna());

                foreach (IBoletosGerados boletoGerado in dicionarioDeBoletosGerados[chave])
                    tabela.AddCell(ObtenhaTabelaDadosDosLancamentos(boletoGerado)); 
            }

            _documento.Add(tabela);
        }

        private PdfPTable ObtenhaTabelaTituloColuna()
        {
            var corCelula = Color.LIGHT_GRAY;
            var tabelaTitulo = new PdfPTable(7);
            tabelaTitulo.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaCedente = new PdfPCell(new Phrase("Cedente", _Fonte2));
            celulaCedente.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaCedente.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaCedente.Border = 0;
            celulaCedente.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaCedente);

            var celulaNumeroBoleto = new PdfPCell(new Phrase("Num. Boleto ", _Fonte2));
            celulaNumeroBoleto.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaNumeroBoleto.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaNumeroBoleto.Border = 0;
            celulaNumeroBoleto.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaNumeroBoleto);

            var celulaNossoNumero = new PdfPCell(new Phrase("Nosso Número", _Fonte2));
            celulaNossoNumero.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaNossoNumero.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaNossoNumero.Border = 0;
            celulaNossoNumero.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaNossoNumero);

            var celulaValor = new PdfPCell(new Phrase("Valor R$", _Fonte2));
            celulaValor.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaValor.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaValor.Border = 0;
            celulaValor.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaValor);

            var celulaDataGeracao = new PdfPCell(new Phrase("Data Geração", _Fonte2));
            celulaDataGeracao.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDataGeracao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataGeracao.Border = 0;
            celulaDataGeracao.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDataGeracao);

            var celulaDataVencimento = new PdfPCell(new Phrase("Data Vencimento", _Fonte2));
            celulaDataVencimento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDataVencimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataVencimento.Border = 0;
            celulaDataVencimento.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDataVencimento);

            var celulaObservacao = new PdfPCell(new Phrase("Observação", _Fonte2));
            celulaObservacao.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaObservacao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaObservacao.Border = 0;
            celulaObservacao.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaObservacao);

            return tabelaTitulo;
        }

        private PdfPTable ObtenhaTabelaDadosDosLancamentos(IBoletosGerados boletoGerado)
        {
            var tabelaBoleto = new PdfPTable(7);
            tabelaBoleto.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaCedente = new PdfPCell(new Phrase(boletoGerado.Cedente.Pessoa.Nome, _Fonte1));
            celulaCedente.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaCedente.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaCedente.Border = 0;
            tabelaBoleto.AddCell(celulaCedente);

            var celulaNumeroDoBoleto = new PdfPCell(new Phrase(boletoGerado.NumeroBoleto, _Fonte1));
            celulaNumeroDoBoleto.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaNumeroDoBoleto.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaNumeroDoBoleto.Border = 0;
            tabelaBoleto.AddCell(celulaNumeroDoBoleto);

            var celulaNossoNumero = new PdfPCell(new Phrase(boletoGerado.NossoNumero.HasValue ? boletoGerado.NossoNumero.Value.ToString() : "", _Fonte1));
            celulaNossoNumero.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaNossoNumero.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaNossoNumero.Border = 0;
            tabelaBoleto.AddCell(celulaNossoNumero);

            var celulaValor = new PdfPCell(new Phrase(boletoGerado.Valor.ToString(), _Fonte1));
            celulaValor.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaValor.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaValor.Border = 0;
            tabelaBoleto.AddCell(celulaValor);

            string dataGeracao = boletoGerado.DataGeracao.HasValue ? boletoGerado.DataGeracao.Value.ToString("dd/MM/yyyy") : "";

            var celulaDataRecebimento = new PdfPCell(new Phrase(dataGeracao, _Fonte1));
            celulaDataRecebimento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDataRecebimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataRecebimento.Border = 0;
            tabelaBoleto.AddCell(celulaDataRecebimento);

            string dataVencimento = boletoGerado.DataVencimento.HasValue ? boletoGerado.DataVencimento.Value.ToString("dd/MM/yyyy") : "";

            var celulaFormaDeRecebimento = new PdfPCell(new Phrase(dataVencimento, _Fonte1));
            celulaFormaDeRecebimento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaFormaDeRecebimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaFormaDeRecebimento.Border = 0;
            tabelaBoleto.AddCell(celulaFormaDeRecebimento);

            var celulaObservacao = new PdfPCell(new Phrase(boletoGerado.Observacao,  _Fonte1));
            celulaObservacao.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaObservacao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaObservacao.Border = 0;
            tabelaBoleto.AddCell(celulaObservacao);

            return tabelaBoleto;
        }

        private class Ouvinte : IPdfPageEvent
        {
            private Font font1;
            private Font font2;
            private Font font3;
            private Font font4;
            private IEmpresa empresa;

            public Ouvinte(Font font1, Font font2, Font font3, Font font4, IEmpresa empresa)
            {
                this.font1 = font1;
                this.font2 = font2;
                this.font3 = font3;
                this.font4 = font4;
                this.empresa = empresa;
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

                var tabelaTitulo = new Table(1);
                tabelaTitulo.Border = 0;
                tabelaTitulo.Width = 100;

                var celulaTitulo = new Cell(new Phrase("Relatório de Boletos Gerados " + DateTime.Now.ToString("dd/MM/yyyy") , font3));
                celulaTitulo.Border = 0;
                celulaTitulo.Width = 70;
                celulaTitulo.Colspan = 1;

                tabelaTitulo.AddCell(celulaTitulo);

                var linhaVaziaNumeroRevista = new Cell(new Phrase("\n", font2));
                linhaVaziaNumeroRevista.Border = 0;
                linhaVaziaNumeroRevista.Width = 70;
                linhaVaziaNumeroRevista.Colspan = 1;
                linhaVaziaNumeroRevista.DisableBorderSide(1);

                tabelaTitulo.AddCell(linhaVaziaNumeroRevista);
                document.Add(tabelaTitulo);
            }

            public void OnEndPage(PdfWriter writer, Document document)
            {
                HeaderFooter rodape;
                var texto = new StringBuilder();

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