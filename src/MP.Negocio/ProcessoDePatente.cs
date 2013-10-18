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

        public DateTime? DataDaConcessao
        {
            get;
            set;
            
        }

        public IProcurador Procurador
        {
            get;
            set;
        }

        public bool ProcessoEhEstrangeiro
        {
            get; set;
        }

        public bool Ativo
        {
            get; set; 
        }

        public bool EhPCT 
        { 
            get; set;
        }

        public string NumeroPCT
        {
            get; 
            set;
        }
    }
}