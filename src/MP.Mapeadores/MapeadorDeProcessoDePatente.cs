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
            sql.Append("DATAENTRADA, PROCESSODETERCEIRO, ");
            sql.Append("IDPROCURADOR, EHESTRANGEIRO, ATIVO)");
            sql.Append("VALUES (");
            sql.Append(String.Concat(processoDePatente.IdProcessoDePatente.Value, ", "));
            sql.Append(String.Concat(processoDePatente.Patente.Identificador, ", "));
            sql.Append(String.Concat(processoDePatente.Processo, ", "));
            sql.Append(String.Concat(processoDePatente.DataDeEntrada.ToString("yyyyMMdd"), ", "));
            sql.Append(processoDePatente.ProcessoEhDeTerceiro ? "1, " : "0, ");
            sql.Append(String.Concat(processoDePatente.Procurador != null ? processoDePatente.Procurador.Pessoa.ID.Value.ToString() : "NULL" , ", "));
            sql.Append(processoDePatente.ProcessoEhDeTerceiro ? "1, " : "0, ");
            sql.Append(processoDePatente.Ativo ? "1)" : "0)");
            
            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(IProcessoDePatente processoDePatente)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_PROCESSOPATENTE ");
            sql.Append("SET IDPATENTE = " + processoDePatente.Patente.Identificador + ", ");
            sql.Append(String.Concat("PROCESSO = ", processoDePatente.Processo, ", "));
            sql.Append(String.Concat("DATAENTRADA = ", processoDePatente.DataDeEntrada.ToString("yyyyMMdd"), ", "));
            sql.Append("PROCESSODETERCEIRO = " + (processoDePatente.ProcessoEhDeTerceiro ? "1, " : "0, "));
            sql.Append(String.Concat("IDPROCURADOR = ", processoDePatente.Procurador != null ? processoDePatente.Procurador.Pessoa.ID.Value.ToString() : "NULL", ", "));
            sql.Append("EHESTRANGEIRO = " + (processoDePatente.ProcessoEhDeTerceiro ? "1, " : "0, "));
            sql.Append("ATIVO = " + (processoDePatente.ProcessoEhDeTerceiro ? "1" : "0"));
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

            sql.AppendLine(" ORDER BY DATAENTRADA DESC");

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

            processo.DataDeEntrada = UtilidadesDePersistencia.getValorDate(leitor, "DATAENTRADA").Value;

            processo.ProcessoEhDeTerceiro = UtilidadesDePersistencia.GetValorBooleano(leitor, "PROCESSODETERCEIRO");

            processo.Procurador = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IProcuradorLazyLoad>(
                    UtilidadesDePersistencia.GetValorLong(leitor, "IDPROCURADOR"));

            processo.Ativo = UtilidadesDePersistencia.GetValorBooleano(leitor, "ATIVO");

            processo.ProcessoEhEstrangeiro = UtilidadesDePersistencia.GetValorBooleano(leitor, "EHESTRANGEIRO");
            
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
    }
}
