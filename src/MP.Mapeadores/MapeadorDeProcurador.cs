using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Negocio.LazyLoad;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeProcurador : IMapeadorDeProcurador
    {
        private IProcurador MapeieObjetoProcurador(IDataReader reader, IPessoa pessoa)
        {
            var procurador = FabricaGenerica.GetInstancia().CrieObjeto<IProcurador>(new object[] {pessoa});

            procurador.MatriculaAPI = UtilidadesDePersistencia.GetValorString(reader, "MATRICULAAPI");
            procurador.SiglaOrgaoProfissional = UtilidadesDePersistencia.GetValorString(reader, "SIGLAORGAO");
            procurador.NumeroRegistroProfissional = UtilidadesDePersistencia.GetValorString(reader, "NRREGISTROORGAO");
            procurador.DataRegistroProfissional = UtilidadesDePersistencia.getValorDate(reader, "DATAREGISTROORGAO");
            procurador.ObservacaoContato = UtilidadesDePersistencia.GetValorString(reader, "MATRICULAAPI");

            return procurador;
        }

        public void Inserir(IProcurador procurador)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("INSERT INTO MP_PROCURADORES(IDPESSOA, TIPOPESSOA, MATRICULAAPI, SIGLAORGAO, NRREGISTROORGAO, DATAREGISTROORGAO, OBSCONTATO) VALUES(");
            comandoSQL.Append(procurador.Pessoa.ID + ", ");
            comandoSQL.Append(procurador.Pessoa.Tipo.ID + ", ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(procurador.MatriculaAPI) + "', ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(procurador.SiglaOrgaoProfissional) + "', ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(procurador.NumeroRegistroProfissional) + "', ");
            comandoSQL.Append(procurador.DataRegistroProfissional == null ? "NULL, " : procurador.DataRegistroProfissional.Value.ToString("yyyyMMdd") + ", ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(procurador.ObservacaoContato) + "')");

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        public void Remover(long idProcurador)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PROCURADORES WHERE IDPESSOA = " + idProcurador);    

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        public void Atualizar(IProcurador procurador)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("UPDATE MP_PROCURADORES SET ");
            comandoSQL.Append("MATRICULAAPI = '" + UtilidadesDePersistencia.FiltraApostrofe(procurador.MatriculaAPI) + "', ");
            comandoSQL.Append("SIGLAORGAO = '" + UtilidadesDePersistencia.FiltraApostrofe(procurador.SiglaOrgaoProfissional) + "', ");
            comandoSQL.Append("NRREGISTROORGAO = '" + UtilidadesDePersistencia.FiltraApostrofe(procurador.NumeroRegistroProfissional) + "', ");
            comandoSQL.Append("DATAREGISTROORGAO = " + (procurador.DataRegistroProfissional == null ? "NULL, " : procurador.DataRegistroProfissional.Value.ToString("yyyyMMdd")) + ", ");
            comandoSQL.Append("OBSCONTATO = '" + UtilidadesDePersistencia.FiltraApostrofe(procurador.ObservacaoContato) + "' ");
            comandoSQL.Append("WHERE IDPESSOA = " + procurador.Pessoa.ID);

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        public IList<IProcurador> ObtenhaTodosProcuradores()
        {
            var listaDeProcuradores = new List<IProcurador>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPESSOA, TIPOPESSOA, MATRICULAAPI, SIGLAORGAO, NRREGISTROORGAO, DATAREGISTROORGAO, OBSCONTATO ");
            comandoSQL.Append("FROM MP_PROCURADORES");
            
            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                {
                    TipoDePessoa tipoDePessoa = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(reader, "TIPOPESSOA"));
                    IPessoa pessoa;

                    if (tipoDePessoa.Equals(TipoDePessoa.Fisica))
                        pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaFisicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "IDPESSOA"));
                    else
                        pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaJuridicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "IDPESSOA"));
                    
                    listaDeProcuradores.Add(MapeieObjetoProcurador(reader, pessoa));
                }

            return listaDeProcuradores;
        }

        public IProcurador ObtenhaProcurador(IPessoa pessoa)
        {
            IProcurador procuradorRetorno = null;
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPESSOA, TIPOPESSOA, MATRICULAAPI, SIGLAORGAO, NRREGISTROORGAO, DATAREGISTROORGAO, OBSCONTATO ");
            comandoSQL.Append("FROM MP_PROCURADORES ");
            comandoSQL.Append("WHERE IDPESSOA = " + pessoa.ID);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    procuradorRetorno = MapeieObjetoProcurador(reader, pessoa);

            return procuradorRetorno;
        }

        public IList<IProcurador> ObtenhaProcuradorPeloNome(string nomeDoProcurador, int quantidadeMaximaDeRegistros)
        {
            var listaDeProcuradores = new List<IProcurador>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT ID, NOME, IDPESSOA, TIPOPESSOA, MATRICULAAPI, SIGLAORGAO, NRREGISTROORGAO, DATAREGISTROORGAO, OBSCONTATO ");
            comandoSQL.Append("FROM MP_PROCURADORES PROCURADORES, ");
            comandoSQL.Append("NCL_PESSOA PESSOA ");
            comandoSQL.Append("WHERE PESSOA.ID = PROCURADORES.IDPESSOA ");
            comandoSQL.Append("AND PESSOA.TIPO = PROCURADORES.TIPOPESSOA "); 

            if(!string.IsNullOrEmpty(nomeDoProcurador))
                comandoSQL.Append("AND PESSOA.NOME LIKE '%" + nomeDoProcurador + "%'");

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString(), quantidadeMaximaDeRegistros))
                while (reader.Read())
                {
                    TipoDePessoa tipoDePessoa = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(reader, "TIPO"));
                    IPessoa pessoa;

                    if (tipoDePessoa.Equals(TipoDePessoa.Fisica))
                        pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaFisicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "ID"));
                    else
                        pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaJuridicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "ID"));
                    listaDeProcuradores.Add(MapeieObjetoProcurador(reader, pessoa));
                }

            return listaDeProcuradores;
        }

        public IProcurador ObtenhaProcurador(long id)
        {
            IProcurador procuradorRetorno = null;
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT ID, NOME, TIPO, IDPESSOA, TIPOPESSOA, MATRICULAAPI, SIGLAORGAO, NRREGISTROORGAO, DATAREGISTROORGAO, OBSCONTATO ");
            comandoSQL.Append("FROM MP_PROCURADORES PROCURADORES, ");
            comandoSQL.Append("NCL_PESSOA PESSOA ");
            comandoSQL.Append("WHERE PESSOA.ID = PROCURADORES.IDPESSOA ");
            comandoSQL.Append("AND PESSOA.TIPO = PROCURADORES.TIPOPESSOA ");
            comandoSQL.Append(String.Concat("AND IDPESSOA = ", id.ToString()));

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                {
                    TipoDePessoa tipoDePessoa = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(reader, "TIPO"));
                    IPessoa pessoa;

                    if (tipoDePessoa.Equals(TipoDePessoa.Fisica))
                        pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaFisicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "ID"));
                    else
                        pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaJuridicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "ID"));
                    procuradorRetorno = MapeieObjetoProcurador(reader, pessoa);
                }

            return procuradorRetorno;            
        }
    }
}
