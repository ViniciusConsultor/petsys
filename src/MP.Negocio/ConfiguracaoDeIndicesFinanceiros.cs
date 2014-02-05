using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class ConfiguracaoDeIndicesFinanceiros : IConfiguracaoDeIndicesFinanceiros
    {
        public double? ValorDoSalarioMinimo
        {
            get; set; 
        }
    }
}
