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
        string Processo { get; set; }
        DateTime DataDeEntrada { get; set; }
        bool ProcessoEhDeTerceiro { get; set; }
        Nullable<DateTime> DataDaConcessao { get; set; }
        IProcurador Procurador { get; set; }
        bool ProcessoEhEstrangeiro { get; set; }
        bool Ativo { get; set; }
        bool EhPCT { get; set; }
        string NumeroPCT { get; set; } 
    }
}