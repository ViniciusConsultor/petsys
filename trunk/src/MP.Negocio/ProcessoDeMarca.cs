using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class ProcessoDeMarca : IProcessoDeMarca
    {
        public long? IdProcessoDeMarca {get; set; }
        public IMarcas Marca {get; set; }
        public long Processo {get; set; }
        public DateTime DataDoCadastro { get; set; }
        public DateTime? DataDoDeposito { get; set; }
        public DateTime? DataDeConcessao {get; set; }
        public DateTime? DataDaVigencia
        {
            get
            {
                if (DataDoDeposito.HasValue)
                    return DataDoDeposito.Value.AddYears(10);
                
                return null;
            }
        }
        public bool ProcessoEhDeTerceiro {get; set; }
        public IDespachoDeMarcas Despacho {get; set;}
        public string TextoComplementarDoDespacho { get; set; }
        public IProcurador Procurador {get; set; }
        public string Apostila { get; set; }
        public bool Ativo {get; set; }
    }
}