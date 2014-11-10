using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;

namespace MP.Negocio.Filtros.Marcas
{
    [Serializable]
    public class FiltroLeituraDeRevistaDeMarcas : IFiltroLeituraDeRevistaDeMarcas
    {
        public string NumeroDoProcesso { get; set; }

        public string UF
        { get; set; }

        public string Procurador
        { get; set; }

        public IDespachoDeMarcas Despacho
        { get; set; }
    }
}
