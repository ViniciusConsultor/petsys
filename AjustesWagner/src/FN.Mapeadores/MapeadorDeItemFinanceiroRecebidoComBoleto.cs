using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using FN.Interfaces.Mapeadores;
using FN.Interfaces.Negocio;

namespace FN.Mapeadores
{
    public class MapeadorDeItemFinanceiroRecebidoComBoleto : IMapeadorDeItemFinanceiroRecebidoComBoleto
    {
        public void Insira(long idItemFinanRecebimento, long idBoleto)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("INSERT INTO FN_ITEMFINANRECBOLETO (");
            sql.Append("IDITEMFINANREC, IDBOLETO )");
            sql.Append("VALUES (");
            sql.Append(String.Concat(idItemFinanRecebimento, ", "));
            sql.Append(String.Concat(idBoleto, ") "));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public long ObtenhaItemFinanRecebimentoPorIdBoleto(long idBoleto)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDITEMFINANREC FROM FN_ITEMFINANRECBOLETO ");
            sql.Append("WHERE IDBOLETO = " + idBoleto);

            var DBHelper = ServerUtils.criarNovoDbHelper();
            long idItemFinanRec = 0;

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                try
                {
                    while (leitor.Read())
                    {
                        idItemFinanRec = UtilidadesDePersistencia.GetValorLong(leitor, "IDITEMFINANREC");
                    }
                }
                finally
                {
                    leitor.Close();
                }
            }

            return idItemFinanRec;
        }

        public long ObtenhaBoletoPorIdItemFinanRecebimento(long idItemFinanRecebimento)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDBOLETO FROM FN_ITEMFINANRECBOLETO ");
            sql.Append("WHERE IDITEMFINANREC = " + idItemFinanRecebimento);

            var DBHelper = ServerUtils.criarNovoDbHelper();
            long idBoleto = 0;

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                try
                {
                    while (leitor.Read())
                    {
                        idBoleto = UtilidadesDePersistencia.GetValorLong(leitor, "IDBOLETO");
                    }
                }
                finally
                {
                    leitor.Close();
                }
            }

            return idBoleto;
        }

        public void Excluir(long idItemFinanRecebimento)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM FN_ITEMFINANRECBOLETO");
            sql.Append(string.Concat(" WHERE IDITEMFINANREC = ", idItemFinanRecebimento));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
