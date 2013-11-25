using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio.Repositorios
{
    public interface IRepositorioDeDespachodeMarcas
    {
        IDespachoDeMarcas obtenhaDespachoDeMarcasPeloId(long id);
    }
}
