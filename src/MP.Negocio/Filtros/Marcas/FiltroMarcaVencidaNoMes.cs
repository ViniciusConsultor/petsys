using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;

namespace MP.Negocio.Filtros.Marcas
{
    [Serializable]
    public class FiltroMarcaVencidaNoMes : Filtro, IFiltroMarcaVencidaNoMes
    {
        public override string ObtenhaQuery()
        {
             var sql = new StringBuilder();
             var dataProximoMes = DateTime.Now.AddMonths(1);

            sql.AppendLine("SELECT MP_PROCESSOMARCA.IDPROCESSO, MP_PROCESSOMARCA.IDMARCA IDDAMARCA, MP_PROCESSOMARCA.PROCESSO, ");
            sql.AppendLine("MP_PROCESSOMARCA.DATADECADASTRO, MP_PROCESSOMARCA.DATACONCESSAO, MP_PROCESSOMARCA.PROCESSOEHTERCEIRO, MP_PROCESSOMARCA.IDDESPACHO,");
            sql.AppendLine("MP_PROCESSOMARCA.IDPROCURADOR, MP_PROCESSOMARCA.DATADODEPOSITO, MP_PROCESSOMARCA.TXTCOMPLDESPACHO, ");
            sql.AppendLine("MP_PROCESSOMARCA.APOSTILA, MP_PROCESSOMARCA.ATIVO, ");
            sql.AppendLine("MP_MARCAS.IDMARCA, MP_MARCAS.CODIGONCL, MP_MARCAS.CODIGOAPRESENTACAO, MP_MARCAS.IDCLIENTE, MP_MARCAS.CODIGONATUREZA");
            sql.AppendLine(" FROM MP_PROCESSOMARCA, MP_MARCAS");
            sql.AppendLine(" WHERE MP_PROCESSOMARCA.IDMARCA = MP_MARCAS.IDMARCA ");
            sql.Append(" AND MP_MARCAS.PAGAMANUTENCAO = 1 AND MP_MARCAS.DATAPROXIMAMANUTENCAO BETWEEN ");
            sql.Append(string.Concat(DateTime.Now.ToString("yyyyMMdd"), " AND ", dataProximoMes.ToString("yyyyMMdd")));
			sql.Append(" AND MP_PROCESSOMARCA.ATIVO = 1");
            sql.Append(
                " AND MP_PROCESSOMARCA.IDPROCESSO NOT IN (select idconceito from MP_INTERFACEFN where CONCEITO = 'MARCA' AND IDCONCEITO = IDPROCESSO AND DATAVENCIMENTO = DATAPROXIMAMANUTENCAO  )");
			

            return sql.ToString();
        }
    }
}
