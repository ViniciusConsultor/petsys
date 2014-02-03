using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class BoletosGeradosAux : IBoletosGeradosAux
    {
        public long? ID { get; set; }

        public long? ProximoNumeroBoleto
        { get; set; }

        public long? ProximoNossoNumero
        { get; set; }
    }
}
