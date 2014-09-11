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
        DateTime? DataDaProximaManutencao { get; set; }
        bool ManutencaoEstaVencida();
        bool ManutencaoVenceNoMes(Mes mes);
        double ObtenhaValorRealEmEspecie();
        DateTime ObtenhaProximaDataDeManutencao();
    }
}