using System;
using System.Collections.Generic;
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
    public class MapeadorDeMarcas : IMapeadorDeMarcas
    {
        public IMarcas obtenhaMarcasPeloId(long idMarca)
        {
            var sql = new StringBuilder();

            sql = retornaSQLSelecionaTodos();
            sql.Append("WHERE IDMARCA = " + idMarca);

            IMarcas marca = null;

            IList<IMarcas> listaDeMarcas = new List<IMarcas>();

            listaDeMarcas = obtenhaMarca(sql, int.MaxValue);

            if (listaDeMarcas.Count > 0)
                marca = listaDeMarcas[0];

            return marca;
        }

        private IList<IMarcas> obtenhaMarca(StringBuilder sql, int quantidadeMaximaRegistros)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();
            IList<IMarcas> listaDeMarcas = new List<IMarcas>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeMaximaRegistros))
            {
                while (leitor.Read())
                {
                    var marca = FabricaGenerica.GetInstancia().CrieObjeto<IMarcas>();
                    marca.IdMarca = UtilidadesDePersistencia.getValorInteger(leitor, "IdMarca");
                    marca.NCL = NCL.ObtenhaPorCodigo(UtilidadesDePersistencia.GetValorString(leitor, "NCL"));
                    marca.Apresentacao =
                        Apresentacao.ObtenhaPorCodigo(UtilidadesDePersistencia.getValorInteger(leitor, "Apresentacao"));

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "CodigoDaClasse"))
                        marca.CodigoDaClasse = UtilidadesDePersistencia.getValorInteger(leitor, "CodigoDaClasse");

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "CodigoDaSubClasse1"))
                        marca.CodigoDaSubClasse1 = UtilidadesDePersistencia.getValorInteger(leitor, "CodigoDaSubClasse1");

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "CodigoDaSubClasse2"))
                        marca.CodigoDaSubClasse2 = UtilidadesDePersistencia.getValorInteger(leitor, "CodigoDaSubClasse2");

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "CodigoDaSubClasse3"))
                        marca.CodigoDaSubClasse3 = UtilidadesDePersistencia.getValorInteger(leitor, "CodigoDaSubClasse3");

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "DescricaoDaMarca"))
                        marca.DescricaoDaMarca = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoDaMarca");

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "EspecificacaoDeProdutosEServicos"))
                        marca.EspecificacaoDeProdutosEServicos = UtilidadesDePersistencia.GetValorString(leitor, "EspecificacaoDeProdutosEServicos");

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "ImagemDaMarca"))
                        marca.ImagemDaMarca = UtilidadesDePersistencia.GetValorString(leitor, "ImagemDaMarca");

                    marca.Natureza =
                        NaturezaDeMarca.ObtenhaPorCodigo(UtilidadesDePersistencia.getValorInteger(leitor, "Natureza"));

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "ObservacaoDaMarca"))
                        marca.ObservacaoDaMarca = UtilidadesDePersistencia.GetValorString(leitor, "ObservacaoDaMarca");

                    var cliente =
                        FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IClienteLazyLoad>(
                            UtilidadesDePersistencia.GetValorLong(leitor, "Cliente"));

                    marca.Cliente = cliente;

                    listaDeMarcas.Add(marca);
                }
            }

            return listaDeMarcas;
        }

        private StringBuilder retornaSQLSelecionaTodos()
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDMARCA IdMarca, CODIGONCL NCL, CODIGOAPRESENTACAO Apresentacao, IDCLIENTE Cliente, CODIGONATUREZA Natureza, ");
            sql.Append("DESCRICAO_MARCA DescricaoDaMarca, ESPECIFICACAO_PROD_SERV EspecificacaoDeProdutosEServicos, IMAGEM_MARCA ImagemDaMarca, OBSERVACAO_MARCA ObservacaoDaMarca, CODIGOCLASSE CodigoDaClasse, ");
            sql.Append("CODIGOCLASSE_SUBCLASSE1 CodigoDaSubClasse1, CODIGOCLASSE_SUBCLASSE2 CodigoDaSubClasse2, CODIGOCLASSE_SUBCLASSE3 CodigoDaSubClasse3 ");
            sql.Append("FROM MP_MARCAS ");

            return sql;
        }

        public IList<IMarcas> obtenhaMarcasPelaDescricaoComoFiltro(string descricaoDaMarca, int quantidadeMaximaDeRegistros)
        {
            var sql = new StringBuilder();

            sql = retornaSQLSelecionaTodos();

            if (!string.IsNullOrEmpty(descricaoDaMarca))
            {
                sql.Append(string.Concat("WHERE DESCRICAO_MARCA LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(descricaoDaMarca), "%' "));
            }

            sql.AppendLine(" ORDER BY DescricaoDaMarca");

            return obtenhaMarca(sql, quantidadeMaximaDeRegistros);
        }

        public void Inserir(IMarcas marca)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            marca.IdMarca = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_MARCAS (");
            sql.Append("IDMARCA, CODIGONCL, CODIGOAPRESENTACAO, IDCLIENTE, CODIGONATUREZA, DESCRICAO_MARCA, ESPECIFICACAO_PROD_SERV, ");
            sql.Append("IMAGEM_MARCA, OBSERVACAO_MARCA, CODIGOCLASSE, CODIGOCLASSE_SUBCLASSE1, CODIGOCLASSE_SUBCLASSE2, CODIGOCLASSE_SUBCLASSE3) ");
            sql.Append("VALUES (");
            sql.Append(String.Concat(marca.IdMarca.Value.ToString(), ", "));
            sql.Append(String.Concat("'", marca.NCL.Codigo, "', "));
            sql.Append(String.Concat(marca.Apresentacao.Codigo.ToString(), ", "));
            sql.Append(String.Concat(marca.Cliente.Pessoa.ID.Value.ToString(), ", "));
            sql.Append(String.Concat(marca.Natureza.Codigo.ToString(), ", "));
            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(marca.DescricaoDaMarca), "', "));
            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(marca.EspecificacaoDeProdutosEServicos), "', "));
            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(marca.ImagemDaMarca), "', "));
            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(marca.ObservacaoDaMarca), "', "));

            sql.Append(marca.CodigoDaClasse.HasValue ? String.Concat(marca.CodigoDaClasse.ToString(), ", ") : "NULL, ");
            sql.Append(marca.CodigoDaSubClasse1.HasValue ? String.Concat(marca.CodigoDaSubClasse1.ToString(), ", ") : "NULL, ");
            sql.Append(marca.CodigoDaSubClasse2.HasValue ? String.Concat(marca.CodigoDaSubClasse2.ToString(), ", ") : "NULL, ");
            sql.Append(marca.CodigoDaSubClasse3.HasValue ? String.Concat(marca.CodigoDaSubClasse3.ToString(), ") ") : "NULL) ");

            DBHelper.ExecuteNonQuery(sql.ToString());

            if (marca.RadicalMarcas.Count > 0)
                InserirRadicalMarcas(marca);
        }

        public void Modificar(IMarcas marca)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_MARCAS SET ");
            sql.Append(String.Concat("CODIGOAPRESENTACAO = ", marca.Apresentacao.Codigo.ToString(), " , "));
            sql.Append(String.Concat("IDCLIENTE = ", marca.Cliente.Pessoa.ID.Value.ToString(), " , "));

            sql.Append(marca.CodigoDaClasse.HasValue
                           ? String.Concat("CODIGOCLASSE = ", marca.CodigoDaClasse.Value, " , ")
                           : "CODIGOCLASSE = NULL, ");

            sql.Append(marca.CodigoDaSubClasse1.HasValue
                           ? String.Concat("CODIGOCLASSE_SUBCLASSE1 = ", marca.CodigoDaSubClasse1.Value, " , ")
                           : "CODIGOCLASSE_SUBCLASSE1 = NULL, ");

            sql.Append(marca.CodigoDaSubClasse2.HasValue
                           ? String.Concat("CODIGOCLASSE_SUBCLASSE2 = ", marca.CodigoDaSubClasse2.Value, " , ")
                           : "CODIGOCLASSE_SUBCLASSE2 = NULL, ");

            sql.Append(marca.CodigoDaSubClasse3.HasValue
                           ? String.Concat("CODIGOCLASSE_SUBCLASSE3 = ", marca.CodigoDaSubClasse3.Value, " , ")
                           : "CODIGOCLASSE_SUBCLASSE3 = NULL, ");

            sql.Append(String.Concat("DESCRICAO_MARCA = '", UtilidadesDePersistencia.FiltraApostrofe(marca.DescricaoDaMarca), "' , "));
            sql.Append(String.Concat("ESPECIFICACAO_PROD_SERV = '", UtilidadesDePersistencia.FiltraApostrofe(marca.EspecificacaoDeProdutosEServicos), "' , "));
            sql.Append(String.Concat("IMAGEM_MARCA = '", marca.ImagemDaMarca, "' , "));
            sql.Append(String.Concat("CODIGONCL = '", marca.NCL.Codigo, "' , "));
            sql.Append(String.Concat("CODIGONATUREZA = ", marca.Natureza.Codigo, " , "));
            sql.Append(String.Concat("OBSERVACAO_MARCA = '", marca.ObservacaoDaMarca, "' "));
            sql.Append(String.Concat("WHERE IDMARCA = ", marca.IdMarca.Value.ToString()));


            DBHelper.ExecuteNonQuery(sql.ToString());
            ModificarRadicaisMarca(marca);
        }

        private void ModificarRadicaisMarca(IMarcas marca)
        {
            RemovaRadicaisDaMarca(marca.IdMarca.Value);
            InserirRadicalMarcas(marca);
        }

        private void RemovaRadicaisDaMarca(long idDaMarca )
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM MP_RADICAL_MARCA");
            sql.Append(string.Concat(" WHERE IDMARCA = ", idDaMarca.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString()); 
        }

        public void Excluir(long idMarca)
        {
            RemovaRadicaisDaMarca(idMarca);
            
            var sql = new StringBuilder();
            IDBHelper DBHelper;
            DBHelper = ServerUtils.getDBHelper();
            
            sql.Append("DELETE FROM MP_MARCAS");
            sql.Append(string.Concat(" WHERE IDMARCA = ", idMarca.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());

        }

        private void InserirRadicalMarcas(IMarcas marca)
        {
            foreach (var radical in marca.RadicalMarcas)
            {
                var sql = new StringBuilder();
                IDBHelper DBHelper;

                DBHelper = ServerUtils.getDBHelper();

                radical.IdRadicalMarca = GeradorDeID.getInstancia().getProximoID();

                sql.Append("INSERT INTO MP_RADICAL_MARCA (");
                sql.Append("IDRADICAL, DESCRICAORADICAL, CODIGONCL, IDMARCA) ");
                sql.Append("VALUES (");
                sql.Append(String.Concat(radical.IdRadicalMarca.Value.ToString(), ", "));
                sql.Append(String.Concat("'", UtilidadesDeString.RemoveAcentos(radical.DescricaoRadical), "', "));

                sql.Append(radical.NCL != null && !string.IsNullOrEmpty(radical.NCL.Codigo)
                           ? String.Concat("'", radical.NCL.Codigo, "', ")
                           : "NULL , ");

                sql.Append(String.Concat(marca.IdMarca.Value.ToString(), ") "));
                DBHelper.ExecuteNonQuery(sql.ToString());
            }
        }
    }
}
