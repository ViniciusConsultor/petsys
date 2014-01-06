using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IPasta
    {
        long? ID { get; set; }
        string Codigo { get; set; }
        string Nome { get; set; }
    }
}
