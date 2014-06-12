using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Negocio;
using MP.Interfaces.Negocio.Filtros.Patentes;

namespace MP.Negocio.Filtros.Patentes
{
    [Serializable]
    public class FiltroPatenteVencidaNoMes : Filtro, IFiltroPatenteVencidaNoMes
    {
        public override string ObtenhaQuery()
        {
            var dataProximoMes = DateTime.Now.AddMonths(1);
            var sql = new StringBuilder();

            sql.AppendLine("SELECT MP_PROCESSOPATENTE.IDPROCESSOPATENTE, MP_PROCESSOPATENTE.IDPATENTE IDDAPATENTE, MP_PROCESSOPATENTE.PROCESSO, ");
            sql.AppendLine("MP_PROCESSOPATENTE.DATADEPUBLICACAO, MP_PROCESSOPATENTE.DATADEDEPOSITO, MP_PROCESSOPATENTE.DATADECONCESSAO, MP_PROCESSOPATENTE.DATADEEXAME, ");
            sql.AppendLine("MP_PROCESSOPATENTE.DATADECADASTRO, MP_PROCESSOPATENTE.PROCESSODETERCEIRO, MP_PROCESSOPATENTE.IDPROCURADOR, ");
            sql.AppendLine("MP_PROCESSOPATENTE.EHESTRANGEIRO, MP_PROCESSOPATENTE.NUMEROPCT, MP_PROCESSOPATENTE.NUMEROWO, MP_PROCESSOPATENTE.DATAPUBLICACAOPCT, ");
            sql.AppendLine("MP_PROCESSOPATENTE.DATADEPOSITOPCT, MP_PROCESSOPATENTE.IDDESPACHO, MP_PROCESSOPATENTE.ATIVO, MP_PROCESSOPATENTE.IDPASTA, ");
            sql.AppendLine("MP_PROCESSOPATENTE.PAIS, MP_PASTA.NOME NOMEPASTA,  MP_PASTA.CODIGO CODIGOPASTA, MP_PATENTE.IDPATENTE ");
            sql.AppendLine(" FROM MP_PROCESSOPATENTE");
            sql.AppendLine(" INNER JOIN MP_PATENTE ON MP_PATENTE.IDPATENTE = MP_PROCESSOPATENTE.IDPATENTE");
            sql.AppendLine(" AND MP_PATENTE.PAGAMANUTENCAO = 1 AND MP_PATENTE.DATAPROXIMAMANUTENCAO BETWEEN ");
            sql.AppendLine(string.Concat(DateTime.Now.ToString("yyyyMMdd"), " AND ", dataProximoMes.ToString("yyyyMMdd")));
            sql.AppendLine(
                " AND MP_PROCESSOPATENTE.IDPROCESSOPATENTE NOT IN (select idconceito from MP_INTERFACEFN where CONCEITO = 'PATENTE' AND IDCONCEITO = IDPROCESSOPATENTE AND DATAVENCIMENTO = DATAPROXIMAMANUTENCAO  )");
            sql.AppendLine(" LEFT JOIN MP_PASTA ON MP_PASTA.ID = MP_PROCESSOPATENTE.IDPASTA");
            sql.AppendLine(" WHERE MP_PROCESSOPATENTE.ATIVO = 1");

            return sql.ToString();
        }
    }
}
