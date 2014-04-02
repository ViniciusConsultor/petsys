using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;

namespace MP.Interfaces.Negocio.Filtros.Patentes
{
    public interface IFiltroRelatorioGeralPatente : IFiltro
    {
        string TipoNatureza { get; set; }
        TipoClassificacaoPatente ClassificacaoPatente { get; set; }
        string TipoDeOrigem { get; set; }
        string StatusDoProcesso { get; set; }
        ICliente Cliente { get; set; }
        ITitular Titular { get; set; }
        IInventor Inventor { get; set; }
    }
}
