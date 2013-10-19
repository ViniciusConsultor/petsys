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
    public class MapeadorDeDespachoDePatentes : IMapeadorDeDespachoDePatentes
    {
        public IDespachoDePatentes obtenhaDespachoDePatentesPeloId(long idDespachoDePatentes)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDDESPACHOPATENTE IdDespachoDePatente, CODIGO_DESPACHO CodigoDespachoDePatente, DETALHE_DESPACHO DetalheDespachoDePatente, ");
            sql.Append("CODIGO_SITUACAOPROCESSOPATENTE CodigoSituacaoProcessoDePatente, DESCRICAO DescricaoDespachoDePatente ");
            sql.Append("FROM MP_DESPACHO_PATENTE ");
            sql.Append("WHERE IDDESPACHOPATENTE = " + idDespachoDePatentes);

            IDespachoDePatentes despachoDePatentes = null;

            IList<IDespachoDePatentes> listaDeDespachoDePatentes = new List<IDespachoDePatentes>();

            listaDeDespachoDePatentes = obtenhaDespachoDePatentes(sql, int.MaxValue);

            if (listaDeDespachoDePatentes.Count > 0)
                despachoDePatentes = listaDeDespachoDePatentes[0];

            return despachoDePatentes;
        }

        private IList<IDespachoDePatentes> obtenhaDespachoDePatentes(StringBuilder sql, int quantidadeMaximaRegistros)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();
            IList<IDespachoDePatentes> listaDeDespachoDePatentes = new List<IDespachoDePatentes>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeMaximaRegistros))
            {
                while (leitor.Read())
                {
                    var despachoDePatentes = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDePatentes>();
                    despachoDePatentes.IdDespachoDePatente = UtilidadesDePersistencia.getValorInteger(leitor, "IdDespachoDePatente");
                    despachoDePatentes.CodigoDespachoDePatente = UtilidadesDePersistencia.GetValorString(leitor, "CodigoDespachoDePatente");

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "DetalheDespachoDePatente"))
                        despachoDePatentes.DetalheDespachoDePatente = UtilidadesDePersistencia.GetValorString(leitor, "DetalheDespachoDePatente");

                    //if (!UtilidadesDePersistencia.EhNulo(leitor, "CodigoSituacaoProcesso"))
                    despachoDePatentes.SituacaoDoProcessoDePatente = SituacaoDoProcessoDePatente.ObtenhaPorCodigo(UtilidadesDePersistencia.GetValorString(leitor, "CodigoSituacaoProcessoDePatente"));

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "DescricaoDespachoDePatente"))
                        despachoDePatentes.DescricaoDespachoDePatente = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoDespachoDePatente");

                    listaDeDespachoDePatentes.Add(despachoDePatentes);
                }
            }

            return listaDeDespachoDePatentes;
        }

        public IList<IDespachoDePatentes> ObtenhaPorCodigoDoDespachoComoFiltro(string codigo, int quantidadeMaximaDeRegistros)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDDESPACHOPATENTE IdDespachoDePatente, CODIGO_DESPACHO CodigoDespachoDePatente, DETALHE_DESPACHO DetalheDespachoDePatente, ");
            sql.Append("CODIGO_SITUACAOPROCESSOPATENTE CodigoSituacaoProcessoDePatente, DESCRICAO DescricaoDespachoDePatente ");
            sql.Append("FROM MP_DESPACHO_PATENTE ");

            if (!string.IsNullOrEmpty(codigo))
            {
                sql.Append(string.Concat("WHERE CODIGO_DESPACHO = '", codigo, "'"));
            }

            IList<IDespachoDePatentes> listaDeDespachoDePatentes = new List<IDespachoDePatentes>();

            listaDeDespachoDePatentes = obtenhaDespachoDePatentes(sql, quantidadeMaximaDeRegistros);

            return listaDeDespachoDePatentes;
        }

        public void Inserir(IDespachoDePatentes despachoDePatentes)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            despachoDePatentes.IdDespachoDePatente = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_DESPACHO_PATENTE (");
            sql.Append("IDDESPACHOPATENTE, CODIGO_DESPACHO, DETALHE_DESPACHO, CODIGO_SITUACAOPROCESSOPATENTE, DESCRICAO) ");
            sql.Append("VALUES (");
            sql.Append(String.Concat(despachoDePatentes.IdDespachoDePatente.Value.ToString(), ", "));
            sql.Append(String.Concat("'", despachoDePatentes.CodigoDespachoDePatente, "', "));

            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(despachoDePatentes.DetalheDespachoDePatente), "', "));

            sql.Append(String.Concat("'", despachoDePatentes.SituacaoDoProcessoDePatente.CodigoSituacaoProcessoDePatente, "', "));

            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(despachoDePatentes.DescricaoDespachoDePatente), "') "));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(IDespachoDePatentes despachoDePatentes)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_DESPACHO_PATENTE SET ");
            sql.Append(String.Concat("CODIGO_DESPACHO = '", despachoDePatentes.CodigoDespachoDePatente, "', "));
            sql.Append(String.Concat("DETALHE_DESPACHO = '", despachoDePatentes.DetalheDespachoDePatente, "', "));
            sql.Append(String.Concat("CODIGO_SITUACAOPROCESSOPATENTE = '", despachoDePatentes.SituacaoDoProcessoDePatente.CodigoSituacaoProcessoDePatente, "', "));
            sql.Append(String.Concat("DESCRICAO = '", despachoDePatentes.DescricaoDespachoDePatente, "' "));

            sql.Append(String.Concat("WHERE IDDESPACHOPATENTE = ", despachoDePatentes.IdDespachoDePatente.Value.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Excluir(long idDespachoDePatentes)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM MP_DESPACHO_PATENTE");
            sql.Append(string.Concat(" WHERE IDDESPACHOPATENTE = ", idDespachoDePatentes.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
