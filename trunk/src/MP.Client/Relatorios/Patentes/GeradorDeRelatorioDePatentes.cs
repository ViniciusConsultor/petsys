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
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace MP.Client.Relatorios.Patentes
{
    public class GeradorDeRelatorioDePatentes
    {
        private Document _documento;
        private Font _Fonte1;
        private Font _Fonte2;
        private Font _Fonte3;
        private Font _Fonte4;
        private IEmpresa empresa;
        private IList<IProcessoDePatente> _processosDePatentes;

        public GeradorDeRelatorioDePatentes(IList<IProcessoDePatente> processosPatente)
        {
            _processosDePatentes = processosPatente;
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
            escritor.PageEvent = new GeradorDeRelatorioDePatentes.Ouvinte(_Fonte1, _Fonte2, _Fonte3, _Fonte4, empresa);
            escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE);
            escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE);
            return nomeDoArquivoDeSaida;
        }

        private void MonteDocumentoDoRelatorio()
        {
            var corBackgroudHeader = Color.GRAY;
            var tabela = new PdfPTable(1);

            tabela.WidthPercentage = 100;
            tabela.DefaultCell.Border = Rectangle.NO_BORDER;

            tabela.AddCell(ObtenhaTabelaTituloColuna());

            foreach (IProcessoDePatente processo in _processosDePatentes)
                tabela.AddCell(ObtenhaTabelaDadosDoProcesso(processo));

            _documento.Add(tabela);
        }

        private PdfPTable ObtenhaTabelaTituloColuna()
        {
            var corCelula = Color.LIGHT_GRAY;
            float[] larguraColunas = { 20, 10, 50, 20 };
            var tabelaTitulo = new PdfPTable(larguraColunas);
            tabelaTitulo.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaProcesso = new PdfPCell(new Phrase("Processo", _Fonte2));
            celulaProcesso.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.Border = 0;
            celulaProcesso.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaProcesso);

            var celulaNatureza = new PdfPCell(new Phrase("Natureza", _Fonte2));
            celulaNatureza.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaNatureza.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaNatureza.Border = 0;
            celulaNatureza.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaNatureza);

            var celulaDescricao = new PdfPCell(new Phrase("Descrição", _Fonte2));
            celulaDescricao.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDescricao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDescricao.Border = 0;
            celulaDescricao.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDescricao);

            var celulaConcessao = new PdfPCell(new Phrase("Data Depósito", _Fonte2));
            celulaConcessao.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaConcessao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaConcessao.Border = 0;
            celulaConcessao.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaConcessao);

            return tabelaTitulo;
        }

        private PdfPTable ObtenhaTabelaDadosDoProcesso(IProcessoDePatente processoDePatente)
        {
            float[] larguraColunas = { 20, 10, 50, 20 };
            var tabelaCliente = new PdfPTable(larguraColunas);
            tabelaCliente.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaCliente = new PdfPCell(new Phrase(processoDePatente.NumeroDoProcessoFormatado, _Fonte1));
            celulaCliente.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaCliente.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaCliente.Border = 0;
            tabelaCliente.AddCell(celulaCliente);

            var celulaProcesso = new PdfPCell(new Phrase(processoDePatente.Patente.NaturezaPatente.IdNaturezaPatente.HasValue ?
                processoDePatente.Patente.NaturezaPatente.IdNaturezaPatente.Value.ToString() : "", _Fonte1));
            celulaProcesso.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.Border = 0;
            tabelaCliente.AddCell(celulaProcesso);

            var celulaConcessao = new PdfPCell(new Phrase(ObtenhaReferenciaFormatada(processoDePatente.Patente.TituloPatente), _Fonte1));
            celulaConcessao.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaConcessao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaConcessao.Border = 0;
            tabelaCliente.AddCell(celulaConcessao);

            var celulaDeposito = new PdfPCell(new Phrase(processoDePatente.DataDoDeposito.HasValue && !processoDePatente.DataDoDeposito.Value.Equals(DateTime.MinValue) ?
                                                         processoDePatente.DataDoDeposito.Value.ToString("dd/MM/yyyy") : "", _Fonte1));
            celulaDeposito.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.Border = 0;
            tabelaCliente.AddCell(celulaDeposito);

            return tabelaCliente;
        }

        private string ObtenhaReferenciaFormatada(string referencia)
        {
            string referenciaFormatada = string.Empty;
            string[] referenciaSeparada = referencia.Split(Convert.ToChar(" "));

            if (referenciaSeparada.Length > 3)
            {
                for (int i = 0; i < 3; i++)
                    referenciaFormatada += referenciaSeparada[i] + " ";

                referenciaFormatada = referenciaFormatada.Substring(0, referenciaFormatada.Length - 1) + "...";
            }
            else
            {
                for (int i = 0; i < referenciaSeparada.Length; i++)
                    referenciaFormatada += referenciaSeparada[i] + " ";

                referenciaFormatada = referenciaFormatada.Substring(0, referenciaFormatada.Length - 1);
            }

            return referenciaFormatada;
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

                //Adicionando linha que informa o número da revista 
                var tabelaTitulo = new Table(1);
                tabelaTitulo.Border = 0;
                tabelaTitulo.Width = 100;

                var celulaTitulo = new Cell(new Phrase("Relatório dos Processos de Patentes " + DateTime.Now.ToString("dd/MM/yyyy"), font3));
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