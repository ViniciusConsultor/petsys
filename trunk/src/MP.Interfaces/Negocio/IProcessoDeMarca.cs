using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IProcessoDeMarca
    {
        Nullable<long> IdProcessoDeMarca { get; set; }
        IMarcas Marca { get; set; }
        Nullable<long> Protocolo { get; set; } 
        Nullable<long> Processo { get; set; }
        DateTime DataDeEntrada { get; set; }
        Nullable<DateTime> DataDeConcessao { get; set; }
        bool ProcessoEhDeTerceiro { get; set; }
        IDespachoDeMarcas Despacho { get; set; }
        Nullable<DateTime> DataDeProrrogacao { get;}
        IProcurador Procurador { get; set; }
        SituacaoDoProcessoDeMarca SituacaoDoProcesso { get; set; }
    }
}
