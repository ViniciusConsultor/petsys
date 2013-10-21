using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface ITitularPatente
    {
        long Identificador { get; set; }
        string ContatoTitular { get; set; }
        IInventor Iventor { get; set; }
    }
}
