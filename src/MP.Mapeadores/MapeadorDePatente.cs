using System;
using System.Collections.Generic;
using System.Data;
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
using MP.Interfaces.Negocio.LazyLoad;

namespace MP.Mapeadores
{
    public class MapeadorDePatente : IMapeadorDePatente
    {
        public void Insira(IPatente patente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            patente.Identificador = GeradorDeID.getInstancia().getProximoID();

            comandoSQL.Append("INSERT INTO MP_PATENTE(IDPATENTE, TITULOPATENTE, IDNATUREZAPATENTE, OBRIGACAOGERADA, DATACADASTRO, OBSERVACAO,");
            comandoSQL.Append("RESUMO_PATENTE, QTDEREINVINDICACAO, IMAGEM, PAGAMANUTENCAO, DATAPRIMEIRAMANUTENCAO, PERIODO, FORMADECOBRANCA, VALORDECOBRANCA, MES) VALUES(");
            comandoSQL.Append(patente.Identificador + ", ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(patente.TituloPatente) + "', ");
            comandoSQL.Append(patente.NaturezaPatente.IdNaturezaPatente + ", ");
            comandoSQL.Append("'" + (patente.ObrigacaoGerada ? "1" : "0") + "', ");
            comandoSQL.Append(patente.DataCadastro.HasValue ? patente.DataCadastro.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(patente.Observacao) + "', ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(patente.Resumo) + "', ");
            comandoSQL.Append(patente.QuantidadeReivindicacao + ", ");

            comandoSQL.Append(string.IsNullOrEmpty(patente.Imagem)
                                  ? "NULL, "
                                  : string.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(patente.Imagem), "', "));

            if (patente.Manutencao == null)
                comandoSQL.Append("'0', NULL, NULL, NULL, NULL, NULL)");

            else
            {
                comandoSQL.Append("'1', ");
                comandoSQL.Append(patente.Manutencao.DataDaPrimeiraManutencao.HasValue
                               ? String.Concat(patente.Manutencao.DataDaPrimeiraManutencao.Value.ToString("yyyyMMdd"), ", ")
                               : "NULL, ");
                comandoSQL.Append(String.Concat("'", patente.Manutencao.Periodo.Codigo, "', "));
                comandoSQL.Append(String.Concat("'", patente.Manutencao.FormaDeCobranca.Codigo, "', "));
                comandoSQL.Append(String.Concat(UtilidadesDePersistencia.TPVd(patente.Manutencao.ValorDeCobranca), ", "));

                comandoSQL.Append(patente.Manutencao.MesQueIniciaCobranca == null
                               ? "NULL) "
                               : String.Concat("'", patente.Manutencao.MesQueIniciaCobranca.Codigo, "') "));
            }


            DBHelper.ExecuteNonQuery(comandoSQL.ToString());

            if (patente.Anuidades != null)
                foreach (IAnuidadePatente anuidadePatente in patente.Anuidades)
                    InserirAnuidade(anuidadePatente, patente.Identificador);

            if (patente.Classificacoes != null)
                foreach (IClassificacaoPatente classificacaoPatente in patente.Classificacoes)
                    InserirClassificacao(classificacaoPatente, patente.Identificador);

            if (patente.PrioridadesUnionista != null)
                foreach (IPrioridadeUnionistaPatente prioridadeUnionistaPatente in patente.PrioridadesUnionista)
                    InserirPrioridadeUnionista(prioridadeUnionistaPatente, patente.Identificador);

            if (patente.Inventores != null)
                foreach (IInventor inventor in patente.Inventores)
                    InserirInventorPatente(inventor, patente.Identificador);

            if (patente.Clientes != null)
                foreach (ICliente clientePatente in patente.Clientes)
                    InserirClientesPatente(clientePatente, patente.Identificador);

            if (patente.Radicais != null)
                foreach (IRadicalPatente radicalPatente in patente.Radicais)
                    InserirRadicaisPatente(radicalPatente, patente.Identificador);

            if (patente.Titulares != null)
                foreach (ITitular titular in patente.Titulares)
                    InserirTitularPatente(titular, patente.Identificador);
        }

        public void Modificar(IPatente patente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("UPDATE MP_PATENTE SET ");
            comandoSQL.Append("TITULOPATENTE = '" + UtilidadesDePersistencia.FiltraApostrofe(patente.TituloPatente) + "', ");
            comandoSQL.Append("IDNATUREZAPATENTE = " + patente.NaturezaPatente.IdNaturezaPatente + ", ");
            comandoSQL.Append("DATACADASTRO = " + (patente.DataCadastro.HasValue ? patente.DataCadastro.Value.ToString("yyyyMMdd") + ", " : "NULL, "));
            comandoSQL.Append("OBSERVACAO = '" + patente.Observacao + "', ");
            comandoSQL.Append("RESUMO_PATENTE = '" + patente.Resumo + "', ");
            comandoSQL.Append("QTDEREINVINDICACAO = " + patente.QuantidadeReivindicacao + ", ");

            comandoSQL.Append(string.IsNullOrEmpty(patente.Imagem)
                                  ? "IMAGEM = NULL, "
                                  : string.Concat("IMAGEM = '", UtilidadesDePersistencia.FiltraApostrofe(patente.Imagem), "', "));

            
            if (patente.Manutencao == null)
            {
                comandoSQL.Append("PAGAMANUTENCAO = '0', ");
                comandoSQL.Append("DATAPRIMEIRAMANUTENCAO = NULL, ");
                comandoSQL.Append("PERIODO = NULL, ");
                comandoSQL.Append("FORMADECOBRANCA = NULL, ");
                comandoSQL.Append("VALORDECOBRANCA = NULL, ");
                comandoSQL.Append("MES = NULL ");
            }
            else
            {
                comandoSQL.Append("PAGAMANUTENCAO = '1', ");
                comandoSQL.Append(patente.Manutencao.DataDaPrimeiraManutencao.HasValue
                               ? String.Concat("DATAPRIMEIRAMANUTENCAO = ", patente.Manutencao.DataDaPrimeiraManutencao.Value.ToString("yyyyMMdd"), ", ")
                               : "NULL, ");
                comandoSQL.Append(String.Concat("PERIODO = '", patente.Manutencao.Periodo.Codigo, "', "));
                comandoSQL.Append(String.Concat("FORMADECOBRANCA = '", patente.Manutencao.FormaDeCobranca.Codigo, "', "));
                comandoSQL.Append(string.Concat("VALORDECOBRANCA = ", UtilidadesDePersistencia.TPVd(patente.Manutencao.ValorDeCobranca), ", "));

                comandoSQL.Append(patente.Manutencao.MesQueIniciaCobranca == null
                               ? "MES = NULL "
                               : String.Concat("MES = '", patente.Manutencao.MesQueIniciaCobranca.Codigo, "' "));
            }

            comandoSQL.Append(" WHERE IDPATENTE = " + patente.Identificador);

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());

            ExcluirAnuidade(patente.Identificador);
            ExcluirClassificacao(patente.Identificador);
            ExcluirPrioridadeUnionista(patente.Identificador);
            ExcluirInventores(patente.Identificador);
            ExcluirCliente(patente.Identificador);
            ExcluirRadicais(patente.Identificador);
            ExcluirTitulares(patente.Identificador);

            if (patente.Anuidades != null)
                foreach (IAnuidadePatente anuidadePatente in patente.Anuidades)
                    InserirAnuidade(anuidadePatente, patente.Identificador);

            if (patente.Classificacoes != null)
                foreach (IClassificacaoPatente classificacaoPatente in patente.Classificacoes)
                    InserirClassificacao(classificacaoPatente, patente.Identificador);

            if (patente.PrioridadesUnionista != null)
                foreach (IPrioridadeUnionistaPatente prioridadeUnionistaPatente in patente.PrioridadesUnionista)
                    InserirPrioridadeUnionista(prioridadeUnionistaPatente, patente.Identificador);

            if (patente.Inventores != null)
                foreach (IInventor inventor in patente.Inventores)
                    InserirInventorPatente(inventor, patente.Identificador);

            if (patente.Clientes != null)
                foreach (ICliente clientePatente in patente.Clientes)
                    InserirClientesPatente(clientePatente, patente.Identificador);

            if (patente.Radicais != null)
                foreach (IRadicalPatente radicalPatente in patente.Radicais)
                    InserirRadicaisPatente(radicalPatente, patente.Identificador);

            if (patente.Titulares != null)
                foreach (ITitular titular in patente.Titulares)
                    InserirTitularPatente(titular, patente.Identificador);
        }

