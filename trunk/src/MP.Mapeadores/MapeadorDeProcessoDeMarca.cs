﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Negocio.LazyLoad;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;
using MP.Interfaces.Negocio.LazyLoad;

namespace MP.Mapeadores
{
    public class MapeadorDeProcessoDeMarca : IMapeadorDeProcessoDeMarca
    {
        public void Inserir(IProcessoDeMarca processoDeMarca)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            processoDeMarca.IdProcessoDeMarca = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_PROCESSOMARCA (");
            sql.Append("IDPROCESSO, IDMARCA, PROCESSO,");
            sql.Append("DATAENTRADA, DATACONCESSAO, PROCESSOEHTERCEIRO, IDDESPACHO,");
            sql.Append("IDPROCURADOR)");
            sql.Append("VALUES (");
            sql.Append(String.Concat(processoDeMarca.IdProcessoDeMarca.Value, ", "));
            sql.Append(String.Concat(processoDeMarca.Marca.IdMarca.Value, ", "));

            sql.Append(String.Concat(processoDeMarca.Processo, ", "));

            sql.Append(String.Concat(processoDeMarca.DataDeEntrada.ToString("yyyyMMdd"),", "));

            sql.Append(processoDeMarca.DataDeConcessao.HasValue
                           ? String.Concat(processoDeMarca.DataDeConcessao.Value.ToString("yyyyMMdd"), ", ")
                           : "NULL, ");

            sql.Append(processoDeMarca.ProcessoEhDeTerceiro ? "1, " : "0, ");

            sql.Append(processoDeMarca.Despacho != null
                           ? String.Concat(processoDeMarca.Despacho.IdDespacho, ", ")
                           : "NULL, ");

            sql.Append(String.Concat(processoDeMarca.Procurador != null ? processoDeMarca.Procurador.Pessoa.ID.Value.ToString() + ")" : "NULL)"));
            
            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(IProcessoDeMarca processoDeMarca)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_PROCESSOMARCA ");
            sql.Append("SET IDMARCA = " + processoDeMarca.Marca.IdMarca + ", ");

            sql.Append(String.Concat("PROCESSO = ", processoDeMarca.Processo, ", "));
            
            sql.Append(String.Concat("DATAENTRADA = ",processoDeMarca.DataDeEntrada.ToString("yyyyMMdd"),", "));

            sql.Append(processoDeMarca.DataDeConcessao.HasValue
                           ? String.Concat("DATACONCESSAO = ",processoDeMarca.DataDeConcessao.Value.ToString("yyyyMMdd"), ", ")
                           : "DATACONCESSAO = NULL, ");

            sql.Append("PROCESSOEHTERCEIRO = " + (processoDeMarca.ProcessoEhDeTerceiro ? "1, " : "0, "));

            sql.Append(processoDeMarca.Despacho != null
                           ? String.Concat("IDDESPACHO = ",processoDeMarca.Despacho.IdDespacho, ", ")
                           : "IDDESPACHO = NULL, ");

            sql.Append(String.Concat("IDPROCURADOR = ", processoDeMarca.Procurador != null ? processoDeMarca.Procurador.Pessoa.ID.Value.ToString() : "NULL"));
            
            sql.Append(" WHERE IDPROCESSO = " + processoDeMarca.IdProcessoDeMarca);

            DBHelper.ExecuteNonQuery(sql.ToString());
            
        }

        public void Excluir(long ID)
        {
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();
            DBHelper.ExecuteNonQuery("DELETE FROM MP_PROCESSOMARCA WHERE IDPROCESSO=" + ID.ToString());
        }

        public IList<IProcessoDeMarca> ObtenhaProcessosDeMarcas(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append(filtro.ObtenhaQuery());

            sql.AppendLine(" ORDER BY DATAENTRADA DESC");

            var processos = new List<IProcessoDeMarca>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(),quantidadeDeRegistros,offSet))
            {
                try
                {
                    while (leitor.Read())
                    {
                        processos.Add(MontaProcessoDeMarca(leitor));
                    }
                }
                finally
                {
                    leitor.Close();
                }
            }

