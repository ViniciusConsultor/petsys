using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Negocio;
using MP.Interfaces.Negocio.Filtros.Patentes;

namespace MP.Negocio.Filtros.Patentes
{
    [Serializable]
    public class FiltroPatentePorNatureza : Filtro, IFiltroPatentePorNatureza
    {
        public override string ObtenhaQuery()
        {
             var sql = new StringBuilder();
             sql.AppendLine("SELECT MP_PROCESSOPATENTE.IDPROCESSOPATENTE, MP_PROCESSOPATENTE.IDPATENTE IDDAPATENTE, MP_PROCESSOPATENTE.PROCESSO, ");
             sql.AppendLine("MP_PROCESSOPATENTE.DATADEPUBLICACAO, MP_PROCESSOPATENTE.DATADEDEPOSITO, MP_PROCESSOPATENTE.DATADECONCESSAO, MP_PROCESSOPATENTE.DATADEEXAME, ");
             sql.AppendLine("MP_PROCESSOPATENTE.DATADECADASTRO, MP_PROCESSOPATENTE.PROCESSODETERCEIRO, MP_PROCESSOPATENTE.IDPROCURADOR, ");
             sql.AppendLine("MP_PROCESSOPATENTE.EHESTRANGEIRO, MP_PROCESSOPATENTE.NUMEROPCT, MP_PROCESSOPATENTE.NUMEROWO, MP_PROCESSOPATENTE.DATAPUBLICACAOPCT, ");
             sql.AppendLine("MP_PROCESSOPATENTE.DATADEPOSITOPCT, MP_PROCESSOPATENTE.IDDESPACHO, MP_PROCESSOPATENTE.ATIVO, MP_PROCESSOPATENTE.IDPASTA, MP_PASTA.NOME NOMEPASTA, MP_PASTA.CODIGO CODIGOPASTA, ");
             sql.AppendLine("MP_PATENTE.IDPATENTE, MP_PROCESSOPATENTE.PAIS ");
             sql.AppendLine(" FROM MP_PROCESSOPATENTE");
             sql.AppendLine(" INNER JOIN MP_PATENTE ON MP_PATENTE.IDPATENTE = MP_PROCESSOPATENTE.IDPATENTE");
             sql.AppendLine(" LEFT JOIN MP_PASTA ON MP_PASTA.ID = MP_PROCESSOPATENTE.IDPASTA");
             sql.AppendLine(" WHERE " + ObtenhaFiltroMontado("MP_PATENTE.IDNATUREZAPATENTE", false));

            return sql.ToString();
        }
    }
}
