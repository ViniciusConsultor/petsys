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
    public class MapeadorDeProcurador : IMapeadorDeProcurador
    {
        private IProcurador MapeieObjetoProcurador(IDataReader reader, IPessoa pessoa)
        {
            var procurador = FabricaGenerica.GetInstancia().CrieObjeto<IProcurador>(new object[] {pessoa});

            procurador.MatriculaAPI = UtilidadesDePersistencia.GetValorString(reader, "MATRICULAAPI");
            procurador.SiglaOrgaoProfissional = UtilidadesDePersistencia.GetValorString(reader, "SIGLAORGAO");
            procurador.NumeroRegistroProfissional = UtilidadesDePersistencia.GetValorString(reader, "NRREGISTROORGAO");
            procurador.DataRegistroProfissional = UtilidadesDePersistencia.p_getValorDate(UtilidadesDePersistencia.getValorDate(reader, "DATAREGISTROORGAO"));
            procurador.ObservacaoContato = UtilidadesDePersistencia.GetValorString(reader, "MATRICULAAPI");

            return procurador;
        }

        public void Inserir(IProcurador procurador)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("INSERT INTO MP_PROCURADORES(IDPESSOA, TIPOPESSOA, MATRICULAAPI, SIGLAORGAO, NRREGISTROORGAO, DATAREGISTROORGAO, OBSCONTATO) ");
            comandoSQL.Append(procurador.ID + ", ");
            comandoSQL.Append(procurador.Tipo.ID + ", ");
            comandoSQL.Append("'" + procurador.MatriculaAPI + "', ");
            comandoSQL.Append("'" + procurador.SiglaOrgaoProfissional + "', ");
            comandoSQL.Append("'" + procurador.NumeroRegistroProfissional + "', ");
            comandoSQL.Append(procurador.DataRegistroProfissional + "', ");
            comandoSQL.Append("'" + procurador.ObservacaoContato + "')");

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        public void Remover(IProcurador procurador)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PROCURADORES WHERE IDPESSOA = " + procurador.ID);

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        public void Atualizar(IProcurador procurador)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("UPDATE MP_PROCURADORES SET");
            comandoSQL.Append("MATRICULAAPI = '" + procurador.MatriculaAPI + "' ");
            comandoSQL.Append("SIGLAORGAO = '" + procurador.SiglaOrgaoProfissional + "' ");
            comandoSQL.Append("NRREGISTROORGAO = '" + procurador.NumeroRegistroProfissional + "' ");
            comandoSQL.Append("DATAREGISTROORGAO = '" + procurador.DataRegistroProfissional + "' ");
            comandoSQL.Append("OBSCONTATO = '" + procurador.ObservacaoContato + "' ");
            comandoSQL.Append("WHERE = '" + procurador.ID + "' ");

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        public List<IProcurador> ObtenhaTodosProcuradores()
        {
            var listaDeProcuradores = new List<IProcurador>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("SELECT IDPESSOA, TIPOPESSOA, MATRICULAAPI, SIGLAORGAO, NRREGISTROORGAO, DATAREGISTROORGAO, OBSCONTATO ");
            comandoSQL.Append("FROM MP_PROCURADORES");
            
            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                {
                    TipoDePessoa tipoDePessoa = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(reader, "TIPO"));
                    IPessoa pessoa = tipoDePessoa.Equals(TipoDePessoa.Fisica) ? (IPessoa) FabricaGenerica.GetInstancia().CrieObjeto<IPessoaFisica>() :
                                                                                FabricaGenerica.GetInstancia().CrieObjeto<IPessoaJuridica>();

                    pessoa.ID = UtilidadesDePersistencia.GetValorLong(reader, "ID");
                    pessoa.Nome = UtilidadesDePersistencia.GetValorString(reader, "NOME");
                    listaDeProcuradores.Add(MapeieObjetoProcurador(reader, pessoa));
                }

            return listaDeProcuradores;
        }

        public IProcurador ObtenhaProcurador(IProcurador procurador)
        {
            var procuradorRetorno = FabricaGenerica.GetInstancia().CrieObjeto<IProcurador>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("SELECT IDPESSOA, TIPOPESSOA, MATRICULAAPI, SIGLAORGAO, NRREGISTROORGAO, DATAREGISTROORGAO, OBSCONTATO ");
            comandoSQL.Append("FROM MP_PROCURADORES");
            comandoSQL.Append("WHERE IDPESSOA = " + procurador.ID);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                {
                    TipoDePessoa tipoDePessoa = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(reader, "TIPO"));
                    IPessoa pessoa = tipoDePessoa.Equals(TipoDePessoa.Fisica) ? (IPessoa)FabricaGenerica.GetInstancia().CrieObjeto<IPessoaFisica>() :
                                                                                FabricaGenerica.GetInstancia().CrieObjeto<IPessoaJuridica>();

                    pessoa.ID = UtilidadesDePersistencia.GetValorLong(reader, "ID");
                    pessoa.Nome = UtilidadesDePersistencia.GetValorString(reader, "NOME");

                    procuradorRetorno = MapeieObjetoProcurador(reader, pessoa);
                }

            return procuradorRetorno;
        }
    }
}
