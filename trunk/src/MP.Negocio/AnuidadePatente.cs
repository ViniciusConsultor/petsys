using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    public class AnuidadePatente : IAnuidadePatente
    {
        public long Identificador { get; set; }

        public string DescricaoAnuidade { get; set; }

        public DateTime? DataLancamento { get; set; }

        public DateTime? DataVencimento { get; set; }

        public DateTime? DataPagamento { get; set; }

        public double ValorPagamento { get; set; }

        public bool AnuidadePaga { get; set; }

        public bool PedidoExame { get; set; }

        public DateTime? DataVencimentoSemMulta { get; set; }

        public DateTime? DataVencimentoComMulta { get; set; }
    }
}
