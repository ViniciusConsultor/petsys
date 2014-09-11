using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio.Filtros.Patentes
{
    public interface IFiltroLeituraDeRevistaDePatentes
    {
        EnumeradorFiltroPatente EnumeradorFiltro { get; set; }
        string ValorFiltro { get; set; }
    }
}
