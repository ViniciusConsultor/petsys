using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;

namespace MP.Interfaces.Negocio.Filtros.Patentes
{
    public interface IFiltroRelatorioDeManutencoesDaPatente : IFiltro
    {
        IList<ICliente> ClientesSelecionados { get; set; }
        DateTime? DataDeInicio { get; set; }
        DateTime? DataTermino { get; set; }
    }
}
