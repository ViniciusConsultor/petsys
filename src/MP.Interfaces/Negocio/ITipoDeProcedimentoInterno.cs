using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface ITipoDeProcedimentoInterno
    {
        long? Id { get; set; }
        string Descricao { get; set; }
    }
}
