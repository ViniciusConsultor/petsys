using System;
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
            sql.Append("IDPROCESSO, IDMARCA, PROTOCOLO, PROCESSO,");
            sql.Append("DATAENTRADA, DATACONCESSAO, PROCESSOEHTERCEIRO, IDDESPACHO,");
            sql.Append("IDPROCURADOR, SITUACAO)");
            sql.Append("VALUES (");
            sql.Append(String.Concat(processoDeMarca.IdProcessoDeMarca.Value, ", "));
            sql.Append(String.Concat(processoDeMarca.Marca.IdMarca.Value, ", "));

            sql.Append(processoDeMarca.Protocolo.HasValue
                           ? String.Concat(processoDeMarca.Protocolo.Value, ", ")
                           : "NULL, ");
            
            sql.Append(processoDeMarca.Processo.HasValue
                           ? String.Concat(processoDeMarca.Processo.Value, ", ")
                           : "NULL, ");

            sql.Append(String.Concat(processoDeMarca.DataDeEntrada.ToString("yyyyMMdd"),", "));

            sql.Append(processoDeMarca.DataDeConcessao.HasValue
                           ? String.Concat(processoDeMarca.DataDeConcessao.Value.ToString("yyyyMMdd"), ", ")
                           : "NULL, ");

            sql.Append(processoDeMarca.ProcessoEhDeTerceiro ? "1, " : "0, ");

            sql.Append(processoDeMarca.Despacho != null
                           ? String.Concat(processoDeMarca.Despacho.IdDespacho, ", ")
                           : "NULL, ");

            sql.Append(processoDeMarca.Procurador != null
                           ? String.Concat(processoDeMarca.Procurador.Pessoa.ID.Value, ", ")
                           : "NULL, ");


            sql.Append(processoDeMarca.SituacaoDoProcesso != null
               ? String.Concat(processoDeMarca.SituacaoDoProcesso.CodigoSituacaoProcesso, ")")
               : "NULL)");


            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(IProcessoDeMarca processoDeMarca)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_PROCESSOMARCA ");
            sql.Append("SET IDMARCA = " + processoDeMarca.Marca.IdMarca + ", ");

            
            sql.Append(processoDeMarca.Protocolo.HasValue
                           ? String.Concat("PROTOCOLO = ",processoDeMarca.Protocolo.Value, ", ")
                           : "PROTOCOLO = NULL, ");
            
            sql.Append(processoDeMarca.Processo.HasValue
                           ? String.Concat("PROCESSO = ",processoDeMarca.Processo.Value, ", ")
                           : "PROCESSO = NULL, ");

            sql.Append(String.Concat("DATAENTRADA = ",processoDeMarca.DataDeEntrada.ToString("yyyyMMdd"),", "));

            sql.Append(processoDeMarca.DataDeConcessao.HasValue
                           ? String.Concat("DATACONCESSAO = ",processoDeMarca.DataDeConcessao.Value.ToString("yyyyMMdd"), ", ")
                           : "DATACONCESSAO = NULL, ");

            sql.Append("PROCESSOEHTERCEIRO = " + (processoDeMarca.ProcessoEhDeTerceiro ? "1, " : "0, "));

            sql.Append(processoDeMarca.Despacho != null
                           ? String.Concat("IDDESPACHO = ",processoDeMarca.Despacho.IdDespacho, ", ")
                           : "IDDESPACHO = NULL, ");

            sql.Append(processoDeMarca.Procurador != null
                           ? String.Concat("IDPROCURADOR = ",processoDeMarca.Procurador.Pessoa.ID.Value, ", ")
                           : "IDPROCURADOR = NULL, ");


            sql.Append(processoDeMarca.SituacaoDoProcesso != null
               ? String.Concat("SITUACAO = ",processoDeMarca.SituacaoDoProcesso.CodigoSituacaoProcesso)
               : "SITUACAO = NULL");

            sql.Append(" WHERE IDPROCESSO = " + processoDeMarca.IdProcessoDeMarca);

            try
            {
                DBHelper.ExecuteNonQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
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

            sql.AppendLine(" ORDER BY IDPROCESSO");

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

            if (!UtilidadesDePersistencia.EhNulo(leitor, "PROTOCOLO"))
                processoDeMarca.Protocolo = UtilidadesDePersistencia.GetValorLong(leitor, "PROTOCOLO");

            if (!UtilidadesDePersistencia.EhNulo(leitor, "PROCESSO"))
                processoDeMarca.Processo = UtilidadesDePersistencia.GetValorLong(leitor, "PROCESSO");

            processoDeMarca.DataDeEntrada = UtilidadesDePersistencia.getValorDate(leitor, "DATAENTRADA").Value;
            
            if (!UtilidadesDePersistencia.EhNulo(leitor, "DATACONCESSAO"))
                processoDeMarca.DataDeConcessao = UtilidadesDePersistencia.getValorDate(leitor, "DATACONCESSAO");

            processoDeMarca.ProcessoEhDeTerceiro = UtilidadesDePersistencia.GetValorBooleano(leitor, "PROCESSOEHTERCEIRO");
            
            if (!UtilidadesDePersistencia.EhNulo(leitor, "SITUACAO"))
                processoDeMarca.SituacaoDoProcesso =
                    SituacaoDoProcesso.ObtenhaPorCodigo(UtilidadesDePersistencia.getValorInteger(leitor, "SITUACAO"));

            if (!UtilidadesDePersistencia.EhNulo(leitor, "IDPROCURADOR"))
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
    }
}
