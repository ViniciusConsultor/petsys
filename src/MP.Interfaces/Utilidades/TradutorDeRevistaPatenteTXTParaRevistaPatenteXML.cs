using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Utilidades
{
    public class TradutorDeRevistaPatenteTXTParaRevistaPatenteXML
    {
        private void PreenchaObjetoDTOLayoutLeituraRevistaPatente(string linha, ref DTOLayoutLeituraRevistaPatente dtoLayoutLeituraRevistaPatente,
            ref string ultimaTagPreenchida)
        {
            if (linha.Length < 4)
                return;

            if (dtoLayoutLeituraRevistaPatente == null)
                dtoLayoutLeituraRevistaPatente = new DTOLayoutLeituraRevistaPatente();

            string valorDaLinha = linha.Remove(0, 4);

            dtoLayoutLeituraRevistaPatente.NumeroDaPatente = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout11.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.DataDaProrrogacao = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout15.IdentificadorCampo, ultimaTagPreenchida, DateTime.Parse(valorDaLinha.Trim()));
            dtoLayoutLeituraRevistaPatente.NumeroDoPedido = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout21.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.DataDoDeposito = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout22.IdentificadorCampo, ultimaTagPreenchida, DateTime.Parse(valorDaLinha.Trim()));
            dtoLayoutLeituraRevistaPatente.PrioridadeUnionista = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout30.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha.Trim());
            dtoLayoutLeituraRevistaPatente.DataDaPublicacaoDoPedido = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout43.IdentificadorCampo, ultimaTagPreenchida, DateTime.Parse(valorDaLinha.Trim()));
            //dtoLayoutLeituraRevistaPatente.DataDeConcessao = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout45, ultimaTagPreenchida.IdentificadorCampo, DateTime.Parse(valorDaLinha.Trim()));
            dtoLayoutLeituraRevistaPatente.ClassificacaoInternacional = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout51.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.ClassificacaoNacional = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout52.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Titulo = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout54.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Resumo = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout57.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.DadosDoPedidoDaPatente = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout61.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.DadosDoPedidoOriginal = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout62.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.PrioridadeInterna = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout66.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Depositante = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout71.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Inventor = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout72.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Titular = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout73.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Procurador = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout74.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.PaisesDesignados = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout81.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.DataInicioFaseNacional = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout85.IdentificadorCampo, ultimaTagPreenchida, DateTime.Parse(valorDaLinha.Trim()));
            dtoLayoutLeituraRevistaPatente.DadosDepositoInternacional = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout86.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.DadosPublicacaoInternacional = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.Layout87.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.CodigoDoDespacho = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCD.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.ResponsavelPagamentoImpostoDeRenda = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRP.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Complemento = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCO.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Decisao = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutDE.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Recorrente = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRE.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.NumeroDoProcesso = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutNP.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Cedente = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCE.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Cessionaria = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCS.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Objeto = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutOB.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.UltimaInformacao = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutUI.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.CertificadoDeAverbacao = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCA.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.PaisCedente = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutPE.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.PaisDaCessionaria = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutPS.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Setor = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutSE.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.EnderecoDaCessionaria = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutES.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.NaturezaDoDocumento = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutND.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.MoedaDePagamento = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutMO.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Valor = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutVA.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Pagamento = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutPG.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Prazo = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutPZ.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.ServicosIsentosDeAverbacao = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutIA.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Criador = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCR.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Linguagem = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutLG.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.CampoDeAplicacao = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCP.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.TipoDePrograma = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutTP.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.DataDaCriacao = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutCR.IdentificadorCampo, ultimaTagPreenchida, DateTime.Parse(valorDaLinha.Trim()));
            dtoLayoutLeituraRevistaPatente.RegimeDeGuarda = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRG.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Requerente = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRQ.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
            dtoLayoutLeituraRevistaPatente.Redacao = VerifiqueTagAtualEPreenchaObjeto(linha.Substring(0, 4), LayoutRevistaPatente.LayoutRD.IdentificadorCampo, ultimaTagPreenchida, valorDaLinha);
        }

        private string VerifiqueTagAtualEPreenchaObjeto(string tagAtual, string identificadorLayout, string ultimaTagAvaliada, string valorLinha)
        {
            if (tagAtual.Equals(identificadorLayout) || identificadorLayout.Equals(ultimaTagAvaliada))
                return valorLinha;

            return string.Empty;
        }

        private DateTime? VerifiqueTagAtualEPreenchaObjeto(string tagAtual, string identificadorLayout, string ultimaTagAvaliada, DateTime valorLinha)
        {
            if (tagAtual.Equals(identificadorLayout) || identificadorLayout.Equals(ultimaTagAvaliada))
                return valorLinha;

            return null;
        }
    }
}
