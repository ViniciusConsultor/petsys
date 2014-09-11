using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Negocio;
using FN.Interfaces.Negocio.Filtros.BoletosGerados;

namespace FN.Negocio.Filtros.BoletosGerados
{
    [Serializable]
    public class FiltroBoletosGeradosSemFiltro : Filtro, IFiltroBoletosGeradosSemFiltro
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();

            sql.Append(
                "SELECT ID, NUMEROBOLETO, NOSSONUMERO, IDCLIENTE, VALOR, DATAGERACAO, DATAVENCIMENTO, OBSERVACAO, IDCEDENTE, INSTRUCOES, STATUSBOLETO, EHBOLETOAVULSO ");
            sql.Append("FROM FN_BOLETOS_GERADOS ");

            return sql.ToString();
        }
    }
}
