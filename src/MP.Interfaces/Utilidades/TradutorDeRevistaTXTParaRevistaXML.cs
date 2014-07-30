using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Compartilhados;

namespace MP.Interfaces.Utilidades
{
    public class TradutorDeRevistaTxtParaRevistaXml
    {

        public static void TraduzaRevistaDeMarcas(DateTime dataDaRevista, string numeroDaRevista, StreamReader arquivoTxt, string localParaGravarXml)
        {
            DtoDadosDaRevistaDeMarca dadosDaRevista = null;
            var revista = new List<DtoDadosDaRevistaDeMarca>();
            var primeiraLinhaNaoEVazia = true;

            using (var arquivoTxtConvertido = UtilidadesDeStream.ConvertaArquivoAnsiParaUtf8(arquivoTxt.BaseStream))
            {
                while (!arquivoTxtConvertido.EndOfStream)
                {
                    var linha = arquivoTxtConvertido.ReadLine();

                    if (string.IsNullOrEmpty(linha))
                    {
                        if (dadosDaRevista != null && !string.IsNullOrEmpty(dadosDaRevista.NumeroProcesso) && !revista.Contains(dadosDaRevista))
                            revista.Add(dadosDaRevista);
                        dadosDaRevista = new DtoDadosDaRevistaDeMarca();
                        primeiraLinhaNaoEVazia = false;
                        continue;
                    }

                    if (primeiraLinhaNaoEVazia)
                    {
                        dadosDaRevista = new DtoDadosDaRevistaDeMarca();
                        primeiraLinhaNaoEVazia = false;
                    }

                    if (linha.StartsWith("No.")) CarregueOsDadosDaLinhaDeNo(linha, dadosDaRevista);
                    if (linha.StartsWith("Tit.")) CarregueOsDadosDaLinhaDeTitular(linha, dadosDaRevista);
                    if (linha.StartsWith("Procurador:")) CarregueOsDadosDaLinhaDeProcurador(linha, dadosDaRevista);
                    if (linha.StartsWith("*")) CarregueOsDadosDaLinhaDeTextoComplementar(linha, dadosDaRevista);
                    if (linha.StartsWith("Marca:")) CarregueOsDadosDaLinhaMarca(linha, dadosDaRevista);
                    if (linha.StartsWith("Apres.:"))
                        CarregueOsDadosDaLinhaDeApresentacaoENatureza(linha, dadosDaRevista);
                    if (linha.StartsWith("Apostila:")) CarregueOsDadosDaLinhaApostila(linha, dadosDaRevista);
                    if (linha.StartsWith("NCL")) CarregueOsDadosDaLinhaNCL(linha, dadosDaRevista);
                    if (linha.StartsWith("CFE(4)")) CarregueOsDadosDaLinhaClasseVienaEdicao4(linha, dadosDaRevista);
                    if (linha.StartsWith("Prior.:")) CarregueOsDadosDaLinhaPrioridadeUnionista(linha, dadosDaRevista);
                    if (linha.StartsWith("Clas.Prod/Serv:"))
                        CarregueOsDadosDaLinhaClasseNacional(linha, dadosDaRevista);
                    if (linha.StartsWith("Especific.:"))
                        CarregueOsDadosDaLinhaEspecificacaoDeClasseNacional(linha, dadosDaRevista);
                }
                arquivoTxtConvertido.Close();
            }

            MontaRevistaXmlDeMarcas(localParaGravarXml, numeroDaRevista, dataDaRevista, revista);
        }

        private static void MontaRevistaXmlDeMarcas(string caminhoDoArquivo, string numeroDaRevista, DateTime dataDaRevista, IList<DtoDadosDaRevistaDeMarca> dadosDaRevista)
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

                foreach (var dado in dadosDaRevista)
                    CrieElementoProcessoParaRevistaDeMarca(revistaXml, dado);

