using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Negocio.LazyLoad;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeInventor : IMapeadorDeInventor
    {
        public void Inserir(IInventor inventor)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();
            
            sql.Append("INSERT INTO MP_INVENTOR (");
            sql.Append("IDPESSOA, TIPOPESSOA, DTCADASTRO, INFOADICIONAL)");
            sql.Append("VALUES (");
            sql.Append(String.Concat(inventor.Pessoa.ID.Value, ", "));
            sql.Append(String.Concat(inventor.Pessoa.Tipo.ID, ", "));
            sql.Append(String.Concat(inventor.DataDoCadastro.Value.ToString("yyyyMMdd"), ", "));

            if (string.IsNullOrEmpty(inventor.InformacoesAdicionais))
                sql.Append("NULL)");
            else
                sql.Append(String.Concat("'", inventor.InformacoesAdicionais, "')"));
            
            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Remover(long ID)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM MP_INVENTOR WHERE ");
            sql.Append("IDPESSOA = " + ID);
            
            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Atualizar(IInventor inventor)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_INVENTOR SET ");
            sql.Append("INFOADICIONAL = ");
            
            if (string.IsNullOrEmpty(inventor.InformacoesAdicionais))
            {
                sql.Append("NULL)");
            }
            else
            {
                sql.Append(String.Concat("'", inventor.InformacoesAdicionais, "'"));
            }

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public IList<IInventor> ObtenhaPorNomeComoFiltro(string nome, int quantidadeMaxima)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;
            IList<IInventor> inventores = new List<IInventor>(); 
            
            DBHelper = ServerUtils.criarNovoDbHelper();

            sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.NOME, NCL_PESSOA.TIPO,");
            sql.Append("MP_INVENTOR.IDPESSOA, MP_INVENTOR.TIPOPESSOA, MP_INVENTOR.DTCADASTRO, MP_INVENTOR.INFOADICIONAL ");
            sql.Append("FROM NCL_PESSOA, MP_INVENTOR ");
            sql.Append("WHERE MP_INVENTOR.IDPESSOA = NCL_PESSOA.ID ");
            sql.Append("AND MP_INVENTOR.TIPOPESSOA = NCL_PESSOA.TIPO ");
            
            if (!string.IsNullOrEmpty(nome))
                sql.Append(String.Concat(" AND NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(nome), "%'"));

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeMaxima))
            {
                try
                {
                    while (leitor.Read())
                    {
                        IInventor inventor = null;
                        TipoDePessoa tipoDePessoa = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(leitor, "TIPO"));
                        IPessoa pessoa;

                        if (tipoDePessoa.Equals(TipoDePessoa.Fisica))
                            pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaFisicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "ID"));
                        else
                            pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaJuridicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "ID"));
                        inventor = MontaObjetoInvetor(leitor, pessoa);
                        inventores.Add(inventor);
                    }
                }
                finally
                {
                    leitor.Close();
                }
            }

            return inventores;

        }

        public IInventor Obtenha(long ID)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;
            IInventor inventor = null;
            DBHelper = ServerUtils.criarNovoDbHelper();

            sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.TIPO,");
            sql.Append("MP_INVENTOR.IDPESSOA, MP_INVENTOR.TIPOPESSOA, MP_INVENTOR.DTCADASTRO, MP_INVENTOR.INFOADICIONAL  ");
            sql.Append("FROM NCL_PESSOA, MP_INVENTOR ");
            sql.Append("WHERE MP_INVENTOR.IDPESSOA = NCL_PESSOA.ID ");
            sql.Append("AND MP_INVENTOR.TIPOPESSOA = NCL_PESSOA.TIPO ");
            sql.Append(String.Concat("AND IDPESSOA = ", ID.ToString()));

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), int.MaxValue))
            {
                try
                {
                    if (leitor.Read())
                    {
                        TipoDePessoa tipoDePessoa = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(leitor, "TIPO"));
                        IPessoa pessoa;

                        if (tipoDePessoa.Equals(TipoDePessoa.Fisica))
                            pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaFisicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "ID"));
                        else
                            pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaJuridicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "ID"));
                        inventor = MontaObjetoInvetor(leitor, pessoa);
                    }
                }
                finally
                {
                    leitor.Close();
                }
            }

            return inventor;
        }

        public IInventor Obtenha(IPessoa pessoa)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;
            IInventor inventor = null;

            DBHelper = ServerUtils.criarNovoDbHelper();

            sql.Append("SELECT IDPESSOA, TIPOPESSOA, DTCADASTRO, INFOADICIONAL FROM MP_INVENTOR WHERE ");
            sql.Append(String.Concat("IDPESSOA = ", pessoa.ID.Value));

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), int.MaxValue))
            {
                try
                {
                    if (leitor.Read())
                        inventor = MontaObjetoInvetor(leitor, pessoa);
                }
                finally
                {
                    leitor.Close();
                }
            }

            return inventor;
        }

        private IInventor MontaObjetoInvetor(IDataReader leitor, IPessoa pessoa)
        {
            var inventor = FabricaGenerica.GetInstancia().CrieObjeto<IInventor>(new object[] {pessoa});

            inventor.DataDoCadastro = UtilidadesDePersistencia.getValorDate(leitor, "DTCADASTRO");

            if (!UtilidadesDePersistencia.EhNulo(leitor, "INFOADICIONAL"))
            {
                inventor.InformacoesAdicionais = UtilidadesDePersistencia.GetValorString(leitor, "INFOADICIONAL");
            }

            return inventor;
        }
    }
}
