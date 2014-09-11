using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class RadicalMarcas : IRadicalMarcas
    {
        public long? IdRadicalMarca { get; set; }

        public string DescricaoRadical
        { get; set; }

        public long? IdMarca
        { get; set; }

        public NCL NCL
        { get; set; }
    }
}
