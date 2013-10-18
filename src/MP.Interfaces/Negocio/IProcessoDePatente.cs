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
        Nullable<DateTime> DataDaConcessao { get; set; }
        IProcurador Procurador { get; set; }
        bool ProcessoEhEstrangeiro { get; set; }
        bool Ativo { get; set; }
        bool EhPCT { get; set; }
        string NumeroPCT { get; set; } 
    }
}



    //IDDESPACHO						BIGINT					NULL,
	
    //NUMEROWO						VARCHAR(20)				NULL,
    //DATA_DEPOSITOPCT				INT						NULL,
    //DATA_PUBLICACAOPCT				INT						NULL,