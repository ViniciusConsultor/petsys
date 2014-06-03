﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.FN.Negocio;
using Core.Negocio;
using FN.Interfaces.Negocio.Filtros.ContasAReceber;

namespace FN.Negocio.Filtros.ContasAReceber
{
    [Serializable]
    public class FiltroContaAReceberPorSituacao : Filtro, IFiltroContaAReceberPorSituacao
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();

            sql.Append(
                "SELECT ID, IDCLIENTE, VALOR, OBSERVACAO, DATALACAMENTO, DATAVENCIMENTO,  SITUACAO, DATARECEBIMENTO, TIPOLANCAMENTO, DESCRICAO, FORMARECEBIMENTO ");
            sql.Append("FROM FN_ITEMFINANREC ");
            sql.Append(" WHERE " + ObtenhaFiltroMontado("FN_ITEMFINANREC.SITUACAO", false));
            sql.Append(" AND SITUACAO <> '" + Situacao.AguardandoCobranca.ID + "'");

            return sql.ToString();
        }
    }
}
