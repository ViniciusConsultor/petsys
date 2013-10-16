using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio.LazyLoad;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.LazyLoad;

namespace MP.Mapeadores
{
    public class MapeadorDaPatente : IMapeadorDePatente
    {
        public void Insira(IPatente patente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            patente.Identificador = ObtenhaProximoIDPatente();

            comandoSQL.Append("INSERT INTO MP_PATENTE(IDPROCESSOPATENTE, TITULOPATENTE, IDTIPOPATENTE, LINKINPI, OBRIGACAOGERADA, DATACADASTRO, OBSERVACAO,");
            comandoSQL.Append("RESUMO_PATENTE, QTDEREINVINDICACAO) VALUES(");
            comandoSQL.Append(patente.Identificador + ", ");
            comandoSQL.Append("'" + patente.TituloPatente + "', ");
            comandoSQL.Append(patente.TipoDePatente.IdTipoDePatente + ", ");
            comandoSQL.Append("'" + patente.LinkINPI + "', ");
            comandoSQL.Append("'" + (patente.ObrigacaoGerada ? "1" : "0") + "', ");
            comandoSQL.Append(patente.DataCadastro != null ? "'" + patente.DataCadastro.Value.ToString("yyyyMMdd") + "', " : "NULL, ");
            comandoSQL.Append("'" + patente.Observacao + "', ");
            comandoSQL.Append("'" + patente.Resumo + "', ");
            comandoSQL.Append(patente.QuantidadeReivindicacao + ", ");
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());

            foreach (IAnuidadePatente anuidadePatente in patente.Anuidades)
                InserirAnuidade(anuidadePatente, patente.Identificador);

            foreach (IClassificacaoPatente classificacaoPatente in patente.Classificacoes)
                InserirClassificacao(classificacaoPatente, patente.Identificador);

            foreach (IPrioridadeUnionistaPatente prioridadeUnionistaPatente in patente.PrioridadesUnionista)
                InserirPrioridadeUnionista(prioridadeUnionistaPatente, patente.Identificador);

            foreach (ITitularPatente titularPatente in patente.Titulares)
                InserirTitularPatente(titularPatente, patente.Identificador);
        }

        public void Modificar(IPatente patente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("UPDATE MP_PATENTE SET ");
            comandoSQL.Append("TITULOPATENTE = '" + patente.TituloPatente + "', ");
            comandoSQL.Append("IDTIPOPATENTE = " + patente.TipoDePatente.IdTipoDePatente + ", ");
            comandoSQL.Append("LINKINPI = '" + patente.LinkINPI + "', ");
            comandoSQL.Append("OBRIGACAOGERADA = '" + (patente.ObrigacaoGerada ? "1" : "0") + "', ");
            comandoSQL.Append("DATACADASTRO = " + (patente.DataCadastro != null ? "'" + patente.DataCadastro.Value.ToString("yyyyMMdd") + "', " : "NULL, "));
            comandoSQL.Append("OBSERVACAO = '" + patente.Observacao + "', ");
            comandoSQL.Append("RESUMO_PATENTE = '" + patente.Resumo + "', ");
            comandoSQL.Append("QTDEREINVINDICACAO = " + patente.QuantidadeReivindicacao + ", ");
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        public void Exluir(int codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTE WHERE IDPROCESSOPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());

            ExluirAnuidade(codigoPatente);
            ExluirClassificacao(codigoPatente);
            ExluirPrioridadeUnionista(codigoPatente);
            ExluirTitular(codigoPatente);
        }

        public IAnuidadePatente ObtenhaAnuidade(long id)
        {
            IAnuidadePatente anuidadePatente = null;
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPATENTEANUIDADE, IDPATENTE, DESCRICAOANUIDADE, DATALANCAMENTO, DATAVENCIMENTO, DATAPAGAMENTO, VALORPAGAMENTO, ANUIDADEPAGA,");
            comandoSQL.Append("PEDIDOEXAME, DATAVENCTO_SEM_MULTA, DATAVENCTO_COM_MULTA ");
            comandoSQL.Append("FROM MP_PATENTEANUIDADE WHERE IDPATENTEANUIDADE = " + id);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    anuidadePatente = MapeieObjetoAnuidadePatente(reader);

            return anuidadePatente;
        }

        public IClassificacaoPatente ObtenhaClassificacao(long id)
        {
            IClassificacaoPatente classificacaoPatente = null;
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPATENTECLASSIFICACAO, CLASSIFICACAO, DESCRICAO_CLASSIFICACAO, IDPATENTE, TIPO_CLASSIFICACAO FROM MP_PATENTECLASSIFICACAO ");
            comandoSQL.Append("WHERE IDPATENTECLASSIFICACAO = " + id);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    classificacaoPatente = MapeieObjetoClassificacaoPatente(reader);

            return classificacaoPatente;
        }

