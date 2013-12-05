﻿using System;
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
using MP.Interfaces.Negocio.Filtros.Patentes;
using MP.Interfaces.Negocio.LazyLoad;

namespace MP.Mapeadores
{
    public class MapeadorDeProcessoDePatente : IMapeadorDeProcessoDePatente
    {

        public void Inserir(IProcessoDePatente processoDePatente)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();
            processoDePatente.IdProcessoDePatente = GeradorDeID.getInstancia().getProximoID();
            
            sql.Append("INSERT INTO MP_PROCESSOPATENTE (");
            sql.Append("IDPROCESSOPATENTE, IDPATENTE, PROCESSO,");
            sql.Append("DATADECADASTRO, DATADEPUBLICACAO,  DATADEDEPOSITO, DATADECONCESSAO, DATADEEXAME, PROCESSODETERCEIRO, ");
            sql.Append("IDPROCURADOR, EHESTRANGEIRO, NUMEROPCT, NUMEROWO, DATAPUBLICACAOPCT, DATADEPOSITOPCT, IDDESPACHO, IDPASTA, ATIVO)");
            sql.Append("VALUES (");
            sql.Append(String.Concat(processoDePatente.IdProcessoDePatente.Value, ", "));
            sql.Append(String.Concat(processoDePatente.Patente.Identificador, ", "));
            sql.Append(String.Concat("'",UtilidadesDePersistencia.FiltraApostrofe(processoDePatente.Processo), "', "));
            sql.Append(String.Concat(processoDePatente.DataDoCadastro.ToString("yyyyMMdd"), ", "));

            sql.Append(processoDePatente.DataDaPublicacao.HasValue
                           ? String.Concat(processoDePatente.DataDaPublicacao.Value.ToString("yyyyMMdd"), ", ")
                           : "NULL, ");

            sql.Append(processoDePatente.DataDoDeposito.HasValue
                           ? String.Concat(processoDePatente.DataDoDeposito.Value.ToString("yyyyMMdd"), ", ")
                           : "NULL, ");

            sql.Append(processoDePatente.DataDaConcessao.HasValue
                           ? String.Concat(processoDePatente.DataDaConcessao.Value.ToString("yyyyMMdd"), ", ")
                           : "NULL, ");

            sql.Append(processoDePatente.DataDoExame.HasValue
                           ? String.Concat(processoDePatente.DataDoExame.Value.ToString("yyyyMMdd"), ", ")
                           : "NULL, ");

            sql.Append(processoDePatente.ProcessoEhDeTerceiro ? "'1', " : "'0', ");
            sql.Append(String.Concat(processoDePatente.Procurador != null ? processoDePatente.Procurador.Pessoa.ID.Value.ToString() : "NULL" , ", "));
            sql.Append(processoDePatente.ProcessoEhEstrangeiro ? "'1', " : "'0', ");


            if (processoDePatente.PCT != null)
            {
                var pct = processoDePatente.PCT;

                sql.Append(!string.IsNullOrEmpty(pct.Numero)
                               ? string.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(pct.Numero), "', ")
                               : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(pct.NumeroWO)
                               ? string.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(pct.Numero), "', ")
                               : "NULL, ");

                sql.Append(pct.DataDaPublicacao.HasValue? string.Concat(pct.DataDaPublicacao.Value.ToString("yyyyMMdd"), ", ")
                               : "NULL, ");

