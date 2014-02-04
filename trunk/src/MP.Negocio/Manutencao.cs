using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class Manutencao : IManutencao
    {
        public Periodo Periodo { get; set; }
        public FormaCobrancaManutencao FormaDeCobranca { get; set; }
        public double ValorDeCobranca { get; set; }
        public Mes MesQueIniciaCobranca { get; set; }
        public double ObtenhaValorRealEmEspecie()
        {
            if (FormaDeCobranca.Equals(FormaCobrancaManutencao.ValorFixo)) return ValorDeCobranca;

            //TODO: Aqui faz o calculo buscando o valor do SM
            return 0;
        }
    }
}
