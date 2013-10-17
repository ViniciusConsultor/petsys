using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;
using MP.Interfaces.Negocio.Filtros.Patentes;

namespace MP.Negocio.Filtros.Patentes
{
    [Serializable]
    public class FiltroPatenteSemFiltro : Filtro, IFiltroPatenteSemFiltro
    {
        public override string ObtenhaQuery()
        {
            return string.Empty;
        }
    }
}