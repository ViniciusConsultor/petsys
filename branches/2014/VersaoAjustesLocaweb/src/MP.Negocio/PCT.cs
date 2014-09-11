using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class PCT : IPCT
    {
        public string Numero { get; set; }
        public DateTime? DataDoDeposito { get; set; }
        public string NumeroWO { get; set; }
        public DateTime? DataDaPublicacao { get; set; }
    }
}
