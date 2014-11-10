using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Utilidades
{
    public class TradutorDeRevistaPatenteTXTParaRevistaPatenteXML
    {
        public static void TraduzaRevistaDePatente(DateTime dataDaRevista, string numeroDaRevista, StreamReader arquivoTxt, string localParaGravarXml)
        {
            string linha = string.Empty;
            int contador = 0;
            IList<DTOLayoutLeituraRevistaPatente> ListaDeLayoutLeituraRevistaPatentes = new List<DTOLayoutLeituraRevistaPatente>();
            DTOLayoutLeituraRevistaPatente dtoLayoutLeituraRevista = null;
            string ultimaTagPreenchida = string.Empty;
            DateTime dataDaPublicacaoDaRevista = DateTime.Now;

            using (var arquivoTxtConvertido = UtilidadesDeStream.ConvertaArquivoAnsiParaUtf8(arquivoTxt.BaseStream))
            {
                LayoutRevistaPatente.CarregueLista();

                while ((linha = arquivoTxtConvertido.ReadLine()) != null)
                {
                    linha = linha.ToUpper();

                    if (linha.StartsWith("NO") && contador==0)
                        dataDaPublicacaoDaRevista = Convert.ToDateTime(linha.Substring(linha.Length - 10, 10));

                    if (contador == 0 || contador == 1 || contador == 2)
                    {
                        contador++;
                        continue;
                    }

                    if (linha.StartsWith(LayoutRevistaPatente.LayoutCD.IdentificadorCampo))
                    {
                        if (dtoLayoutLeituraRevista != null)
                            ListaDeLayoutLeituraRevistaPatentes.Add(dtoLayoutLeituraRevista);

                        dtoLayoutLeituraRevista = null;
                        ultimaTagPreenchida = string.Empty;
                    }

                    PreenchaObjetoDTOLayoutLeituraRevistaPatente(linha, ref dtoLayoutLeituraRevista, ref ultimaTagPreenchida);
                    contador++;
                }

                arquivoTxtConvertido.Close();

            }

            MontaRevistaXmlDePatente(localParaGravarXml, numeroDaRevista, dataDaPublicacaoDaRevista, ListaDeLayoutLeituraRevistaPatentes);
        }

        private static void PreenchaObjetoDTOLayoutLeituraRevistaPatente(string linha, ref DTOLayoutLeituraRevistaPatente dtoLayoutLeituraRevistaPatente,
            ref string ultimaTagPreenchida)
        {
            if (linha.Length < 4)
                return;

            if (dtoLayoutLeituraRevistaPatente == null)
                dtoLayoutLeituraRevistaPatente = new DTOLayoutLeituraRevistaPatente();

            string valorDaLinha = linha.Remove(0, 4);

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout11.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.NumeroDaPatente += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout11.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout15.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DataDaProrrogacao = valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout15.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout21.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.NumeroDoPedido += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout21.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout22.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DataDoDeposito = valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout22.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout30.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.PrioridadeUnionista += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout30.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout43.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DataDaPublicacaoDoPedido = valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout43.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout45.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DataDeConcessao = valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout45.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout51.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.ClassificacaoInternacional += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout51.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout52.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.ClassificacaoNacional += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout52.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout54.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Titulo += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout54.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout57.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Resumo += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout57.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout61.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DadosDoPedidoDaPatente += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout61.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout62.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DadosDoPedidoOriginal += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout62.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout66.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.PrioridadeInterna += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout66.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout71.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Depositante += valorDaLinha.TrimStart().TrimEnd();

                string paisEEstado = valorDaLinha.Substring(valorDaLinha.IndexOf('(') + 1, (valorDaLinha.IndexOf(')') - 1) - valorDaLinha.IndexOf('(')).Trim();
                var paisEstadosSeparados = paisEEstado.Split('/');

                dtoLayoutLeituraRevistaPatente.PaisTitular = paisEstadosSeparados[0].Trim();

                if (paisEstadosSeparados.Count() > 1)
                    dtoLayoutLeituraRevistaPatente.UFTitular = paisEstadosSeparados[1].Trim();

                ultimaTagPreenchida = LayoutRevistaPatente.Layout71.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout72.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Inventor += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout72.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout73.IdentificadorCampo, ultimaTagPreenchida))
            {
                if (!valorDaLinha.Contains("(") && !valorDaLinha.Contains(")"))
                {
                    dtoLayoutLeituraRevistaPatente.Titular = valorDaLinha.TrimStart().TrimEnd();
                    return;
                }

                dtoLayoutLeituraRevistaPatente.Titular += valorDaLinha.Substring(0, valorDaLinha.IndexOf("(") - 1).Trim();
                string paisEEstado = valorDaLinha.Substring(valorDaLinha.IndexOf('(') + 1, (valorDaLinha.IndexOf(')') - 1) - valorDaLinha.IndexOf('(')).Trim();
                var paisEstadosSeparados = paisEEstado.Split('/');

                dtoLayoutLeituraRevistaPatente.PaisTitular = paisEstadosSeparados[0].Trim();

                if (paisEstadosSeparados.Count() > 1)
                    dtoLayoutLeituraRevistaPatente.UFTitular = paisEstadosSeparados[1].Trim();

                ultimaTagPreenchida = LayoutRevistaPatente.Layout73.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout74.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Procurador += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout74.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout81.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.PaisesDesignados += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout81.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout85.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DataInicioFaseNacional = valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout85.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout86.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DadosDepositoInternacional += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout86.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout87.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DadosPublicacaoInternacional += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.Layout87.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCD.IdentificadorCampo, ultimaTagPreenchida) && !linha.StartsWith("(DI)"))
            {
                dtoLayoutLeituraRevistaPatente.CodigoDoDespacho += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCD.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRP.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.ResponsavelPagamentoImpostoDeRenda += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutRP.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCO.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Complemento += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCO.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutDE.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Decisao += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutDE.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRE.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Recorrente += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutRE.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutNP.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.NumeroDoProcesso += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutNP.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCE.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Cedente += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCE.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCS.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Cessionaria += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCS.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutOB.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Observacao += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutOB.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutUI.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.UltimaInformacao += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutUI.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCA.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.CertificadoDeAverbacao += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCA.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutPE.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.PaisCedente += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutPE.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutPS.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.PaisDaCessionaria += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutPS.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutSE.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Setor += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutSE.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutES.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.EnderecoDaCessionaria += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutES.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutND.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.NaturezaDoDocumento += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutND.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutMO.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.MoedaDePagamento += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutMO.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutVA.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Valor += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutVA.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutPG.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Pagamento += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutPG.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutPZ.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Prazo += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutPZ.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutIA.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.ServicosIsentosDeAverbacao += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutIA.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCR.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Criador += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCR.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutLG.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Linguagem += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutLG.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCP.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.CampoDeAplicacao += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCP.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutTP.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.TipoDePrograma += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutTP.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCR.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DataDaCriacao = valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCR.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRG.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.RegimeDeGuarda += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutRG.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRQ.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Requerente += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutRQ.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRD.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Redacao += valorDaLinha.TrimStart().TrimEnd();
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutRD.IdentificadorCampo;
                return;
            }
        }

        private static bool VerifiqueTagAtualEPreenchaObjeto(string tagAtual, string identificadorLayout, string ultimaTagAvaliada)
        {
            return tagAtual.Equals(identificadorLayout) || (LayoutRevistaPatente.ObtenhaPorIdentificador(tagAtual) == null && identificadorLayout.Equals(ultimaTagAvaliada));
        }

        private static void MontaRevistaXmlDePatente(string caminhoDoArquivo, string numeroDaRevista, DateTime dataDaRevista, IList<DTOLayoutLeituraRevistaPatente> dadosDaRevistaPatente)
        {
            var nomeDoArquivo = numeroDaRevista + ".xml";

            using (var revistaXml = new XmlTextWriter(caminhoDoArquivo + nomeDoArquivo, Encoding.UTF8))
            {
                revistaXml.Formatting = Formatting.Indented;
                revistaXml.Indentation = 2;
                revistaXml.WriteStartDocument();
                revistaXml.WriteStartElement("revista");
                revistaXml.WriteAttributeString("numero", numeroDaRevista);
                revistaXml.WriteAttributeString("data", dataDaRevista.ToString("dd/MM/yyyy"));

                foreach (var dado in dadosDaRevistaPatente)
                    CrieElementoProcessoRevistaDePatente(revistaXml, dado);

                revistaXml.WriteEndElement();
                revistaXml.WriteEndDocument();
                revistaXml.Flush();
                revistaXml.Close();
            }
        }

        private static void CrieElementoProcessoRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            revistaPatenteXml.WriteStartElement("processo");

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.NumeroDoProcesso))
                revistaPatenteXml.WriteAttributeString("numero", dadosDaRevistaPatente.NumeroDoProcesso);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.DataDoDeposito))
                revistaPatenteXml.WriteAttributeString("data-deposito", dadosDaRevistaPatente.DataDoDeposito);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.DataDaProrrogacao))
                revistaPatenteXml.WriteAttributeString("data-prorrogacao", dadosDaRevistaPatente.DataDaProrrogacao);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.DataDeConcessao))
                revistaPatenteXml.WriteAttributeString("data-concenssao", dadosDaRevistaPatente.DataDeConcessao);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.DataInicioFaseNacional))
                revistaPatenteXml.WriteAttributeString("data-inicio-fase-nacional", dadosDaRevistaPatente.DataInicioFaseNacional);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.MoedaDePagamento))
                revistaPatenteXml.WriteAttributeString("moeda-pagamento", dadosDaRevistaPatente.MoedaDePagamento);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.Valor))
                revistaPatenteXml.WriteAttributeString("valor", dadosDaRevistaPatente.Valor);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.Pagamento))
                revistaPatenteXml.WriteAttributeString("pagamento", dadosDaRevistaPatente.Pagamento);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.Prazo))
                revistaPatenteXml.WriteAttributeString("prazo", dadosDaRevistaPatente.Prazo);

            CrieElementoDespachoParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoTitularesParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoPatenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoNumeroDoPedidoParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoPrioridadeUnionistaParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoDepositanteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoProcuradorParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoPaisesDesignadosParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoDadosDoDepositoInternacionalParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoDadosDaPublicacaoInternacionalParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoResponsavelIRParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoComplementoParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoDecisaoParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoRecorrenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoCedenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoCessionariaParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoObservacaoParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoUltimaInformacaoParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoCertificadoDeAverbacaoParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoPaisCedenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoPaisDaCessionariaParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoSetorParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoIsentosDeAverbacaoParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoRegimeDeGuardaParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoRequerenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoRedacaoParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);

            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoDespachoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            revistaPatenteXml.WriteStartElement("despachos");
            revistaPatenteXml.WriteStartElement("despacho");
            revistaPatenteXml.WriteAttributeString("codigo", dadosDaRevistaPatente.CodigoDoDespacho);
            revistaPatenteXml.WriteEndElement();
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoTitularesParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Titular))
                return;

            revistaPatenteXml.WriteStartElement("titulares");
            revistaPatenteXml.WriteStartElement("titular");
            revistaPatenteXml.WriteAttributeString("nome-razao-social", dadosDaRevistaPatente.Titular);
            revistaPatenteXml.WriteAttributeString("pais", dadosDaRevistaPatente.PaisTitular);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.UFTitular))
                revistaPatenteXml.WriteAttributeString("uf", dadosDaRevistaPatente.UFTitular);

            revistaPatenteXml.WriteEndElement();
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.NumeroDaPatente))
                return;

            revistaPatenteXml.WriteStartElement("patente");

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.NumeroDaPatente))
                revistaPatenteXml.WriteAttributeString("numero", dadosDaRevistaPatente.NumeroDaPatente);

            CrieElementoTituloPatenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoDadosDoPedidoOriginalPatenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoNaturezaPatenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoObservacaoPatenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoResumoPatenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoInventoresPatenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoClassificacaoPatenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoDadosDoPedidoAdicaoPatenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);
            CrieElementoProgramaDeComputadorPatenteParaRevistaDePatente(revistaPatenteXml, dadosDaRevistaPatente);

            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoTituloPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Titulo)) return;

            revistaPatenteXml.WriteStartElement("titulo");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Titulo);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoNaturezaPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.NaturezaDoDocumento)) return;

            revistaPatenteXml.WriteStartElement("natureza");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.NaturezaDoDocumento);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoObservacaoPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Observacao)) return;

            revistaPatenteXml.WriteStartElement("observacao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Observacao);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoResumoPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Resumo)) return;

            revistaPatenteXml.WriteStartElement("resumo");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Resumo);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoInventoresPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Inventor)) return;

            revistaPatenteXml.WriteStartElement("inventores");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Inventor);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoClassificacaoPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.ClassificacaoInternacional))
            {
                revistaPatenteXml.WriteStartElement("classificacaoInternacional");
                revistaPatenteXml.WriteString(dadosDaRevistaPatente.ClassificacaoInternacional);
                revistaPatenteXml.WriteEndElement();
            }
            else if (!string.IsNullOrEmpty(dadosDaRevistaPatente.ClassificacaoNacional))
            {
                revistaPatenteXml.WriteStartElement("classificacaoNacional");
                revistaPatenteXml.WriteString(dadosDaRevistaPatente.ClassificacaoNacional);
                revistaPatenteXml.WriteEndElement();
            }
        }

        private static void CrieElementoDadosDoPedidoAdicaoPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.DadosDoPedidoDaPatente)) return;

            revistaPatenteXml.WriteStartElement("adicaoPatente");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.DadosDoPedidoDaPatente);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoDadosDoPedidoOriginalPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.DadosDoPedidoOriginal)) return;

            revistaPatenteXml.WriteStartElement("dadosDaPatente");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.DadosDoPedidoOriginal);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoNumeroDoPedidoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.NumeroDoPedido)) return;

            revistaPatenteXml.WriteStartElement("numeroDoPedido");

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.DataDaPublicacaoDoPedido))
                revistaPatenteXml.WriteAttributeString("data-publicacao", dadosDaRevistaPatente.DataDaPublicacaoDoPedido);

            revistaPatenteXml.WriteString(dadosDaRevistaPatente.NumeroDoPedido);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoPrioridadeUnionistaParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.PrioridadeUnionista)) return;

            revistaPatenteXml.WriteStartElement("prioridadeUnionista");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.PrioridadeUnionista);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoDepositanteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Depositante)) return;

            revistaPatenteXml.WriteStartElement("depositante");

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.PaisTitular))
                revistaPatenteXml.WriteAttributeString("pais", dadosDaRevistaPatente.PaisTitular);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.UFTitular))
                revistaPatenteXml.WriteAttributeString("uf", dadosDaRevistaPatente.UFTitular);

            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Depositante);

            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoProcuradorParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Procurador)) return;

            revistaPatenteXml.WriteStartElement("procurador");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Procurador);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoPaisesDesignadosParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.PaisesDesignados)) return;

            revistaPatenteXml.WriteStartElement("paisesDesignados");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.PaisesDesignados);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoDadosDoDepositoInternacionalParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.DadosDepositoInternacional)) return;

            revistaPatenteXml.WriteStartElement("paisesDepositoInternacional");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.DadosDepositoInternacional);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoDadosDaPublicacaoInternacionalParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.DadosPublicacaoInternacional)) return;

            revistaPatenteXml.WriteStartElement("paisesPublicacaoInternacional");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.DadosPublicacaoInternacional);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoResponsavelIRParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.ResponsavelPagamentoImpostoDeRenda)) return;

            revistaPatenteXml.WriteStartElement("responsavelIR");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.ResponsavelPagamentoImpostoDeRenda);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoComplementoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Complemento)) return;

            revistaPatenteXml.WriteStartElement("complemento");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Complemento);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoDecisaoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Decisao)) return;

            revistaPatenteXml.WriteStartElement("decisao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Decisao);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoRecorrenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Recorrente)) return;

            revistaPatenteXml.WriteStartElement("recorrente");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Recorrente);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoCedenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Cedente)) return;

            revistaPatenteXml.WriteStartElement("cedente");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Cedente);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoCessionariaParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Cessionaria)) return;

            revistaPatenteXml.WriteStartElement("cessionaria");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Cessionaria);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoObservacaoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Observacao)) return;

            revistaPatenteXml.WriteStartElement("observacao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Observacao);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoUltimaInformacaoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.UltimaInformacao)) return;

            revistaPatenteXml.WriteStartElement("ultimaInformacao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.UltimaInformacao);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoCertificadoDeAverbacaoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.CertificadoDeAverbacao)) return;

            revistaPatenteXml.WriteStartElement("certificadoAverbacao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.CertificadoDeAverbacao);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoPaisCedenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.PaisCedente)) return;

            revistaPatenteXml.WriteStartElement("paisCedente");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.PaisCedente);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoPaisDaCessionariaParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.PaisDaCessionaria)) return;

            revistaPatenteXml.WriteStartElement("paisCessionaria", dadosDaRevistaPatente.PaisDaCessionaria);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.EnderecoDaCessionaria))
                revistaPatenteXml.WriteAttributeString("endereco", dadosDaRevistaPatente.EnderecoDaCessionaria);

            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoSetorParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Setor)) return;

            revistaPatenteXml.WriteStartElement("setor");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Setor);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoIsentosDeAverbacaoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.ServicosIsentosDeAverbacao)) return;

            revistaPatenteXml.WriteStartElement("isentosAverbacao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.ServicosIsentosDeAverbacao);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoProgramaDeComputadorPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Criador) && string.IsNullOrEmpty(dadosDaRevistaPatente.Linguagem) &&
                string.IsNullOrEmpty(dadosDaRevistaPatente.CampoDeAplicacao) && string.IsNullOrEmpty(dadosDaRevistaPatente.TipoDePrograma) &&
                string.IsNullOrEmpty(dadosDaRevistaPatente.DataDaCriacao))
                return;

            revistaPatenteXml.WriteStartElement("programaDeComputador");

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.Criador))
                revistaPatenteXml.WriteAttributeString("criador", dadosDaRevistaPatente.Criador);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.Linguagem))
                revistaPatenteXml.WriteAttributeString("liguagem", dadosDaRevistaPatente.Linguagem);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.CampoDeAplicacao))
                revistaPatenteXml.WriteAttributeString("campo-aplicacao", dadosDaRevistaPatente.CampoDeAplicacao);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.TipoDePrograma))
                revistaPatenteXml.WriteAttributeString("tipo-programa", dadosDaRevistaPatente.TipoDePrograma);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.DataDaCriacao))
                revistaPatenteXml.WriteAttributeString("data-criacao", dadosDaRevistaPatente.DataDaCriacao);

            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoRegimeDeGuardaParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.RegimeDeGuarda)) return;

            revistaPatenteXml.WriteStartElement("regimeGuarda");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.RegimeDeGuarda);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoRequerenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Requerente)) return;

            revistaPatenteXml.WriteStartElement("requerente");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Requerente);
            revistaPatenteXml.WriteEndElement();
        }

        private static void CrieElementoRedacaoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Redacao)) return;

            revistaPatenteXml.WriteStartElement("redacao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Redacao);
            revistaPatenteXml.WriteEndElement();
        }
    }
}
