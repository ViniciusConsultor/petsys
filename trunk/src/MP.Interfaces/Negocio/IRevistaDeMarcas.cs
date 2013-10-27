using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IRevistaDeMarcas
    {
        long? IdRevistaMarcas { get; set; }
        int NumeroRevistaMarcas { get; set; }
        string DataPublicacao { get; set; }
        bool Processada { get; set; }
    }
}
