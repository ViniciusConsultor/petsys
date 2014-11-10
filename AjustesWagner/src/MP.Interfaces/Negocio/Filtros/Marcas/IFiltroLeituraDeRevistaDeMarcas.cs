using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio.Filtros.Marcas
{
    public interface IFiltroLeituraDeRevistaDeMarcas
    {
        string NumeroDoProcesso { get; set; }
        string UF { get; set; }
        string Procurador { get; set; }
        IDespachoDeMarcas Despacho { get; set; }
    }
}
