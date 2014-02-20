using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.FN.Negocio;
using Compartilhados.Interfaces.FN.Servicos;
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

            IConfiguracaoDeIndicesFinanceiros configuracao;

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConfiguracaoDeIndicesFinanceiros>())
                    configuracao = servico.Obtenha();
            }
            catch (Exception ex)
            {
                Logger.GetInstancia().Erro("Erro ao obter o valor real em espécie da manuntenção. Provavelmente o módulo financeiro não está instalado.", ex);
                return 0;
            }
            
            if (configuracao != null)
            {
                var valorSalarioMinimoVigente = configuracao.ValorDoSalarioMinimo;

                if (valorSalarioMinimoVigente.HasValue)
                    return ValorDeCobranca * valorSalarioMinimoVigente.Value;
            }
            return 0;
        }
    }
}
