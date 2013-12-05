﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class RevistaDePatente : IRevistaDePatente
    {

        public long? IdRevistaPatente { get; set; }

        public int NumeroRevistaPatente { get; set; }

        public DateTime? DataPublicacao { get; set; }

        public DateTime? DataProcessamento { get; set; }

        public bool Processada { get; set; }

        public string ExtensaoArquivo { get; set; }

        public DateTime? DataDeDeposito { get; set; }

        public string NumeroProcessoDaPatente { get; set; }

        public long NumeroDoPedido { get; set; }

        public DateTime? DataDaPublicacaoDoPedido { get; set; }

        public DateTime? DataDeConcessao { get; set; }

        public string PrioridadeUnionista { get; set; }

        public string ClassificacaoInternacional { get; set; }

        public string Titulo { get; set; }

        public string Resumo { get; set; }

        public string DadosDoPedidoDaPatente { get; set; }

        public string DadosDoPedidoOriginal { get; set; }

        public string PrioridadeInterna { get; set; }

        public string Depositante { get; set; }

        public string Inventor { get; set; }

        public string Titular { get; set; }

        public string UFTitular { get; set; }

        public string PaisTitular { get; set; }

        public string Procurador { get; set; }

        public string PaisesDesignados { get; set; }

        public DateTime? DataInicioFaseNacional { get; set; }

        public string DadosDepositoInternacional { get; set; }

        public string DadosPublicacaoInternacional { get; set; }

        public string CodigoDoDespacho { get; set; }

        public string ResponsavelPagamentoImpostoDeRenda { get; set; }

        public string Complemento { get; set; }

        public string Decisao { get; set; }

        public string Recorrente { get; set; }

        public long NumeroDoProcesso { get; set; }

        public string Cedente { get; set; }

        public string Cessionaria { get; set; }

        public string Observacao { get; set; }

        public string UltimaInformacao { get; set; }

        public string CertificadoDeAverbacao { get; set; }

        public string PaisCedente { get; set; }

        public string PaisDaCessionaria { get; set; }

        public string Setor { get; set; }

        public string EnderecoDaCessionaria { get; set; }

        public string NaturezaDoDocumento { get; set; }

        public string MoedaDePagamento { get; set; }

        public string Valor { get; set; }

        public string Pagamento { get; set; }

        public string Prazo { get; set; }

        public string ServicosIsentosDeAverbacao { get; set; }

        public string Criador { get; set; }

        public string Linguagem { get; set; }

        public string CampoDeAplicacao { get; set; }

        public string TipoDePrograma { get; set; }

        public DateTime? DataDaCriacao { get; set; }

        public string RegimeDeGuarda { get; set; }

        public string Requerente { get; set; }

        public string Redacao { get; set; }

        public DateTime? DataDaProrrogacao { get; set; }

        public string ClassificacaoNacional { get; set; }

        public string CodigoDoDespachoAnterior { get; set; }
    }
}