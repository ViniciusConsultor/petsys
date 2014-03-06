using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.FN.Negocio;
using Compartilhados.Interfaces.FN.Servicos;
using FN.Interfaces.Mapeadores;

namespace FN.Servicos.Local
{
    public class ServicoDeItensFinanceirosDeRecebimentoLocal : Servico, IServicoDeItensFinanceirosDeRecebimento
    {
        public ServicoDeItensFinanceirosDeRecebimentoLocal(ICredencial Credencial) : base(Credencial)
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

        public int ObtenhaQuantidadeDeProcessosCadastrados(IFiltro filtro)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItensFinanceirosDeRecebimento>();

            try
            {
                return mapeador.ObtenhaQuantidadeDeProcessosCadastrados(filtro);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IItemLancamentoFinanceiroRecebimento> ObtenhaProcessosDeMarcas(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItensFinanceirosDeRecebimento>();

            try
            {
                return mapeador.ObtenhaProcessosDeMarcas(filtro, quantidadeDeRegistros, offSet);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
