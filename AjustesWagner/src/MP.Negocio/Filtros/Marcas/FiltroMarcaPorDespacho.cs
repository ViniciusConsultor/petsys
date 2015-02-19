﻿// -----------------------------------------------------------------------
// <copyright file="FiltroMarcaPorDespacho.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Core.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;

namespace MP.Negocio.Filtros.Marcas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class FiltroMarcaPorDespacho : Filtro, IFiltroMarcaPorDespacho
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT MP_PROCESSOMARCA.IDPROCESSO, MP_PROCESSOMARCA.IDMARCA IDDAMARCA, MP_PROCESSOMARCA.PROCESSO, ");
            sql.AppendLine("MP_PROCESSOMARCA.DATADECADASTRO, MP_PROCESSOMARCA.DATACONCESSAO, MP_PROCESSOMARCA.PROCESSOEHTERCEIRO, MP_PROCESSOMARCA.IDDESPACHO,");
            sql.AppendLine("MP_PROCESSOMARCA.IDPROCURADOR, MP_PROCESSOMARCA.DATADODEPOSITO, MP_PROCESSOMARCA.TXTCOMPLDESPACHO, ");
            sql.AppendLine("MP_PROCESSOMARCA.APOSTILA, MP_PROCESSOMARCA.ATIVO, ");
            sql.AppendLine("MP_MARCAS.IDMARCA, MP_MARCAS.CODIGONCL, MP_MARCAS.CODIGOAPRESENTACAO, MP_MARCAS.IDCLIENTE, MP_MARCAS.CODIGONATUREZA");
            sql.AppendLine(" FROM MP_PROCESSOMARCA, MP_MARCAS");
            sql.AppendLine(" WHERE MP_PROCESSOMARCA.IDMARCA = MP_MARCAS.IDMARCA ");
            sql.AppendLine(" AND " + ObtenhaFiltroMontado("MP_PROCESSOMARCA.IDDESPACHO", false));

            return sql.ToString();
        }
    }
}
