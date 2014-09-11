using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class PrioridadeUnionistaPatente : IPrioridadeUnionistaPatente
    {
        public long Identificador { get; set; }

        public DateTime? DataPrioridade { get; set; }

        public string NumeroPrioridade { get; set; }

        public IPais Pais { get; set; }
        
    }
}
