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
using MP.Client.Relatorios.Patentes;
using MP.Interfaces.Negocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Color = iTextSharp.text.Color;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;
using Rectangle = iTextSharp.text.Rectangle;

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

            tabela.AddCell(ObtenhaTabelaTitulo());

            foreach (long chave in dicionarioDeManutencoes.Keys)
            {
                string nomeDoCliente = string.Empty;

                if (dicionarioDeManutencoes[chave].ProcessoDeMarcas != null && dicionarioDeManutencoes[chave].ProcessoDeMarcas.Count > 0)
                    nomeDoCliente = dicionarioDeManutencoes[chave].ProcessoDeMarcas[0].Marca.Cliente.Pessoa.Nome;
                else if (dicionarioDeManutencoes[chave].ProcessoDePatentes != null && dicionarioDeManutencoes[chave].ProcessoDePatentes.Count > 0)
                    nomeDoCliente = dicionarioDeManutencoes[chave].ProcessoDePatentes[0].Patente.Clientes[0].Pessoa.Nome;

                if (dicionarioDeManutencoes[chave].ProcessoDePatentes != null)
                    foreach (IProcessoDePatente processoDePatente in dicionarioDeManutencoes[chave].ProcessoDePatentes)
                        tabela.AddCell(ObtenhaTabelaDadosDaPatente(processoDePatente, nomeDoCliente));

                if (dicionarioDeManutencoes[chave].ProcessoDeMarcas != null)
                    foreach (IProcessoDeMarca processoDeMarca in dicionarioDeManutencoes[chave].ProcessoDeMarcas)
                        tabela.AddCell(ObtenhaTabelaDadosDaMarca(processoDeMarca, nomeDoCliente));
            }

            _documento.Add(tabela);
        }

        private PdfPTable ObtenhaTabelaTitulo()
        {
            var corCelula = Color.LIGHT_GRAY;
            float[] larguraColunas = { 30, 15, 25, 10, 10, 10 };
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

            var celulaTituloPatente = new PdfPCell(new Phrase("Referência", _Fonte2));
            celulaTituloPatente.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaTituloPatente.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaTituloPatente.Border = 0;
            celulaTituloPatente.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaTituloPatente);

            var celulaDeposito = new PdfPCell(new Phrase("Depósito", _Fonte2));
            celulaDeposito.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.Border = 0;
            celulaDeposito.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaDeposito);

            var celulaVencimento = new PdfPCell(new Phrase("Vencimento", _Fonte2));
            celulaVencimento.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaVencimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaVencimento.Border = 0;
            celulaVencimento.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaVencimento);

            var celulaValor = new PdfPCell(new Phrase("Valor", _Fonte2));
            celulaValor.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaValor.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaValor.Border = 0;
            celulaValor.BackgroundColor = corCelula;
            tabelaTitulo.AddCell(celulaValor);

            return tabelaTitulo;
        }

        private PdfPTable ObtenhaTabelaDadosDaPatente(IProcessoDePatente processoDePatente, string nomeDoCliente)
        {
            float[] larguraColunas = { 30, 15, 25, 10, 10, 10 };
            var tabelaCliente = new PdfPTable(larguraColunas);
            tabelaCliente.DefaultCell.Border = Rectangle.NO_BORDER;
            
            var celulaCliente = new PdfPCell(new Phrase(nomeDoCliente, _Fonte1));
            celulaCliente.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaCliente.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaCliente.Border = 0;
            tabelaCliente.AddCell(celulaCliente);

            var celulaProcesso = new PdfPCell(new Phrase(processoDePatente.NumeroDoProcessoFormatado, _Fonte1));
            celulaProcesso.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.Border = 0;
            tabelaCliente.AddCell(celulaProcesso);

            var celulaReferencia = new PdfPCell(new Phrase(ObtenhaReferenciaFormatada(processoDePatente.Patente.TituloPatente), _Fonte1));
            celulaReferencia.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaReferencia.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaReferencia.Border = 0;
            tabelaCliente.AddCell(celulaReferencia);

            var celulaDeposito = new PdfPCell(new Phrase(processoDePatente.DataDoDeposito.HasValue ? 
                                                         processoDePatente.DataDoDeposito.Value.ToString("dd/MM/yyyy") : "", _Fonte1));
            celulaDeposito.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.Border = 0;
            tabelaCliente.AddCell(celulaDeposito);

            var celulaDataDoVencimento = new PdfPCell(new Phrase(processoDePatente.Patente.Manutencao.DataDaProximaManutencao.HasValue ?
                                                          processoDePatente.Patente.Manutencao.DataDaProximaManutencao.Value.ToString("dd/MM/yyyy") : "", _Fonte1));
            celulaDataDoVencimento.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDataDoVencimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataDoVencimento.Border = 0;
            tabelaCliente.AddCell(celulaDataDoVencimento);

            var celulaValor = new PdfPCell(new Phrase("R$ " + string.Format("{0:N2}", processoDePatente.Patente.Manutencao.ObtenhaValorRealEmEspecie()), _Fonte1));
            celulaValor.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaValor.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaValor.Border = 0;
            tabelaCliente.AddCell(celulaValor);

            return tabelaCliente;
        }

        private PdfPTable ObtenhaTabelaDadosDaMarca(IProcessoDeMarca processoDeMarca, string nomeDoCliente)
        {
            float[] larguraColunas = { 30, 15, 25, 10, 10, 10 };
            var tabelaCliente = new PdfPTable(larguraColunas);
            tabelaCliente.DefaultCell.Border = Rectangle.NO_BORDER;

            var celulaCliente = new PdfPCell(new Phrase(nomeDoCliente, _Fonte1));
            celulaCliente.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaCliente.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaCliente.Border = 0;
            tabelaCliente.AddCell(celulaCliente);

            var celulaProcesso = new PdfPCell(new Phrase(processoDeMarca.Processo.ToString(), _Fonte1));
            celulaProcesso.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaProcesso.Border = 0;
            tabelaCliente.AddCell(celulaProcesso);

            var celulaDescricaoDaMarca = new PdfPCell(new Phrase(ObtenhaReferenciaFormatada(processoDeMarca.Marca.DescricaoDaMarca), _Fonte1));
            celulaDescricaoDaMarca.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDescricaoDaMarca.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDescricaoDaMarca.Border = 0;
            tabelaCliente.AddCell(celulaDescricaoDaMarca);

            var celulaDeposito = new PdfPCell(new Phrase(processoDeMarca.DataDoDeposito.HasValue ? processoDeMarca.DataDoDeposito.Value.ToString("dd/MM/yyyy") : "", _Fonte1));
            celulaDeposito.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDeposito.Border = 0;
            tabelaCliente.AddCell(celulaDeposito);

            var celulaDataDoVencimento = new PdfPCell(new Phrase(processoDeMarca.Marca.Manutencao.DataDaProximaManutencao.HasValue ?
                                                          processoDeMarca.Marca.Manutencao.DataDaProximaManutencao.Value.ToString("dd/MM/yyyy") : "", _Fonte1));
            celulaDataDoVencimento.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaDataDoVencimento.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaDataDoVencimento.Border = 0;
            tabelaCliente.AddCell(celulaDataDoVencimento);

            var celulaValor = new PdfPCell(new Phrase("R$ " + string.Format("{0:N2}", processoDeMarca.Marca.Manutencao.ObtenhaValorRealEmEspecie()), _Fonte1));
            celulaValor.HorizontalAlignment = Cell.ALIGN_CENTER;
            celulaValor.VerticalAlignment = Cell.ALIGN_CENTER;
            celulaValor.Border = 0;
            tabelaCliente.AddCell(celulaValor);

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

            public void OnOpenDocument(PdfWriter writer, Document document)
            {
                var pessoaJuridica = empresa.Pessoa as IPessoaJuridica;
                Chunk imagem;

                if (!string.IsNullOrEmpty(pessoaJuridica.Logomarca))
                {
                    var imghead = Image.GetInstance(HttpContext.Current.Server.MapPath(pessoaJuridica.Logomarca));
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

            public void OnStartPage(PdfWriter writer, Document document)
            {
                
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