        public IPrioridadeUnionistaPatente ObtenhaPrioridadeUnionista(long id)
        {
            IPrioridadeUnionistaPatente prioridadeUnionistaPatente = null;
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPRIORIDADEUNIONISTA, DATA_PRIORIDADE, NUMERO_PRIORIDADE, IDPATENTE, IDPAIS FROM MP_PATENTEPRIORIDADEUNIONISTA ");
            comandoSQL.Append("WHERE IDPRIORIDADEUNIONISTA = " + id);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    prioridadeUnionistaPatente = MapeieObjetoPrioridadeUnionistaPatente(reader);

            return prioridadeUnionistaPatente;
        }

        public ITitularPatente ObtenhaTitular(long id)
        {
            ITitularPatente titularPatente = null;
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPATENTETITULARINVENTOR, IDPATENTE, IDPROCURADOR, CONTATO_TITULAR FROM MP_PATENTETITULARINVENTOR ");
            comandoSQL.Append("WHERE IDPATENTETITULARINVENTOR = " + id);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    titularPatente = MapeieObjetoTitularPatente(reader);

            return titularPatente;
        }

        public IPatente ObtenhaPatente(long id)
        {
            IPatente patente = null;
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPATENTE, TITULOPATENTE, IDTIPOPATENTE, LINKINPI, OBRIGACAOGERADA, DATACADASTRO, OBSERVACAO, RESUMO_PATENTE,");
            comandoSQL.Append("QTDEREINVINDICACAO FROM MP_PATENTE ");
            comandoSQL.Append("WHERE IDPATENTE = " + id);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    patente = MapeieObjetoPatente(reader);

            return patente;
        }

        public IList<IPatente> ObtenhaPatentesPeloTitulo(string titulo, int quantidadeDeRegistros)
        {
            IList<IPatente> patentes = new List<IPatente>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPATENTE, TITULOPATENTE, IDTIPOPATENTE, LINKINPI, OBRIGACAOGERADA, DATACADASTRO, OBSERVACAO, RESUMO_PATENTE,");
            comandoSQL.Append("QTDEREINVINDICACAO FROM MP_PATENTE ");
            comandoSQL.Append("WHERE TITULOPATENTE like '%" + titulo + "%'");

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString(), quantidadeDeRegistros))
                while (reader.Read())
                    patentes.Add(MapeieObjetoPatente(reader));

            return patentes;
        }

