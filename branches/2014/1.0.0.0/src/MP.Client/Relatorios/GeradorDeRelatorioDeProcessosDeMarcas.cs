﻿using System;
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
        private IEmpresa empresa;

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

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeEmpresa>())
                empresa = servico.Obtenha(FabricaDeContexto.GetInstancia().GetContextoAtual().EmpresaLogada.ID);

            nomeDoArquivoDeSaida = String.Concat(DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf");
            caminho = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_LOADS);
            _documento = new Document();
            _documento.SetPageSize(PageSize.A4.Rotate());
            escritor = PdfWriter.GetInstance(_documento,
                                             new FileStream(Path.Combine(caminho, nomeDoArquivoDeSaida),
                                                             FileMode.Create));
            escritor.PageEvent = new Ouvinte(_Fonte1, _Fonte2, _Fonte3, _Fonte4, empresa);
            escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE);
            escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE);
            
            _documento.Open();
            EscrevaProcessosNoDocumento();
            _documento.Close();
            return nomeDoArquivoDeSaida;
        }

        private void EscrevaProcessosNoDocumento()
        {
            Table tabela = new Table(9);
            
            tabela.Widths = new Single[] {100, 100, 100, 100, 100, 400, 400, 90, 85};

            tabela.Padding = 1;
            tabela.Spacing = 0;
            tabela.Width = 100;
            tabela.AutoFillEmptyCells = true;

            var corBackgroudHeader = new Color(211, 211, 211);

            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Número do processo", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Data do cadastro", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Data do depósito", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Data de concessão", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Data da vigência", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Marca", _Fonte2, Cell.ALIGN_LEFT, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Cliente", _Fonte2, Cell.ALIGN_LEFT, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Despacho", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Ativo?", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, true));

            tabela.EndHeaders();
            
            foreach (var processo in _processos)
            {
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Processo.ToString(), _Fonte1, Cell.ALIGN_CENTER,0, false));
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.DataDoCadastro.ToString("dd/MM/yyyy"), _Fonte1, Cell.ALIGN_CENTER, 0, false));
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.DataDoDeposito.HasValue ? processo.DataDoDeposito.Value.ToString("dd/MM/yyyy") : "", _Fonte1, Cell.ALIGN_CENTER, 0, false));
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.DataDeConcessao.HasValue ? processo.DataDeConcessao.Value.ToString("dd/MM/yyyy") : "", _Fonte1, Cell.ALIGN_CENTER, 0, false));
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.DataDaVigencia.HasValue ? processo.DataDaVigencia.Value.ToString("dd/MM/yyyy") : "", _Fonte1, Cell.ALIGN_CENTER, 0, false));
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Marca.DescricaoDaMarca, _Fonte1, Cell.ALIGN_LEFT,0 , false));
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Marca.Cliente.Pessoa.Nome, _Fonte1, Cell.ALIGN_LEFT, 0, false));
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Despacho != null ? processo.Despacho.CodigoDespacho.ToString() : "", _Fonte1, Cell.ALIGN_CENTER, 0, false));
                
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Ativo ? "SIM" : "NÃO", _Fonte1, Cell.ALIGN_CENTER, 0, false));
            }
            
            _documento.Add(tabela);
            //  Chunk linhaQuantidadeDeItens = new Chunk(String.Concat("Quantidade de processos de marcas : ", _processos.Count), _Fonte4);
           // _documento.Add(linhaQuantidadeDeItens);
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