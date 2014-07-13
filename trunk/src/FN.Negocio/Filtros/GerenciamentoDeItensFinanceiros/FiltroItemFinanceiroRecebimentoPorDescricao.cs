using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Negocio;
using FN.Interfaces.Negocio.Filtros.GerenciamentoDeItensFinanceiros;

namespace FN.Negocio.Filtros.GerenciamentoDeItensFinanceiros
{
    [Serializable]
    public class FiltroItemFinanceiroRecebimentoPorDescricao : Filtro, IFiltroItemFinanceiroRecebimentoPorDescricao
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();

            sql.Append(
                "SELECT ID, IDCLIENTE, VALOR, OBSERVACAO, DATALACAMENTO, DATAVENCIMENTO, SITUACAO, DATARECEBIMENTO, TIPOLANCAMENTO, DESCRICAO, FORMARECEBIMENTO, NUMEROBOLETOGERADO ");
            sql.Append("FROM FN_ITEMFINANREC ");
            sql.Append(" WHERE " + ObtenhaFiltroMontado("FN_ITEMFINANREC.DESCRICAO", true));

            return sql.ToString();
        }
    }
}
