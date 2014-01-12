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

        public GeradorDeRelatorioDeProcessosDeMarcasPublicacoesProprias(IList<IProcessoDeMarca> processos)
        {
            _processos = processos;
            _Fonte1 = new Font(Font.TIMES_ROMAN, 10);
            _Fonte2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
            _Fonte3 = new Font(Font.TIMES_ROMAN, 14, Font.BOLDITALIC);
            _Fonte4 = new Font(Font.TIMES_ROMAN, 10, Font.BOLDITALIC);
        }

        public string GereRelatorioAnalitico()
        {
            var nomeDoArquivoDeSaida = GereInformacoesUteisParaOArquivoDeSaidaDoRelatorio();

            _documento.Open();
            EscrevaProcessosNoDocumentoAnalitico();
            _documento.Close();
            return nomeDoArquivoDeSaida;
        }

        private void EscrevaProcessosNoDocumentoAnalitico()
        {
            var tabela = new Table(1);

            //tabela.Widths = new Single[] { 100, 100, 100, 100, 100, 400, 400, 90, 85 };
            tabela.Widths = new Single[] { 100 };

            tabela.Padding = 1;
            tabela.Spacing = 0;
            tabela.Width = 100;
            tabela.AutoFillEmptyCells = true;

            var corBackgroudHeader = new Color(211, 211, 211);
            
            //tabela.AddCell(iTextSharpUtilidades.CrieCelula("Número do processo", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, false));
            //tabela.AddCell(iTextSharpUtilidades.CrieCelula("Data do cadastro", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, false));
            //tabela.AddCell(iTextSharpUtilidades.CrieCelula("Cliente", _Fonte2, Cell.ALIGN_LEFT, 0, corBackgroudHeader, true));
            //tabela.AddCell(iTextSharpUtilidades.CrieCelula("Marca", _Fonte2, Cell.ALIGN_LEFT, 0, corBackgroudHeader, true));
            //tabela.AddCell(iTextSharpUtilidades.CrieCelula("Despacho", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, true));
            //tabela.AddCell(iTextSharpUtilidades.CrieCelula("Texto do despacho", _Fonte2, Cell.ALIGN_LEFT, 0, corBackgroudHeader, true));
            //tabela.AddCell(iTextSharpUtilidades.CrieCelula("Procurador", _Fonte2, Cell.ALIGN_LEFT, 0, corBackgroudHeader, true));

            //tabela.EndHeaders();

            foreach (var processo in _processos)
            {
                tabela.AddCell(iTextSharpUtilidades.CrieCelula("Número do processo: " + processo.Processo.ToString(), _Fonte1, Cell.ALIGN_LEFT, 0, false));
                tabela.AddCell(iTextSharpUtilidades.CrieCelula("Data do cadastro: " + processo.DataDoCadastro.ToString("dd/MM/yyyy"), _Fonte1, Cell.ALIGN_LEFT, 0, false));
                
                //tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Marca.Cliente.Pessoa.Nome, _Fonte1, Cell.ALIGN_LEFT, 0, false));

                //if (processo.Marca != null && !string.IsNullOrEmpty(processo.Marca.DescricaoDaMarca))
                //    tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Marca.DescricaoDaMarca, _Fonte1, Cell.ALIGN_LEFT, 0, false));
                //else
                //    tabela.AddCell(iTextSharpUtilidades.CrieCelula(string.Empty, _Fonte1, Cell.ALIGN_LEFT, 0, false));

                //tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Despacho != null ? processo.Despacho.CodigoDespacho : "", _Fonte1, Cell.ALIGN_CENTER, 0, false));

                //tabela.AddCell(iTextSharpUtilidades.CrieCelula(!string.IsNullOrEmpty(processo.TextoComplementarDoDespacho) ? processo.TextoComplementarDoDespacho : "", _Fonte1, Cell.ALIGN_LEFT, 0, false));

                //if (processo.Procurador != null && processo.Procurador.Pessoa != null &&
                //    !string.IsNullOrEmpty(processo.Procurador.Pessoa.Nome))
                //    tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Procurador.Pessoa.Nome, _Fonte1, Cell.ALIGN_LEFT, 0, false));
                //else
                //    tabela.AddCell(iTextSharpUtilidades.CrieCelula(string.Empty, _Fonte1, Cell.ALIGN_LEFT, 0, false));



                //tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Processo.ToString(), _Fonte1, Cell.ALIGN_CENTER, 0, false));
                //tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.DataDoCadastro.ToString("dd/MM/yyyy"), _Fonte1, Cell.ALIGN_CENTER, 0, false));
                //tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Marca.Cliente.Pessoa.Nome, _Fonte1, Cell.ALIGN_LEFT, 0, false));

                //if (processo.Marca != null && !string.IsNullOrEmpty(processo.Marca.DescricaoDaMarca))
                //    tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Marca.DescricaoDaMarca, _Fonte1, Cell.ALIGN_LEFT, 0, false));
                //else
                //tabela.AddCell(iTextSharpUtilidades.CrieCelula(string.Empty, _Fonte1, Cell.ALIGN_LEFT, 0, false));

                //tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Despacho != null ? processo.Despacho.CodigoDespacho : "", _Fonte1, Cell.ALIGN_CENTER, 0, false));

                //tabela.AddCell(iTextSharpUtilidades.CrieCelula(!string.IsNullOrEmpty(processo.TextoComplementarDoDespacho) ? processo.TextoComplementarDoDespacho : "", _Fonte1, Cell.ALIGN_LEFT, 0, false));

                //if(processo.Procurador != null && processo.Procurador.Pessoa != null && 
                //    !string.IsNullOrEmpty(processo.Procurador.Pessoa.Nome))
                //    tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Procurador.Pessoa.Nome, _Fonte1, Cell.ALIGN_LEFT, 0, false));
                //else
                //    tabela.AddCell(iTextSharpUtilidades.CrieCelula(string.Empty, _Fonte1, Cell.ALIGN_LEFT, 0, false));
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
            escritor.PageEvent = new Ouvinte(_Fonte1, _Fonte2, _Fonte3, _Fonte4, empresa);
            escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE);
            escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE);
            return nomeDoArquivoDeSaida;
        }

        public string GereRelatorioSintetico()
        {
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
                IPessoaJuridica pessoaJuridica = empresa.Pessoa as IPessoaJuridica;

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
                    dadosEmpresa.Add("Telefone " + telefone.ToString());
                }

                Phrase fraseCabecalho = new Phrase();

                var tabela = new Table(2);
                tabela.Border = 0;
                tabela.Width = 100;

                var cell = new Cell(new Phrase(imagem));
                cell.Border = 0;
                cell.Width = 30;

                tabela.AddCell(cell);

                var cell1 = new Cell(dadosEmpresa);
                cell1.Border = 0;
                cell1.Width = 70;
                tabela.AddCell(cell1);
                fraseCabecalho.Add(tabela);

                var cabecalho = new HeaderFooter(fraseCabecalho, false);
                cabecalho.Border = HeaderFooter.NO_BORDER;
                cabecalho.UseVariableBorders = true;

                document.Header = cabecalho;
            }

            public void OnStartPage(PdfWriter writer, Document document)
            {

            }

            public void OnEndPage(PdfWriter writer, Document document)
            {

                if (document.PageNumber > 1)
                    document.Header = null;

                HeaderFooter rodape;
                StringBuilder texto = new StringBuilder();

                texto.AppendLine(String.Concat("Impressão em: ", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));

                rodape = new HeaderFooter(new Phrase(texto.ToString() + " página :" + document.PageNumber, font4), false);
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