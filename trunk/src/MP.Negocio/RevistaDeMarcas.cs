using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class RevistaDeMarcas : IRevistaDeMarcas
    {
        public long? IdRevistaMarcas { get; set; }

        public int NumeroRevistaMarcas
        { get; set; }

        public string DataPublicacao
        { get; set; }

        public bool Processada
        { get; set; }
    }
}
