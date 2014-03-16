using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Schedule;
using MP.Interfaces.Negocio.Schedules;
using MP.Interfaces.Servicos;

namespace MP.Negocio.Schedules
{
    [Serializable]
    public class ScheduleManutencaoDePatentes : Schedule, IScheduleManutencaoDePatentes
    {
        protected override void Inicialize()
        {
            
        }

        protected override void ExecuteTarefa()
        {
            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeInterfaceComModuloFinanceiro>())
                    servico.ProcureEAgendeItemDeRecebimentoDePatentesVencidasNoMes();
            }
            catch (Exception ex)
            {
                Logger.GetInstancia().Erro("Ocorreu um erro ao tentar ProcureEAgendeItemDeRecebimentoDePatentesVencidasNoMes", ex);

            }
        }

        public override string Nome
        {
            get { return "Schedule para criar automaticamente as manutenções de patentes."; }
        }
    }
}
