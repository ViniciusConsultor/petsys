// -----------------------------------------------------------------------
// <copyright file="FiltroPorNumeroDeProcesso.cs" company="Microsoft">
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
    public class FiltroPorNumeroDeProcesso : Filtro, IFiltroPorNumeroDeProcesso
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
            sql.Append(ObtenhaFiltroMontado("NUMEROPROCESSODEMARCA", false));

            if (NumeroDaRevista != null)
                sql.Append(" AND NUMERODAREVISTA = " + NumeroDaRevista.Value);

            return sql.ToString();
        }

        public int? NumeroDaRevista { get; set; }
    }
}
