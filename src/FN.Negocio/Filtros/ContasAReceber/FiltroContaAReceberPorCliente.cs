﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Negocio;
using FN.Interfaces.Negocio.Filtros.ContasAReceber;

namespace FN.Negocio.Filtros.ContasAReceber
{
    [Serializable]
    public class FiltroContaAReceberPorCliente : Filtro, IFiltroContaAReceberPorCliente
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();

            sql.Append(
                "SELECT ID, IDCLIENTE, VALOR, OBSERVACAO, DATALACAMENTO, DATAVENCIMENTO, SITUACAO, DATARECEBIMENTO, TIPOLANCAMENTO, DESCRICAO, FORMARECEBIMENTO, IDBOLETO ");
            sql.Append("FROM FN_ITEMFINANREC ");
            sql.Append(" WHERE " + ObtenhaFiltroMontado("FN_ITEMFINANREC.IDCLIENTE", false));

            return sql.ToString();
        }
    }
}
