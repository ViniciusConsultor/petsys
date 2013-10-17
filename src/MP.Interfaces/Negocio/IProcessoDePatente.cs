using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IProcessoDePatente
    {
        Nullable<long> IdProcessoDePatente { get; set; }
        IPatente Patente { get; set; }
        Nullable<long> Processo { get; set; }
        Nullable<long> Protocolo { get; set; }
        DateTime DataDeEntrada { get; set; }
        bool ProcessoEhDeTerceiro { get; set; }
    }
}
