// -----------------------------------------------------------------------
// <copyright file="MapeadorDeProcessoDePatenteRevista.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MapeadorDeProcessoDePatenteRevista : IMapeadorDeProcessoDePatenteRevista
    {
        public void Grave(KeyValuePair<int, IList<DTOProcessoPatenteRevista>> listaDeProcessoPatenteRevista)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            DBHelper.ExecuteNonQuery("DELETE FROM PMP_PROCESSOSPATENTEEVISTA WHERE NUMERODAREVISTA = " + listaDeProcessoPatenteRevista.Key.ToString());

                foreach (var processoRevista in listaDeProcessoPatenteRevista.Value)
                {

                    sql.Clear();

                    sql.Append(
                        "INSERT INTO PMP_PROCESSOSMARCAREVISTA (ID, NUMERODAREVISTA, DATAPUBLICACAOREVISTA, NUMEROPROCESSODEPATENTE, DATADODEPOSITO, ");
                    sql.Append("CLASSIFICACAO, TITULO, 	RESUMO,	NUMERODEPOSITOPCT, ");
                    sql.Append("DEPOSITANTES, INVENTORES, NUMEROPRIORIDADE, NUMEROPRIORIDADE, DATAPRIORIDADE, PAISPRIORIDADE)  ");
                    sql.Append("VALUES (");

                    sql.Append("'" + processoRevista.ID + "', ");
                    sql.Append(processoRevista.NumeroDaRevista + ", ");
                    sql.Append(processoRevista.DataDePublicacaoDaRevista.ToString("yyyyMMdd") + ", ");
                    sql.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(processoRevista.NumeroProcesso) + "', ");
                    sql.Append(processoRevista.DataDoDeposito == null
                                   ? "NULL, "
                                   : processoRevista.DataDoDeposito.Value.ToString("yyyyMMdd") + ", ");

                    sql.Append(string.IsNullOrEmpty(processoRevista.Classificacao)
                                   ? "NULL, "
                                   : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoRevista.Classificacao) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoRevista.Titulo)
                                   ? "NULL, "
                                   : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoRevista.Titulo) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoRevista.Resumo)
                                 ? "NULL, "
                                 : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoRevista.Resumo) + "', ");

                    sql.Append(string.IsNullOrEmpty(processoRevista.NumeroDepositoPCT)
                                 ? "NULL, "
                                 : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoRevista.NumeroDepositoPCT) + "', ");

                    sql.Append(processoRevista.Depositantes == null
                                 ? "NULL, "
                                 : "'" + UtilidadesDePersistencia.ObtenhaStringMapeadaDeListaDeString(processoRevista.Depositantes, '|') + "', ");

                    sql.Append(processoRevista.Invetores == null
                                ? "NULL, "
                                : "'" + UtilidadesDePersistencia.ObtenhaStringMapeadaDeListaDeString(processoRevista.Invetores, '|') + "', ");

                    sql.Append(string.IsNullOrEmpty(processoRevista.NumeroDaPrioridade)
                               ? "NULL, "
                               : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoRevista.NumeroDaPrioridade) + "', ");

                    sql.Append(processoRevista.DataDaPrioridade == null
                                   ? "NULL, "
                                   : processoRevista.DataDaPrioridade.Value.ToString("yyyyMMdd") + ", ");
                    
                    sql.Append(string.IsNullOrEmpty(processoRevista.PaisDaPrioridade)
                               ? "NULL) "
                               : "'" + UtilidadesDePersistencia.FiltraApostrofe(processoRevista.PaisDaPrioridade) + "') ");


                    DBHelper.ExecuteNonQuery(sql.ToString());
            }
        }

        public IList<DTOProcessoPatenteRevista> ObtenhaResultadoDaPesquisa(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append(filtro.ObtenhaQuery());

            sql.AppendLine(" ORDER BY DATAPUBLICACAOREVISTA ");
            var processos = new List<DTOProcessoPatenteRevista>();

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

        private DTOProcessoPatenteRevista MontaProcesso(IDataReader leitor)
        {
            var processo = new DTOProcessoPatenteRevista();


            processo.ID = UtilidadesDePersistencia.GetValorString(leitor, "ID");
            processo.NumeroDaRevista = UtilidadesDePersistencia.getValorInteger(leitor, "NUMERODAREVISTA");
            processo.DataDePublicacaoDaRevista = UtilidadesDePersistencia.getValorDate(leitor, "DATAPUBLICACAOREVISTA").Value;
            processo.NumeroProcesso = UtilidadesDePersistencia.GetValorString(leitor, "NUMEROPROCESSODEPATENTE");
            processo.DataDoDeposito = UtilidadesDePersistencia.getValorDate(leitor, "DATADODEPOSITO");
            processo.Classificacao = UtilidadesDePersistencia.EhNulo(leitor, "CLASSIFICACAO") ? null : UtilidadesDePersistencia.GetValorString(leitor, "CLASSIFICACAO");
            processo.Titulo = UtilidadesDePersistencia.EhNulo(leitor, "TITULO") ? null : UtilidadesDePersistencia.GetValorString(leitor, "TITULO");
            processo.Resumo = UtilidadesDePersistencia.EhNulo(leitor, "RESUMO") ? null : UtilidadesDePersistencia.GetValorString(leitor, "RESUMO");
            processo.NumeroDepositoPCT = UtilidadesDePersistencia.EhNulo(leitor, "NUMERODEPOSITOPCT") ? null : UtilidadesDePersistencia.GetValorString(leitor, "NUMERODEPOSITOPCT");
            processo.Depositantes = UtilidadesDePersistencia.EhNulo(leitor, "DEPOSITANTES") ? null : UtilidadesDePersistencia.MapeieStringParaListaDeString(UtilidadesDePersistencia.GetValorString(leitor, "DEPOSITANTES"), '|');
            processo.Depositantes = UtilidadesDePersistencia.EhNulo(leitor, "INVENTORES") ? null : UtilidadesDePersistencia.MapeieStringParaListaDeString(UtilidadesDePersistencia.GetValorString(leitor, "INVENTORES"), '|');
            processo.NumeroDaPrioridade = UtilidadesDePersistencia.EhNulo(leitor, "NUMEROPRIORIDADE") ? null : UtilidadesDePersistencia.GetValorString(leitor, "NUMEROPRIORIDADE");
            processo.DataDaPrioridade = UtilidadesDePersistencia.getValorDate(leitor, "DATAPRIORIDADE");
            processo.PaisDaPrioridade =  UtilidadesDePersistencia.EhNulo(leitor, "PAISPRIORIDADE") ? null : UtilidadesDePersistencia.GetValorString(leitor, "PAISPRIORIDADE");

            return processo;
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
    }
}