                sql.Append(pct.DataDoDeposito.HasValue ? string.Concat(pct.DataDoDeposito.Value.ToString("yyyyMMdd"), ", ")
                             : "NULL, ");
            }

            else
                sql.Append("NULL, NULL, NULL, NULL, ");


            sql.Append(processoDePatente.Despacho != null
                           ? string.Concat(processoDePatente.Despacho.IdDespachoDePatente.Value, ", ")
                           : "NULL, ");

            sql.Append(processoDePatente.Pasta != null
                           ? string.Concat(processoDePatente.Pasta.ID.Value.ToString(), ", ")
                           : "NULL, ");

            sql.Append(processoDePatente.Ativo ? "'1')" : "'0')");
            
            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(IProcessoDePatente processoDePatente)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_PROCESSOPATENTE ");
            sql.Append("SET IDPATENTE = " + processoDePatente.Patente.Identificador + ", ");
            sql.Append(String.Concat("PROCESSO = '", processoDePatente.Processo, "', "));

            sql.Append(processoDePatente.DataDaPublicacao.HasValue
                         ? String.Concat("DATADEPUBLICACAO=", processoDePatente.DataDaPublicacao.Value.ToString("yyyyMMdd"), ", ")
                         : "DATADEPUBLICACAO=NULL, ");

            sql.Append(processoDePatente.DataDoDeposito.HasValue
                           ? String.Concat("DATADEDEPOSITO=",processoDePatente.DataDoDeposito.Value.ToString("yyyyMMdd"), ", ")
                           : "DATADEDEPOSITO=NULL, ");

            sql.Append(processoDePatente.DataDaConcessao.HasValue
                           ? String.Concat("DATADECONCESSAO=",processoDePatente.DataDaConcessao.Value.ToString("yyyyMMdd"), ", ")
                           : "DATADECONCESSAO=NULL, ");

            sql.Append(processoDePatente.DataDoExame.HasValue
                           ? String.Concat("DATADEEXAME=",processoDePatente.DataDoExame.Value.ToString("yyyyMMdd"), ", ")
                           : "DATADEEXAME=NULL, ");

            sql.Append("PROCESSODETERCEIRO = " + (processoDePatente.ProcessoEhDeTerceiro ? "'1', " : "'0', "));
            sql.Append(String.Concat("IDPROCURADOR = ", processoDePatente.Procurador != null ? processoDePatente.Procurador.Pessoa.ID.Value.ToString() : "NULL", ", "));
            sql.Append("EHESTRANGEIRO = " + (processoDePatente.ProcessoEhEstrangeiro ? "1, " : "0, "));
            
            if (processoDePatente.PCT != null)
            {
                var pct = processoDePatente.PCT;

                sql.Append(!string.IsNullOrEmpty(pct.Numero)
                               ? string.Concat("NUMEROPCT='", UtilidadesDePersistencia.FiltraApostrofe(pct.Numero), "', ")
                               : "NUMEROPCT=NULL, ");

                sql.Append(!string.IsNullOrEmpty(pct.NumeroWO)
                               ? string.Concat("NUMEROWO='", UtilidadesDePersistencia.FiltraApostrofe(pct.Numero), "', ")
                               : "NUMEROWO=NULL, ");

                sql.Append(pct.DataDaPublicacao.HasValue? string.Concat("DATAPUBLICACAOPCT=",pct.DataDaPublicacao.Value.ToString("yyyyMMdd"), ", ")
                               : "DATAPUBLICACAOPCT=NULL, ");

                sql.Append(pct.DataDoDeposito.HasValue ? string.Concat("DATADEPOSITOPCT=",pct.DataDoDeposito.Value.ToString("yyyyMMdd"), ", ")
                             : "DATADEPOSITOPCT=NULL, ");
            }

            else
                sql.Append("NUMEROPCT=NULL, NUMEROWO=NULL, DATAPUBLICACAOPCT=NULL, DATADEPOSITOPCT=NULL, ");


            sql.Append(processoDePatente.Despacho != null
                           ? string.Concat("IDDESPACHO=", processoDePatente.Despacho.IdDespachoDePatente.Value, ", ")
                           : "IDDESPACHO=NULL, ");

            sql.Append(processoDePatente.Pasta != null
                           ? string.Concat("IDPASTA = ", processoDePatente.Pasta.ID.Value.ToString(), ", ")
                           : "IDPASTA = NULL, ");

            sql.Append("ATIVO = " + (processoDePatente.Ativo ? "1" : "0"));
            sql.Append(" WHERE IDPROCESSOPATENTE = " + processoDePatente.IdProcessoDePatente);

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Excluir(long ID)
        {
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();
            DBHelper.ExecuteNonQuery("DELETE FROM MP_PROCESSOPATENTE WHERE IDPROCESSOPATENTE=" + ID.ToString());
        }

        public IList<IProcessoDePatente> ObtenhaProcessosDePatentes(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append(filtro.ObtenhaQuery());

            sql.AppendLine(" ORDER BY DATADECADASTRO DESC");

            var processos = new List<IProcessoDePatente>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeDeRegistros, offSet))
            {
                try
                {
                    while (leitor.Read())
                        processos.Add(MontaProcessoDePatente(leitor));
                }
                finally
                {
                    leitor.Close();
                }
            }

            return processos;
        }

        private IProcessoDePatente MontaProcessoDePatente(IDataReader leitor)
        {
            var processo = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDePatente>();
            processo.IdProcessoDePatente = UtilidadesDePersistencia.GetValorLong(leitor, "IDPROCESSOPATENTE");
            processo.Patente =
                FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPatenteLazyLoad>(
                    UtilidadesDePersistencia.GetValorLong(leitor, "IDPATENTE"));
            processo.Processo = UtilidadesDePersistencia.GetValorString(leitor, "PROCESSO");
            processo.DataDoCadastro = UtilidadesDePersistencia.getValorDate(leitor, "DATADECADASTRO").Value;
            processo.DataDaPublicacao = UtilidadesDePersistencia.getValorDate(leitor, "DATADEPUBLICACAO");
            processo.DataDoDeposito = UtilidadesDePersistencia.getValorDate(leitor, "DATADEDEPOSITO");
            processo.DataDaConcessao = UtilidadesDePersistencia.getValorDate(leitor, "DATADECONCESSAO");
            processo.DataDoExame = UtilidadesDePersistencia.getValorDate(leitor, "DATADEEXAME");
            processo.ProcessoEhDeTerceiro = UtilidadesDePersistencia.GetValorBooleano(leitor, "PROCESSODETERCEIRO");
            processo.Procurador = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IProcuradorLazyLoad>(
                    UtilidadesDePersistencia.GetValorLong(leitor, "IDPROCURADOR"));
            processo.ProcessoEhEstrangeiro = UtilidadesDePersistencia.GetValorBooleano(leitor, "EHESTRANGEIRO");

            if (!UtilidadesDePersistencia.EhNulo(leitor, "NUMEROPCT") ||
                !UtilidadesDePersistencia.EhNulo(leitor, "NUMEROWO") ||
                !UtilidadesDePersistencia.EhNulo(leitor, "DATAPUBLICACAOPCT") ||
                !UtilidadesDePersistencia.EhNulo(leitor, "DATADEPOSITOPCT"))
            {
                var pct = FabricaGenerica.GetInstancia().CrieObjeto<IPCT>();

                if (!UtilidadesDePersistencia.EhNulo(leitor, "NUMEROPCT"))
                    pct.Numero = UtilidadesDePersistencia.GetValorString(leitor, "NUMEROPCT");

                if (!UtilidadesDePersistencia.EhNulo(leitor, "NUMEROWO"))
                    pct.Numero = UtilidadesDePersistencia.GetValorString(leitor, "NUMEROWO");

                pct.DataDaPublicacao = UtilidadesDePersistencia.getValorDate(leitor, "DATAPUBLICACAOPCT");
                pct.DataDoDeposito = UtilidadesDePersistencia.getValorDate(leitor, "DATADEPOSITOPCT");
            }

            if (!UtilidadesDePersistencia.EhNulo(leitor,"IDDESPACHO"))
                processo.Despacho = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IDespachoDePatentesLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "IDDESPACHO"));

            if (!UtilidadesDePersistencia.EhNulo(leitor,"IDPASTA"))
            {
                var pasta = FabricaGenerica.GetInstancia().CrieObjeto<IPasta>();
                pasta.ID = UtilidadesDePersistencia.GetValorLong(leitor, "IDPASTA");
                pasta.Nome = UtilidadesDePersistencia.GetValorString(leitor, "NOMEPASTA");
                processo.Pasta = pasta;
            }
            
            processo.Ativo = UtilidadesDePersistencia.GetValorBooleano(leitor, "ATIVO");
            
            return processo;
        }

        public IProcessoDePatente Obtenha(long ID)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatentePorID>();
            filtro.Operacao = OperacaoDeFiltro.IgualA;
            filtro.ValorDoFiltro = ID.ToString();

            var processos = new List<IProcessoDePatente>();

            using (var leitor = DBHelper.obtenhaReader(filtro.ObtenhaQuery()))
            {
                try
                {
                    if (leitor.Read())
                        return MontaProcessoDePatente(leitor);

                }
                finally
                {
                    leitor.Close();
                }
            }

            return null;
        }

        public int ObtenhaQuantidadeDeProcessosCadastrados(IFiltro filtro)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            using (var leitor = DBHelper.obtenhaReader(filtro.ObtenhaQueryParaQuantidade()))
            {
                try
                {
                    if (leitor.Read())
                        return UtilidadesDePersistencia.getValorInteger(leitor, "QUANTIDADE");
                }
                finally
                {
                    leitor.Close();
                }
            }

            return 0;
        }


        public IList<long> ObtenhaTodosNumerosDeProcessosCadastrados()
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            IList<long> listaDeNumerosDosProcessos = new List<long>();

            using (var leitor = DBHelper.obtenhaReader("SELECT PROCESSO FROM MP_PROCESSOPATENTE ORDER BY PROCESSO"))
                try
                {
                    while (leitor.Read())
                        listaDeNumerosDosProcessos.Add(UtilidadesDePersistencia.GetValorLong(leitor, "PROCESSO"));
                }
                finally
                {
                    leitor.Close();
                }

            return listaDeNumerosDosProcessos;
        }

        public DateTime? ObtenhaDataDepositoDoProcessoVinvuladoAPatente(long idPatente)
        {
            IDBHelper DBHelper = ServerUtils.criarNovoDbHelper();

            using (var leitor = DBHelper.obtenhaReader("SELECT DATADEDEPOSITO FROM MP_PROCESSOPATENTE WHERE IDPATENTE = " + idPatente))
                try
                {
                    while (leitor.Read())
                    {
                        string dataDeposito = UtilidadesDePersistencia.getValorInteger(leitor, "DATADEDEPOSITO").ToString();
                        return new DateTime(int.Parse(dataDeposito.Substring(0, 4)), int.Parse(dataDeposito.Substring(4, 2)), int.Parse(dataDeposito.Substring(6, 2)));
                    }
                        
                }
                finally
                {
                    leitor.Close();
                }

            return null;
        }
    }
}
