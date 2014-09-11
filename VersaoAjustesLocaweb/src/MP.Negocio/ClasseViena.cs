using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class ClasseViena : IClasseViena
    {
        public IList<string> ListaDeCodigosClasseViena
        { get; set; }

        public string EdicaoClasseViena { get; set; }
    }
}
