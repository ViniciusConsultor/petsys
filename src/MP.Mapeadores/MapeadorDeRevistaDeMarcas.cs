using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeRevistaDeMarcas : IMapeadorDeRevistaDeMarcas
    {
        public void InserirDadosRevistaXml(IList<IRevistaDeMarcas> listaDeProcessosExistentesNaRevista)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            foreach (var processoDaRevistaDeMarca in listaDeProcessosExistentesNaRevista)
            {
                processoDaRevistaDeMarca.IdRevistaMarcas = GeradorDeID.getInstancia().getProximoID();

                sql.Append("INSERT INTO MP_REVISTA_MARCAS (");
                sql.Append("IDREVISTAMARCAS, NUMEROREVISTAMARCAS, DATAPUBLICACAO, DATAPROCESSAMENTO, NUMEROPROCESSODEMARCA, ");
                sql.Append("CODIGODESPACHOANTERIOR, CODIGODESPACHOATUAL, APOSTILA, TEXTODODESPACHO, PROCESSADA, EXTENSAOARQUIVO) ");
                sql.Append("VALUES (");
                sql.Append(String.Concat(processoDaRevistaDeMarca.IdRevistaMarcas.Value.ToString(), ", "));
                sql.Append(String.Concat(processoDaRevistaDeMarca.NumeroRevistaMarcas, ", "));

                sql.Append(String.Concat(processoDaRevistaDeMarca.DataPublicacao.ToString("yyyyMMdd"), ", "));

                sql.Append(processoDaRevistaDeMarca.DataProcessamento != null
                           ? String.Concat(processoDaRevistaDeMarca.DataProcessamento.ToString("yyyyMMdd"), ", ")
                           : "NULL, ");

                sql.Append(String.Concat(processoDaRevistaDeMarca.NumeroProcessoDeMarca, ", "));

                sql.Append(processoDaRevistaDeMarca.CodigoDespachoAnterior != null
                           ? String.Concat("'" + processoDaRevistaDeMarca.CodigoDespachoAnterior, "', ")
                           : "NULL, ");

                sql.Append(processoDaRevistaDeMarca.CodigoDespachoAtual != null
                           ? String.Concat("'" + processoDaRevistaDeMarca.CodigoDespachoAtual, "', ")
                           : "NULL, ");

                sql.Append(processoDaRevistaDeMarca.Apostila != null
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDeMarca.Apostila), "', ")
                           : "NULL, ");

                sql.Append(processoDaRevistaDeMarca.TextoDoDespacho != null
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDeMarca.TextoDoDespacho), "', ")
                           : "NULL, ");

                sql.Append(processoDaRevistaDeMarca.Processada ? "1, " : "0, ");

                if (!string.IsNullOrEmpty(processoDaRevistaDeMarca.ExtensaoArquivo))
                    sql.Append(String.Concat("'", processoDaRevistaDeMarca.ExtensaoArquivo, "' ) "));

                DBHelper.ExecuteNonQuery(sql.ToString());
            }
        }

        public void Modificar(IRevistaDeMarcas revistaDeMarcas)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_REVISTA_MARCAS SET ");
            sql.Append(String.Concat("NUMEROREVISTAMARCAS = ", revistaDeMarcas.NumeroRevistaMarcas, " , "));

            sql.Append(String.Concat("DATAPUBLICACAO = ", revistaDeMarcas.DataPublicacao.ToString("yyyyMMdd"), " , "));

            sql.Append("PROCESSADA = " + (revistaDeMarcas.Processada ? "1, " : "0, "));

            if (!string.IsNullOrEmpty(revistaDeMarcas.ExtensaoArquivo))
                sql.Append(String.Concat("EXTENSAOARQUIVO = '", revistaDeMarcas.ExtensaoArquivo, "' "));

            sql.Append(" WHERE IDREVISTAMARCAS = " + revistaDeMarcas.IdRevistaMarcas.Value);

        }

        public IList<IRevistaDeMarcas> ObtenhaRevistasAProcessar(int quantidadeDeRegistros)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append("SELECT IDREVISTAMARCAS IdRevistaMarcas, NUMEROREVISTAMARCAS NumeroRevistaMarcas, DATAPUBLICACAO DataPublicacao, ");
            sql.Append("PROCESSADA Processada, EXTENSAOARQUIVO ExtensaoArquivo ");
            sql.Append("FROM MP_REVISTA_MARCAS ");
            sql.Append("WHERE PROCESSADA = 0");
            sql.AppendLine(" ORDER BY NUMEROREVISTAMARCAS DESC");

            IList<IRevistaDeMarcas> revistas = new List<IRevistaDeMarcas>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeDeRegistros, 0))
            {
                try
                {
                    while (leitor.Read())
                    {
                        revistas.Add(MontaRevistaDeMarcas(leitor));
                    }
                }
                finally
                {
                    leitor.Close();
                }
            }

            return revistas;
        }

        private IRevistaDeMarcas MontaRevistaDeMarcas(IDataReader leitor)
        {
            var revistaDeMarcas = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDeMarcas>();

            //revistaDeMarcas.IdRevistaMarcas = UtilidadesDePersistencia.GetValorLong(leitor, "IdRevistaMarcas");

            revistaDeMarcas.NumeroRevistaMarcas = UtilidadesDePersistencia.getValorInteger(leitor, "NumeroRevistaMarcas");

            revistaDeMarcas.DataPublicacao = UtilidadesDePersistencia.getValorDate(leitor, "DataPublicacao").Value;

            revistaDeMarcas.Processada = UtilidadesDePersistencia.GetValorBooleano(leitor, "Processada");

             if (!UtilidadesDePersistencia.EhNulo(leitor, "ExtensaoArquivo"))
                 revistaDeMarcas.ExtensaoArquivo = UtilidadesDePersistencia.GetValorString(leitor, "ExtensaoArquivo");

            return revistaDeMarcas;
        }

        public IList<IRevistaDeMarcas> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append("SELECT distinct(NUMEROREVISTAMARCAS) NumeroRevistaMarcas, DATAPUBLICACAO DataPublicacao, ");
            sql.Append("PROCESSADA Processada, EXTENSAOARQUIVO ExtensaoArquivo ");
            sql.Append("FROM MP_REVISTA_MARCAS ");
            sql.Append("WHERE PROCESSADA = 1");
            sql.AppendLine(" ORDER BY NUMEROREVISTAMARCAS DESC");

            IList<IRevistaDeMarcas> revistas = new List<IRevistaDeMarcas>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeDeRegistros))
            {
                try
                {
                    while (leitor.Read())
                    {
                        revistas.Add(MontaRevistaDeMarcas(leitor));
                    }
                }
                finally
                {
                    leitor.Close();
                }
            }

            return revistas;
        }
    }
}
