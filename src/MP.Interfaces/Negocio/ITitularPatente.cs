using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface ITitularPatente
    {
        IInventor Iventor { get; set; }
        IProcurador Procurador { get; set; }
        string ContatoTitular { get; set; }
    }
}
