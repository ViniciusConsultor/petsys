// -----------------------------------------------------------------------
// <copyright file="FiltroContaAReceberVencidos.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Compartilhados.Interfaces.FN.Negocio;
using Core.Negocio;
using FN.Interfaces.Negocio.Filtros.ContasAReceber;

namespace FN.Negocio.Filtros.ContasAReceber
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class FiltroContaAReceberVencidos :  Filtro, IFiltroContaAReceberVencidos
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();

            sql.Append(
               "SELECT FN_ITEMFINANREC.ID, NOME, IDCLIENTE, VALOR, OBSERVACAO, DATALACAMENTO, DATAVENCIMENTO, SITUACAO, DATARECEBIMENTO, TIPOLANCAMENTO, DESCRICAO, FORMARECEBIMENTO, NUMEROBOLETOGERADO ");
            sql.Append("FROM FN_ITEMFINANREC ");
            sql.Append(" INNER JOIN NCL_PESSOA ON IDCLIENTE = NCL_PESSOA.ID");
            sql.Append(" WHERE SITUACAO IN (" + Situacao.CobrancaEmAberto.ID +
                       "," + Situacao.CobrancaGerada.ID + ")");
            sql.Append(" AND DATAVENCIMENTO < " + DateTime.Now.ToString("yyyyMMdd"));

            return sql.ToString();
        }
    }
}
