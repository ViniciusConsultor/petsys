using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.FN.Negocio;

namespace FN.Negocio
{
    [Serializable]
    public class ConfiguracaoDeIndicesFinanceiros : IConfiguracaoDeIndicesFinanceiros
    {
        public double? ValorDoSalarioMinimo {get ; set;}
        
    }
}
