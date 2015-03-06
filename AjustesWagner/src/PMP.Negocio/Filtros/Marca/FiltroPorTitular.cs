// -----------------------------------------------------------------------
// <copyright file="FiltroPorTitular.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Core.Negocio;
using PMP.Interfaces.Negocio.Filtros.Marca;

namespace PMP.Negocio.Filtros.Marca
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class FiltroPorTitular : Filtro, IFiltroPorTitular
    {
        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();

            sql.Append("SELECT NUMERODAREVISTA, DATAPUBLICACAOREVISTA, NUMEROPROCESSODEMARCA, DATADODEPOSITO, ");
            sql.Append("DATADACONCESSAO, DATADAVIGENCIA, CODIGODODESPACHO, NOMEDODESPACHO, ");
            sql.Append("TEXTOCOMPLEMENTARDESPACHO, TITULAR, PAISTITULAR, UFTITULAR, ");
            sql.Append("MARCA, APRESENTACAO, NATUREZA, EDICAOCLASSEVIENA, ");
            sql.Append("CODIGOCLASSEVIENA, CODIGOCLASSENACIONAL, CODIGOSUBCLASSENACIONAL, CODIGOCLASSENICE, ");
            sql.Append("ESPECIFICACAOCLASSENICE, PROCURADOR, APOSTILA FROM PMP_PROCESSOSMARCAREVISTA WHERE ");
            sql.Append(ObtenhaFiltroMontado("TITULAR", true));

            if (!string.IsNullOrEmpty(UF))
                sql.Append(" AND UFTITULAR = '" + UF.ToUpper() + "'");

            if (!string.IsNullOrEmpty(Pais))
                sql.Append(" AND PAISTITULAR = '" + Pais.ToUpper() + "'");
        

            return sql.ToString();
        }

        public string UF { get; set; }
        public string Pais { get; set; }
    }
}
