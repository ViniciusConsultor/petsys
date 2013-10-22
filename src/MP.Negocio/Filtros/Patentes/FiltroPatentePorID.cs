using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Negocio;
using MP.Interfaces.Negocio.Filtros.Patentes;

namespace MP.Negocio.Filtros.Patentes
{
    [Serializable]
    public class FiltroPatentePorID : Filtro, IFiltroPatentePorID
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT MP_PROCESSOPATENTE.IDPROCESSOPATENTE, MP_PROCESSOPATENTE.IDPATENTE IDDAPATENTE, MP_PROCESSOPATENTE.PROCESSO, ");
            sql.AppendLine("MP_PROCESSOPATENTE.DATAENTRADA, MP_PROCESSOPATENTE.PROCESSODETERCEIRO, MP_PROCESSOPATENTE.IDPROCURADOR, ");
            sql.AppendLine("MP_PROCESSOPATENTE.EHESTRANGEIRO, MP_PROCESSOPATENTE.ATIVO, ");
            sql.AppendLine("MP_PATENTE.IDPATENTE ");
            sql.AppendLine(" FROM MP_PROCESSOPATENTE, MP_PATENTE");
            sql.AppendLine(" WHERE MP_PROCESSOPATENTE.IDPATENTE = MP_PATENTE.IDPATENTE ");
            sql.AppendLine(" AND " + ObtenhaFiltroMontado("MP_PROCESSOPATENTE.IDPROCESSOPATENTE", false));

            return sql.ToString();
        }
    }
}
