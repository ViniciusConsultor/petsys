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
        long Processo { get; set; }
        DateTime DataDoCadastro { get; set; }
        Nullable<DateTime> DataDoDeposito { get; set; }
        Nullable<DateTime> DataDeConcessao { get; set; }
        Nullable<DateTime> DataDaVigencia { get; }
        bool ProcessoEhDeTerceiro { get; set; }
        IDespachoDeMarcas Despacho { get; set; }
        string TextoComplementarDoDespacho { get; set; }
        IProcurador Procurador { get; set; }
        string Apostila { get; set; }
        bool Ativo { get; set; }
      

    }
}