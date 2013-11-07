using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Compartilhados;
using Compartilhados.Componentes.Web;
using MP.Interfaces.Negocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.rtf;

namespace MP.Client.Relatorios
{
    public class GeradorDeRelatorioDeProcessosDeMarcas
    {
        private IList<IProcessoDeMarca> _processos;
        private Document _documento;
        private Font _Fonte1;
        private Font _Fonte2;
        private Font _Fonte3;
        private Font _Fonte4;

        public GeradorDeRelatorioDeProcessosDeMarcas(IList<IProcessoDeMarca> processos)
        {
            _processos = processos;
            _Fonte1 = new Font(Font.TIMES_ROMAN, 10);
            _Fonte2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
            _Fonte3 = new Font(Font.TIMES_ROMAN, 14, Font.BOLDITALIC);
            _Fonte4 = new Font(Font.TIMES_ROMAN, 10, Font.BOLDITALIC);
        }

        public string GereRelatorio()
        {
            PdfWriter escritor;
            string caminho;
            string nomeDoArquivoDeSaida;

            nomeDoArquivoDeSaida = String.Concat(DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf");
            caminho = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_LOADS);
            _documento = new Document();
            escritor = PdfWriter.GetInstance(_documento,
                                              new FileStream(Path.Combine(caminho, nomeDoArquivoDeSaida),
                                                             FileMode.Create));
            EscrevaCabecalho();
            EscrevaRodape();
            _documento.Open();
            EscrevaProcessosNoDocumento();
            _documento.Close();
            return nomeDoArquivoDeSaida;
        }

        private void EscrevaCabecalho()
        {
            HeaderFooter cabecalho;
            Phrase frase;

            frase = new Phrase("Processos de marca " + Environment.NewLine, _Fonte3);
            cabecalho = new HeaderFooter(frase, false);
            cabecalho.Border = HeaderFooter.NO_BORDER;
            cabecalho.Alignment = HeaderFooter.ALIGN_RIGHT;
            _documento.Header = cabecalho;
        }

        private void EscrevaRodape()
        {
            HeaderFooter rodape;
            StringBuilder texto = new StringBuilder();

            texto.AppendLine(String.Concat("Impressão em: ", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));

            rodape = new HeaderFooter(new Phrase(texto.ToString(), _Fonte4), false);
            rodape.Border = HeaderFooter.NO_BORDER;
            rodape.Alignment = HeaderFooter.ALIGN_RIGHT;
            _documento.Footer = rodape;
        }

        private void EscrevaProcessosNoDocumento()
        {
            Table tabela = new Table(5);

            tabela.Widths = new Single[] {85, 120, 400, 400, 85};

            tabela.Padding = 1;
            tabela.Spacing = 1;
            tabela.Width = 100;

            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Número do processo", _Fonte2, Cell.ALIGN_CENTER, 13, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Data do cadastro", _Fonte2, Cell.ALIGN_CENTER, 13,true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Marca", _Fonte2, Cell.ALIGN_CENTER, 13, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Cliente", _Fonte2, Cell.ALIGN_CENTER, 13, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Ativo?", _Fonte2, Cell.ALIGN_CENTER, 13, true));
            
            foreach (var processo in _processos)
            {
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Processo.ToString(), _Fonte1, Cell.ALIGN_CENTER,13, false));
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.DataDoCadastro.ToString("dd/MM/yyyy"), _Fonte1, Cell.ALIGN_LEFT, 13, false));
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Marca.DescricaoDaMarca, _Fonte1, Cell.ALIGN_LEFT, 13, false));
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Marca.Cliente.Pessoa.Nome, _Fonte1, Cell.ALIGN_LEFT, 13, false));
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Ativo ? "SIM" : "NÃO", _Fonte1, Cell.ALIGN_CENTER, 13, false));
            }
            
            _documento.Add(tabela);
            Chunk linhaQuantidadeDeItens = new Chunk(String.Concat("Quantidade de processos de marcas : ", _processos.Count), _Fonte4);
            _documento.Add(linhaQuantidadeDeItens);
        }
    }
}