            return processos;
        }


        private IProcessoDeMarca MontaProcessoDeMarca(IDataReader leitor)
        {
            var processoDeMarca = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDeMarca>();

            processoDeMarca.Marca = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IMarcasLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "IDMARCA"));

            processoDeMarca.IdProcessoDeMarca = UtilidadesDePersistencia.GetValorLong(leitor, "IDPROCESSO");
            
            processoDeMarca.Processo = UtilidadesDePersistencia.GetValorLong(leitor, "PROCESSO");

            processoDeMarca.DataDeEntrada = UtilidadesDePersistencia.getValorDate(leitor, "DATAENTRADA").Value;
            
            if (!UtilidadesDePersistencia.EhNulo(leitor, "DATACONCESSAO"))
                processoDeMarca.DataDeConcessao = UtilidadesDePersistencia.getValorDate(leitor, "DATACONCESSAO");

            processoDeMarca.ProcessoEhDeTerceiro = UtilidadesDePersistencia.GetValorBooleano(leitor, "PROCESSOEHTERCEIRO");
            
            processoDeMarca.Procurador =  FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IProcuradorLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "IDPROCURADOR"));

            if (!UtilidadesDePersistencia.EhNulo(leitor, "IDDESPACHO"))
                processoDeMarca.Despacho = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IDespachoDeMarcasLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "IDDESPACHO"));

            return processoDeMarca;
        }

        public IProcessoDeMarca Obtenha(long ID)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaPorID>();
            filtro.Operacao = OperacaoDeFiltro.IgualA;
            filtro.ValorDoFiltro = ID.ToString();
            
            var processos = new List<IProcessoDeMarca>();

            using (var leitor = DBHelper.obtenhaReader(filtro.ObtenhaQuery()))
            {
                try
                {
                    if (leitor.Read())
                        return MontaProcessoDeMarca(leitor);

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

        public IProcessoDeMarca ObtenhaProcessoDeMarcaPeloNumero(long numeroDoProcesso)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append("SELECT IDPROCESSO IdProcessoDeMarca, IDMARCA Marca, PROCESSO Processo, DATAENTRADA DataDeEntrada, ");
            sql.Append("DATACONCESSAO DataDeConcessao, PROCESSOEHTERCEIRO ProcessoEhDeTerceiro, IDDESPACHO Despacho, IDPROCURADOR Procurador ");
            sql.Append("FROM MP_PROCESSOMARCA ");
            sql.Append("WHERE PROCESSO = " + numeroDoProcesso);

            var processoDeMarca = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDeMarca>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                while (leitor.Read())
                {
                    processoDeMarca.IdProcessoDeMarca = UtilidadesDePersistencia.GetValorLong(leitor, "IdProcessoDeMarca");
                    processoDeMarca.Marca = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IMarcasLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "Marca"));
                    processoDeMarca.Processo = UtilidadesDePersistencia.GetValorLong(leitor, "Processo");
                    processoDeMarca.DataDeEntrada = UtilidadesDePersistencia.getValorDate(leitor, "DataDeEntrada").Value;

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "DataDeConcessao"))
                        processoDeMarca.DataDeConcessao = UtilidadesDePersistencia.getValorDate(leitor, "DataDeConcessao");

                    processoDeMarca.ProcessoEhDeTerceiro = UtilidadesDePersistencia.GetValorBooleano(leitor, "ProcessoEhDeTerceiro");

                    processoDeMarca.Procurador = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IProcuradorLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "Procurador"));

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "Despacho"))
                        processoDeMarca.Despacho = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IDespachoDeMarcasLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "Despacho"));
                }
            }

            return processoDeMarca;
        }
    }
}
