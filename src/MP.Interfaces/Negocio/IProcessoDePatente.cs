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
        Nullable<DateTime> DataDaConcessao { get; set; }
        Nullable<DateTime> DataDaPublicacao { get; set; }
        Nullable<DateTime> DataDoDeposito { get; set; }
        Nullable<DateTime> DataDaVigencia { get; set; }
        Nullable<DateTime> DataDoExame { get; set; } 
        IProcurador Procurador { get; set; }
        bool ProcessoEhEstrangeiro { get; set; }
        bool ProcessoEhDeTerceiro { get; set; }
        bool Ativo { get; set; }
        IPCT PCT { get; set; }
    }
}