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
        public long? IdProcessoDePatente
        {
            get;set; 
        }

        public IPatente Patente
        {
            get; set; 
        }

        public long? Processo
        {
            get; set;
        }

        public long? Protocolo
        {
            get; set; 
        }

        public DateTime DataDeEntrada
        {
            get; set;
        }

        public bool ProcessoEhDeTerceiro
        {
            get; set;
        }
    }
}
