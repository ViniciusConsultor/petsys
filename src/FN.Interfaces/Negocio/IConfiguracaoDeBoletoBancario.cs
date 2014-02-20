﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;

namespace FN.Interfaces.Negocio
{
    public interface IConfiguracaoDeBoletoBancario
    {
        string ImagemDeCabecalhoDoReciboDoSacado { get; set; }
        ICedente Cedente { get; set; }
    }
}