using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class RadicalPatente : IRadicalPatente
    {
        public long? IdRadicalPatente { get; set; }

        public string Colidencia { get; set; }

        public long? IdPatente { get; set; }

        //Está propriedade é utilizada somente na leitura da revista de patente
        public string Classificacao { get; set; }
    }
}
