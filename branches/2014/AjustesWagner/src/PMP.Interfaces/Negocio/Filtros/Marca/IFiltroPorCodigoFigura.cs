using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMP.Interfaces.Negocio.Filtros.Marca
{
    public interface IFiltroPorCodigoFigura : IFiltroPMP
    {
        string NCL { get; set; }
    
    }
}
