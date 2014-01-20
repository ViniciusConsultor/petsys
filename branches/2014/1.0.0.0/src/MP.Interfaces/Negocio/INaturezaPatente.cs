using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface INaturezaPatente
    {
        long? IdNaturezaPatente { get; set; }
        string DescricaoNaturezaPatente { get; set; }
        string SiglaNatureza { get; set; }
        int TempoInicioAnos { get; set; }
        int QuantidadePagamento { get; set; }
        int TempoEntrePagamento { get; set; }
        int SequenciaInicioPagamento { get; set; }
        bool TemPagamentoIntermediario { get; set; }
        int InicioIntermediarioSequencia { get; set; }
        int QuantidadePagamentoIntermediario { get; set; }
        int TempoEntrePagamentoIntermediario { get; set; }
        string DescricaoPagamento { get; set; }
        string DescricaoPagamentoIntermediario { get; set; }
        bool TemPedidoDeExame { get; set; }
    }
}
