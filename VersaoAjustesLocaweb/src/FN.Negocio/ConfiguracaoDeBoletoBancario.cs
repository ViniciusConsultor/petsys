using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using FN.Interfaces.Negocio;

namespace FN.Negocio
{
    [Serializable]
    public class ConfiguracaoDeBoletoBancario : IConfiguracaoDeBoletoBancario
    {
        public string ImagemDeCabecalhoDoReciboDoSacado { get; set; }
        public ICedente Cedente { get; set; }
    }
}
