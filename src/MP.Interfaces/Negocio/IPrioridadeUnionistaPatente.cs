using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;

namespace MP.Interfaces.Negocio
{
    public interface IPrioridadeUnionistaPatente
    {
        DateTime DataPrioridade { get; set; }
        string NumeroPrioridade { get; set; }
        IPais Pais { get; set; }
    }
}
