using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Negocio
{
    [Serializable]
    public class Manutencao : IManutencao
    {
        public Periodo Periodo { get; set; }
        public FormaCobrancaManutencao FormaDeCobranca { get; set; }
        public double ValorDeCobranca { get; set; }
        public Mes MesQueIniciaCobranca { get; set; }

        public DateTime? DataDaPrimeiraManutencao
        { get; set; }

        public double ObtenhaValorRealEmEspecie()
        {
            if (FormaDeCobranca.Equals(FormaCobrancaManutencao.ValorFixo)) return ValorDeCobranca;
            
            IConfiguracaoDeModulo configuracaoDeModulo;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConfiguracoesDoModulo>())
                configuracaoDeModulo = servico.ObtenhaConfiguracao();

            if (configuracaoDeModulo != null && configuracaoDeModulo.ConfiguracaoDeIndicesFinanceiros !=null) 
            {
                var valorSalarioMinimoVigente = configuracaoDeModulo.ConfiguracaoDeIndicesFinanceiros.ValorDoSalarioMinimo ;
             
                if (valorSalarioMinimoVigente.HasValue)
                    return ValorDeCobranca * valorSalarioMinimoVigente.Value;
            }
            return 0;
        }
    }
}
