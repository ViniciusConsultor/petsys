using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IClassificacaoPatente
    {
        long Identificador { get; set; }
        string Classificacao { get; set; }
        string DescricaoClassificacao { get; set; }
        TipoClassificacaoPatente TipoClassificacao { get; set; }
    }
}
