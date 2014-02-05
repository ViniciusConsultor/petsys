using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class ConfiguracaoDeModulo : IConfiguracaoDeModulo
    {
        public IConfiguracaoDeBoletoBancario ConfiguracaoDeBoletoBancario { get; set; }
        public IConfiguracaoDeIndicesFinanceiros ConfiguracaoDeIndicesFinanceiros { get; set; }
    }
}
