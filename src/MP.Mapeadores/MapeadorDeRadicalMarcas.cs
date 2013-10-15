using System;
using System.Collections.Generic;
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
    public class MapeadorDeRadicalMarcas : IMapeadorDeRadicalMarcas
    {
        public IList<IRadicalMarcas> ObtenhaPorIdDoRadicalMarcasComoFiltro(string idRadicalMarcas, int quantidadeMaximaDeRegistros)
        {
            return new List<IRadicalMarcas>();
        }

         private StringBuilder retornaSQLSelecionaTodos()
         {
             var sql = new StringBuilder();

             sql.Append("SELECT IDRADICAL IdRadicalMarca, DESCRICAORADICAL DescricaoRadical, IDMARCA IdMarca, CODIGONCL NCL ");
             sql.Append("FROM MP_RADICAL_MARCA ");

             return sql;
         }

        public IList<IRadicalMarcas> obtenhaRadicalMarcasPelaDescricaoComoFiltro(string descricaoDoRadicalMarcas, int quantidadeMaximaDeRegistros)
        {
            var sql = new StringBuilder();

            sql = retornaSQLSelecionaTodos();

            if (!string.IsNullOrEmpty(descricaoDoRadicalMarcas))
            {
                sql.Append(string.Concat("WHERE DescricaoDaMarca LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(descricaoDoRadicalMarcas), "%' "));
            }

            return obtenhaRadicalMarcas(sql, quantidadeMaximaDeRegistros);
        }

        private IList<IRadicalMarcas> obtenhaRadicalMarcas(StringBuilder sql, int quantidadeMaximaDeRegistros)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();
            IList<IRadicalMarcas> listaDeRadicalMarcas = new List<IRadicalMarcas>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeMaximaDeRegistros))
            {
                while (leitor.Read())
                {
                    var radicalMarcas = FabricaGenerica.GetInstancia().CrieObjeto<IRadicalMarcas>();
                    radicalMarcas.IdRadicalMarca = UtilidadesDePersistencia.getValorInteger(leitor, "IdRadicalMarca");
                    radicalMarcas.DescricaoRadical = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoRadical");
                    radicalMarcas.IdMarca = UtilidadesDePersistencia.getValorInteger(leitor, "IdMarca");

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "NCL"))
                        radicalMarcas.NCL = NCL.ObtenhaPorCodigo(UtilidadesDePersistencia.getValorInteger(leitor, "NCL"));

                    listaDeRadicalMarcas.Add(radicalMarcas);
                }
            }

            return listaDeRadicalMarcas;
        }

        public IRadicalMarcas obtenhaRadicalMarcasPeloId(long idRadicalMarcas)
        {
            var sql = new StringBuilder();

            sql = retornaSQLSelecionaTodos();
            sql.Append("WHERE IDRADICAL = " + idRadicalMarcas);

            IRadicalMarcas radicalMarca = null;

            IList<IRadicalMarcas> listaDeRadicalMarcas = new List<IRadicalMarcas>();

            listaDeRadicalMarcas = obtenhaRadicalMarcas(sql, int.MaxValue);

            if (listaDeRadicalMarcas.Count > 0)
                radicalMarca = listaDeRadicalMarcas[0];

            return radicalMarca;
        }

        public void Inserir(IRadicalMarcas radicalMarcas)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            radicalMarcas.IdRadicalMarca = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_RADICAL_MARCA (");
            sql.Append("IDRADICAL, DESCRICAORADICAL, IDMARCA, CODIGONCL) ");
            sql.Append("VALUES (");
            sql.Append(String.Concat(radicalMarcas.IdRadicalMarca.Value.ToString(), ", "));
            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(radicalMarcas.DescricaoRadical), "', "));
            sql.Append(String.Concat("'", radicalMarcas.IdMarca.Value.ToString(), "', "));

            if (radicalMarcas.NCL != null && string.IsNullOrEmpty(radicalMarcas.NCL.Codigo.ToString()))
            {
                sql.Append(String.Concat("'", radicalMarcas.NCL.Codigo.ToString(), "') "));
            }
            else
            {
                sql.Append(String.Concat("'", 0, "') "));
            }

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(IRadicalMarcas radicalMarcas)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_RADICAL_MARCA SET ");
            sql.Append(String.Concat("DESCRICAORADICAL = '", UtilidadesDePersistencia.FiltraApostrofe(radicalMarcas.DescricaoRadical), "' , "));
            sql.Append(String.Concat("IDMARCA = '", radicalMarcas.IdMarca.Value.ToString(), "' , "));

            if(radicalMarcas.NCL != null && radicalMarcas.NCL.Codigo != 0)
            {
                sql.Append(String.Concat("CODIGONCL = '", radicalMarcas.NCL.Codigo.ToString(), "' , "));
            }
            else
            {
                sql.Append(String.Concat("CODIGONCL = '", 0, "' , "));
            }

            sql.Append(String.Concat("WHERE IDRADICAL = ", radicalMarcas.IdRadicalMarca.Value.ToString()));


            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Excluir(long idRadicalMarcas)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM MP_RADICAL_MARCA");
            sql.Append(string.Concat(" WHERE IDRADICAL = ", idRadicalMarcas.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }


        public IList<IRadicalMarcas> obtenhaRadicalMarcasPeloIdDaMarcaComoFiltro(long idMarca, int quantidadeMaximaDeRegistros)
        {
            var sql = new StringBuilder();

            sql = retornaSQLSelecionaTodos();

            if (!string.IsNullOrEmpty(idMarca.ToString()))
            {
                sql.Append(string.Concat("WHERE IDMARCA =", idMarca, " "));
            }

            return obtenhaRadicalMarcas(sql, quantidadeMaximaDeRegistros);
        }
    }
}
