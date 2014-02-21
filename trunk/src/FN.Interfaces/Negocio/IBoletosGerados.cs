using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;

namespace FN.Interfaces.Negocio
{
    public interface IBoletosGerados
    {
        long? ID { get; set; }
        string NumeroBoleto { get; set; }
        long? NossoNumero { get; set; }
        ICliente Cliente { get; set; }
        double Valor { get; set; }
        DateTime? DataGeracao { get; set; }
        DateTime? DataVencimento { get; set; }
        string Observacao { get; set; }
    }
}
