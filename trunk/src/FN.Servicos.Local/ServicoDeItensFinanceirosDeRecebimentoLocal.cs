using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.FN.Mapeadores;
using Compartilhados.Interfaces.FN.Negocio;
using Compartilhados.Interfaces.FN.Servicos;

namespace FN.Servicos.Local
{
    public class ServicoDeItensFinanceirosDeRecebimentoLocal : Servico, IServicoDeItensFinanceirosDeRecebimento
    {
        public ServicoDeItensFinanceirosDeRecebimentoLocal(ICredencial Credencial)
            : base(Credencial)
        {
        }

        public void Insira(IItemLancamentoFinanceiroRecebimento Item)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItensFinanceirosDeRecebimento>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Insira(Item);
                ServerUtils.CommitTransaction();
            }
            catch
            {
                ServerUtils.RollbackTransaction();
                throw;
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Modifique(IItemLancamentoFinanceiroRecebimento Item)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItensFinanceirosDeRecebimento>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Modifique(Item);
                GerenciadorDeGatilhos.GetInstancia().DispareGatilhoDepois(GetType().FullName, "Modifique", new object[] { Item }); 
                ServerUtils.CommitTransaction();
            }
            catch
            {
                ServerUtils.RollbackTransaction();
                throw;
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public int ObtenhaQuantidadeDeItensFinanceiros(IFiltro filtro)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItensFinanceirosDeRecebimento>();

            try
            {
                return mapeador.ObtenhaQuantidadeDeItensFinanceiros(filtro);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IItemLancamentoFinanceiroRecebimento> ObtenhaItensFinanceiros(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItensFinanceirosDeRecebimento>();

            try
            {
                return mapeador.ObtenhaItensFinanceiros(filtro, quantidadeDeRegistros, offSet);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IItemLancamentoFinanceiroRecebimento Obtenha(long ID)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItensFinanceirosDeRecebimento>();

            try
            {
                return mapeador.Obtenha(ID);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
