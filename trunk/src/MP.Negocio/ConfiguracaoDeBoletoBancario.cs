using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class ConfiguracaoDeBoletoBancario : IConfiguracaoDeBoletoBancario
    {
        public string ImagemDeCabecalhoDoReciboDoSacado { get; set; }
        public ICedente Cedente { get; set; }
    }
}