                revistaXml.WriteEndElement();
                revistaXml.WriteEndDocument();
                revistaXml.Flush();
                revistaXml.Close();
            }
        }

        private static void CrieElementoProcuradorRevistaDeMarca(XmlTextWriter revistaXml, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            if (string.IsNullOrEmpty(dadosDaRevista.Procurador)) return;
            revistaXml.WriteStartElement("procurador");
            revistaXml.WriteString(dadosDaRevista.Procurador);
            revistaXml.WriteEndElement();
        }

        private static void CrieElementoApostilaRevistaDeMarca(XmlTextWriter revistaXml, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            if (string.IsNullOrEmpty(dadosDaRevista.Apostila)) return;
            revistaXml.WriteStartElement("apostila");
            revistaXml.WriteString(dadosDaRevista.Apostila);
            revistaXml.WriteEndElement();
        }

        private static void CrieElementoPrioridadeUnionistaRevistaDeMarca(XmlTextWriter revistaXml, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            if (string.IsNullOrEmpty(dadosDaRevista.NumeroPrioridadeUnionista)) return;
            revistaXml.WriteStartElement("prioridade-unionista");
            revistaXml.WriteStartElement("prioridade");
            revistaXml.WriteAttributeString("data", dadosDaRevista.DataPrioridadeUnionista);
            revistaXml.WriteAttributeString("numero", dadosDaRevista.NumeroPrioridadeUnionista);
            revistaXml.WriteAttributeString("pais", dadosDaRevista.PaisPrioridadeUnionista);
            revistaXml.WriteEndElement();
            revistaXml.WriteEndElement();
        }

        private static void CrieElementoClasseNiceRevistaDeMarca(XmlTextWriter revistaXml, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            if (string.IsNullOrEmpty(dadosDaRevista.CodigoNCL)) return;
            revistaXml.WriteStartElement("classe-nice");
            revistaXml.WriteAttributeString("codigo", dadosDaRevista.CodigoNCL);

            if (!string.IsNullOrEmpty(dadosDaRevista.EspecificacaoNCL))
            {
                revistaXml.WriteStartElement("especificacao");
                revistaXml.WriteString(dadosDaRevista.EspecificacaoNCL);
                revistaXml.WriteEndElement();
            }

            revistaXml.WriteEndElement();
        }

        private static void CrieElementoClasseNacionalRevistaDeMarca(XmlTextWriter revistaXml, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            if (string.IsNullOrEmpty(dadosDaRevista.CodigoDaClasseNacional)) return;
            revistaXml.WriteStartElement("classe-nacional");
            revistaXml.WriteAttributeString("codigo", dadosDaRevista.CodigoDaClasseNacional);

            if (!string.IsNullOrEmpty(dadosDaRevista.EspecificacaoClasseNacional))
            {
                revistaXml.WriteStartElement("especificacao");
                revistaXml.WriteString(dadosDaRevista.EspecificacaoClasseNacional);
                revistaXml.WriteEndElement();
            }

            revistaXml.WriteStartElement("sub-classes-nacional");

            foreach (var codigoSubClasse in dadosDaRevista.CodigosDasSubClassesNacionais)
            {
                revistaXml.WriteStartElement("sub-classe-nacional");
                revistaXml.WriteAttributeString("codigo", codigoSubClasse);
                revistaXml.WriteEndElement();
            }

            revistaXml.WriteEndElement();

            revistaXml.WriteEndElement();
        }

        private static void CrieElementoClassesVienaParaRevistaDeMarca(XmlTextWriter revistaXml, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            if (dadosDaRevista.CodigosClasseVienaEdicao4.Count <= 0) return;
            revistaXml.WriteStartElement("classes-vienna");
            revistaXml.WriteAttributeString("edicao", "4");

            foreach (var codigoClasseViena in dadosDaRevista.CodigosClasseVienaEdicao4)
            {
                revistaXml.WriteStartElement("classe-vienna");
                revistaXml.WriteAttributeString("codigo", codigoClasseViena);
                revistaXml.WriteEndElement();
            }

            revistaXml.WriteEndElement();
        }

        private static void CrieElementoDespachoParaRevistaDeMarca(XmlTextWriter revistaXml, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            revistaXml.WriteStartElement("despachos");
            revistaXml.WriteStartElement("despacho");
            revistaXml.WriteAttributeString("codigo", dadosDaRevista.CodigoDoDespacho);

            if (!string.IsNullOrEmpty(dadosDaRevista.TextoComplementarDoDespacho))
            {
                revistaXml.WriteStartElement("texto-complementar");
                revistaXml.WriteString(dadosDaRevista.TextoComplementarDoDespacho);
                revistaXml.WriteEndElement();
            }

            revistaXml.WriteEndElement();
            revistaXml.WriteEndElement();
        }

        private static void CrieElementoMarcaParaRevistaDeMarca(XmlTextWriter revistaXml, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            if (string.IsNullOrEmpty(dadosDaRevista.NomeDaMarca)) return;
            revistaXml.WriteStartElement("marca");
            revistaXml.WriteAttributeString("apresentacao", dadosDaRevista.ApresentacaoDaMarca);
            revistaXml.WriteAttributeString("natureza", dadosDaRevista.NaturezaDaMarca);
            revistaXml.WriteStartElement("nome");
            revistaXml.WriteString(dadosDaRevista.NomeDaMarca);
            revistaXml.WriteEndElement();
            revistaXml.WriteEndElement();
        }

        private static void CrieElementoTitularesParaRevistaDeMarca(XmlTextWriter revistaXml, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            revistaXml.WriteStartElement("titulares");
            revistaXml.WriteStartElement("titular");
            revistaXml.WriteAttributeString("nome-razao-social", dadosDaRevista.Titular);
            revistaXml.WriteAttributeString("pais", dadosDaRevista.PaisTitular);

            if (!string.IsNullOrEmpty(dadosDaRevista.UfTitular))
                revistaXml.WriteAttributeString("uf", dadosDaRevista.UfTitular);
            revistaXml.WriteEndElement();
            revistaXml.WriteEndElement();
        }

        private static void CrieElementoProcessoParaRevistaDeMarca(XmlTextWriter revistaXml, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            revistaXml.WriteStartElement("processo");
            revistaXml.WriteAttributeString("numero", dadosDaRevista.NumeroProcesso);

            if (!string.IsNullOrEmpty(dadosDaRevista.DataDoDeposito))
                revistaXml.WriteAttributeString("data-deposito", dadosDaRevista.DataDoDeposito);

            CrieElementoDespachoParaRevistaDeMarca(revistaXml, dadosDaRevista);
            CrieElementoTitularesParaRevistaDeMarca(revistaXml, dadosDaRevista);
            CrieElementoMarcaParaRevistaDeMarca(revistaXml, dadosDaRevista);
            CrieElementoClassesVienaParaRevistaDeMarca(revistaXml, dadosDaRevista);
            CrieElementoClasseNacionalRevistaDeMarca(revistaXml, dadosDaRevista);
            CrieElementoClasseNiceRevistaDeMarca(revistaXml, dadosDaRevista);
            CrieElementoPrioridadeUnionistaRevistaDeMarca(revistaXml, dadosDaRevista);
            CrieElementoApostilaRevistaDeMarca(revistaXml, dadosDaRevista);
            CrieElementoProcuradorRevistaDeMarca(revistaXml, dadosDaRevista);
            revistaXml.WriteEndElement();
        }

        private static void CarregueOsDadosDaLinhaDeNo(string linha, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            var linhaSemIdentificador = linha.Remove(0, 3);
            var partes = linhaSemIdentificador.Split(' ');

            dadosDaRevista.NumeroProcesso = partes[0].Trim();

            if (partes.Count() > 3)
            {
                dadosDaRevista.DataDoDeposito = partes[2].Trim();
                dadosDaRevista.CodigoDoDespacho = partes[4].Trim();
            }
            else
                dadosDaRevista.CodigoDoDespacho = partes[2].Trim();
        }

        private static void CarregueOsDadosDaLinhaDeTitular(string linha, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            var linhaSemMarcadorDeTitular = linha.Remove(0, 4);
            dadosDaRevista.Titular = linhaSemMarcadorDeTitular.Substring(0, linhaSemMarcadorDeTitular.IndexOf('(') - 1).Trim();
            var linhaPaisUf =
                linhaSemMarcadorDeTitular.Substring(linhaSemMarcadorDeTitular.IndexOf('(') + 1, (linhaSemMarcadorDeTitular.IndexOf(')') - 1) - linhaSemMarcadorDeTitular.IndexOf('(')).Trim();

            var partesPaisUF = linhaPaisUf.Split('/');
            dadosDaRevista.PaisTitular = partesPaisUF[0].Trim();
            if (partesPaisUF.Count() > 1) dadosDaRevista.UfTitular = partesPaisUF[1].Trim();
        }

        private static void CarregueOsDadosDaLinhaDeProcurador(string linha, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            dadosDaRevista.Procurador = linha.Remove(0, 11).Trim();
        }

        private static void CarregueOsDadosDaLinhaDeTextoComplementar(string linha, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            dadosDaRevista.TextoComplementarDoDespacho = linha.Remove(0, 1).Trim();
        }

        private static void CarregueOsDadosDaLinhaMarca(string linha, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            dadosDaRevista.NomeDaMarca = linha.Remove(0, 6).Trim();
        }

        private static void CarregueOsDadosDaLinhaDeApresentacaoENatureza(string linha, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            var partes = linha.Split(';');
            dadosDaRevista.ApresentacaoDaMarca = partes[0].Trim().Remove(0, 7).Trim();
            dadosDaRevista.NaturezaDaMarca = partes[1].Trim().Remove(0, 5).Trim();
        }

        private static void CarregueOsDadosDaLinhaApostila(string linha, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            dadosDaRevista.Apostila = linha.Remove(0, 9).Trim();
        }

        private static void CarregueOsDadosDaLinhaNCL(string linha, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            var linhaNCLSemMarcador = linha.Remove(0, 7).Trim();

            dadosDaRevista.CodigoNCL = linhaNCLSemMarcador.Substring(0, 2);
            dadosDaRevista.EspecificacaoNCL = linhaNCLSemMarcador.Substring(2).Trim();
        }

        private static void CarregueOsDadosDaLinhaClasseVienaEdicao4(string linha, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            var linhaSemMarcador = linha.Remove(0, 6).Trim();
            var partes = linhaSemMarcador.Split(';');
            var codigosClasseVienaEdicao4 = partes.Select(codigoClasseViena => codigoClasseViena.Trim()).ToList();
            dadosDaRevista.CodigosClasseVienaEdicao4 = codigosClasseVienaEdicao4;
        }

        private static void CarregueOsDadosDaLinhaPrioridadeUnionista(string linha, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            var linhaSemMarcador = linha.Remove(0, 7).Trim();
            dadosDaRevista.NumeroPrioridadeUnionista = linhaSemMarcador.Substring(0, 18).Trim();
            dadosDaRevista.DataPrioridadeUnionista = linhaSemMarcador.Substring(18, 10).Trim();
            dadosDaRevista.PaisPrioridadeUnionista = linhaSemMarcador.Substring(28).Trim();
        }

        private static void CarregueOsDadosDaLinhaClasseNacional(string linha, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            var linhaSemMarcador = linha.Remove(0, 15).Trim();
            var codigosCompletos = linhaSemMarcador.Split(';');

            foreach (var partesDoCodigo in codigosCompletos.Select(codigoCompleto => codigoCompleto.Split('.')))
            {
                dadosDaRevista.CodigoDaClasseNacional = partesDoCodigo[0].Trim();
                if (!dadosDaRevista.CodigosDasSubClassesNacionais.Contains(partesDoCodigo[1]))
                    dadosDaRevista.CodigosDasSubClassesNacionais.Add(partesDoCodigo[1].Trim());
            }
        }

        private static void CarregueOsDadosDaLinhaEspecificacaoDeClasseNacional(string linha, DtoDadosDaRevistaDeMarca dadosDaRevista)
        {
            var linhaSemMarcador = linha.Remove(0, 11).Trim();
            dadosDaRevista.EspecificacaoClasseNacional = linhaSemMarcador;
        }
    }


    internal class DtoDadosDaRevistaDeMarca
    {
        public DtoDadosDaRevistaDeMarca()
        {
            CodigosClasseVienaEdicao4 = new List<string>();
            CodigosDasSubClassesNacionais = new List<string>();
        }

        public string NumeroProcesso { get; set; }
        public string DataDoDeposito { get; set; }
        public string CodigoDoDespacho { get; set; }
        public string TextoComplementarDoDespacho { get; set; }
        public string Titular { get; set; }
        public string PaisTitular { get; set; }
        public string UfTitular { get; set; }
        public string Procurador { get; set; }
        public string NomeDaMarca { get; set; }
        public string ApresentacaoDaMarca { get; set; }
        public string NaturezaDaMarca { get; set; }
        public string Apostila { get; set; }
        public string CodigoNCL { get; set; }
        public string EspecificacaoNCL { get; set; }
        public IList<string> CodigosClasseVienaEdicao4 { get; set; }
        public string NumeroPrioridadeUnionista { get; set; }
        public string DataPrioridadeUnionista { get; set; }
        public string PaisPrioridadeUnionista { get; set; }
        public string CodigoDaClasseNacional { get; set; }
        public IList<string> CodigosDasSubClassesNacionais { get; set; }
        public string EspecificacaoClasseNacional { get; set; }

 }
}