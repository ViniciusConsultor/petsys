﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Patentes;

namespace MP.Negocio.Filtros.Patentes
{
    [Serializable]
    public class FiltroLeituraDeRevistaDePatentes : IFiltroLeituraDeRevistaDePatentes
    {
        public IDictionary<EnumeradorFiltroPatente, string> ValoresDoFiltro { get; set; }
    }
}