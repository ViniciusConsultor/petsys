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
    public class GeradorDeRelatorioDePublicacoesDasPatentes
    {
        private IList<IProcessoDePatente> _processosPatentes;
        private Document _documento;
        private Font _Fonte1;
        private Font _Fonte2;
        private Font _Fonte3;
        private Font _Fonte4;
        private IEmpresa empresa;
        private string _NumeroDaRevistaSelecionada;
        private IList<IRevistaDePatente> _revistasPatentes;

        public GeradorDeRelatorioDePublicacoesDasPatentes(IList<IProcessoDePatente> processosPatentes, IList<IRevistaDePatente> revistasPatentes)
        {
            _processosPatentes = processosPatentes;
            _revistasPatentes = revistasPatentes;
            _Fonte1 = new Font(Font.TIMES_ROMAN, 10);
            _Fonte2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
            _Fonte3 = new Font(Font.TIMES_ROMAN, 14, Font.BOLDITALIC);
            _Fonte4 = new Font(Font.TIMES_ROMAN, 10, Font.BOLDITALIC);
        }

        public string GereRelatorioAnalitico(string numeroDaRevistaSelecionada)
        {
            _NumeroDaRevistaSelecionada = numeroDaRevistaSelecionada;

            var nomeDoArquivoDeSaida = GereInformacoesUteisParaOArquivoDeSaidaDoRelatorio();

            _documento.Open();
            EscrevaProcessosNoDocumentoAnalitico();
            _documento.Close();
            return nomeDoArquivoDeSaida;
        }

        //private void EscrevaProcessosNoDocumentoAnalitico()
        //{
        //    var tabela = new Table(1);

        //    tabela.Widths = new Single[] { 218 };

        //    tabela.Padding = 0;
        //    tabela.Spacing = 0;
        //    tabela.Width = 100;
        //    tabela.AutoFillEmptyCells = true;

        //    tabela.EndHeaders();

        //    foreach (var processo in _processosPatentes)
        //    {
        //        var tabela1 = new Table(6);
        //        tabela1.Widths = new Single[] { 29, 50, 24, 50, 15, 50 };
        //        tabela1.Padding = 0;
        //        tabela1.Spacing = 0;
        //        tabela1.Width = 100;
        //        tabela1.AutoFillEmptyCells = true;
        //        tabela1.Border = 0;
        //        tabela1.EndHeaders();

        //        var labelNumeroProcesso = new Cell(new Phrase("Número do processo: ", _Fonte2));
        //        labelNumeroProcesso.DisableBorderSide(0);
        //        tabela1.AddCell(labelNumeroProcesso);

        //        var valorNumeroProcesso = new Cell(new Phrase(processo.Processo.ToString(), _Fonte1));
        //        valorNumeroProcesso.DisableBorderSide(0);
        //        tabela1.AddCell(valorNumeroProcesso);

        //        var labelDataDoCadastro = new Cell(new Phrase("Data do cadastro: ", _Fonte2));
        //        labelDataDoCadastro.DisableBorderSide(0);
        //        tabela1.AddCell(labelDataDoCadastro);

        //        var valorDataDoCadastro = new Cell(new Phrase(processo.DataDoCadastro.ToString("dd/MM/yyyy"), _Fonte1));
        //        valorDataDoCadastro.DisableBorderSide(0);
        //        tabela1.AddCell(valorDataDoCadastro);

        //        var labelDespacho = new Cell(new Phrase("Despacho: ", _Fonte2));
        //        labelDespacho.DisableBorderSide(0);
        //        tabela1.AddCell(labelDespacho);

        //        var valorDespacho = processo.Despacho != null ? new Cell(new Phrase(processo.Despacho.Codigo, _Fonte1)) : new Cell(new Phrase(string.Empty, _Fonte1));
        //        valorDespacho.DisableBorderSide(0);
        //        tabela1.AddCell(valorDespacho);

        //        var tabela2 = new Table(4);
        //        tabela2.Widths = new Single[] { 5, 19, 4, 31 };
        //        tabela2.Padding = 0;
        //        tabela2.Spacing = 0;
        //        tabela2.Width = 100;
        //        tabela2.AutoFillEmptyCells = true;
        //        tabela2.Border = 0;
        //        tabela2.EndHeaders();

        //        var labelClassificacao = new Cell(new Phrase("Classificação: ", _Fonte2));
        //        labelClassificacao.DisableBorderSide(0);
        //        tabela2.AddCell(labelClassificacao);

        //        string classificacoes = string.Empty;

        //        if (processo.Patente != null)
        //        {
        //            classificacoes = processo.Patente.Classificacoes.Aggregate(classificacoes, (current, classificacao) => current + (classificacao.Classificacao + " - "));

        //            if (!string.IsNullOrEmpty(classificacoes))
        //                classificacoes = classificacoes.Substring(0, classificacoes.Length - 3);
        //        }

        //        Cell valorClassificacao = new Cell(new Phrase(classificacoes, _Fonte1));
        //        valorClassificacao.DisableBorderSide(0);
        //        tabela2.AddCell(valorClassificacao);

        //        var labelNatureza = new Cell(new Phrase("Natureza: ", _Fonte2));
        //        labelNatureza.DisableBorderSide(0);
        //        tabela2.AddCell(labelNatureza);

        //        Cell valorNatureza = processo.Patente != null && processo.Patente.NaturezaPatente != null ?
        //                new Cell(new Phrase(processo.Patente.NaturezaPatente.SiglaNatureza, _Fonte1)) : new Cell(new Phrase(string.Empty, _Fonte1));
        //        valorNatureza.DisableBorderSide(0);
        //        tabela2.AddCell(valorNatureza);

        //        var tabela3 = new Table(2);
        //        tabela3.Widths = new Single[] { 30, 330 };
        //        tabela3.Padding = 0;
        //        tabela3.Spacing = 0;
        //        tabela3.Width = 100;
        //        tabela3.AutoFillEmptyCells = true;
        //        tabela3.Border = 0;
        //        tabela3.EndHeaders();

        //        var labelCliente = new Cell(new Phrase("Cliente: ", _Fonte2));
        //        labelCliente.DisableBorderSide(0);
        //        tabela3.AddCell(labelCliente);

        //        string clientes = string.Empty;

        //        if (processo.Patente != null)
        //        {
        //            clientes = processo.Patente.Clientes.Aggregate(clientes, (current, cliente) => current + (cliente.Pessoa.Nome + " - "));

        //            if (!string
        //                .IsNullOrEmpty(clientes))
        //                clientes = clientes.Substring(0, clientes.Length - 3);
        //        }

        //        var valorCliente = new Cell(new Phrase(clientes, _Fonte1));
        //        valorCliente.DisableBorderSide(0);
        //        tabela3.AddCell(valorCliente);

        //        var labelPatente = new Cell(new Phrase("Patente: ", _Fonte2));
        //        labelPatente.DisableBorderSide(0);
        //        tabela3.AddCell(labelPatente);

        //        Cell valorPatente = processo.Patente != null && !string.IsNullOrEmpty(processo.Patente.TituloPatente) ?
        //            new Cell(new Phrase(processo.Patente.TituloPatente, _Fonte1)) : new Cell(new Phrase(string.Empty, _Fonte1));
        //        valorPatente.DisableBorderSide(0);
        //        tabela3.AddCell(valorPatente);

        //        var labelProcurador = new Cell(new Phrase("Procurador: ", _Fonte2));
        //        labelProcurador.DisableBorderSide(0);
        //        tabela3.AddCell(labelProcurador);

        //        Cell valorProcurador = processo.Procurador != null && processo.Procurador.Pessoa != null && !string.IsNullOrEmpty(processo.Procurador.Pessoa.Nome) ?
        //            new Cell(new Phrase(processo.Procurador.Pessoa.Nome, _Fonte1)) : new Cell(new Phrase(string.Empty, _Fonte1));
        //        valorProcurador.Colspan = 1;
        //        valorProcurador.DisableBorderSide(0);
        //        tabela3.AddCell(valorProcurador);

        //        tabela3.AddCell(ObtenhaCelulaVazia());
        //        tabela3.AddCell(ObtenhaCelulaVazia());

        //        tabela.AddCell(new Cell(tabela1));
        //        tabela.AddCell(new Cell(tabela2));
        //        tabela.AddCell(new Cell(tabela3));
        //    }

        //    _documento.Add(tabela);
        //}

        private void EscrevaProcessosNoDocumentoAnalitico()
        {
            var tabela = new Table(1);

            tabela.Widths = new Single[] { 218 };

            tabela.Padding = 0;
            tabela.Spacing = 0;
            tabela.Width = 100;
            tabela.AutoFillEmptyCells = true;

            tabela.EndHeaders();

            foreach (var revistaDePatente in _revistasPatentes)
            {
                var tabela1 = new Table(1);
                tabela1.Widths = new Single[] { 100 };
                tabela1.Padding = 0;
                tabela1.Spacing = 0;
                tabela1.Width = 100;
                tabela1.AutoFillEmptyCells = true;
                tabela1.Border = 0;
                tabela1.EndHeaders();

                var tabelaRevistas = new Cell(ObtenhaTabelaInformacoesRevista(revistaDePatente));
                tabela1.AddCell(tabelaRevistas);

                tabela.AddCell(new Cell(tabela1));
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
            var escritor = PdfWriter.GetInstance(_documento, new FileStream(Path.Combine(caminho, nomeDoArquivoDeSaida), FileMode.Create));
            escritor.PageEvent = new GeradorDeRelatorioDePublicacoesDasPatentes.Ouvinte(_Fonte1, _Fonte2, _Fonte3, _Fonte4, empresa, _NumeroDaRevistaSelecionada);
            escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE);
            escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE);
            return nomeDoArquivoDeSaida;
        }

        public string GereRelatorioSintetico(string numeroDaRevistaSelecionada)
        {
            _NumeroDaRevistaSelecionada = numeroDaRevistaSelecionada;

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
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Patente", _Fonte2, Cell.ALIGN_LEFT, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Despacho", _Fonte2, Cell.ALIGN_CENTER, 0, corBackgroudHeader, true));
            tabela.AddCell(iTextSharpUtilidades.CrieCelula("Cliente", _Fonte2, Cell.ALIGN_LEFT, 0, corBackgroudHeader, true));

            tabela.EndHeaders();

            foreach (var processo in _processosPatentes)
            {
                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Processo.ToString(), _Fonte1, Cell.ALIGN_CENTER, 0, false));

                if (processo.Patente != null && !string.IsNullOrEmpty(processo.Patente.TituloPatente))
                    tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Patente.TituloPatente, _Fonte1, Cell.ALIGN_LEFT, 0, false));
                else
                    tabela.AddCell(iTextSharpUtilidades.CrieCelula(string.Empty, _Fonte1, Cell.ALIGN_LEFT, 0, false));

                tabela.AddCell(iTextSharpUtilidades.CrieCelula(processo.Despacho != null ? processo.Despacho.Codigo : "", _Fonte1, Cell.ALIGN_CENTER, 0, false));
                
                string clientes = string.Empty;

                if (processo.Patente != null)
                {
                    clientes = processo.Patente.Clientes.Aggregate(clientes, (current, cliente) => current + (cliente.Pessoa.Nome + " - "));
                    if (!string.IsNullOrEmpty(clientes))
                        clientes = clientes.Substring(0, clientes.Length - 3);
                }

                tabela.AddCell(iTextSharpUtilidades.CrieCelula(clientes, _Fonte1, Cell.ALIGN_LEFT, 0, false));
            }

            _documento.Add(tabela);
        }

        private Cell ObtenhaCelulaVazia()
        {
            var celulaVazia = new Cell(new Phrase("\n", _Fonte1));
            celulaVazia.Colspan = 2;
            celulaVazia.DisableBorderSide(0);
            return celulaVazia;
        }

        private Table ObtenhaTabelaInformacoesRevista(IRevistaDePatente revista)
        {
            var tabela = new Table(1);
            tabela.Widths = new Single[] { 100 };
            tabela.Padding = 0;
            tabela.Spacing = 0;
            tabela.Width = 100;
            tabela.AutoFillEmptyCells = true;
            tabela.Border = 0;
            tabela.EndHeaders();

            if(revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataDeDeposito.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout22.IdentificadorCampo + " " + revista.DataDeDeposito.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (string.IsNullOrEmpty(revista.NumeroProcessoDaPatente))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout11.IdentificadorCampo + " " + revista.NumeroProcessoDaPatente, _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (string.IsNullOrEmpty(revista.NumeroDoPedido))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout21.IdentificadorCampo + " " + revista.NumeroDoPedido, _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataDaPublicacaoDoPedido.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataDaPublicacaoDoPedido.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataDeConcessao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout45.IdentificadorCampo + " " + revista.DataDeConcessao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (string.IsNullOrEmpty(revista.PrioridadeUnionista))
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }

            if (revista.DataPublicacao.HasValue)
            {
                var celula = new Cell(new Phrase(LayoutRevistaPatente.Layout43.IdentificadorCampo + " " + revista.DataPublicacao.Value.ToString("dd/MM/yyyy"), _Fonte2));
                celula.DisableBorderSide(0);
                tabela.AddCell(celula);
            }


            return tabela;
        }

        private class Ouvinte : IPdfPageEvent
        {
            private Font font1;
            private Font font2;
            private Font font3;
            private Font font4;
            private IEmpresa empresa;
            private string _numeroDaRevistaSelecionada;

            public Ouvinte(Font font1, Font font2, Font font3, Font font4, IEmpresa empresa, string numeroDaRevistaSelecionada)
            {
                this.font1 = font1;
                this.font2 = font2;
                this.font3 = font3;
                this.font4 = font4;
                this.empresa = empresa;
                _numeroDaRevistaSelecionada = numeroDaRevistaSelecionada;
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


                 //Adicionando linha que informa o número da revista 
                var tabelaNumeroDaRevista = new Table(1);
                tabelaNumeroDaRevista.Border = 0;
                tabelaNumeroDaRevista.Width = 100;

                var celulaNumeroDaRevista = new Cell(new Phrase("Processos de clientes publicados na revista de patentes: " + _numeroDaRevistaSelecionada, font3));
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