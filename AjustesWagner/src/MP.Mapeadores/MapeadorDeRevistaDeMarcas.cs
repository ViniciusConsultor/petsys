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
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            foreach (var processoDaRevistaDeMarca in listaDeProcessosExistentesNaRevista)
            {
                var sql = new StringBuilder();
                processoDaRevistaDeMarca.IdRevistaMarcas = GeradorDeID.getInstancia().getProximoID();
                
                sql.Append("INSERT INTO MP_REVISTA_MARCAS (");
                sql.Append("IDREVISTAMARCAS, NUMEROREVISTAMARCAS, DATAPUBLICACAO, DATAPROCESSAMENTO, NUMEROPROCESSODEMARCA, ");
                sql.Append("CODIGODESPACHO, APOSTILA, TEXTODODESPACHO, PROCESSADA, EXTENSAOARQUIVO, ");
                sql.Append("DATADODEPOSITO, DATACONCESSAO) ");
                sql.Append("VALUES (");
                sql.Append(String.Concat(processoDaRevistaDeMarca.IdRevistaMarcas.Value.ToString(), ", "));
                sql.Append(String.Concat(processoDaRevistaDeMarca.NumeroRevistaMarcas, ", "));

                sql.Append(processoDaRevistaDeMarca.DataPublicacao.HasValue
                          ? String.Concat(processoDaRevistaDeMarca.DataPublicacao.Value .ToString("yyyyMMdd"), ", ")
                          : "NULL, ");

                sql.Append(processoDaRevistaDeMarca.DataProcessamento.HasValue
                           ? String.Concat(processoDaRevistaDeMarca.DataProcessamento.Value.ToString("yyyyMMdd"), ", ")
                           : "NULL, ");

                sql.Append(String.Concat(processoDaRevistaDeMarca.NumeroProcessoDeMarca, ", "));
                          

                sql.Append(processoDaRevistaDeMarca.CodigoDespacho != null
                           ? String.Concat("'" + processoDaRevistaDeMarca.CodigoDespacho, "', ")
                           : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDeMarca.Apostila)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDeMarca.Apostila), "', ")
                           : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDeMarca.TextoDoDespacho)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDeMarca.TextoDoDespacho), "', ")
                           : "NULL, ");

                sql.Append(processoDaRevistaDeMarca.Processada ? "1, " : "0, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDeMarca.ExtensaoArquivo)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDeMarca.ExtensaoArquivo), "', ")
                           : "NULL, ");

                sql.Append(processoDaRevistaDeMarca.DataDeDeposito.HasValue
                           ? String.Concat(processoDaRevistaDeMarca.DataDeDeposito.Value.ToString("yyyyMMdd"), ", ")
                           : "NULL, ");

                sql.Append(processoDaRevistaDeMarca.DataDeConcessao.HasValue
                           ? String.Concat(processoDaRevistaDeMarca.DataDeConcessao.Value.ToString("yyyyMMdd"), ") ")
                           : "NULL) ");

                DBHelper.ExecuteNonQuery(sql.ToString());
            }
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

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeDeRegistros))
            {
                try
                {
                    while (leitor.Read())
                        revistas.Add(MontaRevistaDeMarcas(leitor));
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
            
            revistaDeMarcas.NumeroRevistaMarcas = UtilidadesDePersistencia.getValorInteger(leitor, "NumeroRevistaMarcas");

            if (UtilidadesDePersistencia.getValorDate(leitor, "DataPublicacao").HasValue)
            {
                revistaDeMarcas.DataPublicacao = UtilidadesDePersistencia.getValorDate(leitor, "DataPublicacao").Value;
            }
            
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
                        revistas.Add(MontaRevistaDeMarcas(leitor));
                }
                finally
                {
                    leitor.Close();
                }
            }

            return revistas;
        }

        public void Excluir(int numeroDaRevistaDeMarcas)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM MP_REVISTA_MARCAS ");
            sql.Append(String.Concat("WHERE NUMEROREVISTAMARCAS = ", numeroDaRevistaDeMarcas, " "));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
