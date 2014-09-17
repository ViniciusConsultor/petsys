using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Schedule;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Schedules;
using MP.Interfaces.Servicos;

namespace MP.Negocio.Schedules
{
    [Serializable]
    public class ScheduleManutencaoDeMarcas : Schedule, IScheduleManutencaoDeMarcas
    {
        private ICredencial _credencial;

        protected override void Inicialize(ICredencial credencial)
        {
            _credencial = credencial;
        }

        protected override void ExecuteTarefa()
        {
            try
            {
                var fabrica = FabricaGenerica.GetInstancia();
               
                using (var servico = fabrica.CrieObjeto<IServicoDeInterfaceComModuloFinanceiro>(_credencial))
                    servico.ProcureEAgendeItemDeRecebimentoDeMarcasVencidasNoMes();
            }
            catch (Exception ex)
            {
                Logger.GetInstancia().Erro("Ocorreu um erro ao tentar ProcureEAgendeItemDeRecebimentoDeMarcasVencidasNoMes", ex);
                
            }
            
        }

        public override string Nome
        {
            get { return "Schedule para criar automaticamente as manutenções de marcas."; }
        }
    }
}
