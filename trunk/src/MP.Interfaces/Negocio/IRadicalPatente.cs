using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IRadicalPatente
    {
        long? IdRadicalPatente { get; set; }
        string Colidencia { get; set; }
        long? IdPatente { get; set; }
    }
}
