using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MP.Interfaces.Negocio;

namespace MP.Client
{
    public class Util
    {

        public static bool PeriodoEhTrimestreSemestreOuAnual(Periodo periodo)
        {
           if (periodo == null) return false;

            return (periodo.Equals(Periodo.Trimestral) || periodo.Equals(Periodo.Semestral) ||
                    periodo.Equals(Periodo.Anual));

        }
    }
}