        public void Exluir(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            ExcluirAnuidade(codigoPatente);
            ExcluirClassificacao(codigoPatente);
            ExcluirPrioridadeUnionista(codigoPatente);
            ExcluirInventores(codigoPatente);
            ExcluirCliente(codigoPatente);
            ExcluirRadicais(codigoPatente);
            ExcluirTitulares(codigoPatente);

            comandoSQL.Append("DELETE FROM MP_PATENTE WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
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

        public IInventor ObtenhaInventor(long id)
        {
            IInventor inventorPatente = null;
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDINVENTOR, IDPATENTE FROM MP_PATENTEINVENTOR ");
            comandoSQL.Append("WHERE IDINVENTOR = " + id);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    inventorPatente = MapeieObjetoIventorPatente(reader);

            return inventorPatente;
        }

        public ITitular ObtenhaTitular(long id)
        {
            ITitular titular = null;
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDTITULAR, IDPATENTE FROM MP_PATENTETITULAR ");
            comandoSQL.Append("WHERE IDTITULAR = " + id);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    titular = MapeieObjetoTitularPatente(reader);

            return titular;
        }

        public IPatente ObtenhaPatente(long id)
        {
            IPatente patente = null;
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPATENTE, TITULOPATENTE, IDNATUREZAPATENTE, OBRIGACAOGERADA, DATACADASTRO, OBSERVACAO, RESUMO_PATENTE,");
            comandoSQL.Append("PAGAMANUTENCAO, DATAPRIMEIRAMANUTENCAO, PERIODO, FORMADECOBRANCA, VALORDECOBRANCA, QTDEREINVINDICACAO, MES, IMAGEM FROM MP_PATENTE ");
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

            comandoSQL.Append("SELECT IDPATENTE, TITULOPATENTE, IDNATUREZAPATENTE, OBRIGACAOGERADA, DATACADASTRO, OBSERVACAO, RESUMO_PATENTE,");
            comandoSQL.Append("PAGAMANUTENCAO, DATAPRIMEIRAMANUTENCAO, PERIODO, FORMADECOBRANCA, VALORDECOBRANCA, QTDEREINVINDICACAO, MES, IMAGEM FROM MP_PATENTE ");

            if (!string.IsNullOrEmpty(titulo))
                comandoSQL.Append("WHERE TITULOPATENTE like '%" + titulo + "%'");

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString(), quantidadeDeRegistros))
                while (reader.Read())
                    patentes.Add(MapeieObjetoPatente(reader));

            return patentes;
        }

