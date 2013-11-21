using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IRevistaDePatente
    {
        long? IdRevistaPatente { get; set; }
        int NumeroRevistaPatente { get; set; }
        DateTime? DataPublicacao { get; set; }
        DateTime? DataProcessamento { get; set; }
        bool Processada { get; set; }
        string ExtensaoArquivo { get; set; }
        DateTime? DataDeDeposito { get; set; }
        long NumeroProcessoDaPatente { get; set; }
        long NumeroDoPedido { get; set; }
        DateTime? DataDaPublicacaoDoPedido { get; set; }
        DateTime? DataDeConcessao { get; set; }
        string PrioridadeUnionista { get; set; }
        string ClassificacaoInternacional { get; set; }
        string Titulo { get; set; }
        string Resumo { get; set; }
        string DadosDoPedidoDaPatente { get; set; }
        string DadosDoPedidoOriginal { get; set; }
        string PrioridadeInterna { get; set; }
        string Depositante { get; set; }
        string Inventor { get; set; }
        string Titular { get; set; }
        string UFTitular { get; set; }
        string PaisTitular { get; set; }
        string Procurador { get; set; }
        string PaisesDesignados { get; set; }
        DateTime? DataInicioFaseNacional { get; set; }
        DateTime? DadosDepositoInternacional { get; set; }
        DateTime? DadosPublicacaoInternacional { get; set; }
        string CodigoDoDespacho { get; set; }
        string ResponsavelPagamentoImpostoDeRenda { get; set; }
        string Complemento { get; set; }
        string Decisao { get; set; }
        string Recorrente { get; set; }
        string NumeroDoProcesso { get; set; }
        string Cedente { get; set; }
        string Cessionaria { get; set; }
        string Observacao { get; set; }
        string UltimaInformacao { get; set; }
        string CertificadoDeAverbacao { get; set; }
        string PaisCedente { get; set; }
        string PaisDaCessionaria { get; set; }
        string Setor { get; set; }
        string EnderecoDaCessionaria { get; set; }
        string NaturezaDoDocumento { get; set; }
        string MoedaDePagamento { get; set; }
        string Valor { get; set; }
        string Pagamento { get; set; }
        string Prazo { get; set; }
        string ServicosIsentosDeAverbacao { get; set; }
        string Criador { get; set; }
        string Linguagem { get; set; }
        string CampoDeAplicacao { get; set; }
        string TipoDePrograma { get; set; }
        DateTime? DataDaCriacao { get; set; }
        string RegimeDeGuarda { get; set; }
        string Requerente { get; set; }
        string Redacao { get; set; }
        DateTime? DataDaProrrogacao { get; set; }
        string ClassificacaoNacional { get; set; }
    }
}
