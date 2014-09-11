using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FN.Interfaces.Negocio;

namespace FN.Negocio
{
    public class ConfiguracaoGeralFinanceiro : IConfiguracaoGeralFinanceiro
    {
        public string InstrucoesDoBoleto { get; set; }
    }
}
