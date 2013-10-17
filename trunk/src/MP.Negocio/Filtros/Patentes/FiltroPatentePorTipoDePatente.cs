using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Negocio;
using MP.Interfaces.Negocio.Filtros.Patentes;

namespace MP.Negocio.Filtros.Patentes
{
    public class FiltroPatentePorTipoDePatente : Filtro, IFiltroPatentePorTipoDePatente
    {
        public override string ObtenhaQuery()
        {
            throw new NotImplementedException();
        }
    }
}
