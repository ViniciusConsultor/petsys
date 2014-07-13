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
    public class FiltroItemFinanceiroRecebimentoPorIntervaloDAtaDeVencimento : Filtro, IFiltroItemFinanceiroRecebimentoPorIntervaloDAtaDeVencimento
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();

            sql.Append(
                "SELECT ID, IDCLIENTE, VALOR, OBSERVACAO, DATALACAMENTO, DATAVENCIMENTO, SITUACAO, DATARECEBIMENTO, TIPOLANCAMENTO, DESCRICAO, FORMARECEBIMENTO, NUMEROBOLETOGERADO ");
            sql.Append("FROM FN_ITEMFINANREC ");
            sql.Append(" WHERE " + ObtenhaFiltroMontado("FN_ITEMFINANREC.DATAVENCIMENTO", false));
            sql.Append(" AND SITUACAO = '" + Situacao.AguardandoCobranca.ID + "'");

            return sql.ToString();
        }
    }
}
