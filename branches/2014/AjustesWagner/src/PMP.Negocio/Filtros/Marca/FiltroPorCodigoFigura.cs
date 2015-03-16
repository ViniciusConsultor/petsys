using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Core.Negocio;
using PMP.Interfaces.Negocio.Filtros.Marca;

namespace PMP.Negocio.Filtros.Marca
{
    [Serializable]
    public class FiltroPorCodigoFigura : Filtro, IFiltroPorCodigoFigura
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
            sql.Append(ObtenhaFiltroMontado("CODIGOCLASSEVIENA", true));

            if (!string.IsNullOrEmpty(NCL))
                sql.Append(" AND CODIGOCLASSENICE = '" + UtilidadesDeString.RemoveAcentos(UtilidadesDePersistencia.FiltraApostrofe(NCL.ToUpper())) + "'");

            if (NumeroDaRevista.HasValue)
                sql.Append(" AND NUMERODAREVISTA = " + NumeroDaRevista.Value);

            return sql.ToString();
        }

        public int? NumeroDaRevista { get; set; }
        public string NCL { get; set; }
    }
}
