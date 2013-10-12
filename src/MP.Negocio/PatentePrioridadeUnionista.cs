using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    public class PatentePrioridadeUnionista : IPatentePrioridadeUnionista
    {
        public DateTime DataPrioridade { get; set; }

        public string NumeroPrioridade { get; set; }

        public int IdPais { get; set; }
    }
}
