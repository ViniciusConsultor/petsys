using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IManutencao
    {
        Periodo Periodo { get; set; }
        FormaCobrancaManutencao FormaDeCobranca { get; set; }
        double ValorDeCobranca { get; set; }
        Mes MesQueIniciaCobranca { get; set; }
        DateTime? DataDaPrimeiraManutencao { get; set; }
    }
}
