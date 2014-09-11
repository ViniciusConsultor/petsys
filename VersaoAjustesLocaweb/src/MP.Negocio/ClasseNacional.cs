using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class ClasseNacional : IClasseNacional
    {
        public string CodigoClasseNacional { get; set; }

        public string EspecificacaoClasseNacional
        { get; set; }

        public IList<string> listaDeCodigosDeSubClasse
        { get; set; }
    }
}
