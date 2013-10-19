using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class DespachoDePatentes : IDespachoDePatentes
    {
        public long? IdDespachoDePatente { get; set; }

        public string CodigoDespachoDePatente { get; set; }

        public string DetalheDespachoDePatente { get; set; }

        public string DescricaoDespachoDePatente { get; set; }

        public SituacaoDoProcessoDePatente SituacaoDoProcessoDePatente { get; set; }
    }
}
