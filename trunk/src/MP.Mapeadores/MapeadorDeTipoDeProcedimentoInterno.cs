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
    public class MapeadorDeTipoDeProcedimentoInterno : IMapeadorDeTipoDeProcedimentoInterno
    {
        public ITipoDeProcedimentoInterno obtenhaTipoProcedimentoInternoPeloId(long idTipoProcedimentosInternos)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDTIPO_PROCEDIMENTO_INTERNO, DESCRICAO_TIPO ");
            sql.Append("FROM MP_TIPO_PROCEDIMENTO_INTERNO ");
            sql.Append("WHERE IDTIPO_PROCEDIMENTO_INTERNO = " + idTipoProcedimentosInternos);

            ITipoDeProcedimentoInterno procedimentosInternos = null;

            IList<ITipoDeProcedimentoInterno> listaDeProcedimentosInternos = new List<ITipoDeProcedimentoInterno>();

            listaDeProcedimentosInternos = obtenhaTiposDeProcedimentoInterno(sql, int.MaxValue);

            if (listaDeProcedimentosInternos.Count > 0)
                procedimentosInternos = listaDeProcedimentosInternos[0];

            return procedimentosInternos;
        }

        public IList<ITipoDeProcedimentoInterno> obtenhaTipoProcedimentoInternoPelaDescricao(string descricao)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDTIPO_PROCEDIMENTO_INTERNO, DESCRICAO_TIPO ");
            sql.Append("FROM MP_TIPO_PROCEDIMENTO_INTERNO");

            if (!string.IsNullOrEmpty(descricao))
            {
                sql.Append(string.Concat("WHERE DESCRICAO_TIPO LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(descricao), "%'"));
            }

            return obtenhaTiposDeProcedimentoInterno(sql, int.MaxValue);
        }

        
        private IList<ITipoDeProcedimentoInterno> obtenhaTiposDeProcedimentoInterno(StringBuilder sql, int quantidadeMaximaRegistros)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();
            IList<ITipoDeProcedimentoInterno> listaDeTiposProcedimentoInterno = new List<ITipoDeProcedimentoInterno>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                while (leitor.Read() && listaDeTiposProcedimentoInterno.Count < quantidadeMaximaRegistros)
                {
                    var tipoDeProcedimentoInterno = FabricaGenerica.GetInstancia().CrieObjeto<ITipoDeProcedimentoInterno>();
                    tipoDeProcedimentoInterno.Id = UtilidadesDePersistencia.getValorInteger(leitor, "IDTIPO_PROCEDIMENTO_INTERNO");
                    tipoDeProcedimentoInterno.Descricao = UtilidadesDePersistencia.GetValorString(leitor, "DESCRICAO_TIPO");

                    listaDeTiposProcedimentoInterno.Add(tipoDeProcedimentoInterno);
                }
            }

            return listaDeTiposProcedimentoInterno;
        }

        public void Inserir(ITipoDeProcedimentoInterno tipoProcedimentoInterno)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            tipoProcedimentoInterno.Id = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_TIPO_PROCEDIMENTO_INTERNO (");
            sql.Append("IDTIPO_PROCEDIMENTO_INTERNO, DESCRICAO_TIPO) ");
            sql.Append("VALUES (");
            sql.Append(String.Concat(tipoProcedimentoInterno.Id.Value.ToString(), ", "));
            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(tipoProcedimentoInterno.Descricao), "') "));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(ITipoDeProcedimentoInterno tipoDeProcedimentoInterno)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_TIPO_PROCEDIMENTO_INTERNO SET ");
            sql.Append(String.Concat("DESCRICAO_TIPO = '", UtilidadesDePersistencia.FiltraApostrofe(tipoDeProcedimentoInterno.Descricao), "' "));
            sql.Append(String.Concat("WHERE IDTIPO_PROCEDIMENTO_INTERNO = ", tipoDeProcedimentoInterno.Id.Value.ToString()));
            
            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Excluir(long idTipoProcedimentoInterno)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM MP_TIPO_PROCEDIMENTO_INTERNO");
            sql.Append(string.Concat(" WHERE IDTIPO_PROCEDIMENTO_INTERNO = ", idTipoProcedimentoInterno.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
