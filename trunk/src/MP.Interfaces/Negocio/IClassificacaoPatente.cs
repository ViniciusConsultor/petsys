using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IClassificacaoPatente
    {
        int Codigo { get; set; }
        string Classificacao { get; set; }
        string DescricaoClassificacao { get; set; }
        TipoClassificacaoPatente TipoClassificacao { get; set; }
    }
}
