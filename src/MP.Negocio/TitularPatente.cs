using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class TitularPatente : ITitularPatente
    {
        public long Identificador { get; set; }

        public IInventor Iventor { get; set; }

        public string ContatoTitular { get; set; }
    }
}
