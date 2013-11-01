using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Negocio;
using MP.Interfaces.Negocio.Filtros.Patentes;

namespace MP.Negocio.Filtros.Patentes
{
    [Serializable]
    public class FiltroPatentePorDataDeEntrada : Filtro, IFiltroPatentePorDataDeEntrada
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT MP_PROCESSOPATENTE.IDPROCESSOPATENTE, MP_PROCESSOPATENTE.IDPATENTE IDDAPATENTE, MP_PROCESSOPATENTE.PROCESSO, ");
            sql.AppendLine("MP_PROCESSOPATENTE.DATADEPUBLICACAO, MP_PROCESSOPATENTE.DATADEDEPOSITO, MP_PROCESSOPATENTE.DATADECONCESSAO, MP_PROCESSOPATENTE.DATADEEXAME, ");
            sql.AppendLine("MP_PROCESSOPATENTE.DATADECADASTRO, MP_PROCESSOPATENTE.PROCESSODETERCEIRO, MP_PROCESSOPATENTE.IDPROCURADOR, ");
            sql.AppendLine("MP_PROCESSOPATENTE.EHESTRANGEIRO, MP_PROCESSOPATENTE.NUMEROPCT, MP_PROCESSOPATENTE.NUMEROWO, MP_PROCESSOPATENTE.DATAPUBLICACAOPCT, MP_PROCESSOPATENTE.DATADEPOSITOPCT, MP_PROCESSOPATENTE.IDDESPACHO, MP_PROCESSOPATENTE.ATIVO, ");
            sql.AppendLine("MP_PATENTE.IDPATENTE ");
            sql.AppendLine(" FROM MP_PROCESSOPATENTE, MP_PATENTE");
            sql.AppendLine(" WHERE MP_PROCESSOPATENTE.IDPATENTE = MP_PATENTE.IDPATENTE ");
            sql.AppendLine(" AND " + ObtenhaFiltroMontado("MP_PROCESSOPATENTE.DATADECADASTRO", false));
            
            return sql.ToString();
        }
    }
}