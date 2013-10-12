using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    public class TitularPatente : ITitularPatente
    {
        public IInventor Iventor { get; set; }

        public IProcurador Procurador { get; set; }

        public string ContatoTitular { get; set; }
    }
}
