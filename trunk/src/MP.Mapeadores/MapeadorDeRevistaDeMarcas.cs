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
        public void InserirELerRevistaXml(IRevistaDeMarcas revistaDeMarcas, XmlDocument revistaXml)
        {
            revistaDeMarcas.DataPublicacao = Convert.ToDateTime(ObtenhaDataDePublicacaoDaRevista(revistaXml));

            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            revistaDeMarcas.IdRevistaMarcas = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_REVISTA_MARCAS (");
            sql.Append("IDREVISTAMARCAS, NUMEROREVISTAMARCAS, DATAPUBLICACAO, PROCESSADA, EXTENSAOARQUIVO) ");
            sql.Append("VALUES (");
            sql.Append(String.Concat(revistaDeMarcas.IdRevistaMarcas.Value.ToString(), ", "));
            sql.Append(String.Concat(revistaDeMarcas.NumeroRevistaMarcas, ", "));

            sql.Append(String.Concat(revistaDeMarcas.DataPublicacao.ToString("yyyyMMdd"), ", "));

            sql.Append(revistaDeMarcas.Processada ? "1, " : "0, ");

            if (!string.IsNullOrEmpty(revistaDeMarcas.ExtensaoArquivo))
                sql.Append(String.Concat("'", revistaDeMarcas.ExtensaoArquivo, "' ) "));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        private string ObtenhaDataDePublicacaoDaRevista(XmlDocument revistaXml)
        {
            var xmlrevista = revistaXml.GetElementsByTagName("revista");

            for (int i = 0; i < xmlrevista.Count; i++)
            {
                return xmlrevista[i].Attributes.GetNamedItem("data").Value;
            }

            return string.Empty;
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

            revistaDeMarcas.IdRevistaMarcas = UtilidadesDePersistencia.GetValorLong(leitor, "IdRevistaMarcas");

            revistaDeMarcas.NumeroRevistaMarcas = UtilidadesDePersistencia.getValorInteger(leitor, "NumeroRevistaMarcas");

            revistaDeMarcas.DataPublicacao = UtilidadesDePersistencia.getValorDate(leitor, "DataPublicacao").Value;

            revistaDeMarcas.Processada = UtilidadesDePersistencia.GetValorBooleano(leitor, "Processada");

             if (!UtilidadesDePersistencia.EhNulo(leitor, "ExtensaoArquivo"))
                 revistaDeMarcas.ExtensaoArquivo = UtilidadesDePersistencia.GetValorString(leitor, "NumeroRevistaMarcas");

            return revistaDeMarcas;
        }

        public IList<IRevistaDeMarcas> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append("SELECT IDREVISTAMARCAS IdRevistaMarcas, NUMEROREVISTAMARCAS NumeroRevistaMarcas, DATAPUBLICACAO DataPublicacao, ");
            sql.Append("PROCESSADA Processada, EXTENSAOARQUIVO ExtensaoArquivo ");
            sql.Append("FROM MP_REVISTA_MARCAS ");
            sql.Append("WHERE PROCESSADA = 1");
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
    }
}
