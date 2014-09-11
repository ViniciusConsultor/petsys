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
    public enum OrdenacaoRelatorioGeralPatente
    {
        Cliente,
        Patente
    }

    public class GeradorDeRelatorioGeralDePatentes
    {
        private Document _documento;
        private Font _Fonte1;
        private Font _Fonte2;
        private Font _Fonte3;
        private Font _Fonte4;
        private IEmpresa empresa;
        private IList<IProcessoDePatente> _processosDePatentes;

        public GeradorDeRelatorioGeralDePatentes(IList<IProcessoDePatente> processosDePatentes)
        {
            _processosDePatentes = processosDePatentes;
            _Fonte1 = new Font(Font.TIMES_ROMAN, 10);
            _Fonte2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
            _Fonte3 = new Font(Font.TIMES_ROMAN, 14, Font.BOLDITALIC);
            _Fonte4 = new Font(Font.TIMES_ROMAN, 10, Font.BOLDITALIC);
        }

        public string GereRelatorio(OrdenacaoRelatorioGeralPatente ordenacaoRelatorio)
        {
            var nomeDoArquivoDeSaida = GereInformacoesUteisParaOArquivoDeSaidaDoRelatorio();
            _documento.Open();
            MonteDocumentoDoRelatorio(ordenacaoRelatorio);
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
            escritor.PageEvent = new Ouvinte(_Fonte1, _Fonte2, _Fonte3, _Fonte4, empresa);
            escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE);
            escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE);
            return nomeDoArquivoDeSaida;
        }

        private void MonteDocumentoDoRelatorio(OrdenacaoRelatorioGeralPatente ordenacaoRelatorio)
        {
            var corBackgroudHeader = Color.GRAY;
            var tabela = new PdfPTable(1);

            tabela.WidthPercentage = 100;
            tabela.DefaultCell.Border = Rectangle.NO_BORDER;

            _processosDePatentes = ordenacaoRelatorio.Equals(OrdenacaoRelatorioGeralPatente.Cliente) ? 
                                   _processosDePatentes.OrderBy(processo => processo.Patente.Clientes[0].Pessoa.Nome).ToList() : 
                                   _processosDePatentes.OrderBy(processo => processo.Processo).ToList();

            if (ordenacaoRelatorio.Equals(OrdenacaoRelatorioGeralPatente.Cliente))
            {
                var dicionarioDeClientesEPatentes = new Dictionary<long, List<IProcessoDePatente>>();


                foreach (IProcessoDePatente processo in _processosDePatentes)
                {
                    var cliente = processo.Patente.Clientes[0];

                    if(cliente.Pessoa != null && cliente.Pessoa.ID.HasValue && !dicionarioDeClientesEPatentes.ContainsKey(cliente.Pessoa.ID.Value))
                        dicionarioDeClientesEPatentes.Add(cliente.Pessoa.ID.Value, new List<IProcessoDePatente>());
                    
                    if(cliente.Pessoa != null && cliente.Pessoa.ID.HasValue)
                        dicionarioDeClientesEPatentes[cliente.Pessoa.ID.Value].Add(processo);

                }

                tabela.AddCell(ObtenhaTabelaTituloColuna());

                foreach (long chave in dicionarioDeClientesEPatentes.Keys)
                {
                    string nomeDoCliente = dicionarioDeClientesEPatentes[chave][0].Patente.Clientes[0].Pessoa.Nome;

                    foreach (IProcessoDePatente processoDePatente in dicionarioDeClientesEPatentes[chave])
                        tabela.AddCell(ObtenhaTabelaDadosDoCliente(processoDePatente, nomeDoCliente)); 
                }
            }
            else
            {
                tabela.AddCell(ObtenhaTabelaTituloPatente());
                foreach (IProcessoDePatente processo in _processosDePatentes)
                    tabela.AddCell(ObtenhaTabelaDadosDaPatente(processo));
            }

            _documento.Add(tabela);
        }

        private PdfPTable ObtenhaTabelaTituloColuna()
        {
            var corCelula = Color.LIGHT_GRAY;
            float[] larguraColunas = { 25, 15, 10, 10, 10, 5, 5 };
            var tabelaTitulo = new PdfPTable(larguraColunas);
            tabelaTitulo.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaCliente = new PdfPCell(new Phrase("Cliente", _Fonte2));
            celulaCliente.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaCliente.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaCliente.Border = 0;
            celulaCliente.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaCliente);

            var celulaProcesso = new PdfPCell(new Phrase("Processo", _Fonte2));
            celulaProcesso.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.Border = 0;
            celulaProcesso.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaProcesso);

            var celulaDeposito = new PdfPCell(new Phrase("Depósito", _Fonte2));
            celulaDeposito.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.Border = 0;
            celulaDeposito.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDeposito);

            var celulaConcessao = new PdfPCell(new Phrase("Concessão", _Fonte2));
            celulaConcessao.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaConcessao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaConcessao.Border = 0;
            celulaConcessao.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaConcessao);

            var celulaVigencia = new PdfPCell(new Phrase("Vigência", _Fonte2));
            celulaVigencia.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaVigencia.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaVigencia.Border = 0;
            celulaVigencia.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaVigencia);

            var celulaDespacho = new PdfPCell(new Phrase("Despacho", _Fonte2));
            celulaDespacho.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDespacho.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDespacho.Border = 0;
            celulaDespacho.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDespacho);

            var celulaAtivo = new PdfPCell(new Phrase("Ativo?", _Fonte2));
            celulaAtivo.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaAtivo.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaAtivo.Border = 0;
            celulaAtivo.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaAtivo);

            return tabelaTitulo;
        }

        private PdfPTable ObtenhaTabelaDadosDoCliente(IProcessoDePatente processoDePatente, string nomeDoCliente)
        {
            float[] larguraColunas = { 25, 15, 10, 10, 10, 5, 5 };
            var tabelaCliente = new PdfPTable(larguraColunas);
            tabelaCliente.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaCliente = new PdfPCell(new Phrase(nomeDoCliente, _Fonte1));
            celulaCliente.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaCliente.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaCliente.Border = 0;
            tabelaCliente.AddCell(celulaCliente);

            var celulaProcesso = new PdfPCell(new Phrase(processoDePatente.NumeroDoProcessoFormatado, _Fonte1));
            celulaProcesso.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.Border = 0;
            tabelaCliente.AddCell(celulaProcesso);

            var celulaDeposito = new PdfPCell(new Phrase(processoDePatente.DataDoDeposito.HasValue && !processoDePatente.DataDoDeposito.Value.Equals(DateTime.MinValue) ? 
                                                         processoDePatente.DataDoDeposito.Value.ToString("dd/MM/yyyy") : "", _Fonte1));
            celulaDeposito.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.Border = 0;
            tabelaCliente.AddCell(celulaDeposito);

            var celulaConcessao = new PdfPCell(new Phrase(processoDePatente.DataDaConcessao.HasValue && !processoDePatente.DataDaConcessao.Value.Equals(DateTime.MinValue) ? 
                                                          processoDePatente.DataDaConcessao.Value.ToString("dd/MM/yyyy") : "", _Fonte1));
            celulaConcessao.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaConcessao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaConcessao.Border = 0;
            tabelaCliente.AddCell(celulaConcessao);

            var celulaVigencia = new PdfPCell(new Phrase(processoDePatente.DataDaVigencia.HasValue && !processoDePatente.DataDaVigencia.Value.Equals(DateTime.MinValue) ? 
                                                         processoDePatente.DataDaVigencia.Value.ToString("dd/MM/yyyy") : "", _Fonte1));
            celulaVigencia.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaVigencia.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaVigencia.Border = 0;
            tabelaCliente.AddCell(celulaVigencia);

            var celulaDespacho = new PdfPCell(new Phrase(processoDePatente.Despacho != null  ? processoDePatente.Despacho.Codigo : "", _Fonte1));
            celulaDespacho.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDespacho.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDespacho.Border = 0;
            tabelaCliente.AddCell(celulaDespacho);

            var celulaAtivo = new PdfPCell(new Phrase(processoDePatente.Ativo ? "SIM" : "NÂO", _Fonte1));
            celulaAtivo.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaAtivo.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaAtivo.Border = 0;
            tabelaCliente.AddCell(celulaAtivo);

            return tabelaCliente;
        }

        private PdfPTable ObtenhaTabelaTituloPatente()
        {
            var corCelula = Color.LIGHT_GRAY;
            float[] larguraColunas = { 15, 25, 10, 10, 10, 5, 5 };
            var tabelaTitulo = new PdfPTable(larguraColunas);
            tabelaTitulo.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaPatente = new PdfPCell(new Phrase("Patente", _Fonte2));
            celulaPatente.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaPatente.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaPatente.Border = 0;
            celulaPatente.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaPatente);

            var celulaCliente = new PdfPCell(new Phrase("Cliente", _Fonte2));
            celulaCliente.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaCliente.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaCliente.Border = 0;
            celulaCliente.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaCliente);

            var celulaDeposito = new PdfPCell(new Phrase("Depósito", _Fonte2));
            celulaDeposito.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.Border = 0;
            celulaDeposito.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDeposito);

            var celulaConcessao = new PdfPCell(new Phrase("Concessão", _Fonte2));
            celulaConcessao.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaConcessao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaConcessao.Border = 0;
            celulaConcessao.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaConcessao);

            var celulaVigencia = new PdfPCell(new Phrase("Vigência", _Fonte2));
            celulaVigencia.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaVigencia.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaVigencia.Border = 0;
            celulaVigencia.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaVigencia);

            var celulaDespacho = new PdfPCell(new Phrase("Despacho", _Fonte2));
            celulaDespacho.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDespacho.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDespacho.Border = 0;
            celulaDespacho.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDespacho);

            var celulaExame = new PdfPCell(new Phrase("Ativo?", _Fonte2));
            celulaExame.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaExame.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaExame.Border = 0;
            celulaExame.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaExame);

            return tabelaTitulo;
        }

        private PdfPTable ObtenhaTabelaDadosDaPatente(IProcessoDePatente processoDePatente)
        {
            float[] larguraColunas = { 15, 25, 10, 10, 10, 5, 5 };
            var tabelaCliente = new PdfPTable(larguraColunas);
            tabelaCliente.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaTipo = new PdfPCell(new Phrase(processoDePatente.NumeroDoProcessoFormatado, _Fonte1));
            celulaTipo.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaTipo.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaTipo.Border = 0;
            tabelaCliente.AddCell(celulaTipo);

            var celulaCliente = new PdfPCell(new Phrase(processoDePatente.Patente.Clientes[0].Pessoa.Nome, _Fonte1));
            celulaCliente.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaCliente.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaCliente.Border = 0;
            tabelaCliente.AddCell(celulaCliente);

            var celulaDeposito = new PdfPCell(new Phrase(processoDePatente.DataDoDeposito.HasValue && !processoDePatente.DataDoDeposito.Value.Equals(DateTime.MinValue) ?
                                                         processoDePatente.DataDoDeposito.Value.ToString("dd/MM/yyyy") : "", _Fonte1));
            celulaDeposito.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.Border = 0;
            tabelaCliente.AddCell(celulaDeposito);

            var celulaConcessao = new PdfPCell(new Phrase(processoDePatente.DataDaConcessao.HasValue && !processoDePatente.DataDaConcessao.Value.Equals(DateTime.MinValue) ?
                                                          processoDePatente.DataDaConcessao.Value.ToString("dd/MM/yyyy") : "", _Fonte1));
            celulaConcessao.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaConcessao.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaConcessao.Border = 0;
            tabelaCliente.AddCell(celulaConcessao);

            var celulaVigencia = new PdfPCell(new Phrase(processoDePatente.DataDaVigencia.HasValue && !processoDePatente.DataDaVigencia.Value.Equals(DateTime.MinValue) ?
                                                         processoDePatente.DataDaVigencia.Value.ToString("dd/MM/yyyy") : "", _Fonte1));
            celulaVigencia.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaVigencia.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaVigencia.Border = 0;
            tabelaCliente.AddCell(celulaVigencia);

            var celulaDespacho = new PdfPCell(new Phrase(processoDePatente.Despacho != null ? processoDePatente.Despacho.Codigo : "", _Fonte1));
            celulaDespacho.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDespacho.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDespacho.Border = 0;
            tabelaCliente.AddCell(celulaDespacho);

            var celulaAtivo = new PdfPCell(new Phrase(processoDePatente.Ativo ? "SIM" : "NÂO", _Fonte1));
            celulaAtivo.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaAtivo.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaAtivo.Border = 0;
            tabelaCliente.AddCell(celulaAtivo);

            return tabelaCliente;
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

                var celulaTitulo = new Cell(new Phrase("Relatório Geral de Patentes " + DateTime.Now.ToString("dd/MM/yyyy") , font3));
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