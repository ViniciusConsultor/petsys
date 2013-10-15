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

            sql.Append("SELECT IDTIPO_PATENTE IdTipoDePatente, DESCRICAO_TIPO_PATENTE DescricaoTipoDePatente, SIGLA_TIPO SiglaTipo, TEMPO_INICIO_ANOS TempoInicioAnos, QUANTIDADE_PAGTO QuantidadePagamento, ");
            sql.Append("TEMPO_ENTRE_PAGTO TempoEntrePagamento, SEQUENCIA_INICIO_PAGTO SequenciaInicioPagamento, TEM_PAGTO_INTERMEDIARIO TemPagamentoIntermediario, INICIO_INTERMED_SEQUENCIA InicioIntermediarioSequencia, QUANTIDADE_PAGTO_INTERMED QuantidadePagamentoIntermediario, ");
            sql.Append("TEMPO_ENTRE_PAGTO_INTERMED TempoEntrePagamentoIntermediario, DESCRICAO_PAGTO DescricaoPagamento, DESCRICAO_PAGTO_INTERMED DescricaoPagamentoIntermediario, TEM_PED_EXAME TemPedidoDeExame ");
            sql.Append("FROM MP_TIPO_PATENTE");

            var DBHelper = ServerUtils.criarNovoDbHelper();

            IList<ITipoDePatente> listaDeTiposDePatentes = new List<ITipoDePatente>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                while (leitor.Read())
                {
                    var tipoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<ITipoDePatente>();
                    tipoDePatente.IdTipoDePatente = UtilidadesDePersistencia.getValorInteger(leitor, "IdTipoDePatente");
                    tipoDePatente.DescricaoTipoDePatente = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoTipoDePatente");
                    tipoDePatente.SiglaTipo = UtilidadesDePersistencia.GetValorString(leitor, "SiglaTipo");
                    tipoDePatente.TempoInicioAnos = UtilidadesDePersistencia.getValorInteger(leitor, "TempoInicioAnos");
                    tipoDePatente.QuantidadePagamento = UtilidadesDePersistencia.getValorInteger(leitor, "QuantidadePagamento");
                    tipoDePatente.TempoEntrePagamento = UtilidadesDePersistencia.getValorInteger(leitor, "TempoEntrePagamento");
                    tipoDePatente.SequenciaInicioPagamento = UtilidadesDePersistencia.getValorInteger(leitor, "SequenciaInicioPagamento");
                    tipoDePatente.TemPagamentoIntermediario = UtilidadesDePersistencia.GetValorBooleano(leitor, "TemPagamentoIntermediario");
                    tipoDePatente.InicioIntermediarioSequencia = UtilidadesDePersistencia.getValorInteger(leitor, "InicioIntermediarioSequencia");
                    tipoDePatente.QuantidadePagamentoIntermediario = UtilidadesDePersistencia.getValorInteger(leitor, "QuantidadePagamentoIntermediario");
                    tipoDePatente.TempoEntrePagamentoIntermediario = UtilidadesDePersistencia.getValorInteger(leitor, "TempoEntrePagamentoIntermediario");
                    tipoDePatente.DescricaoPagamento = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoPagamento");
                    tipoDePatente.DescricaoPagamentoIntermediario = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoPagamentoIntermediario");
                    tipoDePatente.TemPedidoDeExame = UtilidadesDePersistencia.GetValorBooleano(leitor, "TemPedidoDeExame");

                    listaDeTiposDePatentes.Add(tipoDePatente);
                }
            }

            return listaDeTiposDePatentes;
        }

        public IList<ITipoDePatente> obtenhaTipoDePatentePelaDescricaoComoFiltro(string descricao, int quantidadeMaximaDeRegistros)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDTIPO_PATENTE IdTipoDePatente, DESCRICAO_TIPO_PATENTE DescricaoTipoDePatente, SIGLA_TIPO SiglaTipo, TEMPO_INICIO_ANOS TempoInicioAnos, QUANTIDADE_PAGTO QuantidadePagamento, ");
            sql.Append("TEMPO_ENTRE_PAGTO TempoEntrePagamento, SEQUENCIA_INICIO_PAGTO SequenciaInicioPagamento, TEM_PAGTO_INTERMEDIARIO TemPagamentoIntermediario, INICIO_INTERMED_SEQUENCIA InicioIntermediarioSequencia, QUANTIDADE_PAGTO_INTERMED QuantidadePagamentoIntermediario, ");
            sql.Append("TEMPO_ENTRE_PAGTO_INTERMED TempoEntrePagamentoIntermediario, DESCRICAO_PAGTO DescricaoPagamento, DESCRICAO_PAGTO_INTERMED DescricaoPagamentoIntermediario, TEM_PED_EXAME TemPedidoDeExame ");
            sql.Append("FROM MP_TIPO_PATENTE ");

            if (!string.IsNullOrEmpty(descricao))
            {
                sql.Append(string.Concat("WHERE DESCRICAO_TIPO_PATENTE LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(descricao), "%' "));
            }

            return obtenhaTipoDePatente(sql, quantidadeMaximaDeRegistros);
        }

        public ITipoDePatente obtenhaTipoDePatentePeloId(long idTipoPatente)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDTIPO_PATENTE IdTipoDePatente, DESCRICAO_TIPO_PATENTE DescricaoTipoDePatente, SIGLA_TIPO SiglaTipo, TEMPO_INICIO_ANOS TempoInicioAnos, QUANTIDADE_PAGTO QuantidadePagamento, ");
            sql.Append("TEMPO_ENTRE_PAGTO TempoEntrePagamento, SEQUENCIA_INICIO_PAGTO SequenciaInicioPagamento, TEM_PAGTO_INTERMEDIARIO TemPagamentoIntermediario, INICIO_INTERMED_SEQUENCIA InicioIntermediarioSequencia, QUANTIDADE_PAGTO_INTERMED QuantidadePagamentoIntermediario, ");
            sql.Append("TEMPO_ENTRE_PAGTO_INTERMED TempoEntrePagamentoIntermediario, DESCRICAO_PAGTO DescricaoPagamento, DESCRICAO_PAGTO_INTERMED DescricaoPagamentoIntermediario, TEM_PED_EXAME TemPedidoDeExame ");
            sql.Append("FROM MP_TIPO_PATENTE ");
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

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeMaximaRegistros))
            {
                while (leitor.Read())
                {
                    var tipoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<ITipoDePatente>();
                    tipoDePatente.IdTipoDePatente = UtilidadesDePersistencia.getValorInteger(leitor, "IdTipoDePatente");
                    tipoDePatente.DescricaoTipoDePatente = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoTipoDePatente");
                    tipoDePatente.SiglaTipo = UtilidadesDePersistencia.GetValorString(leitor, "SiglaTipo");
                    tipoDePatente.TempoInicioAnos = UtilidadesDePersistencia.getValorInteger(leitor, "TempoInicioAnos");
                    tipoDePatente.QuantidadePagamento = UtilidadesDePersistencia.getValorInteger(leitor, "QuantidadePagamento");
                    tipoDePatente.TempoEntrePagamento = UtilidadesDePersistencia.getValorInteger(leitor, "TempoEntrePagamento");
                    tipoDePatente.SequenciaInicioPagamento = UtilidadesDePersistencia.getValorInteger(leitor, "SequenciaInicioPagamento");
                    tipoDePatente.TemPagamentoIntermediario = UtilidadesDePersistencia.GetValorBooleano(leitor, "TemPagamentoIntermediario");
                    tipoDePatente.InicioIntermediarioSequencia = UtilidadesDePersistencia.getValorInteger(leitor, "InicioIntermediarioSequencia");
                    tipoDePatente.QuantidadePagamentoIntermediario = UtilidadesDePersistencia.getValorInteger(leitor, "QuantidadePagamentoIntermediario");
                    tipoDePatente.TempoEntrePagamentoIntermediario = UtilidadesDePersistencia.getValorInteger(leitor, "TempoEntrePagamentoIntermediario");
                    tipoDePatente.DescricaoPagamento = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoPagamento");
                    tipoDePatente.DescricaoPagamentoIntermediario = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoPagamentoIntermediario");
                    tipoDePatente.TemPedidoDeExame = UtilidadesDePersistencia.GetValorBooleano(leitor, "TemPedidoDeExame");

                    listaDeTiposDePatentes.Add(tipoDePatente);
                }
            }

            return listaDeTiposDePatentes;
        }

        public ITipoDePatente obtenhaTipoDePatentePelaDescricaoOuSigla(string descricaoTipoDePatente, string siglaTipo)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDTIPO_PATENTE IdTipoDePatente, DESCRICAO_TIPO_PATENTE DescricaoTipoDePatente, SIGLA_TIPO SiglaTipo, TEMPO_INICIO_ANOS TempoInicioAnos, QUANTIDADE_PAGTO QuantidadePagamento, ");
            sql.Append("TEMPO_ENTRE_PAGTO TempoEntrePagamento, SEQUENCIA_INICIO_PAGTO SequenciaInicioPagamento, TEM_PAGTO_INTERMEDIARIO TemPagamentoIntermediario, INICIO_INTERMED_SEQUENCIA InicioIntermediarioSequencia, QUANTIDADE_PAGTO_INTERMED QuantidadePagamentoIntermediario, ");
            sql.Append("TEMPO_ENTRE_PAGTO_INTERMED TempoEntrePagamentoIntermediario, DESCRICAO_PAGTO DescricaoPagamento, DESCRICAO_PAGTO_INTERMED DescricaoPagamentoIntermediario, TEM_PED_EXAME TemPedidoDeExame ");
            sql.Append("FROM MP_TIPO_PATENTE ");

            if(!string.IsNullOrEmpty(descricaoTipoDePatente))
            {
                sql.Append(string.Concat("WHERE DESCRICAO_TIPO_PATENTE = '", UtilidadesDePersistencia.FiltraApostrofe(descricaoTipoDePatente), "' "));
                sql.Append(string.Concat("OR SIGLA_TIPO = '", UtilidadesDePersistencia.FiltraApostrofe(siglaTipo), "'"));
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

            tipoPatente.IdTipoDePatente = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_TIPO_PATENTE (");
            sql.Append("IDTIPO_PATENTE, DESCRICAO_TIPO_PATENTE, SIGLA_TIPO, TEMPO_INICIO_ANOS, QUANTIDADE_PAGTO, ");
            sql.Append("TEMPO_ENTRE_PAGTO, SEQUENCIA_INICIO_PAGTO, TEM_PAGTO_INTERMEDIARIO, INICIO_INTERMED_SEQUENCIA, QUANTIDADE_PAGTO_INTERMED, ");
            sql.Append("TEMPO_ENTRE_PAGTO_INTERMED, DESCRICAO_PAGTO, DESCRICAO_PAGTO_INTERMED, TEM_PED_EXAME) ");
            sql.Append("VALUES (");
            sql.Append(String.Concat(tipoPatente.IdTipoDePatente.Value.ToString(), ", "));
            sql.Append(String.Concat("'", tipoPatente.DescricaoTipoDePatente, "', "));
            sql.Append(String.Concat("'", tipoPatente.SiglaTipo.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.TempoInicioAnos.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.QuantidadePagamento.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.TempoEntrePagamento.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.SequenciaInicioPagamento.ToString(), "', "));
            sql.Append(tipoPatente.TemPagamentoIntermediario ? String.Concat("'", 1, "', ") : String.Concat("'", 0, "', "));
            sql.Append(String.Concat("'", tipoPatente.InicioIntermediarioSequencia.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.QuantidadePagamentoIntermediario.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.TempoEntrePagamentoIntermediario.ToString(), "', "));
            sql.Append(String.Concat("'", tipoPatente.DescricaoPagamento, "', "));
            sql.Append(String.Concat("'", tipoPatente.DescricaoPagamentoIntermediario, "', "));
            sql.Append(tipoPatente.TemPedidoDeExame ? String.Concat("'", 1, "') ") : String.Concat("'", 0, "') "));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(ITipoDePatente tipoPatente)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_TIPO_PATENTE SET ");
            sql.Append(String.Concat("DESCRICAO_TIPO_PATENTE = '", UtilidadesDePersistencia.FiltraApostrofe(tipoPatente.DescricaoTipoDePatente), "', "));
            sql.Append(String.Concat("SIGLA_TIPO = '", tipoPatente.SiglaTipo.ToString(), "', "));
            sql.Append(String.Concat("TEMPO_INICIO_ANOS = '", tipoPatente.TempoInicioAnos.ToString(), "', "));
            sql.Append(String.Concat("QUANTIDADE_PAGTO = '", tipoPatente.QuantidadePagamento.ToString(), "', "));
            sql.Append(String.Concat("TEMPO_ENTRE_PAGTO = '", tipoPatente.TempoEntrePagamento.ToString(), "', "));
            sql.Append(String.Concat("SEQUENCIA_INICIO_PAGTO = '", tipoPatente.SequenciaInicioPagamento.ToString(), "', "));
            sql.Append(tipoPatente.TemPagamentoIntermediario
                           ? String.Concat("TEM_PAGTO_INTERMEDIARIO = '", 1, "', ")
                           : String.Concat("TEM_PAGTO_INTERMEDIARIO = '", 0, "', "));
            sql.Append(String.Concat("INICIO_INTERMED_SEQUENCIA = '", tipoPatente.InicioIntermediarioSequencia.ToString(), "', "));
            sql.Append(String.Concat("QUANTIDADE_PAGTO_INTERMED = '", tipoPatente.QuantidadePagamentoIntermediario.ToString(), "', "));
            sql.Append(String.Concat("TEMPO_ENTRE_PAGTO_INTERMED = '", tipoPatente.TempoEntrePagamentoIntermediario.ToString(), "', "));
            sql.Append(String.Concat("DESCRICAO_PAGTO = '", tipoPatente.DescricaoPagamento, "', "));
            sql.Append(String.Concat("DESCRICAO_PAGTO_INTERMED = '", tipoPatente.DescricaoPagamentoIntermediario, "', "));
            sql.Append(tipoPatente.TemPedidoDeExame
                           ? String.Concat("TEM_PED_EXAME = '", 1, "' ")
                           : String.Concat("TEM_PED_EXAME = '", 0, "' "));
            sql.Append(String.Concat("WHERE IDTIPO_PATENTE = ", tipoPatente.IdTipoDePatente.Value.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Excluir(long idTipoPatente)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("delete from MP_TIPO_PATENTE");
            sql.Append(string.Concat(" where IDTIPO_PATENTE = ", idTipoPatente.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }


    }
}
