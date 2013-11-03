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

            using (var leitor = DBHelper.obtenhaReader("SELECT ID, NOME FROM MP_PASTA WHERE ID = " + id))
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

        private IPasta MontaPasta(IDataReader leitor)
        {
            var pasta = FabricaGenerica.GetInstancia().CrieObjeto<IPasta>();
            pasta.ID = UtilidadesDePersistencia.GetValorLong(leitor, "ID");
            pasta.Nome = UtilidadesDePersistencia.GetValorString(leitor, "Nome");
            return pasta;
        }

        public IList<IPasta> obtenhaPeloNome(string nome, int quantidadeDeItens)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append("SELECT ID, NOME FROM MP_PASTA ");

            if (!string.IsNullOrEmpty(nome))
                sql.Append("NOME LIKE '%" + UtilidadesDePersistencia.FiltraApostrofe(nome) + "%'");

            sql.AppendLine(" ORDER BY NOME");

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

        public void Inserir(IPasta pasta)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();
            pasta.ID = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_PASTA (");
            sql.Append("ID, NOME)");
            sql.Append("VALUES (");
            sql.Append(String.Concat(pasta.ID.Value, ", "));
            sql.Append(string.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(pasta.Nome), "')"));
            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(IPasta pasta)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_PASTA SET ");
            sql.Append(string.Concat("NOME = '", UtilidadesDePersistencia.FiltraApostrofe(pasta.Nome), "' "));
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
