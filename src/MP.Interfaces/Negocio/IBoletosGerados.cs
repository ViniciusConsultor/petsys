using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;

namespace MP.Interfaces.Negocio
{
    public interface IBoletosGerados
    {
        long? ID { get; set; }
        long? NumeroBoleto { get; set; }
        long? NossoNumero { get; set; }
        ICliente Cliente { get; set; }
        double Valor { get; set; }
        DateTime? DataGeracao { get; set; }
        DateTime? DataVencimento { get; set; }
        long? NumeroProcesso { get; set; }
        string Observacao { get; set; }
    }
}
