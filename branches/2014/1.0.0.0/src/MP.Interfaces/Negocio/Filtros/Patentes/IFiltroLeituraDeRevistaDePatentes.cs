using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio.Filtros.Patentes
{
    public interface IFiltroLeituraDeRevistaDePatentes
    {
        string NumeroDoProcesso { get; set; }
        string Depositante { get; set; }
        string Titular { get; set; }
        IProcurador Procurador { get; set; }
    }
}
