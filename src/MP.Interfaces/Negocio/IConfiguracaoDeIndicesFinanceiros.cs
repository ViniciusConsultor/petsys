using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IConfiguracaoDeIndicesFinanceiros
    {
        Nullable<double> ValorDoSalarioMinimo { get; set; }
    }
}
