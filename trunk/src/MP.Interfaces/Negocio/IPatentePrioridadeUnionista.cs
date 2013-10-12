using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IPatentePrioridadeUnionista
    {
        DateTime DataPrioridade { get; set; }
        string NumeroPrioridade { get; set; }
        int IdPais { get; set; }
    }
}
