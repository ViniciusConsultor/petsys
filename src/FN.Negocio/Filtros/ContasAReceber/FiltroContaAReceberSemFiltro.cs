using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Negocio;
using FN.Interfaces.Negocio.Filtros.ContasAReceber;

namespace FN.Negocio.Filtros.ContasAReceber
{
    [Serializable]
    public class FiltroContaAReceberSemFiltro : Filtro, IFiltroContaAReceberSemFiltro
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();

            sql.Append(
                "SELECT ID, IDCLIENTE, VALOR, OBSERVACAO, DATALACAMENTO, SITUACAO, DATARECEBIMENTO, TIPOLANCAMENTO ");
            sql.Append("FROM FN_ITEMFINANREC ");

            return sql.ToString();
        }
    }
}
