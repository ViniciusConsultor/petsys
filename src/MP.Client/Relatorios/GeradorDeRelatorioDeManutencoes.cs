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
using MP.Client.Relatorios.Patentes;
using MP.Interfaces.Negocio;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace MP.Client.Relatorios
{
    public class GeradorDeRelatorioDeManutencoes
    {
        private Document _documento;
        private Font _Fonte1;
        private Font _Fonte2;
        private Font _Fonte3;
        private Font _Fonte4;
        private IEmpresa empresa;
        private IList<IProcessoDePatente> _processosDePatentes;
        private IList<IProcessoDeMarca> _processosDeMarcas;

        public GeradorDeRelatorioDeManutencoes(IList<IProcessoDePatente> processosDePatentes, IList<IProcessoDeMarca> processosDeMarcas)
        {
            _processosDePatentes = processosDePatentes;
            _processosDeMarcas = processosDeMarcas;
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
            escritor.PageEvent = new Ouvinte(_Fonte1, _Fonte2, _Fonte3, _Fonte4, empresa);
            escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE);
            escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE);
            return nomeDoArquivoDeSaida;
        }

        private void MonteDocumentoDoRelatorio()
        {
            var corBackgroudHeader = Color.GRAY;
            var tabela = new PdfPTable(1);
            var dicionarioDeManutencoes = new Dictionary<long, InformacoesRelatorioDeManutencoes>();
            var dicionarioDePatentes = new Dictionary<long, IList<IProcessoDePatente>>();
            var dicionarioDeMarcas = new Dictionary<long, IList<IProcessoDeMarca>>();
            var listaDeIdsDosClientes = new List<long>();

            tabela.WidthPercentage = 100;
            tabela.DefaultCell.Border = Rectangle.NO_BORDER;

            _processosDePatentes = _processosDePatentes.OrderBy(processo => processo.Patente.Clientes[0].Pessoa.Nome).ToList();
            _processosDeMarcas = _processosDeMarcas.OrderBy(processo => processo.Marca.Cliente.Pessoa.Nome).ToList();

            foreach (IProcessoDePatente processo in _processosDePatentes)
            {
                var cliente = processo.Patente.Clientes[0];

                if (cliente.Pessoa != null && cliente.Pessoa.ID.HasValue && !dicionarioDePatentes.ContainsKey(cliente.Pessoa.ID.Value))
                {
                    dicionarioDePatentes.Add(cliente.Pessoa.ID.Value, new List<IProcessoDePatente>());

                    if(!listaDeIdsDosClientes.Contains(cliente.Pessoa.ID.Value))
                        listaDeIdsDosClientes.Add(cliente.Pessoa.ID.Value);
                }

                if (cliente.Pessoa != null && cliente.Pessoa.ID.HasValue)
                    dicionarioDePatentes[cliente.Pessoa.ID.Value].Add(processo);
            }

            foreach (IProcessoDeMarca processo in _processosDeMarcas)
            {
                var cliente = processo.Marca.Cliente;

                if (cliente.Pessoa != null && cliente.Pessoa.ID.HasValue && !dicionarioDeMarcas.ContainsKey(cliente.Pessoa.ID.Value))
                {
                    dicionarioDeMarcas.Add(cliente.Pessoa.ID.Value, new List<IProcessoDeMarca>());

                    if (!listaDeIdsDosClientes.Contains(cliente.Pessoa.ID.Value))
                        listaDeIdsDosClientes.Add(cliente.Pessoa.ID.Value);
                }

                if (cliente.Pessoa != null && cliente.Pessoa.ID.HasValue)
                    dicionarioDeMarcas[cliente.Pessoa.ID.Value].Add(processo);
            }

            foreach (long identificador in listaDeIdsDosClientes)
            {
                if(!dicionarioDeManutencoes.ContainsKey(identificador))
                {
                    var informacoes = new InformacoesRelatorioDeManutencoes();

                    if(dicionarioDePatentes.ContainsKey(identificador))
                        informacoes.ProcessoDePatentes = dicionarioDePatentes[identificador];

                    if(dicionarioDeMarcas.ContainsKey(identificador))
                        informacoes.ProcessoDeMarcas = dicionarioDeMarcas[identificador];

                    dicionarioDeManutencoes.Add(identificador, informacoes);
                }
            }

            foreach (long chave in dicionarioDeManutencoes.Keys)
            {
                string nomeDoCliente = string.Empty;
                
                if(dicionarioDeManutencoes[chave].ProcessoDeMarcas != null && dicionarioDeManutencoes[chave].ProcessoDeMarcas.Count > 0)
                    nomeDoCliente = dicionarioDeManutencoes[chave].ProcessoDeMarcas[0].Marca.Cliente.Pessoa.Nome;
                else if(dicionarioDeManutencoes[chave].ProcessoDePatentes != null && dicionarioDeManutencoes[chave].ProcessoDePatentes.Count > 0)
                    nomeDoCliente =dicionarioDeManutencoes[chave].ProcessoDePatentes[0].Patente.Clientes[0].Pessoa.Nome;

                var tabela1 = new PdfPTable(1);
                tabela1.DefaultCell.Border = Rectangle.NO_BORDER;

                var celulaCliente = new PdfPCell(new Phrase(nomeDoCliente, _Fonte3));
                celulaCliente.HorizontalAlignment = Cell.ALIGN_LEFT;
                celulaCliente.Border = 0;
                celulaCliente.BackgroundColor = corBackgroudHeader;
                tabela1.AddCell(celulaCliente);
                tabela.AddCell(tabela1);

                if (dicionarioDeManutencoes[chave].ProcessoDePatentes != null)
                {
                    var Patente = new PdfPTable(1);
                    Patente.DefaultCell.Border = Rectangle.NO_BORDER;

                    var celulaPatente = new PdfPCell(new Phrase("Patente(s)", _Fonte3));
                    celulaPatente.HorizontalAlignment = Cell.ALIGN_LEFT;
                    celulaPatente.Border = 0;
                    celulaPatente.BackgroundColor = Color.LIGHT_GRAY;
                    Patente.AddCell(celulaPatente);
                    tabela.AddCell(Patente);

                    tabela.AddCell(ObtenhaTabelaTituloColunaDePatente());

                    foreach (IProcessoDePatente processoDePatente in dicionarioDeManutencoes[chave].ProcessoDePatentes)
                        tabela.AddCell(ObtenhaTabelaDadosDaPatente(processoDePatente));
                }

                if (dicionarioDeManutencoes[chave].ProcessoDeMarcas != null)
                {
                    var Marca = new PdfPTable(1);
                    Marca.DefaultCell.Border = Rectangle.NO_BORDER;

                    var celulaMarca = new PdfPCell(new Phrase("Marca(s)", _Fonte3));
                    celulaMarca.HorizontalAlignment = Cell.ALIGN_LEFT;
                    celulaMarca.Border = 0;
                    celulaMarca.BackgroundColor = Color.LIGHT_GRAY;
                    Marca.AddCell(celulaMarca);
                    tabela.AddCell(Marca);

                    tabela.AddCell(ObtenhaTabelaTituloColunaDeMarca());

                    foreach (IProcessoDeMarca processoDeMarca in dicionarioDeManutencoes[chave].ProcessoDeMarcas)
                        tabela.AddCell(ObtenhaTabelaDadosDaMarca(processoDeMarca));
                }
            }

            _documento.Add(tabela);
        }

        private PdfPTable ObtenhaTabelaTituloColunaDePatente()
        {
            var corCelula = Color.LIGHT_GRAY;
            var tabelaTitulo = new PdfPTable(5);
            tabelaTitulo.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaTipo = new PdfPCell(new Phrase("Tipo", _Fonte2));
            celulaTipo.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaTipo.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaTipo.Border = 0;
            celulaTipo.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaTipo);

            var celulaProcesso = new PdfPCell(new Phrase("Processo", _Fonte2));
            celulaProcesso.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.Border = 0;
            celulaProcesso.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaProcesso);

            var celulaTituloPatente = new PdfPCell(new Phrase("Título da Patente", _Fonte2));
            celulaTituloPatente.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaTituloPatente.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaTituloPatente.Border = 0;
            celulaTituloPatente.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaTituloPatente);

            var celulaDataDoVencimento = new PdfPCell(new Phrase("Data do Vencimento", _Fonte2));
            celulaDataDoVencimento.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDataDoVencimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataDoVencimento.Border = 0;
            celulaDataDoVencimento.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDataDoVencimento);

            var celulaValor = new PdfPCell(new Phrase("Valor", _Fonte2));
            celulaValor.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaValor.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaValor.Border = 0;
            celulaValor.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaValor);

            return tabelaTitulo;
        }

        private PdfPTable ObtenhaTabelaDadosDaPatente(IProcessoDePatente processoDePatente)
        {
            var tabelaCliente = new PdfPTable(5);
            tabelaCliente.DefaultCell.Border = Rectangle.NO_BORDER;
            
            var celulaTipo = new PdfPCell(new Phrase(processoDePatente.DataDaConcessao.HasValue && !processoDePatente.DataDaConcessao.Value.Equals(DateTime.MinValue) ? 
                                                     "PATENTE" : "PEDIDO", _Fonte1));
            celulaTipo.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaTipo.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaTipo.Border = 0;
            tabelaCliente.AddCell(celulaTipo);

            var celulaProcesso = new PdfPCell(new Phrase(processoDePatente.NumeroDoProcessoFormatado, _Fonte1));
            celulaProcesso.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaProcesso.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.Border = 0;
            tabelaCliente.AddCell(celulaProcesso);

            var celulaTituloPatente = new PdfPCell(new Phrase(processoDePatente.Patente.TituloPatente, _Fonte1));
            celulaTituloPatente.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaTituloPatente.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaTituloPatente.Border = 0;
            tabelaCliente.AddCell(celulaTituloPatente);

            var celulaDataDoVencimento = new PdfPCell(new Phrase(processoDePatente.Patente.Manutencao.DataDaProximaManutencao.HasValue ? 
                                                          processoDePatente.Patente.Manutencao.DataDaProximaManutencao.Value.ToString("dd/MM/yyyy") : "", _Fonte1));
            celulaDataDoVencimento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDataDoVencimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataDoVencimento.Border = 0;
            tabelaCliente.AddCell(celulaDataDoVencimento);

            var celulaValor = new PdfPCell(new Phrase(processoDePatente.Patente.Manutencao.ObtenhaValorRealEmEspecie().ToString(), _Fonte1));
            celulaValor.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaValor.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaValor.Border = 0;
            tabelaCliente.AddCell(celulaValor);

            return tabelaCliente;
        }

        private PdfPTable ObtenhaTabelaTituloColunaDeMarca()
        {
            var corCelula = Color.LIGHT_GRAY;
            var tabelaTitulo = new PdfPTable(5);
            tabelaTitulo.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaProcesso = new PdfPCell(new Phrase("Processo", _Fonte2));
            celulaProcesso.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.Border = 0;
            celulaProcesso.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaProcesso);

            var celulaDescricaoDaMarca = new PdfPCell(new Phrase("Marca", _Fonte2));
            celulaDescricaoDaMarca.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDescricaoDaMarca.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDescricaoDaMarca.Border = 0;
            celulaDescricaoDaMarca.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDescricaoDaMarca);

            var celulaDataDoVencimento = new PdfPCell(new Phrase("Data do Vencimento", _Fonte2));
            celulaDataDoVencimento.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDataDoVencimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataDoVencimento.Border = 0;
            celulaDataDoVencimento.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDataDoVencimento);

            var celulaValor = new PdfPCell(new Phrase("Valor", _Fonte2));
            celulaValor.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaValor.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaValor.Border = 0;
            celulaValor.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaValor);

            return tabelaTitulo;
        }

        private PdfPTable ObtenhaTabelaDadosDaMarca(IProcessoDeMarca processoDeMarca)
        {
            var tabelaCliente = new PdfPTable(4);
            tabelaCliente.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaProcesso = new PdfPCell(new Phrase(processoDeMarca.Processo.ToString(), _Fonte1));
            celulaProcesso.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaProcesso.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.Border = 0;
            tabelaCliente.AddCell(celulaProcesso);

            var celulaDescricaoDaMarca = new PdfPCell(new Phrase(processoDeMarca.Marca.DescricaoDaMarca, _Fonte1));
            celulaDescricaoDaMarca.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDescricaoDaMarca.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDescricaoDaMarca.Border = 0;
            tabelaCliente.AddCell(celulaDescricaoDaMarca);

            var celulaDataDoVencimento = new PdfPCell(new Phrase(processoDeMarca.Marca.Manutencao.DataDaProximaManutencao.HasValue ?
                                                          processoDeMarca.Marca.Manutencao.DataDaProximaManutencao.Value.ToString("dd/MM/yyyy") : "", _Fonte1));
            celulaDataDoVencimento.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaDataDoVencimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataDoVencimento.Border = 0;
            tabelaCliente.AddCell(celulaDataDoVencimento);

            var celulaValor = new PdfPCell(new Phrase(processoDeMarca.Marca.Manutencao.ObtenhaValorRealEmEspecie().ToString(), _Fonte1));
            celulaValor.HorizontalAlignment = Cell.ALIGN_LEFT;
            celulaValor.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaValor.Border = 0;
            tabelaCliente.AddCell(celulaValor);

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

                var celulaTitulo = new Cell(new Phrase("Relatório Manutenções " + DateTime.Now.ToString("dd/MM/yyyy"), font3));
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

        private class InformacoesRelatorioDeManutencoes
        {
            public IList<IProcessoDePatente> ProcessoDePatentes { get; set; }
            public IList<IProcessoDeMarca> ProcessoDeMarcas { get; set; }
        }
    }
}