using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class ProcessoDePatente : IProcessoDePatente
    {
        public long? IdProcessoDePatente { get;set; }
        public IPatente Patente {get; set;}
        public string Processo {get; set;}
        public DateTime DataDoCadastro { get; set; }
        public DateTime DataDeEntrada {get; set;}
        public bool ProcessoEhDeTerceiro {get; set;}
        public DateTime? DataDaConcessao {get;set;}
        public DateTime? DataDaPublicacao { get; set; }
        public DateTime? DataDoDeposito { get; set; }
        public DateTime? DataDaVigencia { get; set; }
        public DateTime? DataDoExame { get; set; }
        public IProcurador Procurador {get;set;}
        public bool ProcessoEhEstrangeiro {get; set;}
        public bool Ativo {get; set; }
        public IPCT PCT { get; set; }
        public IDespachoDePatentes Despacho { get; set; }
    }
}