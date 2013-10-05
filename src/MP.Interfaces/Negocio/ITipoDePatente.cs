﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface ITipoDePatente
    {
        long? IdTipoDePatente { get; set; }
        string DescricaoTipoDePatente { get; set; }
        string SiglaTipo { get; set; }
        int TempoInicioAnos { get; set;}
        int QuantidadePagamento { get; set; }
        int TempoEntrePagamento { get; set; }
        int SequenciaInicioPagamento { get; set; }
        string TemPagamentoIntermediario { get; set; }
        int InicioIntermediarioSequencia { get; set; }
        int QuantidadePagamentoIntermediario { get; set; }
        int TempoEntrePagamentoIntermediario { get; set; }
        string DescricaoPagamento { get; set; }
        string DescricaoPagamentoIntermediario { get; set; }
        string TemPedidoDeExame { get; set; }
    }
}
