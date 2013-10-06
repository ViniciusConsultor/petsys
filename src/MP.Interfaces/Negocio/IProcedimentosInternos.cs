using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IProcedimentosInternos
    {
        long? IdTipoAndamentoInterno { get; set; }
        string DescricaoTipo { get; set; }
    }
}
