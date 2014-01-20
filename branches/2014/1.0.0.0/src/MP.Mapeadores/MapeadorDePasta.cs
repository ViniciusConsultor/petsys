using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDePasta : IMapeadorDePasta
    {
        public IPasta obtenha(long id)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();
            IPasta pasta = null;

            using (var leitor = DBHelper.obtenhaReader("SELECT ID, NOME, CODIGO FROM MP_PASTA WHERE ID = " + id))
            {
                try
                {
                    if (leitor.Read())
                        pasta = MontaPasta(leitor);
                }
                finally
                {
                    leitor.Close();
                }
            }

            return pasta;

        }

        public IList<IPasta> obtenhaPeloCodigo(string codigo, int quantidadeDeItens)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append("SELECT ID, NOME, CODIGO FROM MP_PASTA ");

            if (!string.IsNullOrEmpty(codigo))
                sql.Append("CODIGO LIKE '%" + UtilidadesDePersistencia.FiltraApostrofe(codigo) + "%'");

            sql.AppendLine(" ORDER BY CODIGO");

            var pastas = new List<IPasta>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeDeItens))
            {
                try
                {
                    while (leitor.Read())
                        pastas.Add(MontaPasta(leitor));
                }
                finally
                {
                    leitor.Close();
                }
            }

            return pastas;
        }

        private IPasta MontaPasta(IDataReader leitor)
        {
            var pasta = FabricaGenerica.GetInstancia().CrieObjeto<IPasta>();
            pasta.ID = UtilidadesDePersistencia.GetValorLong(leitor, "ID");
            pasta.Codigo = UtilidadesDePersistencia.GetValorString(leitor, "CODIGO");
            pasta.Nome = UtilidadesDePersistencia.GetValorString(leitor, "NOME");
            return pasta;
        }

        public void Inserir(IPasta pasta)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();
            pasta.ID = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_PASTA (");
            sql.Append("ID, CODIGO, NOME)");
            sql.Append("VALUES (");
            sql.Append(String.Concat(pasta.ID.Value, ", "));
            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(pasta.Codigo), "', "));
            sql.Append(string.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(pasta.Nome), "')"));
            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(IPasta pasta)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_PASTA SET ");
            sql.Append(string.Concat("NOME = '", UtilidadesDePersistencia.FiltraApostrofe(pasta.Nome), "', "));
            sql.Append(string.Concat("CODIGO = '", UtilidadesDePersistencia.FiltraApostrofe(pasta.Codigo), "' "));
            sql.Append("WHERE ID = " + pasta.ID.Value.ToString());
            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Excluir(long id)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.getDBHelper();
            DBHelper.ExecuteNonQuery("DELETE FROM MP_PASTA WHERE ID =" + id);
        }
    }
}
