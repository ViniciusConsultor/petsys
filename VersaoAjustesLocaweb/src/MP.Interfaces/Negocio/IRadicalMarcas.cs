using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IRadicalMarcas
    {
        long? IdRadicalMarca { get; set; }
        string DescricaoRadical { get; set; }
        NCL NCL { get; set; }
    }
}