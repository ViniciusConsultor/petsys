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
    public class MapeadorTipoDePatente : IMapeadorTipoDePatente
    {
        public IList<ITipoDePatente> obtenhaTodosTiposDePatentes()
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDTIPO_PATENTE, DESCRICAO_TIPO_PATENTE, SIGLA_TIPO, TEMPO_INICIO_ANOS, QUANTIDADE_PAGTO, ");
            sql.Append("TEMPO_ENTRE_PAGTO, SEQUENCIA_INICIO_PAGTO, TEM_PAGTO_INTERMEDIARIO, INICIO_INTERMED_SEQUENCIA, QUANTIDADE_PAGTO_INTERMED, ");
            sql.Append("TEMPO_ENTRE_PAGTO_INTERMED, DESCRICAO_PAGTO, DESCRICAO_PAGTO_INTERMED, TEM_PED_EXAME ");
            sql.Append("FROM TIPO_PATENTE");

            var DBHelper = ServerUtils.criarNovoDbHelper();

            IList<ITipoDePatente> listaDeTiposDePatentes = new List<ITipoDePatente>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                while (leitor.Read())
                {
                    var tipoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<ITipoDePatente>();
                    tipoDePatente.IdTipoPatente = UtilidadesDePersistencia.getValorInteger(leitor, "IDTIPO_PATENTE");
                    tipoDePatente.DescricaoTipoDePatente = UtilidadesDePersistencia.GetValorString(leitor, "DESCRICAO_TIPO_PATENTE");
                    tipoDePatente.SiglaTipo = UtilidadesDePersistencia.getValorChar(leitor, "SIGLA_TIPO");
                    tipoDePatente.TempoInicioAnos = UtilidadesDePersistencia.getValorInteger(leitor, "TEMPO_INICIO_ANOS");
                    tipoDePatente.QuantidadePagto = UtilidadesDePersistencia.getValorInteger(leitor, "QUANTIDADE_PAGTO");
                    tipoDePatente.TempoEntrePagto = UtilidadesDePersistencia.getValorInteger(leitor, "TEMPO_ENTRE_PAGTO");
                    tipoDePatente.SequenciaInicioPagto = UtilidadesDePersistencia.getValorInteger(leitor, "SEQUENCIA_INICIO_PAGTO");
                    tipoDePatente.TemPagtoIntermediario = UtilidadesDePersistencia.GetValorBooleano(leitor, "TEM_PAGTO_INTERMEDIARIO");
                    tipoDePatente.InicioIntermedSequencia = UtilidadesDePersistencia.getValorInteger(leitor, "INICIO_INTERMED_SEQUENCIA");
                    tipoDePatente.QuantidadePagtoIntermed = UtilidadesDePersistencia.getValorInteger(leitor, "QUANTIDADE_PAGTO_INTERMED");
                    tipoDePatente.TempoEntrePagtoIntermed = UtilidadesDePersistencia.getValorInteger(leitor, "TEMPO_ENTRE_PAGTO_INTERMED");
                    tipoDePatente.DescricaoPagto = UtilidadesDePersistencia.GetValorString(leitor, "DESCRICAO_PAGTO");
                    tipoDePatente.DescricaoPagtoIntermed = UtilidadesDePersistencia.GetValorString(leitor, "DESCRICAO_PAGTO_INTERMED");
                    tipoDePatente.TemPedExame = UtilidadesDePersistencia.GetValorBooleano(leitor, "TEM_PED_EXAME");

                    listaDeTiposDePatentes.Add(tipoDePatente);
                }
            }

            return listaDeTiposDePatentes;
        }

        public ITipoDePatente obtenhaTipoDePatentePeloId(long idTipoPatente)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDTIPO_PATENTE, DESCRICAO_TIPO_PATENTE, SIGLA_TIPO, TEMPO_INICIO_ANOS, QUANTIDADE_PAGTO, ");
            sql.Append("TEMPO_ENTRE_PAGTO, SEQUENCIA_INICIO_PAGTO, TEM_PAGTO_INTERMEDIARIO, INICIO_INTERMED_SEQUENCIA, QUANTIDADE_PAGTO_INTERMED, ");
            sql.Append("TEMPO_ENTRE_PAGTO_INTERMED, DESCRICAO_PAGTO, DESCRICAO_PAGTO_INTERMED, TEM_PED_EXAME ");
            sql.Append("FROM TIPO_PATENTE ");
            sql.Append("WHERE IDTIPO_PATENTE = " + idTipoPatente);

            ITipoDePatente tipoDePatente = null;

            IList<ITipoDePatente> listaDeTiposDePatentes = new List<ITipoDePatente>();

            listaDeTiposDePatentes = obtenhaTipoDePatente(sql, int.MaxValue);

            if (listaDeTiposDePatentes.Count > 0)
                tipoDePatente = listaDeTiposDePatentes[0];

            return tipoDePatente;
        }

        private IList<ITipoDePatente> obtenhaTipoDePatente(StringBuilder sql, int quantidadeMaximaRegistros)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();
            IList<ITipoDePatente> listaDeTiposDePatentes = new List<ITipoDePatente>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                while (leitor.Read() && listaDeTiposDePatentes.Count < quantidadeMaximaRegistros)
                {
                    var tipoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<ITipoDePatente>();
                    tipoDePatente.IdTipoPatente = UtilidadesDePersistencia.getValorInteger(leitor, "IDTIPO_PATENTE");
                    tipoDePatente.DescricaoTipoDePatente = UtilidadesDePersistencia.GetValorString(leitor, "DESCRICAO_TIPO_PATENTE");
                    tipoDePatente.SiglaTipo = UtilidadesDePersistencia.getValorChar(leitor, "SIGLA_TIPO");
                    tipoDePatente.TempoInicioAnos = UtilidadesDePersistencia.getValorInteger(leitor, "TEMPO_INICIO_ANOS");
                    tipoDePatente.QuantidadePagto = UtilidadesDePersistencia.getValorInteger(leitor, "QUANTIDADE_PAGTO");
                    tipoDePatente.TempoEntrePagto = UtilidadesDePersistencia.getValorInteger(leitor, "TEMPO_ENTRE_PAGTO");
                    tipoDePatente.SequenciaInicioPagto = UtilidadesDePersistencia.getValorInteger(leitor, "SEQUENCIA_INICIO_PAGTO");
                    tipoDePatente.TemPagtoIntermediario = UtilidadesDePersistencia.GetValorBooleano(leitor, "TEM_PAGTO_INTERMEDIARIO");
                    tipoDePatente.InicioIntermedSequencia = UtilidadesDePersistencia.getValorInteger(leitor, "INICIO_INTERMED_SEQUENCIA");
                    tipoDePatente.QuantidadePagtoIntermed = UtilidadesDePersistencia.getValorInteger(leitor, "QUANTIDADE_PAGTO_INTERMED");
                    tipoDePatente.TempoEntrePagtoIntermed = UtilidadesDePersistencia.getValorInteger(leitor, "TEMPO_ENTRE_PAGTO_INTERMED");
                    tipoDePatente.DescricaoPagto = UtilidadesDePersistencia.GetValorString(leitor, "DESCRICAO_PAGTO");
                    tipoDePatente.DescricaoPagtoIntermed = UtilidadesDePersistencia.GetValorString(leitor, "DESCRICAO_PAGTO_INTERMED");
                    tipoDePatente.TemPedExame = UtilidadesDePersistencia.GetValorBooleano(leitor, "TEM_PED_EXAME");

                    listaDeTiposDePatentes.Add(tipoDePatente);
                }
            }

            return listaDeTiposDePatentes;
        }

        public ITipoDePatente obtenhaTipoDePatentePelaDescricao(string descricaoTipoDePatente)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDTIPO_PATENTE, DESCRICAO_TIPO_PATENTE, SIGLA_TIPO, TEMPO_INICIO_ANOS, QUANTIDADE_PAGTO, ");
            sql.Append("TEMPO_ENTRE_PAGTO, SEQUENCIA_INICIO_PAGTO, TEM_PAGTO_INTERMEDIARIO, INICIO_INTERMED_SEQUENCIA, QUANTIDADE_PAGTO_INTERMED, ");
            sql.Append("TEMPO_ENTRE_PAGTO_INTERMED, DESCRICAO_PAGTO, DESCRICAO_PAGTO_INTERMED, TEM_PED_EXAME ");
            sql.Append("FROM TIPO_PATENTE ");

            if(!string.IsNullOrEmpty(descricaoTipoDePatente))
            {
                sql.Append(string.Concat("WHERE DESCRICAO_TIPO_PATENTE LIKE '", UtilidadesDePersistencia.FiltraApostrofe(descricaoTipoDePatente), "%'"));
            }

            ITipoDePatente tipoDePatente = null;

            IList<ITipoDePatente> listaTipoDePatente = new List<ITipoDePatente>();

            listaTipoDePatente = obtenhaTipoDePatente(sql, int.MaxValue);

            if (listaTipoDePatente.Count > 0)
                tipoDePatente = listaTipoDePatente[0];

            return tipoDePatente;
        }

        public void Inserir(ITipoDePatente tipoPatente)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            tipoPatente.IdTipoPatente = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO TIPO_PATENTE (");
            sql.Append("IDTIPO_PATENTE, DESCRICAO_TIPO_PATENTE, SIGLA_TIPO, TEMPO_INICIO_ANOS, QUANTIDADE_PAGTO, ");
            sql.Append("TEMPO_ENTRE_PAGTO, SEQUENCIA_INICIO_PAGTO, TEM_PAGTO_INTERMEDIARIO, INICIO_INTERMED_SEQUENCIA, QUANTIDADE_PAGTO_INTERMED, ");
            sql.Append("TEMPO_ENTRE_PAGTO_INTERMED, DESCRICAO_PAGTO, DESCRICAO_PAGTO_INTERMED, TEM_PED_EXAME ");
            sql.Append("VALUES (");
            sql.Append(String.Concat(tipoPatente.IdTipoPatente.Value.ToString(), ", "));
            sql.Append(String.Concat("'", tipoPatente.DescricaoTipoDePatente, "', "));
            sql.Append(String.Concat("'", tipoPatente.SiglaTipo.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.TempoInicioAnos.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.QuantidadePagto.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.TempoEntrePagto.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.SequenciaInicioPagto.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.TemPagtoIntermediario.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.InicioIntermedSequencia.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.QuantidadePagtoIntermed.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.TempoEntrePagtoIntermed.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.DescricaoPagto, "', "));
            sql.Append(String.Concat("'", tipoPatente.DescricaoPagtoIntermed, "', "));
            sql.Append(String.Concat("'", tipoPatente.TemPedExame.ToString(), "') "));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(ITipoDePatente tipoPatente)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE TIPO_PATENTE SET ");
            sql.Append(String.Concat("DESCRICAO_TIPO_PATENTE = '", UtilidadesDePersistencia.FiltraApostrofe(tipoPatente.DescricaoTipoDePatente), "', "));
            sql.Append(String.Concat("SIGLA_TIPO = '", tipoPatente.SiglaTipo.ToString(), "', "));
            sql.Append(String.Concat("TEMPO_INICIO_ANOS = '", tipoPatente.TempoInicioAnos.ToString(), "', "));
            sql.Append(String.Concat("QUANTIDADE_PAGTO = '", tipoPatente.QuantidadePagto.ToString(), "', "));
            sql.Append(String.Concat("TEMPO_ENTRE_PAGTO = '", tipoPatente.TempoEntrePagto.ToString(), "', "));
            sql.Append(String.Concat("SEQUENCIA_INICIO_PAGTO = '", tipoPatente.SequenciaInicioPagto.ToString(), "', "));
            sql.Append(String.Concat("TEM_PAGTO_INTERMEDIARIO = '", tipoPatente.TemPagtoIntermediario.ToString(), "', "));
            sql.Append(String.Concat("INICIO_INTERMED_SEQUENCIA = '", tipoPatente.InicioIntermedSequencia.ToString(), "', "));
            sql.Append(String.Concat("QUANTIDADE_PAGTO_INTERMED = '", tipoPatente.QuantidadePagtoIntermed.ToString(), "', "));
            sql.Append(String.Concat("TEMPO_ENTRE_PAGTO_INTERMED = '", tipoPatente.TempoEntrePagtoIntermed.ToString(), "', "));
            sql.Append(String.Concat("DESCRICAO_PAGTO = '", tipoPatente.DescricaoPagto, "', "));
            sql.Append(String.Concat("DESCRICAO_PAGTO_INTERMED = '", tipoPatente.DescricaoPagtoIntermed, "', "));
            sql.Append(String.Concat("TEM_PED_EXAME = '", tipoPatente.TemPedExame.ToString(), "' "));
            sql.Append(String.Concat("WHERE IDTIPO_PATENTE = ", tipoPatente.IdTipoPatente.Value.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Excluir(long idTipoPatente)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("delete from TIPO_PATENTE");
            sql.Append(string.Concat(" where IDTIPO_PATENTE = ", idTipoPatente.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
