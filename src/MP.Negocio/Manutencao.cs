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
        
        public DateTime? DataDaProximaManutencao
        { get; set; }

        public bool ManutencaoEstaVencida()
        {
            if (!DataDaProximaManutencao.HasValue) return false;

            return Convert.ToInt32(DataDaProximaManutencao.Value.ToString("yyyyMMdd")) <
                   Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
        }

        public bool ManutencaoVenceNoMes(Mes mes)
        {
            if (!DataDaProximaManutencao.HasValue) return false;

            return DataDaProximaManutencao.Value.Month == mes.Codigo;
        }

        public double ObtenhaValorRealEmEspecie()
        {
            if (FormaDeCobranca.Equals(FormaCobrancaManutencao.ValorFixo)) return ValorDeCobranca;
            
            //IConfiguracaoDeModulo configuracaoDeModulo;

            //using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConfiguracoesDoModulo>())
            //    configuracaoDeModulo = servico.ObtenhaConfiguracao();

            //if (configuracaoDeModulo != null && configuracaoDeModulo.ConfiguracaoDeIndicesFinanceiros !=null) 
            //{
            //    var valorSalarioMinimoVigente = configuracaoDeModulo.ConfiguracaoDeIndicesFinanceiros.ValorDoSalarioMinimo ;
             
            //    if (valorSalarioMinimoVigente.HasValue)
            //        return ValorDeCobranca * valorSalarioMinimoVigente.Value;
            //}
            return 0;
        }
    }
}
