﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IAnuidadePatente
    {
        long Identificador { get; set; }
        string DescricaoAnuidade { get; set; }
        DateTime? DataLancamento { get; set; }
        DateTime? DataVencimento { get; set; }
        DateTime? DataPagamento { get; set; }
        double ValorPagamento { get; set; }
        bool AnuidadePaga { get; set; }
        bool PedidoExame { get; set; }
        DateTime? DataVencimentoSemMulta { get; set; }
        DateTime? DataVencimentoComMulta { get; set; }
    }
}
