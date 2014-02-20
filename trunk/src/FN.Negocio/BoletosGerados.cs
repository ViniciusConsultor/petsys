using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using FN.Interfaces.Negocio;

namespace FN.Negocio
{
    [Serializable]
    public class BoletosGerados : IBoletosGerados
    {
        public long? ID { get; set; }

        public long? NumeroBoleto { get; set; }

        public long? NossoNumero { get; set; }

        public ICliente Cliente { get; set; }

        public double Valor { get; set; }

        public DateTime? DataGeracao { get; set; }

        public DateTime? DataVencimento { get; set; }

        public long? NumeroProcesso { get; set; }

        public string Observacao { get; set; }
    }
}
