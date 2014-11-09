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

            //sql.Append("SELECT NUMEROREVISTAMARCAS, DATAPUBLICACAO, DATAPROCESSAMENTO, ");
            //sql.Append("PROCESSADA, NUMEROPROCESSODEMARCA, CODIGODESPACHO, APOSTILA, EXTENSAOARQUIVO, ");
            //sql.Append("TEXTODODESPACHO, DATADODEPOSITO, DATACONCESSAO ");
            //sql.Append("FROM MP_REVISTA_MARCAS ");
            //sql.Append("WHERE PROCESSADA = 0 ");
            //sql.AppendLine(" ORDER BY NUMEROREVISTAMARCAS DESC");

            sql.Append("SELECT distinct(NUMEROREVISTAMARCAS), DATAPUBLICACAO, ");
            sql.Append("PROCESSADA, EXTENSAOARQUIVO ");
            sql.Append("FROM MP_REVISTA_MARCAS ");
            sql.Append("WHERE PROCESSADA = 0");
            sql.AppendLine(" ORDER BY NUMEROREVISTAMARCAS DESC");

            IList<IRevistaDeMarcas> revistas = new List<IRevistaDeMarcas>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeDeRegistros))
            {
                try
                {
                    while (leitor.Read())
                        revistas.Add(MontaRevistaDeMarcas(leitor, false));
                        //revistas.Add(MontaRevistaDeMarcas(leitor, true));
                }
                finally
                {
                    leitor.Close();
                }
            }

            return revistas;
        }

        private IRevistaDeMarcas MontaRevistaDeMarcas(IDataReader leitor, bool carregaCompleto)
        {
            var revistaDeMarcas = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDeMarcas>();

            revistaDeMarcas.NumeroRevistaMarcas = UtilidadesDePersistencia.getValorInteger(leitor, "NUMEROREVISTAMARCAS");

            if (UtilidadesDePersistencia.getValorDate(leitor, "DATAPUBLICACAO").HasValue)
                revistaDeMarcas.DataPublicacao = UtilidadesDePersistencia.getValorDate(leitor, "DATAPUBLICACAO").Value;

            revistaDeMarcas.Processada = UtilidadesDePersistencia.GetValorBooleano(leitor, "PROCESSADA");

            if (!UtilidadesDePersistencia.EhNulo(leitor, "EXTENSAOARQUIVO"))
                revistaDeMarcas.ExtensaoArquivo = UtilidadesDePersistencia.GetValorString(leitor, "EXTENSAOARQUIVO");


            if (carregaCompleto)
            {
                revistaDeMarcas.NumeroProcessoDeMarca = UtilidadesDePersistencia.GetValorLong(leitor,
                                                                                              "NUMEROPROCESSODEMARCA");

                if (UtilidadesDePersistencia.getValorDate(leitor, "DATAPROCESSAMENTO").HasValue)
                    revistaDeMarcas.DataProcessamento =
                        UtilidadesDePersistencia.getValorDate(leitor, "DATAPROCESSAMENTO").Value;


                if (!UtilidadesDePersistencia.EhNulo(leitor, "CODIGODESPACHO"))
                    revistaDeMarcas.CodigoDespacho = UtilidadesDePersistencia.GetValorString(leitor, "CODIGODESPACHO");

                if (!UtilidadesDePersistencia.EhNulo(leitor, "APOSTILA"))
                    revistaDeMarcas.Apostila = UtilidadesDePersistencia.GetValorString(leitor, "APOSTILA");


                if (!UtilidadesDePersistencia.EhNulo(leitor, "TEXTODODESPACHO"))
                    revistaDeMarcas.TextoDoDespacho = UtilidadesDePersistencia.GetValorString(leitor, "TEXTODODESPACHO");

                if (UtilidadesDePersistencia.getValorDate(leitor, "DATACONCESSAO").HasValue)
                    revistaDeMarcas.DataDeConcessao =
                        UtilidadesDePersistencia.getValorDate(leitor, "DATACONCESSAO").Value;

                if (UtilidadesDePersistencia.getValorDate(leitor, "DATADODEPOSITO").HasValue)
                    revistaDeMarcas.DataDeDeposito =
                        UtilidadesDePersistencia.getValorDate(leitor, "DATADODEPOSITO").Value;
            }

            return revistaDeMarcas;
        }

        public IList<IRevistaDeMarcas> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append("SELECT distinct(NUMEROREVISTAMARCAS), DATAPUBLICACAO, ");
            sql.Append("PROCESSADA, EXTENSAOARQUIVO ");
            sql.Append("FROM MP_REVISTA_MARCAS ");
            sql.Append("WHERE PROCESSADA = 1");
            sql.AppendLine(" ORDER BY NUMEROREVISTAMARCAS DESC");

            IList<IRevistaDeMarcas> revistas = new List<IRevistaDeMarcas>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeDeRegistros))
            {
                try
                {
                    while (leitor.Read())
                        revistas.Add(MontaRevistaDeMarcas(leitor, false));
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

        public IList<IRevistaDeMarcas> ObtenhaPublicoesDoProcesso(int numeroProcesso)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append("SELECT NUMEROREVISTAMARCAS, DATAPUBLICACAO, DATAPROCESSAMENTO, ");
            sql.Append("PROCESSADA, NUMEROPROCESSODEMARCA, CODIGODESPACHO, APOSTILA, EXTENSAOARQUIVO, ");
            sql.Append("TEXTODODESPACHO, DATADODEPOSITO, DATACONCESSAO ");
            sql.Append("FROM MP_REVISTA_MARCAS ");
            sql.Append("WHERE PROCESSADA = 1 ");
            sql.Append("AND NUMEROPROCESSODEMARCA = " + numeroProcesso);
            sql.AppendLine(" ORDER BY NUMEROREVISTAMARCAS DESC");

            IList<IRevistaDeMarcas> revistas = new List<IRevistaDeMarcas>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                try
                {
                    while (leitor.Read())
                        revistas.Add(MontaRevistaDeMarcas(leitor, true));
                }
                finally
                {
                    leitor.Close();
                }
            }

            return revistas;
        }

        public bool ExisteRevistaNoBanco(int numeroDaRevista)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append("SELECT NUMEROREVISTAMARCAS, DATAPUBLICACAO, ");
            sql.Append("PROCESSADA, EXTENSAOARQUIVO ");
            sql.Append("FROM MP_REVISTA_MARCAS ");
            sql.Append("WHERE NUMEROREVISTAMARCAS = " + numeroDaRevista);

            IList<IRevistaDeMarcas> revistas = new List<IRevistaDeMarcas>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                try
                {
                    while (leitor.Read())
                        revistas.Add(MontaRevistaDeMarcas(leitor, false));
                }
                finally
                {
                    leitor.Close();
                }
            }

            return revistas.Count > 0;
        }
    }
}
