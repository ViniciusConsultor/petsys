using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.FN.Negocio;
using Core.Negocio;
using FN.Interfaces.Negocio.Filtros.GerenciamentoDeItensFinanceiros;

namespace FN.Negocio.Filtros.GerenciamentoDeItensFinanceiros
{
    [Serializable]
    public class FiltroItemFinanceiroRecebimentoPorCliente : Filtro, IFiltroItemFinanceiroRecebimentoPorCliente
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();

            sql.Append(
                "SELECT ID, IDCLIENTE, VALOR, OBSERVACAO, DATALACAMENTO, DATAVENCIMENTO, SITUACAO, DATARECEBIMENTO, TIPOLANCAMENTO, DESCRICAO, FORMARECEBIMENTO ");
            sql.Append("FROM FN_ITEMFINANREC ");
            sql.Append(" WHERE " + ObtenhaFiltroMontado("FN_ITEMFINANREC.IDCLIENTE", false));
            sql.Append(" AND SITUACAO = '" + Situacao.AguardandoCobranca.ID + "'");

            return sql.ToString();
        }
    }
}