#region Métodos Privados

        private void InserirAnuidade(IAnuidadePatente anuidadePatente, long idPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            anuidadePatente.Identificador = ObtenhaProximoIDAnuidadePatente();

            comandoSQL.Append("INSERT INTO MP_PATENTEANUIDADE(IDPATENTEANUIDADE, IDPATENTE, DESCRICAOANUIDADE, DATALANCAMENTO, DATAVENCIMENTO,");
            comandoSQL.Append("DATAPAGAMENTO, VALORPAGAMENTO, ANUIDADEPAGA, PEDIDOEXAME, DATAVENCTO_SEM_MULTA, DATAVENCTO_COM_MULTA) VALUES(");
            comandoSQL.Append(anuidadePatente.Identificador + ", ");
            comandoSQL.Append(idPatente + ", ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(anuidadePatente.DescricaoAnuidade) + "', ");
            comandoSQL.Append(anuidadePatente.DataLancamento != null ? anuidadePatente.DataLancamento.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append(anuidadePatente.DataVencimento != null ? anuidadePatente.DataVencimento.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append(anuidadePatente.DataPagamento != null ? anuidadePatente.DataPagamento.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append(anuidadePatente.ValorPagamento + ", ");
            comandoSQL.Append("'" + (anuidadePatente.AnuidadePaga ? "1" : "0" )+ "', ");
            comandoSQL.Append("'" + (anuidadePatente.PedidoExame ? "1" : "0") + "', ");
            comandoSQL.Append(anuidadePatente.DataVencimentoSemMulta != null ? anuidadePatente.DataVencimentoSemMulta.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append(anuidadePatente.DataVencimentoComMulta != null ? anuidadePatente.DataVencimentoComMulta.Value.ToString("yyyyMMdd") + ", " : "NULL, ");

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void InserirClassificacao(IClassificacaoPatente classificacaoPatente, long idPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            classificacaoPatente.Identificador = ObtenhaProximoIDClassificacaoPatente();

            comandoSQL.Append("INSERT INTO MP_PATENTECLASSIFICACAO(IDPATENTECLASSIFICACAO, CLASSIFICACAO, DESCRICAO_CLASSIFICACAO, IDPATENTE, TIPO_CLASSIFICACAO) VALUES(");
            comandoSQL.Append( classificacaoPatente.Identificador + ", ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe( classificacaoPatente.Classificacao) + "', ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(classificacaoPatente.DescricaoClassificacao) + "', ");
            comandoSQL.Append(idPatente + ", ");
            comandoSQL.Append(classificacaoPatente.TipoClassificacao.Codigo + ")");
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void InserirPrioridadeUnionista(IPrioridadeUnionistaPatente prioridadeUnionistaPatente, long idPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            prioridadeUnionistaPatente.Identificador = ObtenhaProximoIDPrioridadeUnionista();

            comandoSQL.Append("INSERT INTO MP_PATENTEPRIORIDADEUNIONISTA(IDPRIORIDADEUNIONISTA, DATA_PRIORIDADE, NUMERO_PRIORIDADE, IDPATENTE, IDPAIS) VALUES(");
            comandoSQL.Append(prioridadeUnionistaPatente.Identificador + ", ");
            comandoSQL.Append(prioridadeUnionistaPatente.DataPrioridade != null ? prioridadeUnionistaPatente.DataPrioridade.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(prioridadeUnionistaPatente.NumeroPrioridade) + "', ");
            comandoSQL.Append(idPatente + ", ");
            comandoSQL.Append(prioridadeUnionistaPatente.Pais.ID + ") ");
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void InserirTitularPatente(ITitularPatente titularPatente, long idPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            titularPatente.Identificador = ObtenhaProximoIDTitular();

            comandoSQL.Append("INSERT INTO MP_PATENTETITULARINVENTOR(IDPATENTETITULARINVENTOR, IDPATENTE, IDPROCURADOR, CONTATO_TITULAR) VALUES(");
            comandoSQL.Append(titularPatente.Identificador + ", ");
            comandoSQL.Append(idPatente + ", ");
            comandoSQL.Append(titularPatente.Procurador.Pessoa.ID + ", ");
            comandoSQL.Append("'" + titularPatente.ContatoTitular + "') ");
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private int ObtenhaProximoIDAnuidadePatente()
        {
            int? proximoCodigo = null;
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            using (var reader = DBHelper.obtenhaReader("SELECT MAX(IDPATENTEANUIDADE) CODIGO FROM MP_PATENTEANUIDADE"))
                while (reader.Read())
                    proximoCodigo = UtilidadesDePersistencia.getValorInteger(reader, "CODIGO");

            if (proximoCodigo == null)
                return 1;

            return proximoCodigo.Value + 1;
        }

        private int ObtenhaProximoIDClassificacaoPatente()
        {
            int? proximoCodigo = null;
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            using (var reader = DBHelper.obtenhaReader("SELECT MAX(IDPATENTECLASSIFICACAO) CODIGO FROM MP_PATENTECLASSIFICACAO"))
                while (reader.Read())
                    proximoCodigo = UtilidadesDePersistencia.getValorInteger(reader, "CODIGO");

            if (proximoCodigo == null)
                return 1;

            return proximoCodigo.Value + 1;
        }

        private int ObtenhaProximoIDPrioridadeUnionista()
        {
            int? proximoCodigo = null;
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            using (var reader = DBHelper.obtenhaReader("SELECT MAX(IDPRIORIDADEUNIONISTA) CODIGO FROM MP_PATENTEPRIORIDADEUNIONISTA"))
                while (reader.Read())
                    proximoCodigo = UtilidadesDePersistencia.getValorInteger(reader, "CODIGO");

            if (proximoCodigo == null)
                return 1;

            return proximoCodigo.Value + 1;
        }

        private int ObtenhaProximoIDTitular()
        {
            int? proximoCodigo = null;
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            using (var reader = DBHelper.obtenhaReader("SELECT MAX(IDPATENTETITULARINVENTOR) CODIGO FROM MP_PATENTETITULARINVENTOR"))
                while (reader.Read())
                    proximoCodigo = UtilidadesDePersistencia.getValorInteger(reader, "CODIGO");

            if (proximoCodigo == null)
                return 1;

            return proximoCodigo.Value + 1;
        }

        private int ObtenhaProximoIDPatente()
        {
            int? proximoCodigo = null;
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            using (var reader = DBHelper.obtenhaReader("SELECT MAX(IDPROCESSOPATENTE) CODIGO FROM MP_PATENTE"))
                while (reader.Read())
                    proximoCodigo = UtilidadesDePersistencia.getValorInteger(reader, "CODIGO");

            if (proximoCodigo == null)
                return 1;

            return proximoCodigo.Value + 1;
        }

        public IList<IAnuidadePatente> ObtenhaAnuidadePeloIdDaPatente(long idPatente)
        {
            IList<IAnuidadePatente> anuidadesPatente = new List<IAnuidadePatente>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPATENTEANUIDADE, IDPATENTE, DESCRICAOANUIDADE, DATALANCAMENTO, DATAVENCIMENTO, DATAPAGAMENTO, VALORPAGAMENTO, ANUIDADEPAGA,");
            comandoSQL.Append("PEDIDOEXAME, DATAVENCTO_SEM_MULTA, DATAVENCTO_COM_MULTA ");
            comandoSQL.Append("FROM MP_PATENTEANUIDADE WHERE IDPATENTE = " + idPatente);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    anuidadesPatente.Add(MapeieObjetoAnuidadePatente(reader));

            return anuidadesPatente;
        }

        public IList<IClassificacaoPatente> ObtenhaClassificacaoPeloIdDaPatente(long idPatente)
        {
            IList<IClassificacaoPatente> classificacoesPatente = new List<IClassificacaoPatente>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPATENTECLASSIFICACAO, CLASSIFICACAO, DESCRICAO_CLASSIFICACAO, IDPATENTE, TIPO_CLASSIFICACAO FROM MP_PATENTECLASSIFICACAO ");
            comandoSQL.Append("WHERE IDPATENTECLASSIFICACAO = " + idPatente);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    classificacoesPatente.Add(MapeieObjetoClassificacaoPatente(reader));

            return classificacoesPatente;
        }

        public IList<IPrioridadeUnionistaPatente> ObtenhaPrioridadeUnionistaPeloIdDaPatente(long idPatente)
        {
            IList<IPrioridadeUnionistaPatente> prioridadesUnionistaPatente = new List<IPrioridadeUnionistaPatente>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPRIORIDADEUNIONISTA, DATA_PRIORIDADE, NUMERO_PRIORIDADE, IDPATENTE, IDPAIS FROM MP_PATENTEPRIORIDADEUNIONISTA ");
            comandoSQL.Append("WHERE IDPRIORIDADEUNIONISTA = " + idPatente);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    prioridadesUnionistaPatente.Add(MapeieObjetoPrioridadeUnionistaPatente(reader));

            return prioridadesUnionistaPatente;
        }

        public IList<ITitularPatente> ObtenhaTitularPeloIdDaPatente(long idPatente)
        {
            IList<ITitularPatente> titularesPatente = new List<ITitularPatente>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPATENTETITULARINVENTOR, IDPATENTE, IDPROCURADOR, CONTATO_TITULAR FROM MP_PATENTETITULARINVENTOR ");
            comandoSQL.Append("WHERE IDPATENTETITULARINVENTOR = " + idPatente);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    titularesPatente.Add(MapeieObjetoTitularPatente(reader));

            return titularesPatente;
        }

        private void ExluirAnuidade(int codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTEANUIDADE WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void ExluirClassificacao(int codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTECLASSIFICACAO WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void ExluirPrioridadeUnionista(int codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTEPRIORIDADEUNIONISTA WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void ExluirTitular(int codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTETITULARINVENTOR WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private IAnuidadePatente MapeieObjetoAnuidadePatente(IDataReader reader)
        {
            var anuidadePatente = FabricaGenerica.GetInstancia().CrieObjeto<IAnuidadePatente>();

            anuidadePatente.Identificador = UtilidadesDePersistencia.GetValorLong(reader, "IDPATENTEANUIDADE");
            anuidadePatente.DescricaoAnuidade = UtilidadesDePersistencia.GetValorString(reader, "DESCRICAOANUIDADE");
            anuidadePatente.DataLancamento = UtilidadesDePersistencia.getValorDate(reader, "DATALANCAMENTO");
            anuidadePatente.DataVencimento = UtilidadesDePersistencia.getValorDate(reader, "DATAVENCIMENTO");
            anuidadePatente.DataPagamento = UtilidadesDePersistencia.getValorDate(reader, "DATAPAGAMENTO");
            anuidadePatente.ValorPagamento = UtilidadesDePersistencia.getValorDouble(reader, "VALORPAGAMENTO");
            anuidadePatente.AnuidadePaga = UtilidadesDePersistencia.GetValorBooleano(reader, "ANUIDADEPAGA");
            anuidadePatente.PedidoExame = UtilidadesDePersistencia.GetValorBooleano(reader, "PEDIDOEXAME");
            anuidadePatente.DataVencimentoSemMulta = UtilidadesDePersistencia.getValorDate(reader, "DATAVENCTO_SEM_MULTA");
            anuidadePatente.DataVencimentoComMulta = UtilidadesDePersistencia.getValorDate(reader, "DATAVENCTO_COM_MULTA");
            
            return anuidadePatente;
        }

        private IClassificacaoPatente MapeieObjetoClassificacaoPatente(IDataReader reader)
        {
            var classificacaoPatente = FabricaGenerica.GetInstancia().CrieObjeto<IClassificacaoPatente>();

            classificacaoPatente.Identificador = UtilidadesDePersistencia.GetValorLong(reader, "IDPATENTECLASSIFICACAO");
            classificacaoPatente. Classificacao = UtilidadesDePersistencia.GetValorString(reader, "CLASSIFICACAO");
            classificacaoPatente.DescricaoClassificacao = UtilidadesDePersistencia.GetValorString(reader, "DESCRICAO_CLASSIFICACAO");
            classificacaoPatente.TipoClassificacao = TipoClassificacaoPatente.ObtenhaPorCodigo(UtilidadesDePersistencia.getValorInteger(reader, "TIPO_CLASSIFICACAO"));

            return classificacaoPatente;
        }

        private IPrioridadeUnionistaPatente MapeieObjetoPrioridadeUnionistaPatente(IDataReader reader)
        {
            var prioridadeUnionistaPatente = FabricaGenerica.GetInstancia().CrieObjeto<IPrioridadeUnionistaPatente>();

            prioridadeUnionistaPatente.Identificador = UtilidadesDePersistencia.GetValorLong(reader, "IDPRIORIDADEUNIONISTA");
            prioridadeUnionistaPatente.DataPrioridade = UtilidadesDePersistencia.getValorDate(reader, "DATA_PRIORIDADE");
            prioridadeUnionistaPatente.NumeroPrioridade = UtilidadesDePersistencia.GetValorString(reader, "NUMERO_PRIORIDADE");
            prioridadeUnionistaPatente.Pais = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPaisLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "IDPAIS"));

            return prioridadeUnionistaPatente;
        }

        private ITitularPatente MapeieObjetoTitularPatente(IDataReader reader)
        {
            var titularPatente = FabricaGenerica.GetInstancia().CrieObjeto<ITitularPatente>();

            titularPatente.Identificador = UtilidadesDePersistencia.GetValorLong(reader, "IDPATENTETITULARINVENTOR");
            titularPatente.Procurador = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IProcuradorLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "IDPROCURADOR"));
            titularPatente.ContatoTitular = UtilidadesDePersistencia.GetValorString(reader, "CONTATO_TITULAR");

            return titularPatente;
        }

        private IPatente MapeieObjetoPatente(IDataReader reader)
        {
            var patente = FabricaGenerica.GetInstancia().CrieObjeto<IPatente>();

            patente.Identificador = UtilidadesDePersistencia.GetValorLong(reader, "IDPROCESSOPATENTE");
            patente.TituloPatente = UtilidadesDePersistencia.GetValorString(reader, "TITULOPATENTE");
            patente.TipoDePatente = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<ITipoDePatenteLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "IDTIPOPATENTE"));
            patente.LinkINPI = UtilidadesDePersistencia.GetValorString(reader, "LINKINPI");
            patente.ObrigacaoGerada = UtilidadesDePersistencia.GetValorBooleano(reader, "OBRIGACAOGERADA");
            patente.DataCadastro = UtilidadesDePersistencia.getValorDate(reader, "DATACADASTRO");
            patente.Observacao = UtilidadesDePersistencia.GetValorString(reader, "OBSERVACAO");
            patente.Resumo = UtilidadesDePersistencia.GetValorString(reader, "RESUMO_PATENTE");
            patente.QuantidadeReivindicacao = UtilidadesDePersistencia.getValorInteger(reader, "QTDEREINVINDICACAO");
            patente.Anuidades = ObtenhaAnuidadePeloIdDaPatente(patente.Identificador);
            patente.Classificacoes = ObtenhaClassificacaoPeloIdDaPatente(patente.Identificador);
            patente.PrioridadesUnionista = ObtenhaPrioridadeUnionistaPeloIdDaPatente(patente.Identificador);
            patente.Titulares = ObtenhaTitularPeloIdDaPatente(patente.Identificador);

            return patente;
        }

#endregion
    }
}
