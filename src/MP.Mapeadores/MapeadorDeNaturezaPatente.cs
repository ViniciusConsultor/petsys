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
    public class MapeadorDeNaturezaPatente : IMapeadorDeNaturezaPatente
    {

        public IList<INaturezaPatente> obtenhaNaturezaPatentePelaDescricaoComoFiltro(string descricao, int quantidadeMaximaDeRegistros)
        {
            var consultaSQL = new StringBuilder();

            consultaSQL.Append("SELECT IDTIPO_PATENTE IdTipoDePatente, DESCRICAO_TIPO_PATENTE DescricaoTipoDePatente, SIGLA_TIPO SiglaTipo, TEMPO_INICIO_ANOS TempoInicioAnos, QUANTIDADE_PAGTO QuantidadePagamento, ");
            consultaSQL.Append("TEMPO_ENTRE_PAGTO TempoEntrePagamento, SEQUENCIA_INICIO_PAGTO SequenciaInicioPagamento, TEM_PAGTO_INTERMEDIARIO TemPagamentoIntermediario, INICIO_INTERMED_SEQUENCIA InicioIntermediarioSequencia, QUANTIDADE_PAGTO_INTERMED QuantidadePagamentoIntermediario, ");
            consultaSQL.Append("TEMPO_ENTRE_PAGTO_INTERMED TempoEntrePagamentoIntermediario, DESCRICAO_PAGTO DescricaoPagamento, DESCRICAO_PAGTO_INTERMED DescricaoPagamentoIntermediario, TEM_PED_EXAME TemPedidoDeExame ");
            consultaSQL.Append("FROM MP_TIPO_PATENTE ");

            if (!string.IsNullOrEmpty(descricao))
                consultaSQL.Append(string.Concat("WHERE DESCRICAO_TIPO_PATENTE LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(descricao), "%' "));

            return ObtenhaNaturezaPatente(consultaSQL, quantidadeMaximaDeRegistros);
        }

        public INaturezaPatente obtenhaNaturezaPatentePeloId(long idNaturezaPatente)
        {
            var consultaSQL = new StringBuilder();

            consultaSQL.Append("SELECT IDTIPO_PATENTE IdTipoDePatente, DESCRICAO_TIPO_PATENTE DescricaoTipoDePatente, SIGLA_TIPO SiglaTipo, TEMPO_INICIO_ANOS TempoInicioAnos, QUANTIDADE_PAGTO QuantidadePagamento, ");
            consultaSQL.Append("TEMPO_ENTRE_PAGTO TempoEntrePagamento, SEQUENCIA_INICIO_PAGTO SequenciaInicioPagamento, TEM_PAGTO_INTERMEDIARIO TemPagamentoIntermediario, INICIO_INTERMED_SEQUENCIA InicioIntermediarioSequencia, QUANTIDADE_PAGTO_INTERMED QuantidadePagamentoIntermediario, ");
            consultaSQL.Append("TEMPO_ENTRE_PAGTO_INTERMED TempoEntrePagamentoIntermediario, DESCRICAO_PAGTO DescricaoPagamento, DESCRICAO_PAGTO_INTERMED DescricaoPagamentoIntermediario, TEM_PED_EXAME TemPedidoDeExame ");
            consultaSQL.Append("FROM MP_TIPO_PATENTE ");
            consultaSQL.Append("WHERE IDTIPO_PATENTE = " + idNaturezaPatente);

            INaturezaPatente naturezaPatente = null;

            IList<INaturezaPatente> listaDeNaturezasPatente = new List<INaturezaPatente>();

            listaDeNaturezasPatente = ObtenhaNaturezaPatente(consultaSQL, int.MaxValue);

            if (listaDeNaturezasPatente.Count > 0)
                naturezaPatente = listaDeNaturezasPatente[0];

            return naturezaPatente;
        }

        public INaturezaPatente obtenhaNaturezaPatentePelaDescricaoOuSigla(string descricaoNaturezaPatente, string siglaNatureza)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDTIPO_PATENTE IdTipoDePatente, DESCRICAO_TIPO_PATENTE DescricaoTipoDePatente, SIGLA_TIPO SiglaTipo, TEMPO_INICIO_ANOS TempoInicioAnos, QUANTIDADE_PAGTO QuantidadePagamento, ");
            sql.Append("TEMPO_ENTRE_PAGTO TempoEntrePagamento, SEQUENCIA_INICIO_PAGTO SequenciaInicioPagamento, TEM_PAGTO_INTERMEDIARIO TemPagamentoIntermediario, INICIO_INTERMED_SEQUENCIA InicioIntermediarioSequencia, QUANTIDADE_PAGTO_INTERMED QuantidadePagamentoIntermediario, ");
            sql.Append("TEMPO_ENTRE_PAGTO_INTERMED TempoEntrePagamentoIntermediario, DESCRICAO_PAGTO DescricaoPagamento, DESCRICAO_PAGTO_INTERMED DescricaoPagamentoIntermediario, TEM_PED_EXAME TemPedidoDeExame ");
            sql.Append("FROM MP_TIPO_PATENTE ");

            if (!string.IsNullOrEmpty(descricaoNaturezaPatente))
            {
                sql.Append(string.Concat("WHERE DESCRICAO_TIPO_PATENTE = '", UtilidadesDePersistencia.FiltraApostrofe(descricaoNaturezaPatente), "' "));
                sql.Append(string.Concat("OR SIGLA_TIPO = '", UtilidadesDePersistencia.FiltraApostrofe(siglaNatureza), "'"));
            }

            INaturezaPatente naturezaPatente = null;

            IList<INaturezaPatente> listaDeNaturezaPatente = new List<INaturezaPatente>();

            listaDeNaturezaPatente = ObtenhaNaturezaPatente(sql, int.MaxValue);

            if (listaDeNaturezaPatente.Count > 0)
                naturezaPatente = listaDeNaturezaPatente[0];

            return naturezaPatente;
        }

        public void Inserir(INaturezaPatente naturezaPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            naturezaPatente.IdNaturezaPatente = GeradorDeID.getInstancia().getProximoID();

            comandoSQL.Append("INSERT INTO MP_TIPO_PATENTE (");
            comandoSQL.Append("IDTIPO_PATENTE, DESCRICAO_TIPO_PATENTE, SIGLA_TIPO, TEMPO_INICIO_ANOS, QUANTIDADE_PAGTO, ");
            comandoSQL.Append("TEMPO_ENTRE_PAGTO, SEQUENCIA_INICIO_PAGTO, TEM_PAGTO_INTERMEDIARIO, INICIO_INTERMED_SEQUENCIA, QUANTIDADE_PAGTO_INTERMED, ");
            comandoSQL.Append("TEMPO_ENTRE_PAGTO_INTERMED, DESCRICAO_PAGTO, DESCRICAO_PAGTO_INTERMED, TEM_PED_EXAME) ");
            comandoSQL.Append("VALUES (");
            comandoSQL.Append(String.Concat(naturezaPatente.IdNaturezaPatente.Value.ToString(), ", "));
            comandoSQL.Append(String.Concat("'", naturezaPatente.DescricaoNaturezaPatente, "', "));
            comandoSQL.Append(String.Concat("'", naturezaPatente.SiglaNatureza.ToString(), "', "));
            comandoSQL.Append(String.Concat("'", naturezaPatente.TempoInicioAnos.ToString(), "', "));
            comandoSQL.Append(String.Concat("'", naturezaPatente.QuantidadePagamento.ToString(), "', "));
            comandoSQL.Append(String.Concat("'", naturezaPatente.TempoEntrePagamento.ToString(), "', "));
            comandoSQL.Append(String.Concat("'", naturezaPatente.SequenciaInicioPagamento.ToString(), "', "));
            comandoSQL.Append(naturezaPatente.TemPagamentoIntermediario ? String.Concat("'", 1, "', ") : String.Concat("'", 0, "', "));
            comandoSQL.Append(String.Concat("'", naturezaPatente.InicioIntermediarioSequencia.ToString(), "', "));
            comandoSQL.Append(String.Concat("'", naturezaPatente.QuantidadePagamentoIntermediario.ToString(), "', "));
            comandoSQL.Append(String.Concat("'", naturezaPatente.TempoEntrePagamentoIntermediario.ToString(), "', "));
            comandoSQL.Append(String.Concat("'", naturezaPatente.DescricaoPagamento, "', "));
            comandoSQL.Append(String.Concat("'", naturezaPatente.DescricaoPagamentoIntermediario, "', "));
            comandoSQL.Append(naturezaPatente.TemPedidoDeExame ? String.Concat("'", 1, "') ") : String.Concat("'", 0, "') "));

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        public void Modificar(INaturezaPatente naturezaPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("UPDATE MP_TIPO_PATENTE SET ");
            comandoSQL.Append(String.Concat("DESCRICAO_TIPO_PATENTE = '", UtilidadesDePersistencia.FiltraApostrofe(naturezaPatente.DescricaoNaturezaPatente), "', "));
            comandoSQL.Append(String.Concat("SIGLA_TIPO = '", naturezaPatente.SiglaNatureza.ToString(), "', "));
            comandoSQL.Append(String.Concat("TEMPO_INICIO_ANOS = '", naturezaPatente.TempoInicioAnos.ToString(), "', "));
            comandoSQL.Append(String.Concat("QUANTIDADE_PAGTO = '", naturezaPatente.QuantidadePagamento.ToString(), "', "));
            comandoSQL.Append(String.Concat("TEMPO_ENTRE_PAGTO = '", naturezaPatente.TempoEntrePagamento.ToString(), "', "));
            comandoSQL.Append(String.Concat("SEQUENCIA_INICIO_PAGTO = '", naturezaPatente.SequenciaInicioPagamento.ToString(), "', "));
            comandoSQL.Append(naturezaPatente.TemPagamentoIntermediario ? String.Concat("TEM_PAGTO_INTERMEDIARIO = '", 1, "', ") : 
                                                                          String.Concat("TEM_PAGTO_INTERMEDIARIO = '", 0, "', "));
            comandoSQL.Append(String.Concat("INICIO_INTERMED_SEQUENCIA = '", naturezaPatente.InicioIntermediarioSequencia.ToString(), "', "));
            comandoSQL.Append(String.Concat("QUANTIDADE_PAGTO_INTERMED = '", naturezaPatente.QuantidadePagamentoIntermediario.ToString(), "', "));
            comandoSQL.Append(String.Concat("TEMPO_ENTRE_PAGTO_INTERMED = '", naturezaPatente.TempoEntrePagamentoIntermediario.ToString(), "', "));
            comandoSQL.Append(String.Concat("DESCRICAO_PAGTO = '", naturezaPatente.DescricaoPagamento, "', "));
            comandoSQL.Append(String.Concat("DESCRICAO_PAGTO_INTERMED = '", naturezaPatente.DescricaoPagamentoIntermediario, "', "));
            comandoSQL.Append(naturezaPatente.TemPedidoDeExame ? String.Concat("TEM_PED_EXAME = '", 1, "' ") :
                                                                 String.Concat("TEM_PED_EXAME = '", 0, "' "));
            comandoSQL.Append(String.Concat("WHERE IDTIPO_PATENTE = ", naturezaPatente.IdNaturezaPatente.Value.ToString()));

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        public void Excluir(long idNaturezaPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("delete from MP_TIPO_PATENTE");
            comandoSQL.Append(string.Concat(" where IDTIPO_PATENTE = ", idNaturezaPatente.ToString()));

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private IList<INaturezaPatente> ObtenhaNaturezaPatente(StringBuilder sql, int quantidadeMaximaRegistros)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();
            IList<INaturezaPatente> listaDeNaturezasPatentes = new List<INaturezaPatente>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeMaximaRegistros))
            {
                while (leitor.Read())
                {
                    var naturezaPatente = FabricaGenerica.GetInstancia().CrieObjeto<INaturezaPatente>();
                    naturezaPatente.IdNaturezaPatente = UtilidadesDePersistencia.getValorInteger(leitor, "IdTipoDePatente");
                    naturezaPatente.DescricaoNaturezaPatente = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoTipoDePatente");
                    naturezaPatente.SiglaNatureza = UtilidadesDePersistencia.GetValorString(leitor, "SiglaTipo");
                    naturezaPatente.TempoInicioAnos = UtilidadesDePersistencia.getValorInteger(leitor, "TempoInicioAnos");
                    naturezaPatente.QuantidadePagamento = UtilidadesDePersistencia.getValorInteger(leitor, "QuantidadePagamento");
                    naturezaPatente.TempoEntrePagamento = UtilidadesDePersistencia.getValorInteger(leitor, "TempoEntrePagamento");
                    naturezaPatente.SequenciaInicioPagamento = UtilidadesDePersistencia.getValorInteger(leitor, "SequenciaInicioPagamento");
                    naturezaPatente.TemPagamentoIntermediario = UtilidadesDePersistencia.GetValorBooleano(leitor, "TemPagamentoIntermediario");
                    naturezaPatente.InicioIntermediarioSequencia = UtilidadesDePersistencia.getValorInteger(leitor, "InicioIntermediarioSequencia");
                    naturezaPatente.QuantidadePagamentoIntermediario = UtilidadesDePersistencia.getValorInteger(leitor, "QuantidadePagamentoIntermediario");
                    naturezaPatente.TempoEntrePagamentoIntermediario = UtilidadesDePersistencia.getValorInteger(leitor, "TempoEntrePagamentoIntermediario");
                    naturezaPatente.DescricaoPagamento = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoPagamento");
                    naturezaPatente.DescricaoPagamentoIntermediario = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoPagamentoIntermediario");
                    naturezaPatente.TemPedidoDeExame = UtilidadesDePersistencia.GetValorBooleano(leitor, "TemPedidoDeExame");

                    listaDeNaturezasPatentes.Add(naturezaPatente);
                }
            }

            return listaDeNaturezasPatentes;
        }
    }
}
