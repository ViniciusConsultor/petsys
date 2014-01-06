using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class Pasta : IPasta
    {
        public long? ID { get; set; }

        public string Codigo { get; set;}

        public string Nome { get; set; }
    }
}
