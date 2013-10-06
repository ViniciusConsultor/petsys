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
    public class MapeadorDeProcedimentosInternos : IMapeadorDeProcedimentosInternos
    {
        public IList<IProcedimentosInternos> obtenhaTodosProcedimentosInternos()
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDTIPO_ANDAMENTO_INTERNO as IdTipoAndamentoInterno, DESCRICAO_TIPO as DescricaoTipo ");
            sql.Append("FROM MP_TIPO_ANDAMENTO_INTERNO");

            var DBHelper = ServerUtils.criarNovoDbHelper();

            IList<IProcedimentosInternos> listaDeProcedimentosInternos = new List<IProcedimentosInternos>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                while (leitor.Read())
                {
                    var procedimentoInterno = FabricaGenerica.GetInstancia().CrieObjeto<IProcedimentosInternos>();
                    procedimentoInterno.IdTipoAndamentoInterno = UtilidadesDePersistencia.getValorInteger(leitor, "IdTipoAndamentoInterno");
                    procedimentoInterno.DescricaoTipo = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoTipo");

                    listaDeProcedimentosInternos.Add(procedimentoInterno);
                }
            }

            return listaDeProcedimentosInternos;
        }

        public IProcedimentosInternos obtenhaProcedimentosInternosPeloId(long idProcedimentosInternos)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDTIPO_ANDAMENTO_INTERNO as IdTipoAndamentoInterno, DESCRICAO_TIPO as DescricaoTipo ");
            sql.Append("FROM MP_TIPO_ANDAMENTO_INTERNO ");
            sql.Append("WHERE IDTIPO_ANDAMENTO_INTERNO = " + idProcedimentosInternos);
            
            IProcedimentosInternos procedimentosInternos = null;

            IList<IProcedimentosInternos> listaDeProcedimentosInternos = new List<IProcedimentosInternos>();

            listaDeProcedimentosInternos = obtenhaProcedimentosInternos(sql, int.MaxValue);

            if (listaDeProcedimentosInternos.Count > 0)
                procedimentosInternos = listaDeProcedimentosInternos[0];

            return procedimentosInternos;
        }

        private IList<IProcedimentosInternos> obtenhaProcedimentosInternos(StringBuilder sql, int quantidadeMaximaRegistros)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();
            IList<IProcedimentosInternos> listaDeProcedimentosInternos = new List<IProcedimentosInternos>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                while (leitor.Read() && listaDeProcedimentosInternos.Count < quantidadeMaximaRegistros)
                {
                    var procedimentoInterno = FabricaGenerica.GetInstancia().CrieObjeto<IProcedimentosInternos>();
                    procedimentoInterno.IdTipoAndamentoInterno = UtilidadesDePersistencia.getValorInteger(leitor, "IdTipoAndamentoInterno");
                    procedimentoInterno.DescricaoTipo = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoTipo");

                    listaDeProcedimentosInternos.Add(procedimentoInterno);
                }
            }

            return listaDeProcedimentosInternos;
        }

        public IProcedimentosInternos obtenhaProcedimentosInternosPelaDescricao(string descricaoProcedimentosInternos)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDTIPO_ANDAMENTO_INTERNO as IdTipoAndamentoInterno, DESCRICAO_TIPO as DescricaoTipo ");
            sql.Append("FROM MP_TIPO_ANDAMENTO_INTERNO");

            if (!string.IsNullOrEmpty(descricaoProcedimentosInternos))
            {
                sql.Append(string.Concat("WHERE DESCRICAO_TIPO LIKE '", UtilidadesDePersistencia.FiltraApostrofe(descricaoProcedimentosInternos), "%'"));
            }

            IProcedimentosInternos procedimentosInternos = null;

            IList<IProcedimentosInternos> listaDeProcedimentosInternos = new List<IProcedimentosInternos>();

            listaDeProcedimentosInternos = obtenhaProcedimentosInternos(sql, int.MaxValue);

            if (listaDeProcedimentosInternos.Count > 0)
                procedimentosInternos = listaDeProcedimentosInternos[0];

            return procedimentosInternos;
        }

        public void Inserir(IProcedimentosInternos procedimentosInternos)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            procedimentosInternos.IdTipoAndamentoInterno = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_TIPO_ANDAMENTO_INTERNO (");
            sql.Append("IDTIPO_ANDAMENTO_INTERNO, DESCRICAO_TIPO) ");
            sql.Append("VALUES (");
            sql.Append(String.Concat(procedimentosInternos.IdTipoAndamentoInterno.Value.ToString(), ", "));
            sql.Append(String.Concat("'", procedimentosInternos.DescricaoTipo, "') "));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(IProcedimentosInternos procedimentosInternos)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            procedimentosInternos.IdTipoAndamentoInterno = GeradorDeID.getInstancia().getProximoID();

            sql.Append("UPDATE MP_TIPO_ANDAMENTO_INTERNO SET ");
            sql.Append(String.Concat("DESCRICAO_TIPO = '", UtilidadesDePersistencia.FiltraApostrofe(procedimentosInternos.DescricaoTipo), "' "));
            sql.Append(String.Concat("WHERE IDTIPO_ANDAMENTO_INTERNO = ", procedimentosInternos.IdTipoAndamentoInterno.Value.ToString()));
            
            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Excluir(long idProcedimentosInternos)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("delete from MP_TIPO_ANDAMENTO_INTERNO");
            sql.Append(string.Concat(" where IDTIPO_ANDAMENTO_INTERNO = ", idProcedimentosInternos.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
