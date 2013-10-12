using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    public class ClassificacaoPatente : IClassificacaoPatente
    {
        public int Codigo { get; set; }

        public string Classificacao { get; set; }

        public string DescricaoClassificacao { get; set; }

        public TipoClassificacaoPatente TipoClassificacao { get; set; }
    }
}
