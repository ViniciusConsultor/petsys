﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    interface IConfiguracaoDeModulo
    {
        IConfiguracaoDeBoletoBancario ConfiguracaoDeBoletoBancario { get; set; }
        IConfiguracaoDeIndicesFinanceiros ConfiguracaoDeIndicesFinanceiros { get; set; }
    }
}
