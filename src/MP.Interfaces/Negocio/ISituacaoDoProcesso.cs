using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface ISituacaoDoProcesso
    {
        long? IdSituacaoProcesso { get; set; }
        string DescricaoSituacao { get; set; }
    }
}
