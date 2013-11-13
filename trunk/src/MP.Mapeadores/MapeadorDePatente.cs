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
            comandoSQL.Append("RESUMO_PATENTE, QTDEREINVINDICACAO) VALUES(");
            comandoSQL.Append(patente.Identificador + ", ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(patente.TituloPatente) + "', ");
            comandoSQL.Append(patente.NaturezaPatente.IdNaturezaPatente + ", ");
            comandoSQL.Append("'" + (patente.ObrigacaoGerada ? "1" : "0") + "', ");
            comandoSQL.Append(patente.DataCadastro.HasValue ? patente.DataCadastro.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(patente.Observacao) + "', ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(patente.Resumo) + "', ");
            comandoSQL.Append(patente.QuantidadeReivindicacao + ")");
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
                    InserirTitularPatente(inventor, patente.Identificador);

            if (patente.Clientes != null)
                foreach (ICliente clientePatente in patente.Clientes)
                    InserirClientesPatente(clientePatente, patente.Identificador);
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
            comandoSQL.Append("QTDEREINVINDICACAO = " + patente.QuantidadeReivindicacao);
            comandoSQL.Append(" WHERE IDPATENTE = " + patente.Identificador);

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());

            ExluirAnuidade(patente.Identificador);
            ExluirClassificacao(patente.Identificador);
            ExluirPrioridadeUnionista(patente.Identificador);
            ExluirTitular(patente.Identificador);
            ExluirCliente(patente.Identificador);

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
                    InserirTitularPatente(inventor, patente.Identificador);

            if (patente.Clientes != null)
                foreach (ICliente clientePatente in patente.Clientes)
                    InserirClientesPatente(clientePatente, patente.Identificador);
        }

        public void Exluir(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            ExluirAnuidade(codigoPatente);
            ExluirClassificacao(codigoPatente);
            ExluirPrioridadeUnionista(codigoPatente);
            ExluirTitular(codigoPatente);
            ExluirCliente(codigoPatente);

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

            comandoSQL.Append("SELECT IDTITULARINVENTOR, IDPATENTE FROM MP_PATENTETITULARINVENTOR ");
            comandoSQL.Append("WHERE IDTITULARINVENTOR = " + id);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    inventorPatente = MapeieObjetoIventorPatente(reader);

            return inventorPatente;
        }

        public IPatente ObtenhaPatente(long id)
        {
            IPatente patente = null;
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDPATENTE, TITULOPATENTE, IDNATUREZAPATENTE, OBRIGACAOGERADA, DATACADASTRO, OBSERVACAO, RESUMO_PATENTE,");
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

            comandoSQL.Append("SELECT IDPATENTE, TITULOPATENTE, IDNATUREZAPATENTE, OBRIGACAOGERADA, DATACADASTRO, OBSERVACAO, RESUMO_PATENTE,");
            comandoSQL.Append("QTDEREINVINDICACAO FROM MP_PATENTE ");

            if (!string.IsNullOrEmpty(titulo))
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

            anuidadePatente.Identificador = GeradorDeID.getInstancia().getProximoID();

            comandoSQL.Append("INSERT INTO MP_PATENTEANUIDADE(IDPATENTEANUIDADE, IDPATENTE, DESCRICAOANUIDADE, DATALANCAMENTO, DATAVENCIMENTO,");
            comandoSQL.Append("DATAPAGAMENTO, VALORPAGAMENTO, ANUIDADEPAGA, PEDIDOEXAME, DATAVENCTO_SEM_MULTA, DATAVENCTO_COM_MULTA) VALUES(");
            comandoSQL.Append(anuidadePatente.Identificador + ", ");
            comandoSQL.Append(idPatente + ", ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(anuidadePatente.DescricaoAnuidade) + "', ");
            comandoSQL.Append(anuidadePatente.DataLancamento.HasValue ? anuidadePatente.DataLancamento.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append(anuidadePatente.DataVencimento.HasValue ? anuidadePatente.DataVencimento.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append(anuidadePatente.DataPagamento.HasValue ? anuidadePatente.DataPagamento.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append(anuidadePatente.ValorPagamento + ", ");
            comandoSQL.Append("'" + (anuidadePatente.AnuidadePaga ? "1" : "0" )+ "', ");
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

            prioridadeUnionistaPatente.Identificador = GeradorDeID.getInstancia().getProximoID();

            comandoSQL.Append("INSERT INTO MP_PATENTEPRIORIDADEUNIONISTA(IDPRIORIDADEUNIONISTA, DATA_PRIORIDADE, NUMERO_PRIORIDADE, IDPATENTE, IDPAIS) VALUES(");
            comandoSQL.Append(prioridadeUnionistaPatente.Identificador + ", ");
            comandoSQL.Append(prioridadeUnionistaPatente.DataPrioridade.HasValue ? prioridadeUnionistaPatente.DataPrioridade.Value.ToString("yyyyMMdd") + ", " : "NULL, ");
            comandoSQL.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(prioridadeUnionistaPatente.NumeroPrioridade) + "', ");
            comandoSQL.Append(idPatente + ", ");
            comandoSQL.Append(prioridadeUnionistaPatente.Pais.ID + ") ");
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void InserirTitularPatente(IInventor inventor, long idPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("INSERT INTO MP_PATENTETITULARINVENTOR(IDTITULARINVENTOR, IDPATENTE) VALUES(");
            comandoSQL.Append(inventor.Pessoa.ID + ", ");
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

        public IList<IInventor> ObtenhaTitularPeloIdDaPatente(long idPatente)
        {
            IList<IInventor> inventores = new List<IInventor>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            comandoSQL.Append("SELECT IDTITULARINVENTOR, IDPATENTE FROM MP_PATENTETITULARINVENTOR ");
            comandoSQL.Append("WHERE IDPATENTE = " + idPatente);

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    inventores.Add(MapeieObjetoIventorPatente(reader));

            return inventores;
        }

        private void ExluirAnuidade(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTEANUIDADE WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void ExluirClassificacao(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTECLASSIFICACAO WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void ExluirPrioridadeUnionista(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTEPRIORIDADEUNIONISTA WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void ExluirTitular(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTETITULARINVENTOR WHERE IDPATENTE = " + codigoPatente);
            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        private void ExluirCliente(long codigoPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_PATENTECLIENTE WHERE IDPATENTE = " + codigoPatente);
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

        private IInventor MapeieObjetoIventorPatente(IDataReader reader)
        {
            var inventor = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IInventorLazyLoad>(UtilidadesDePersistencia.GetValorLong(reader, "IDTITULARINVENTOR"));
            return inventor;
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
            patente.Inventores = ObtenhaTitularPeloIdDaPatente(patente.Identificador);
            patente.Clientes = ObtenhaClientesPatente(patente.Identificador);

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

#endregion
    }
}
