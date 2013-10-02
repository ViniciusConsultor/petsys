using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface ITipoDePatente
    {
        long? IdTipoPatente { get; set; }
        string DescricaoTipoDePatente { get; set; }
        char SiglaTipo { get; set; }
        int TempoInicioAnos { get; set;}
        int QuantidadePagto { get; set; }
        int TempoEntrePagto { get; set; }
        int SequenciaInicioPagto { get; set; }
        bool TemPagtoIntermediario { get; set; }
        int InicioIntermedSequencia { get; set; }
        int QuantidadePagtoIntermed { get; set; }
        int TempoEntrePagtoIntermed { get; set; }
        string DescricaoPagto { get; set; }
        string DescricaoPagtoIntermed { get; set; }
        bool TemPedExame { get; set; }
    }
}
