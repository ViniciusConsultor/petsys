using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Schedule;
using MP.Interfaces.Negocio.Schedules;

namespace MP.Negocio.Schedules
{
    [Serializable]
    public class ScheduleManutencaoDeMarcas : Schedule, IScheduleManutencaoDeMarcas
    {
        protected override void Inicialize()
        {
         
        }

        protected override void ExecuteTarefa()
        {
           
            
        }

        public override string Nome
        {
            get { return "Schedule para criar automaticamente as manutenções de marcas."; }
        }
    }
}
