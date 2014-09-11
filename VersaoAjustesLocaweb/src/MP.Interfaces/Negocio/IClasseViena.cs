using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IClasseViena
    {
        IList<string> ListaDeCodigosClasseViena { get; set; }
        string EdicaoClasseViena { get; set; }
    }
}
