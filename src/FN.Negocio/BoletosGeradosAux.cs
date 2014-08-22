using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FN.Interfaces.Negocio;

namespace FN.Negocio
{
    [Serializable]
    public class BoletosGeradosAux : IBoletosGeradosAux
    {
        public long? ID { get; set; }
        
        public long? ProximoNossoNumero
        { get; set; }

        public long? IDCEDENTE { get; set; }
    }
}
