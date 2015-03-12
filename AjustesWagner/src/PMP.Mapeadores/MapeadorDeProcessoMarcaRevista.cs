using System.Data;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Interfaces.Core.Negocio;
using PMP.Interfaces.Mapeadores;
using PMP.Interfaces.Utilidades;

namespace PMP.Mapeadores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

   
    public class MapeadorDeProcessoMarcaRevista : IMapeadorDeProcessoMarcaRevista
    {
        public void GraveEmLote(IDictionary<int, IList<DTOProcessoMarcaRevista>> listaDeProcessoMarcaRevista)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();


            foreach (var item in listaDeProcessoMarcaRevista)
            {
                DBHelper.ExecuteNonQuery("DELETE FROM PMP_PROCESSOSMARCAREVISTA WHERE NUMERODAREVISTA = " +
                                         item.Key.ToString());

                foreach (var processoMarcaRevista in item.Value)
                {

                    sql.Clear();

                    sql.Append(
                        "INSERT INTO PMP_PROCESSOSMARCAREVISTA (ID, NUMERODAREVISTA, DATAPUBLICACAOREVISTA, NUMEROPROCESSODEMARCA, DATADODEPOSITO, ");
                    sql.Append("DATADACONCESSAO, DATADAVIGENCIA, CODIGODODESPACHO, NOMEDODESPACHO, ");
                    sql.Append("TITULAR, PAISTITULAR, UFTITULAR, ");
                    sql.Append("MARCA, APRESENTACAO, NATUREZA, EDICAOCLASSEVIENA, ");
                    sql.Append("CODIGOCLASSEVIENA, CODIGOCLASSENACIONAL, CODIGOSUBCLASSENACIONAL, CODIGOCLASSENICE, ");
                    sql.Append("PROCURADOR) VALUES (");

                    sql.Append("'" + processoMarcaRevista.ID + "', ");
                    sql.Append(processoMarcaRevista.NumeroDaRevista + ", ");
                    sql.Append(processoMarcaRevista.DataDePublicacaoDaRevista.ToString("yyyyMMdd") + ", ");
                    sql.Append("'" +  UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.NumeroProcessoDeMarca) + "', ");
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
                    
                    sql.Append(string.IsNullOrEmpty(processoMarcaRevista.Procurador)
                               ? "NULL) "
                               : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoMarcaRevista.Procurador) + "') ");

                    
                    DBHelper.ExecuteNonQuery(sql.ToString());

                }
            }
        }

        public IList<DTOProcessoMarcaRevista> ObtenhaResultadoDaPesquisa(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append(filtro.ObtenhaQuery());

            sql.AppendLine(" ORDER BY DATAPUBLICACAOREVISTA ");

            var processos = new List<DTOProcessoMarcaRevista>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeDeRegistros, offSet))
                try
                {
                    while (leitor.Read())
                        processos.Add(MontaProcesso(leitor));
                }
                finally
                {
                    leitor.Close();
                }

            return processos;
        }

        public int ObtenhaQuantidadeDeResultadoDaPesquisa(IFiltro filtro)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append(filtro.ObtenhaQueryParaQuantidade());
            

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                try
                {
                    if (leitor.Read())
                        return UtilidadesDePersistencia.getValorInteger(leitor, "QUANTIDADE");
                }
                finally
                {
                    leitor.Close();
                }
            }

            return 0;
        
        }

        private DTOProcessoMarcaRevista MontaProcesso(IDataReader leitor)
        {
            var processo = new DTOProcessoMarcaRevista();

            processo.ID = UtilidadesDePersistencia.GetValorString(leitor, "ID");
            processo.NumeroDaRevista = UtilidadesDePersistencia.getValorInteger(leitor, "NUMERODAREVISTA");
            processo.DataDePublicacaoDaRevista = UtilidadesDePersistencia.getValorDate(leitor, "DATAPUBLICACAOREVISTA").Value;
            processo.NumeroProcessoDeMarca = UtilidadesDePersistencia.GetValorString(leitor, "NUMEROPROCESSODEMARCA");
            processo.DataDoDeposito = UtilidadesDePersistencia.getValorDate(leitor, "DATADODEPOSITO");
            processo.DataDaConcessao = UtilidadesDePersistencia.getValorDate(leitor, "DATADACONCESSAO");
            processo.DataDaVigencia = UtilidadesDePersistencia.getValorDate(leitor, "DATADAVIGENCIA");
            processo.CodigoDoDespacho = UtilidadesDePersistencia.EhNulo(leitor, "CODIGODODESPACHO") ? null : UtilidadesDePersistencia.GetValorString(leitor, "CODIGODODESPACHO");
            processo.NomeDoDespacho = UtilidadesDePersistencia.EhNulo(leitor, "NOMEDODESPACHO") ? null : UtilidadesDePersistencia.GetValorString(leitor, "NOMEDODESPACHO");
            processo.Titular = UtilidadesDePersistencia.EhNulo(leitor, "TITULAR") ? null : UtilidadesDePersistencia.GetValorString(leitor, "TITULAR");
            processo.PaisTitular = UtilidadesDePersistencia.EhNulo(leitor, "PAISTITULAR") ? null : UtilidadesDePersistencia.GetValorString(leitor, "PAISTITULAR");
            processo.UFTitular = UtilidadesDePersistencia.EhNulo(leitor, "UFTITULAR") ? null : UtilidadesDePersistencia.GetValorString(leitor, "UFTITULAR");
            processo.Marca = UtilidadesDePersistencia.EhNulo(leitor, "MARCA") ? null : UtilidadesDePersistencia.GetValorString(leitor, "MARCA");
            processo.Apresentacao = UtilidadesDePersistencia.EhNulo(leitor, "APRESENTACAO") ? null : UtilidadesDePersistencia.GetValorString(leitor, "APRESENTACAO");
            processo.Natureza = UtilidadesDePersistencia.EhNulo(leitor, "NATUREZA") ? null : UtilidadesDePersistencia.GetValorString(leitor, "NATUREZA");
            processo.EdicaoClasseViena = UtilidadesDePersistencia.EhNulo(leitor, "EDICAOCLASSEVIENA") ? null : UtilidadesDePersistencia.GetValorString(leitor, "EDICAOCLASSEVIENA");
            processo.CodigoClasseViena = UtilidadesDePersistencia.EhNulo(leitor, "CODIGOCLASSEVIENA") ? null : UtilidadesDePersistencia.GetValorString(leitor, "CODIGOCLASSEVIENA");
            processo.CodigoClasseNacional = UtilidadesDePersistencia.EhNulo(leitor, "CODIGOCLASSENACIONAL") ? null : UtilidadesDePersistencia.GetValorString(leitor, "CODIGOCLASSENACIONAL");
            processo.CodigoSubClasseNacional = UtilidadesDePersistencia.EhNulo(leitor, "CODIGOSUBCLASSENACIONAL") ? null : UtilidadesDePersistencia.GetValorString(leitor, "CODIGOSUBCLASSENACIONAL");
            processo.CodigoClasseNice = UtilidadesDePersistencia.EhNulo(leitor, "CODIGOCLASSENICE") ? null : UtilidadesDePersistencia.GetValorString(leitor, "CODIGOCLASSENICE");
            processo.Procurador = UtilidadesDePersistencia.EhNulo(leitor, "PROCURADOR") ? null : UtilidadesDePersistencia.GetValorString(leitor, "PROCURADOR");
            
            return processo;
        }
    }
}
