using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.FN.Negocio;
using Core.Negocio;
using FN.Interfaces.Negocio.Filtros.ContasAReceber;

namespace FN.Negocio.Filtros.ContasAReceber
{
    [Serializable]
    public class FiltroContaAReceberPorFormaDeRecebimento : Filtro, IFiltroContaAReceberPorFormaDeRecebimento
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();

            sql.Append(
               "SELECT FN_ITEMFINANREC.ID, NOME, IDCLIENTE, VALOR, OBSERVACAO, DATALACAMENTO, DATAVENCIMENTO, SITUACAO, DATARECEBIMENTO, TIPOLANCAMENTO, DESCRICAO, FORMARECEBIMENTO, NUMEROBOLETOGERADO ");
            sql.Append("FROM FN_ITEMFINANREC ");
            sql.Append(" INNER JOIN NCL_PESSOA ON IDCLIENTE = NCL_PESSOA.ID");
            sql.Append(" WHERE " + ObtenhaFiltroMontado("FN_ITEMFINANREC.FORMARECEBIMENTO", false));
            sql.Append(" AND SITUACAO <> '" + Situacao.AguardandoCobranca.ID + "'");

            return sql.ToString();
        }
    }
}
