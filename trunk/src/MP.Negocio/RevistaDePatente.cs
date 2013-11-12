using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class RevistaDePatente : IRevistaDePatente
    {
        public long? IdRevistaPatente { get; set; }

        public int NumeroRevistaPatente { get; set; }

        public DateTime DataPublicacao { get; set; }

        public DateTime DataProcessamento { get; set; }

        public long NumeroProcessoDePatente { get; set; }

        public string CodigoDespachoAnterior { get; set; }

        public string CodigoDespachoAtual { get; set; }

        public string Apostila { get; set; }

        public string TextoDoDespacho { get; set; }

        public bool Processada { get; set; }

        public string ExtensaoArquivo { get; set; }

        public DateTime DataDeConcessao { get; set; }

        public DateTime DataDeDeposito { get; set; } 
    }
}
