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
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FN.Client.FN.Relatorios
{
    public class GeradorDeRelatorioDeContasAReceber
    {
        private Document _documento;
        private Font _Fonte1;
        private Font _Fonte2;
        private Font _Fonte3;
        private Font _Fonte4;
        private IEmpresa empresa;
        private IList<IItemLancamentoFinanceiroRecebimento> _lancamentosFinanceiros;
        private bool _relatorioDeContasAReceber;

        public GeradorDeRelatorioDeContasAReceber(IList<IItemLancamentoFinanceiroRecebimento> lancamentos, bool relatorioDeContas)
        {
            _lancamentosFinanceiros = lancamentos;
            _Fonte1 = new Font(Font.TIMES_ROMAN, 10);
            _Fonte2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
            _Fonte3 = new Font(Font.TIMES_ROMAN, 14, Font.BOLDITALIC);
            _Fonte4 = new Font(Font.TIMES_ROMAN, 10, Font.BOLDITALIC);
            _relatorioDeContasAReceber = relatorioDeContas;
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
            escritor.PageEvent = new Ouvinte(_Fonte1, _Fonte2, _Fonte3, _Fonte4, empresa, _relatorioDeContasAReceber);
            escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE);
            escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE);
            return nomeDoArquivoDeSaida;
        }

        private void MonteDocumentoDoRelatorio()
        {
            var corBackgroudHeader = Color.GRAY;
            var tabela = new PdfPTable(1);
            var dicionarioDeClienteLancamentos = new Dictionary<long, List<IItemLancamentoFinanceiroRecebimento>>();

            tabela.WidthPercentage = 100;
            tabela.DefaultCell.Border = Rectangle.NO_BORDER;

            foreach (IItemLancamentoFinanceiroRecebimento lancamento in _lancamentosFinanceiros)
            {
                var cliente = lancamento.Cliente;

                if(cliente.Pessoa != null && cliente.Pessoa.ID.HasValue && !dicionarioDeClienteLancamentos.ContainsKey(cliente.Pessoa.ID.Value))
                    dicionarioDeClienteLancamentos.Add(cliente.Pessoa.ID.Value, new List<IItemLancamentoFinanceiroRecebimento>());
                    
                if(cliente.Pessoa != null && cliente.Pessoa.ID.HasValue)
                    dicionarioDeClienteLancamentos[cliente.Pessoa.ID.Value].Add(lancamento);
            }

            foreach (long chave in dicionarioDeClienteLancamentos.Keys)
            {
                string nomeDoCliente = dicionarioDeClienteLancamentos[chave][0].Cliente.Pessoa.Nome;
                var tabela1 = new PdfPTable(1);
                tabela1.DefaultCell.Border = Rectangle.NO_BORDER;
                    
                var celulaCliente = new PdfPCell(new Phrase(nomeDoCliente, _Fonte3));
                celulaCliente.HorizontalAlignment = Cell.ALIGN_LEFT;
                celulaCliente.Border = 0;
                celulaCliente.BackgroundColor = corBackgroudHeader;
                tabela1.AddCell(celulaCliente);
                tabela.AddCell(tabela1);
                tabela.AddCell(ObtenhaTabelaTituloColuna());

                foreach (IItemLancamentoFinanceiroRecebimento lancamentoFinanceiro in dicionarioDeClienteLancamentos[chave])
                    tabela.AddCell(ObtenhaTabelaDadosDosLancamentos(lancamentoFinanceiro)); 
            }

            _documento.Add(tabela);
        }

        private PdfPTable ObtenhaTabelaTituloColuna()
        {
            var corCelula = Color.LIGHT_GRAY;
            var tabelaTitulo = new PdfPTable(8);
            tabelaTitulo.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaDescricao = new PdfPCell(new Phrase("Descrição", _Fonte2));
            celulaDescricao.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDescricao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDescricao.Border = 0;
            celulaDescricao.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDescricao);

            var celulaValor = new PdfPCell(new Phrase("Valor", _Fonte2));
            celulaValor.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaValor.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaValor.Border = 0;
            celulaValor.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaValor);

            var celulaDataLancamento = new PdfPCell(new Phrase("Data do lançamento", _Fonte2));
            celulaDataLancamento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDataLancamento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataLancamento.Border = 0;
            celulaDataLancamento.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDataLancamento);

            var celulaDataVencimento = new PdfPCell(new Phrase("Data do vencimento", _Fonte2));
            celulaDataVencimento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDataVencimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataVencimento.Border = 0;
            celulaDataVencimento.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDataVencimento);

            var celulaDataRecebimento = new PdfPCell(new Phrase("Data do recebimento", _Fonte2));
            celulaDataRecebimento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDataRecebimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataRecebimento.Border = 0;
            celulaDataRecebimento.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDataRecebimento);

            var celulaFormaDeRecebimento = new PdfPCell(new Phrase("Forma de recebimento", _Fonte2));
            celulaFormaDeRecebimento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaFormaDeRecebimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaFormaDeRecebimento.Border = 0;
            celulaFormaDeRecebimento.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaFormaDeRecebimento);

            var celulaSituacao = new PdfPCell(new Phrase("Situação", _Fonte2));
            celulaSituacao.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaSituacao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaSituacao.Border = 0;
            celulaSituacao.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaSituacao);

            var celulaTipoLancamento = new PdfPCell(new Phrase("Tipo de lançamento", _Fonte2));
            celulaTipoLancamento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaTipoLancamento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaTipoLancamento.Border = 0;
            celulaTipoLancamento.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaTipoLancamento);

            return tabelaTitulo;
        }

        private PdfPTable ObtenhaTabelaDadosDosLancamentos(IItemLancamentoFinanceiroRecebimento lancamento)
        {
            var tabelaCliente = new PdfPTable(8);
            tabelaCliente.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaDescricao = new PdfPCell(new Phrase(lancamento.Descricao, _Fonte1));
            celulaDescricao.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDescricao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDescricao.Border = 0;
            tabelaCliente.AddCell(celulaDescricao);

            var celulaValor = new PdfPCell(new Phrase(lancamento.Valor.ToString(), _Fonte1));
            celulaValor.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaValor.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaValor.Border = 0;
            tabelaCliente.AddCell(celulaValor);

            var celulaDataLancamento = new PdfPCell(new Phrase(lancamento.DataDoLancamento.ToString("dd/MM/yyyy"), _Fonte1));
            celulaDataLancamento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDataLancamento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataLancamento.Border = 0;
            tabelaCliente.AddCell(celulaDataLancamento);


            var celulaDataVencimento = new PdfPCell(new Phrase(lancamento.DataDoVencimento.ToString("dd/MM/yyyy"), _Fonte1));
            celulaDataVencimento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDataVencimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataVencimento.Border = 0;
            tabelaCliente.AddCell(celulaDataVencimento);

            string dataDoRecebimento = lancamento.DataDoRecebimento.HasValue ? lancamento.DataDoRecebimento.Value.ToString("dd/MM/yyyy") : "";

            var celulaDataRecebimento = new PdfPCell(new Phrase(dataDoRecebimento, _Fonte1));
            celulaDataRecebimento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDataRecebimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataRecebimento.Border = 0;
            tabelaCliente.AddCell(celulaDataRecebimento);

            var celulaFormaDeRecebimento = new PdfPCell(new Phrase(lancamento.FormaDeRecebimento != null ? lancamento.FormaDeRecebimento.Descricao : "", _Fonte1));
            celulaFormaDeRecebimento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaFormaDeRecebimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaFormaDeRecebimento.Border = 0;
            tabelaCliente.AddCell(celulaFormaDeRecebimento);

            var celulaSituacao = new PdfPCell(new Phrase(lancamento.Situacao.Descricao,  _Fonte1));
            celulaSituacao.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaSituacao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaSituacao.Border = 0;
            tabelaCliente.AddCell(celulaSituacao);

            var celulaTipoLancamento = new PdfPCell(new Phrase(lancamento.TipoLacamento.Descricao,  _Fonte1));
            celulaTipoLancamento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaTipoLancamento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaTipoLancamento.Border = 0;
            tabelaCliente.AddCell(celulaTipoLancamento);

            return tabelaCliente;
        }

        private class Ouvinte : IPdfPageEvent
        {
            private Font font1;
            private Font font2;
            private Font font3;
            private Font font4;
            private IEmpresa empresa;
            private bool relatorioDeContasAReceber;

            public Ouvinte(Font font1, Font font2, Font font3, Font font4, IEmpresa empresa, bool contasAReceber)
            {
                this.font1 = font1;
                this.font2 = font2;
                this.font3 = font3;
                this.font4 = font4;
                this.empresa = empresa;
                this.relatorioDeContasAReceber = contasAReceber;
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

                var celulaTitulo = new Cell(new Phrase((relatorioDeContasAReceber ? "Relatório de Contas a Receber" : "Relatório de Gerenciamento de Itens Financeiros") +
                                                        DateTime.Now.ToString("dd/MM/yyyy"), font3));
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