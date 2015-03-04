// -----------------------------------------------------------------------
// <copyright file="MapeadorDeProcessoMarcaRevista.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Compartilhados;
using Compartilhados.DBHelper;
using PMP.Interfaces.Mapeadores;
using PMP.Interfaces.Utilidades;

namespace PMP.Mapeadores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MapeadorDeProcessoMarcaRevista : IMapeadorDeProcessoMarcaRevista
    {
        public void GraveEmLote(IDictionary<int, IList<DTOProcessoMarcaRevista>> listaDeProcessoMarcaRevista)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();


            foreach (var item in listaDeProcessoMarcaRevista)
            {
                DBHelper.ExecuteNonQuery("DELETE FROM  PMP_PROCESSOSMARCAREVISTA WHERE NUMERODAREVISTA = " +
                                         item.Key.ToString());

                foreach (var processoMarcaRevista in item.Value)
                {

                    sql.Clear();

                    sql.Append(
                        "INSERT INTO PMP_PROCESSOSMARCAREVISTA (NUMERODAREVISTA, DATAPUBLICACAOREVISTA, NUMEROPROCESSODEMARCA, DATADODEPOSITO, ");
                    sql.Append("DATADACONCESSAO, DATADAVIGENCIA, CODIGODODESPACHO, NOMEDODESPACHO, ");
                    sql.Append("TEXTOCOMPLEMENTARDESPACHO, TITULAR, PAISTITULAR, UFTITULAR, ");
                    sql.Append("MARCA, APRESENTACAO, NATUREZA, EDICAOCLASSEVIENA, ");
                    sql.Append("CODIGOCLASSEVIENA, CODIGOCLASSENACIONAL, CODIGOSUBCLASSENACIONAL, CODIGOCLASSENICE, ");
                    sql.Append("ESPECIFICACAOCLASSENICE, PROCURADOR, APOSTILA) VALUES (");

                    sql.Append(processoMarcaRevista.NumeroDaRevista + ", ");
                    sql.Append(processoMarcaRevista.DataDePublicacaoDaRevista.ToString("yyyyMMdd") + ", ");
                    sql.Append(processoMarcaRevista.NumeroProcessoDeMarca + ", ");
                    sql.Append(processoMarcaRevista.DataDoDeposito == null
                                   ? "NULL, "
                                   : processoMarcaRevista.DataDoDeposito.Value.ToString("yyyyMMdd") + ", ");

                    sql.Append(processoMarcaRevista.DataDaConcessao == null
                                   ? "NULL, "
                                   : processoMarcaRevista.DataDaConcessao.Value.ToString("yyyyMMdd") + ", ");

                    sql.Append(processoMarcaRevista.DataDaVigencia == null
                                   ? "NULL, "
                                   : processoMarcaRevista.DataDaVigencia.Value.ToString("yyyyMMdd") + ", ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.CodigoDoDespacho)
                                   ? "NULL, "
                                   : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.CodigoDoDespacho) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.NomeDoDespacho)
                                   ? "NULL, "
                                   : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.NomeDoDespacho) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.TextoComplementarDoDespacho)
                                 ? "NULL, "
                                 : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.TextoComplementarDoDespacho) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.Titular)
                                 ? "NULL, "
                                 : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.Titular) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.PaisTitular)
                                 ? "NULL, "
                                 : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.PaisTitular) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.UFTitular)
                                 ? "NULL, "
                                 : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.UFTitular) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.Marca)
                                 ? "NULL, "
                                 : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.Marca) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.Apresentacao)
                                 ? "NULL, "
                                 : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.Apresentacao) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.Natureza)
                                 ? "NULL, "
                                 : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.Natureza) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.EdicaoClasseViena)
                                 ? "NULL, "
                                 : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.EdicaoClasseViena) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.CodigoClasseViena)
                                 ? "NULL, "
                                 : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.CodigoClasseViena) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.CodigoClasseNacional)
                                ? "NULL, "
                                : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.CodigoClasseNacional) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.CodigoSubClasseNacional)
                                ? "NULL, "
                                : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.CodigoSubClasseNacional) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.CodigoClasseNice)
                               ? "NULL, "
                               : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.CodigoClasseNice) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.EspecificacaoClasseNice)
                               ? "NULL, "
                               : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.EspecificacaoClasseNice) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.Procurador)
                               ? "NULL, "
                               : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.Procurador) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.Apostila)
                               ? "NULL)"
                               : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.Apostila) + "')");

                    DBHelper.ExecuteNonQuery(sql.ToString());

                }
            }
        }
    }
}
