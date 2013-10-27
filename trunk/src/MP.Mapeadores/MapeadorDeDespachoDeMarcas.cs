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
    public class MapeadorDeDespachoDeMarcas : IMapeadorDeDespachoDeMarcas
    {
   
        public IDespachoDeMarcas obtenhaDespachoDeMarcasPeloId(long idDespachoDeMarcas)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDDESPACHO, CODIGO_DESPACHO, DESCRICAO_DESPACHO, ");
            sql.Append("SITUACAODOPROCESSO, PRAZOPROVIDENCIA, PROVIDENCIA, DESATIVAPROCESSO, DESATIVAPESQCOLIDENCIA ");
            sql.Append("FROM MP_DESPACHO_MARCA ");
            sql.Append("WHERE IDDESPACHO = " + idDespachoDeMarcas);

            IDespachoDeMarcas despachoDeMarcas = null;

            IList<IDespachoDeMarcas> listaDeDespachoDeMarcas = new List<IDespachoDeMarcas>();

            listaDeDespachoDeMarcas = obtenhaDespachoDeMarcas(sql, int.MaxValue);

            if (listaDeDespachoDeMarcas.Count > 0)
                despachoDeMarcas = listaDeDespachoDeMarcas[0];

            return despachoDeMarcas;
        }

        private IList<IDespachoDeMarcas> obtenhaDespachoDeMarcas(StringBuilder sql, int quantidadeMaximaRegistros)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();
            IList<IDespachoDeMarcas> listaDeDespachoDeMarcas = new List<IDespachoDeMarcas>();
            
            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeMaximaRegistros))
            {
                while (leitor.Read())
                {
                    var despachoDeMarcas = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDeMarcas>();
                    despachoDeMarcas.IdDespacho = UtilidadesDePersistencia.getValorInteger(leitor, "IDDESPACHO");
                    despachoDeMarcas.CodigoDespacho = UtilidadesDePersistencia.GetValorString(leitor, "CODIGO_DESPACHO");
                    despachoDeMarcas.DescricaoDespacho = UtilidadesDePersistencia.GetValorString(leitor, "DESCRICAO_DESPACHO");

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "SITUACAODOPROCESSO"))
                        despachoDeMarcas.SituacaoProcesso = UtilidadesDePersistencia.GetValorString(leitor, "SITUACAODOPROCESSO");

                    despachoDeMarcas.PrazoParaProvidenciaEmDias = UtilidadesDePersistencia.getValorInteger(leitor, "PRAZOPROVIDENCIA");

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "PROVIDENCIA"))
                        despachoDeMarcas.Providencia = UtilidadesDePersistencia.GetValorString(leitor, "PROVIDENCIA");

                    despachoDeMarcas.DesativaProcesso = UtilidadesDePersistencia.GetValorBooleano(leitor, "DESATIVAPROCESSO");

                    despachoDeMarcas.DesativaPesquisaDeColidencia = UtilidadesDePersistencia.GetValorBooleano(leitor, "DESATIVAPESQCOLIDENCIA");

                    listaDeDespachoDeMarcas.Add(despachoDeMarcas);
                }
            }

            return listaDeDespachoDeMarcas;
        }

        public IList<IDespachoDeMarcas> ObtenhaPorCodigoDoDespachoComoFiltro(string codigo, int quantidadeMaximaDeRegistros)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDDESPACHO, CODIGO_DESPACHO, DESCRICAO_DESPACHO, ");
            sql.Append("SITUACAODOPROCESSO, PRAZOPROVIDENCIA, PROVIDENCIA, DESATIVAPROCESSO, DESATIVAPESQCOLIDENCIA ");
            sql.Append("FROM MP_DESPACHO_MARCA ");

            if (!String.IsNullOrEmpty(codigo))
                sql.Append(string.Concat("WHERE CODIGO_DESPACHO = '", UtilidadesDePersistencia.FiltraApostrofe(codigo), "'"));

            IList<IDespachoDeMarcas> listaDeDespachoDeMarcas = new List<IDespachoDeMarcas>();

            listaDeDespachoDeMarcas = obtenhaDespachoDeMarcas(sql, quantidadeMaximaDeRegistros);
            
            return listaDeDespachoDeMarcas;
        }

        public void Inserir(IDespachoDeMarcas despachoDeMarcas)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            despachoDeMarcas.IdDespacho = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_DESPACHO_MARCA (");
            sql.Append("IDDESPACHO, CODIGO_DESPACHO, DESCRICAO_DESPACHO, SITUACAODOPROCESSO, PRAZOPROVIDENCIA, PROVIDENCIA, DESATIVAPESQCOLIDENCIA, DESATIVAPROCESSO) ");
            sql.Append("VALUES (");
            sql.Append(String.Concat(despachoDeMarcas.IdDespacho.Value.ToString(), ", "));
            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(despachoDeMarcas.CodigoDespacho), "', "));
            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(despachoDeMarcas.DescricaoDespacho), "', "));

            sql.Append(!string.IsNullOrEmpty(despachoDeMarcas.SituacaoProcesso)
                           ? String.Concat("'",
                                           UtilidadesDePersistencia.FiltraApostrofe(despachoDeMarcas.SituacaoProcesso),
                                           "', ")
                           : "NULL, ");
            
            sql.Append(String.Concat(despachoDeMarcas.PrazoParaProvidenciaEmDias, ", "));

            sql.Append(!string.IsNullOrEmpty(despachoDeMarcas.Providencia)
                           ? String.Concat("'",
                                           UtilidadesDePersistencia.FiltraApostrofe(despachoDeMarcas.Providencia),
                                           "', ")
                           : "NULL, ");

            sql.Append(despachoDeMarcas.DesativaPesquisaDeColidencia ? String.Concat("'", 1, "', ") : String.Concat("'", 0, "', "));

            sql.Append(despachoDeMarcas.DesativaProcesso ? String.Concat("'", 1, "') ") : String.Concat("'", 0, "') "));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(IDespachoDeMarcas despachoDeMarcas)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_DESPACHO_MARCA SET ");
            sql.Append(String.Concat("CODIGO_DESPACHO = '", UtilidadesDePersistencia.FiltraApostrofe(despachoDeMarcas.CodigoDespacho), "', "));
            sql.Append(String.Concat("DESCRICAO_DESPACHO = '", UtilidadesDePersistencia.FiltraApostrofe(despachoDeMarcas.DescricaoDespacho), "', "));

            sql.Append(!string.IsNullOrEmpty(despachoDeMarcas.SituacaoProcesso)
                           ? String.Concat("SITUACAODOPROCESSO = '", UtilidadesDePersistencia.FiltraApostrofe(despachoDeMarcas.SituacaoProcesso), "', ")
                           : "SITUACAODOPROCESSO = NULL, ");

            sql.Append(String.Concat("PRAZOPROVIDENCIA = ", despachoDeMarcas.PrazoParaProvidenciaEmDias, ", "));

            sql.Append(!string.IsNullOrEmpty(despachoDeMarcas.Providencia)
                          ? String.Concat("PROVIDENCIA = '", UtilidadesDePersistencia.FiltraApostrofe(despachoDeMarcas.Providencia), "', ")
                          : "PROVIDENCIA = NULL, ");

            sql.Append(despachoDeMarcas.DesativaPesquisaDeColidencia
                           ? String.Concat("DESATIVAPESQCOLIDENCIA = '", 1, "', ")
                           : String.Concat("DESATIVAPESQCOLIDENCIA = '", 0, "', "));

            
            sql.Append(despachoDeMarcas.DesativaProcesso
                           ? String.Concat("DESATIVAPROCESSO = '", 1, "' ")
                           : String.Concat("DESATIVAPROCESSO = '", 0, "' "));

            sql.Append(String.Concat("WHERE IDDESPACHO = ", despachoDeMarcas.IdDespacho.Value.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Excluir(long idDespachoDeMarcas)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM MP_DESPACHO_MARCA");
            sql.Append(string.Concat(" WHERE IDDESPACHO = ", idDespachoDeMarcas.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
