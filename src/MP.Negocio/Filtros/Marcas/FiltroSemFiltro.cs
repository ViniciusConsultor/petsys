using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using Core.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;

namespace MP.Negocio.Filtros.Marcas
{
    public class FiltroSemFiltro : Filtro, IFiltroSemFiltro
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT MP_PROCESSOMARCA.IDPROCESSO, MP_PROCESSOMARCA.IDMARCA IDDAMARCA, MP_PROCESSOMARCA.PROTOCOLO, MP_PROCESSOMARCA.PROCESSO, MP_PROCESSOMARCA.DATAENTRADA, MP_PROCESSOMARCA.DATACONCESSAO, MP_PROCESSOMARCA.PROCESSOEHTERCEIRO, MP_PROCESSOMARCA.IDDESPACHO, MP_PROCESSOMARCA.DATADEPRORROGACAO, MP_PROCESSOMARCA.IDPROCURADOR, MP_PROCESSOMARCA.SITUACAO, ");
            sql.AppendLine(" FROM MP_PROCESSOMARCA");
            
            return sql.ToString();
        }
    }
}
