// -----------------------------------------------------------------------
// <copyright file="FiltroPorTitular.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Compartilhados;
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

            sql.Append("SELECT ID, NUMERODAREVISTA, DATAPUBLICACAOREVISTA, NUMEROPROCESSODEMARCA, DATADODEPOSITO, ");
            sql.Append("DATADACONCESSAO, DATADAVIGENCIA, CODIGODODESPACHO, NOMEDODESPACHO, ");
            sql.Append("TITULAR, PAISTITULAR, UFTITULAR, ");
            sql.Append("MARCA, APRESENTACAO, NATUREZA, EDICAOCLASSEVIENA, ");
            sql.Append("CODIGOCLASSEVIENA, CODIGOCLASSENACIONAL, CODIGOSUBCLASSENACIONAL, CODIGOCLASSENICE, ");
            sql.Append("PROCURADOR FROM PMP_PROCESSOSMARCAREVISTA WHERE ");
            
            if (!string.IsNullOrEmpty(ValorDoFiltro))
                sql.Append(ObtenhaFiltroMontado("TITULAR", true));

            if (!string.IsNullOrEmpty(UF))
                sql.Append(!string.IsNullOrEmpty(ValorDoFiltro)
                               ? " AND UFTITULAR = '" + UtilidadesDeString.RemoveAcentos(UtilidadesDePersistencia.FiltraApostrofe(UF.ToUpper())) + "'"
                               : (" UFTITULAR = '" + UtilidadesDeString.RemoveAcentos(UtilidadesDePersistencia.FiltraApostrofe(UF.ToUpper())) + "'"));

            if (!string.IsNullOrEmpty(Pais))
                sql.Append(!string.IsNullOrEmpty(ValorDoFiltro) || !string.IsNullOrEmpty(UF)
                             ? " AND PAISTITULAR = '" + UtilidadesDeString.RemoveAcentos(UtilidadesDePersistencia.FiltraApostrofe(Pais.ToUpper())) + "'"
                             : (" PAISTITULAR = '" + UtilidadesDeString.RemoveAcentos(UtilidadesDePersistencia.FiltraApostrofe(Pais.ToUpper())) + "'"));

            if (NumeroDaRevista.HasValue)
                sql.Append(" AND NUMERODAREVISTA = " + NumeroDaRevista.Value);

            return sql.ToString();
        }

        public string UF { get; set; }
        public string Pais { get; set; }

        public int? NumeroDaRevista { get; set; }
    }
}