        public IList<IPatente> ObtenhaPatentesDoCliente(string titulo, long idCliente, int quantidadeDeRegistros)
        {
            IList<IPatente> patentes = new List<IPatente>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT PATENTE.IDPATENTE, PATENTE.TITULOPATENTE, PATENTE.IDNATUREZAPATENTE, PATENTE.OBRIGACAOGERADA, ");
            comandoSQL.Append("PATENTE.DATACADASTRO, PATENTE.OBSERVACAO, PATENTE.RESUMO_PATENTE, PATENTE.QTDEREINVINDICACAO, ");
            comandoSQL.Append("PAGAMANUTENCAO, DATAPRIMEIRAMANUTENCAO, PERIODO, FORMADECOBRANCA, VALORDECOBRANCA, MES, IMAGEM FROM MP_PATENTE PATENTE ");
            comandoSQL.Append("INNER JOIN MP_PATENTECLIENTE CLIPATENTE ON CLIPATENTE.IDPATENTE = PATENTE.IDPATENTE ");
            comandoSQL.Append("WHERE CLIPATENTE.IDCLIENTE = " + idCliente);

            if (!string.IsNullOrEmpty(titulo))
                comandoSQL.Append("AND TITULOPATENTE like '%" + titulo + "%'");

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

            anuidadePatente.Identificador = GeradorDeID.getInstancia().getProximoID();

            comandoSQL.Append("INSERT INTO MP_PATENTEANUIDADE(IDPATENTEANUIDADE, IDPATENTE, DESCRICAOANUIDADE, DATALANCAMENTO, DATAVENCIMENTO,");
            comandoSQL.Append("DATAPAGAMENTO, VALORPAGAMENTO, ANUIDADEPAGA, PEDIDOEXAME, DATAVENCTO_SEM_MULTA, DATAVENCTO_COM_MULTA) VALUES(");
            comandoSQL.Append(anuidadePatente.Identificador + ", ");
            comandoSQL.Append(idPatente + ", ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(anuidadePatente.DescricaoAnuidade) + "', ");
            comandoSQL.Append(anuidadePatente.DataLancamento.HasValue ? anuidadePatente.DataLancamento.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append(anuidadePatente.DataVencimento.HasValue ? anuidadePatente.DataVencimento.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append(anuidadePatente.DataPagamento.HasValue ? anuidadePatente.DataPagamento.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append(anuidadePatente.ValorPagamento.ToString().Replace(",", ".") + ", ");
            comandoSQL.Append("'" + (anuidadePatente.AnuidadePaga ? "1" : "0") + "', ");
            comandoSQL.Append("'" + (anuidadePatente.PedidoExame ? "1" : "0") + "', ");
            comandoSQL.Append(anuidadePatente.DataVencimentoSemMulta != null ? anuidadePatente.DataVencimentoSemMulta.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append(anuidadePatente.DataVencimentoComMulta != null ? anuidadePatente.DataVencimentoComMulta.Value.ToString("yyyyMMdd") + ") " : "NULL) ");

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void InserirClassificacao(IClassificacaoPatente classificacaoPatente, long idPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            classificacaoPatente.Identificador = GeradorDeID.getInstancia().getProximoID();

            comandoSQL.Append("INSERT INTO MP_PATENTECLASSIFICACAO(IDPATENTECLASSIFICACAO, CLASSIFICACAO, DESCRICAO_CLASSIFICACAO, IDPATENTE, TIPO_CLASSIFICACAO) VALUES(");
            comandoSQL.Append(classificacaoPatente.Identificador + ", ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(classificacaoPatente.Classificacao) + "', ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(classificacaoPatente.DescricaoClassificacao) + "', ");
            comandoSQL.Append(idPatente + ", ");
            comandoSQL.Append(classificacaoPatente.TipoClassificacao.Codigo + ")");
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void InserirPrioridadeUnionista(IPrioridadeUnionistaPatente prioridadeUnionistaPatente, long idPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            prioridadeUnionistaPatente.Identificador = GeradorDeID.getInstancia().getProximoID();

            comandoSQL.Append("INSERT INTO MP_PATENTEPRIORIDADEUNIONISTA(IDPRIORIDADEUNIONISTA, DATA_PRIORIDADE, NUMERO_PRIORIDADE, IDPATENTE, IDPAIS) VALUES(");
            comandoSQL.Append(prioridadeUnionistaPatente.Identificador + ", ");
            comandoSQL.Append(prioridadeUnionistaPatente.DataPrioridade.HasValue ? prioridadeUnionistaPatente.DataPrioridade.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(prioridadeUnionistaPatente.NumeroPrioridade) + "', ");
            comandoSQL.Append(idPatente + ", ");
            comandoSQL.Append(prioridadeUnionistaPatente.Pais.ID + ") ");
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void InserirInventorPatente(IInventor inventor, long idPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("INSERT INTO MP_PATENTEINVENTOR(IDINVENTOR, IDPATENTE) VALUES(");
            comandoSQL.Append(inventor.Pessoa.ID + ", ");
            comandoSQL.Append(idPatente + ")");
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void InserirTitularPatente(ITitular titular, long idPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("INSERT INTO MP_PATENTETITULAR(IDTITULAR, IDPATENTE) VALUES(");
            comandoSQL.Append(titular.Pessoa.ID + ", ");
            comandoSQL.Append(idPatente + ")");
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
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
            comandoSQL.Append("WHERE IDPATENTE = " + idPatente);

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
            comandoSQL.Append("WHERE IDPATENTE = " + idPatente);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    prioridadesUnionistaPatente.Add(MapeieObjetoPrioridadeUnionistaPatente(reader));

            return prioridadesUnionistaPatente;
        }

        public IList<IInventor> ObtenhaInventoresPeloIdDaPatente(long idPatente)
        {
            IList<IInventor> inventores = new List<IInventor>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDINVENTOR, IDPATENTE FROM MP_PATENTEINVENTOR ");
            comandoSQL.Append("WHERE IDPATENTE = " + idPatente);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    inventores.Add(MapeieObjetoIventorPatente(reader));

            return inventores;
        }

        public IList<ITitular> ObtenhaTitularesPeloIdDaPatente(long idPatente)
        {
            IList<ITitular> titulares = new List<ITitular>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDTITULAR, IDPATENTE FROM MP_PATENTETITULAR ");
            comandoSQL.Append("WHERE IDPATENTE = " + idPatente);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    titulares.Add(MapeieObjetoTitularPatente(reader));

            return titulares;
        }

        private void ExcluirAnuidade(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTEANUIDADE WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void ExcluirClassificacao(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTECLASSIFICACAO WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void ExcluirPrioridadeUnionista(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTEPRIORIDADEUNIONISTA WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void ExcluirInventores(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTEINVENTOR WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void ExcluirCliente(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTECLIENTE WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void ExcluirRadicais(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_RADICAL_PATENTE WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void ExcluirTitulares(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTETITULAR WHERE IDPATENTE = " + codigoPatente);
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
            classificacaoPatente.Classificacao = UtilidadesDePersistencia.GetValorString(reader, "CLASSIFICACAO");
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

        private IInventor MapeieObjetoIventorPatente(IDataReader reader)
        {
            var inventor = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IInventorLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "IDINVENTOR"));
            return inventor;
        }

        private ITitular MapeieObjetoTitularPatente(IDataReader reader)
        {
            var titular = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<ITitularLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "IDTITULAR"));
            return titular;
        }

        private IPatente MapeieObjetoPatente(IDataReader reader)
        {
            var patente = FabricaGenerica.GetInstancia().CrieObjeto<IPatente>();

            patente.Identificador = UtilidadesDePersistencia.GetValorLong(reader, "IDPATENTE");
            patente.TituloPatente = UtilidadesDePersistencia.GetValorString(reader, "TITULOPATENTE");
            patente.NaturezaPatente = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<INaturezaPatenteLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "IDNATUREZAPATENTE"));
            patente.ObrigacaoGerada = UtilidadesDePersistencia.GetValorBooleano(reader, "OBRIGACAOGERADA");
            patente.DataCadastro = UtilidadesDePersistencia.getValorDate(reader, "DATACADASTRO");
            patente.Observacao = UtilidadesDePersistencia.GetValorString(reader, "OBSERVACAO");
            patente.Resumo = UtilidadesDePersistencia.GetValorString(reader, "RESUMO_PATENTE");
            patente.QuantidadeReivindicacao = UtilidadesDePersistencia.getValorInteger(reader, "QTDEREINVINDICACAO");
            patente.Anuidades = ObtenhaAnuidadePeloIdDaPatente(patente.Identificador);
            patente.Classificacoes = ObtenhaClassificacaoPeloIdDaPatente(patente.Identificador);
            patente.PrioridadesUnionista = ObtenhaPrioridadeUnionistaPeloIdDaPatente(patente.Identificador);
            patente.Inventores = ObtenhaInventoresPeloIdDaPatente(patente.Identificador);
            patente.Clientes = ObtenhaClientesPatente(patente.Identificador);
            patente.Radicais = ObtenhaRadicais(patente.Identificador);
            patente.Titulares = ObtenhaTitularesPeloIdDaPatente(patente.Identificador);

            if (!UtilidadesDePersistencia.EhNulo(reader, "IMAGEM"))
                patente.Imagem = UtilidadesDePersistencia.GetValorString(reader, "IMAGEM");

            if (UtilidadesDePersistencia.GetValorBooleano(reader, "PagaManutencao"))
            {
                var manutencao = FabricaGenerica.GetInstancia().CrieObjeto<IManutencao>();

                if (!UtilidadesDePersistencia.EhNulo(reader, "DATAPRIMEIRAMANUTENCAO"))
                    manutencao.DataDaPrimeiraManutencao = UtilidadesDePersistencia.getValorDate(reader,
                                                                                                "DATAPRIMEIRAMANUTENCAO");

                manutencao.Periodo = Periodo.ObtenhaPorCodigo(UtilidadesDePersistencia.getValorInteger(reader, "Periodo"));
                manutencao.FormaDeCobranca = FormaCobrancaManutencao.ObtenhaPorCodigo(UtilidadesDePersistencia.GetValorString(reader, "FormaDeCobranca"));
                manutencao.ValorDeCobranca =  UtilidadesDePersistencia.getValorDouble(reader, "ValorDeCobranca");

                if (!UtilidadesDePersistencia.EhNulo(reader, "Mes"))
                    manutencao.MesQueIniciaCobranca = Mes.ObtenhaPorCodigo(UtilidadesDePersistencia.getValorInteger(reader, "Mes"));

                patente.Manutencao = manutencao;
            }

            
            return patente;
        }

        private ICliente MapeieObjetoClientePatente(IDataReader reader)
        {
            var clientePatente = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IClienteLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "IDCLIENTE"));
            return clientePatente;
        }

        public IList<ICliente> ObtenhaClientesPatente(long id)
        {
            IList<ICliente> clientesPatente = new List<ICliente>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDCLIENTE, IDPATENTE FROM MP_PATENTECLIENTE ");
            comandoSQL.Append("WHERE IDPATENTE = " + id);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    clientesPatente.Add(MapeieObjetoClientePatente(reader));

            return clientesPatente;
        }

        private void InserirClientesPatente(ICliente cliente, long idPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("INSERT INTO MP_PATENTECLIENTE(IDCLIENTE, IDPATENTE) VALUES(");
            comandoSQL.Append(cliente.Pessoa.ID + ", ");
            comandoSQL.Append(idPatente + ")");
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void InserirRadicaisPatente(IRadicalPatente radical, long idPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            radical.IdRadicalPatente = GeradorDeID.getInstancia().getProximoID();
            radical.IdPatente = idPatente;

            comandoSQL.Append("INSERT INTO MP_RADICAL_PATENTE(IDRADICAL, COLIDENCIA, IDPATENTE) VALUES(");
            comandoSQL.Append(radical.IdRadicalPatente + ", ");
            comandoSQL.Append("'" + radical.Colidencia + "', ");
            comandoSQL.Append(radical.IdPatente + ")");
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private IList<IRadicalPatente> ObtenhaRadicais(long id)
        {
            IList<IRadicalPatente> radicais = new List<IRadicalPatente>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDRADICAL, COLIDENCIA, IDPATENTE FROM MP_RADICAL_PATENTE ");
            comandoSQL.Append("WHERE IDPATENTE = " + id);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    radicais.Add(MapeieObjetoRadicaisPatente(reader));

            return radicais;
        }

        private IRadicalPatente MapeieObjetoRadicaisPatente(IDataReader reader)
        {
            var radicalPatente = FabricaGenerica.GetInstancia().CrieObjeto<IRadicalPatente>();

            radicalPatente.IdRadicalPatente = UtilidadesDePersistencia.GetValorLong(reader, "IDRADICAL");
            radicalPatente.Colidencia = UtilidadesDePersistencia.GetValorString(reader, "COLIDENCIA");
            radicalPatente.IdPatente = UtilidadesDePersistencia.GetValorLong(reader, "IDPATENTE");

            return radicalPatente;
        }

        #endregion
    }
}
