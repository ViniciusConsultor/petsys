using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Negocio;
using FN.Interfaces;
using FN.Interfaces.Negocio.Filtros.BoletosGerados;

namespace FN.Negocio.Filtros.BoletosGerados
{
    [Serializable]
    public class FiltroBoletosGeradosVencidos : Filtro, IFiltroBoletosGeradosVencidos
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();

            sql.Append(
                "SELECT ID, NUMEROBOLETO, NOSSONUMERO, IDCLIENTE, VALOR, DATAGERACAO, DATAVENCIMENTO, OBSERVACAO, IDCEDENTE, INSTRUCOES, STATUSBOLETO, EHBOLETOAVULSO ");
            sql.Append("FROM FN_BOLETOS_GERADOS ");
            sql.Append(" WHERE STATUSBOLETO = '" + StatusBoleto.Status.Aberto + "' ");
            sql.Append(" AND DATAVENCIMENTO < " + DateTime.Now.ToString("yyyyMMdd"));

            return sql.ToString();
        }
    }
}
