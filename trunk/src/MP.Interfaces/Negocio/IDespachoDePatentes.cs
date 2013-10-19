using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IDespachoDePatentes
    {
        long? IdDespachoDePatente { get; set; }
        string CodigoDespachoDePatente { get; set; }
        string DetalheDespachoDePatente { get; set; }
        string DescricaoDespachoDePatente { get; set; }
        SituacaoDoProcessoDePatente SituacaoDoProcessoDePatente { get; set; }
    }
}
