using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IClasseNacional
    {
        string CodigoClasseNacional { get; set; }
        string EspecificacaoClasseNacional { get; set; }
        IList<string> listaDeCodigosDeSubClasse { get; set; } 
    }
}
