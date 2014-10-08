using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;

namespace MP.Interfaces.Negocio
{
    public interface IProcessoDePatente
    {
        Nullable<long> IdProcessoDePatente { get; set; }
        IPatente Patente { get; set; }
        string Processo { get; set; }
        DateTime DataDoCadastro { get; set; }
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
        IDespachoDePatentes Despacho { get; set; }
        IPasta Pasta { get; set; }
        IPais Pais { get; set; }
        string NumeroDoProcessoFormatado { get; }
        IList<IEvento> Eventos { get; set; } 
    }
}