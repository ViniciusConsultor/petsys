using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Utilidades
{
    public class TradutorDeRevistaPatenteTXTParaRevistaPatenteXML
    {
        public void ConvertaTextoParaXML(string caminhoArquivo)
        {
            string linha = string.Empty;
            var leitor = new StreamReader(caminhoArquivo);
            int contador = 0;
            var xml = new XmlDocument();
            IList<DTOLayoutLeituraRevistaPatente> ListaDeLayoutLeituraRevistaPatentes = new List<DTOLayoutLeituraRevistaPatente>();
            DTOLayoutLeituraRevistaPatente dtoLayoutLeituraRevista = null;
            string linhaNumeroEDataDaRevista = string.Empty;
            string ultimaTagPreenchida = string.Empty;

            LayoutRevistaPatente.CarregueLista();

            while ((linha = leitor.ReadLine()) != null)
            {
                linha = linha.ToUpper();

                if (contador == 0)
                {
                    linhaNumeroEDataDaRevista = linha;
                    contador++;
                    continue;
                }

                if (contador == 1 || contador == 2)
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

            //MontaRevistaXmlDeMarcas(@"C:\MarcasEPatentes\Revistas\Patente", "P2230", DateTime.Now, ListaDeLayoutLeituraRevistaPatentes);
        }

        private void PreenchaObjetoDTOLayoutLeituraRevistaPatente(string linha, ref DTOLayoutLeituraRevistaPatente dtoLayoutLeituraRevistaPatente,
            ref string ultimaTagPreenchida)
        {
            if (linha.Length < 4)
                return;

            if (dtoLayoutLeituraRevistaPatente == null)
                dtoLayoutLeituraRevistaPatente = new DTOLayoutLeituraRevistaPatente();

            string valorDaLinha = linha.Remove(0, 4);

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout11.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.NumeroDaPatente += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout11.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout15.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DataDaProrrogacao = valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout15.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout21.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.NumeroDoPedido += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout21.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout22.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DataDoDeposito = valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout22.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout30.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.PrioridadeUnionista += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout30.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout43.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DataDaPublicacaoDoPedido = valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout43.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout45.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DataDeConcessao = valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout45.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout51.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.ClassificacaoInternacional += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout51.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout52.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.ClassificacaoNacional += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout52.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout54.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Titulo += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout54.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout57.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Resumo += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout57.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout61.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DadosDoPedidoDaPatente += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout61.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout62.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DadosDoPedidoOriginal += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout62.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout66.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.PrioridadeInterna += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout66.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout71.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Depositante += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout71.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout72.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Inventor += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout72.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout73.IdentificadorCampo, ultimaTagPreenchida))
            {
                if (!valorDaLinha.Contains("(") && !valorDaLinha.Contains(")"))
                {
                    dtoLayoutLeituraRevistaPatente.Titular = valorDaLinha;
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
                dtoLayoutLeituraRevistaPatente.Procurador += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout74.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout81.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.PaisesDesignados += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout81.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout85.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DataInicioFaseNacional = valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout85.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout86.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DadosDepositoInternacional += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout86.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout87.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DadosPublicacaoInternacional += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.Layout87.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCD.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.CodigoDoDespacho += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCD.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRP.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.ResponsavelPagamentoImpostoDeRenda += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutRP.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCO.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Complemento += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCO.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutDE.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Decisao += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutDE.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRE.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Recorrente += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutRE.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutNP.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.NumeroDoProcesso += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutNP.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCE.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Cedente += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCE.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCS.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Cessionaria += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCS.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutOB.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Observacao += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutOB.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutUI.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.UltimaInformacao += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutUI.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCA.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.CertificadoDeAverbacao += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCA.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutPE.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.PaisCedente += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutPE.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutPS.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.PaisDaCessionaria += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutPS.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutSE.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Setor += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutSE.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutES.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.EnderecoDaCessionaria += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutES.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutND.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.NaturezaDoDocumento += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutND.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutMO.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.MoedaDePagamento += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutMO.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutVA.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Valor += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutVA.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutPG.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Pagamento += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutPG.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutPZ.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Prazo += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutPZ.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutIA.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.ServicosIsentosDeAverbacao += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutIA.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCR.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Criador += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCR.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutLG.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Linguagem += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutLG.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCP.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.CampoDeAplicacao += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCP.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutTP.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.TipoDePrograma += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutTP.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCR.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.DataDaCriacao = valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutCR.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRG.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.RegimeDeGuarda += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutRG.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRQ.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Requerente += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutRQ.IdentificadorCampo;
                return;
            }

            if (VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRD.IdentificadorCampo, ultimaTagPreenchida))
            {
                dtoLayoutLeituraRevistaPatente.Redacao += valorDaLinha;
                ultimaTagPreenchida = LayoutRevistaPatente.LayoutRD.IdentificadorCampo;
                return;
            }
        }

        private bool VerifiqueTagAtualEPreenchaObjeto(string tagAtual, string identificadorLayout, string ultimaTagAvaliada)
        {
            return tagAtual.Equals(identificadorLayout) || (LayoutRevistaPatente.ObtenhaPorIdentificador(tagAtual) == null && identificadorLayout.Equals(ultimaTagAvaliada));
        }

        private void MontaRevistaXmlDePatente(string caminhoDoArquivo, string numeroDaRevista, DateTime dataDaRevista, IList<DTOLayoutLeituraRevistaPatente> dadosDaRevistaPatente)
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

        private void CrieElementoProcessoRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
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

        private void CrieElementoDespachoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            revistaPatenteXml.WriteStartElement("despachos");
            revistaPatenteXml.WriteStartElement("despacho");
            revistaPatenteXml.WriteAttributeString("codigo", dadosDaRevistaPatente.CodigoDoDespacho);
            revistaPatenteXml.WriteEndElement();
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoTitularesParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
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

        private void CrieElementoPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
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

        private void CrieElementoTituloPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Titulo)) return;

            revistaPatenteXml.WriteStartElement("titulo");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Titulo);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoNaturezaPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.NaturezaDoDocumento)) return;

            revistaPatenteXml.WriteStartElement("natureza");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.NaturezaDoDocumento);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoObservacaoPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Observacao)) return;

            revistaPatenteXml.WriteStartElement("observacao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Observacao);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoResumoPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Resumo)) return;

            revistaPatenteXml.WriteStartElement("resumo");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Resumo);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoInventoresPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Inventor)) return;

            revistaPatenteXml.WriteStartElement("inventores");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Inventor);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoClassificacaoPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.ClassificacaoInternacional))
            {
                revistaPatenteXml.WriteStartElement("classificacao internacional");
                revistaPatenteXml.WriteString(dadosDaRevistaPatente.ClassificacaoInternacional);
                revistaPatenteXml.WriteEndElement();
            }
            else if (!string.IsNullOrEmpty(dadosDaRevistaPatente.ClassificacaoNacional))
            {
                revistaPatenteXml.WriteStartElement("classificacao nacional");
                revistaPatenteXml.WriteString(dadosDaRevistaPatente.ClassificacaoNacional);
                revistaPatenteXml.WriteEndElement();
            }
        }

        private void CrieElementoDadosDoPedidoAdicaoPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.DadosDoPedidoDaPatente)) return;

            revistaPatenteXml.WriteStartElement("adicao patente");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.DadosDoPedidoDaPatente);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoDadosDoPedidoOriginalPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.DadosDoPedidoOriginal)) return;

            revistaPatenteXml.WriteStartElement("dados da patente");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.DadosDoPedidoOriginal);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoNumeroDoPedidoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.NumeroDoPedido)) return;

            revistaPatenteXml.WriteStartElement("numero do pedido", dadosDaRevistaPatente.NumeroDoPedido);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.DataDaPublicacaoDoPedido))
                revistaPatenteXml.WriteAttributeString("data-publicacao", dadosDaRevistaPatente.DataDaPublicacaoDoPedido);

            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoPrioridadeUnionistaParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.PrioridadeUnionista)) return;

            revistaPatenteXml.WriteStartElement("prioridade unionista");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.PrioridadeUnionista);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoDepositanteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Depositante)) return;

            revistaPatenteXml.WriteStartElement("depositante");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Depositante);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoProcuradorParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Procurador)) return;

            revistaPatenteXml.WriteStartElement("procurador");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Procurador);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoPaisesDesignadosParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.PaisesDesignados)) return;

            revistaPatenteXml.WriteStartElement("paises designados");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.PaisesDesignados);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoDadosDoDepositoInternacionalParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.DadosDepositoInternacional)) return;

            revistaPatenteXml.WriteStartElement("paises deposito internacional");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.DadosDepositoInternacional);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoDadosDaPublicacaoInternacionalParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.DadosPublicacaoInternacional)) return;

            revistaPatenteXml.WriteStartElement("paises publicacao internacional");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.DadosPublicacaoInternacional);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoResponsavelIRParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.ResponsavelPagamentoImpostoDeRenda)) return;

            revistaPatenteXml.WriteStartElement("responsavel IR");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.ResponsavelPagamentoImpostoDeRenda);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoComplementoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Complemento)) return;

            revistaPatenteXml.WriteStartElement("complemento");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Complemento);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoDecisaoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Decisao)) return;

            revistaPatenteXml.WriteStartElement("decisao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Decisao);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoRecorrenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Recorrente)) return;

            revistaPatenteXml.WriteStartElement("recorrente");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Recorrente);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoCedenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Cedente)) return;

            revistaPatenteXml.WriteStartElement("cedente");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Cedente);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoCessionariaParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Cessionaria)) return;

            revistaPatenteXml.WriteStartElement("cessionaria");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Cessionaria);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoObservacaoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Observacao)) return;

            revistaPatenteXml.WriteStartElement("observacao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Observacao);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoUltimaInformacaoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.UltimaInformacao)) return;

            revistaPatenteXml.WriteStartElement("ultima informacao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.UltimaInformacao);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoCertificadoDeAverbacaoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.CertificadoDeAverbacao)) return;

            revistaPatenteXml.WriteStartElement("certificado averbacao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.CertificadoDeAverbacao);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoPaisCedenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.PaisCedente)) return;

            revistaPatenteXml.WriteStartElement("pais cedente");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.PaisCedente);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoPaisDaCessionariaParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.PaisDaCessionaria)) return;

            revistaPatenteXml.WriteStartElement("pais cessionaria", dadosDaRevistaPatente.PaisDaCessionaria);

            if (!string.IsNullOrEmpty(dadosDaRevistaPatente.EnderecoDaCessionaria))
                revistaPatenteXml.WriteAttributeString("endereco", dadosDaRevistaPatente.EnderecoDaCessionaria);

            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoSetorParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Setor)) return;

            revistaPatenteXml.WriteStartElement("setor");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Setor);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoIsentosDeAverbacaoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.ServicosIsentosDeAverbacao)) return;

            revistaPatenteXml.WriteStartElement("isentos averbacao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.ServicosIsentosDeAverbacao);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoProgramaDeComputadorPatenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Criador) && string.IsNullOrEmpty(dadosDaRevistaPatente.Linguagem) &&
                string.IsNullOrEmpty(dadosDaRevistaPatente.CampoDeAplicacao) && string.IsNullOrEmpty(dadosDaRevistaPatente.TipoDePrograma) &&
                string.IsNullOrEmpty(dadosDaRevistaPatente.DataDaCriacao))
                return;

            revistaPatenteXml.WriteStartElement("programa de computador");

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

        private void CrieElementoRegimeDeGuardaParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.RegimeDeGuarda)) return;

            revistaPatenteXml.WriteStartElement("regime guarda");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.RegimeDeGuarda);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoRequerenteParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Requerente)) return;

            revistaPatenteXml.WriteStartElement("requerente");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Requerente);
            revistaPatenteXml.WriteEndElement();
        }

        private void CrieElementoRedacaoParaRevistaDePatente(XmlTextWriter revistaPatenteXml, DTOLayoutLeituraRevistaPatente dadosDaRevistaPatente)
        {
            if (string.IsNullOrEmpty(dadosDaRevistaPatente.Redacao)) return;

            revistaPatenteXml.WriteStartElement("redacao");
            revistaPatenteXml.WriteString(dadosDaRevistaPatente.Redacao);
            revistaPatenteXml.WriteEndElement();
        }
    }
}
