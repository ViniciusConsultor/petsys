using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeTitular : IMapeadorDeTitular
    {
        public void Inserir(ITitular titular)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("INSERT INTO MP_TITULAR (");
            sql.Append("IDPESSOA, TIPOPESSOA, DTCADASTRO, INFOADICIONAL)");
            sql.Append("VALUES (");
            sql.Append(String.Concat(titular.Pessoa.ID.Value, ", "));
            sql.Append(String.Concat(titular.Pessoa.Tipo.ID, ", "));
            sql.Append(String.Concat(titular.DataDoCadastro.Value.ToString("yyyyMMdd"), ", "));
            sql.Append(string.IsNullOrEmpty(titular.InformacoesAdicionais) ? "NULL)" : String.Concat("'", titular.InformacoesAdicionais, "')"));
            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Remover(long ID)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM MP_TITULAR WHERE IDPESSOA = " + ID);

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Atualizar(ITitular titular)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_TITULAR SET ");
            sql.Append("INFOADICIONAL = ");
            sql.Append(string.IsNullOrEmpty(titular.InformacoesAdicionais) ? "NULL)" : String.Concat("'", titular.InformacoesAdicionais, "'"));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public IList<ITitular> ObtenhaPorNomeComoFiltro(string nome, int quantidadeMaxima)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;
            IList<ITitular> titulares = new List<ITitular>();

            DBHelper = ServerUtils.criarNovoDbHelper();

            sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.NOME, NCL_PESSOA.TIPO,");
            sql.Append("MP_TITULAR.IDPESSOA, MP_TITULAR.TIPOPESSOA, MP_TITULAR.DTCADASTRO, MP_TITULAR.INFOADICIONAL ");
            sql.Append("FROM NCL_PESSOA, MP_TITULAR ");
            sql.Append("WHERE MP_TITULAR.IDPESSOA = NCL_PESSOA.ID ");
            sql.Append("AND MP_TITULAR.TIPOPESSOA = NCL_PESSOA.TIPO ");

            if (!string.IsNullOrEmpty(nome))
                sql.Append(String.Concat(" AND NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(nome), "%'"));

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeMaxima))
            {
                try
                {
                    while (leitor.Read())
                    {
                        IPessoa pessoa;
                        ITitular titular = null;

                        var tipo = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(leitor, "TIPO"));

                        if (tipo.Equals(TipoDePessoa.Fisica))
                            pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaFisica>();
                        else
                            pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaJuridica>();

                        pessoa.ID = UtilidadesDePersistencia.GetValorLong(leitor, "ID");
                        pessoa.Nome = UtilidadesDePersistencia.GetValorString(leitor, "NOME");
                        titular = MontaObjetoTitular(leitor, pessoa);
                        titulares.Add(titular);
                    }
                }
                finally
                {
                    leitor.Close();
                }
            }

            return titulares;
        }

        public ITitular Obtenha(long ID)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;
            ITitular titular = null;
            DBHelper = ServerUtils.criarNovoDbHelper();

            sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.NOME, NCL_PESSOA.TIPO,");
            sql.Append("MP_TITULAR.IDPESSOA, MP_TITULAR.TIPOPESSOA, MP_TITULAR.DTCADASTRO, MP_TITULAR.INFOADICIONAL  ");
            sql.Append("FROM NCL_PESSOA, MP_TITULAR ");
            sql.Append("WHERE MP_TITULAR.IDPESSOA = NCL_PESSOA.ID ");
            sql.Append("AND MP_TITULAR.TIPOPESSOA = NCL_PESSOA.TIPO ");
            sql.Append(String.Concat("AND IDPESSOA = ", ID.ToString()));

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), int.MaxValue))
            {
                try
                {
                    if (leitor.Read())
                    {
                        IPessoa pessoa;

                        var tipo = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(leitor, "TIPO"));

                        if (tipo.Equals(TipoDePessoa.Fisica))
                            pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaFisica>();
                        else
                            pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaJuridica>();

                        pessoa.ID = UtilidadesDePersistencia.GetValorLong(leitor, "ID");
                        pessoa.Nome = UtilidadesDePersistencia.GetValorString(leitor, "NOME");
                        titular = MontaObjetoTitular(leitor, pessoa);
                    }
                }
                finally
                {
                    leitor.Close();
                }
            }

            return titular;
        }

        public ITitular Obtenha(IPessoa pessoa)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;
            ITitular titular = null;

            DBHelper = ServerUtils.criarNovoDbHelper();

            sql.Append("SELECT IDPESSOA, TIPOPESSOA, DTCADASTRO, INFOADICIONAL FROM MP_TITULAR WHERE ");
            sql.Append(String.Concat("IDPESSOA = ", pessoa.ID.Value));

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), int.MaxValue))
            {
                try
                {
                    if (leitor.Read())
                        titular = MontaObjetoTitular(leitor, pessoa);
                }
                finally
                {
                    leitor.Close();
                }
            }

            return titular;
        }

        private ITitular MontaObjetoTitular(IDataReader leitor, IPessoa pessoa)
        {
            var titular = FabricaGenerica.GetInstancia().CrieObjeto<ITitular>(new object[] { pessoa });

            titular.DataDoCadastro = UtilidadesDePersistencia.getValorDate(leitor, "DTCADASTRO");

            if (!UtilidadesDePersistencia.EhNulo(leitor, "INFOADICIONAL"))
                titular.InformacoesAdicionais = UtilidadesDePersistencia.GetValorString(leitor, "INFOADICIONAL");

            return titular;
        }
    }
}
