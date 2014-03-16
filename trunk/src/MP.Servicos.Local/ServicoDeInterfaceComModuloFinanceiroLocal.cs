using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.FN.Mapeadores;
using Compartilhados.Interfaces.FN.Negocio;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    public class ServicoDeInterfaceComModuloFinanceiroLocal : Servico, IServicoDeInterfaceComModuloFinanceiro
    {
        public ServicoDeInterfaceComModuloFinanceiroLocal(ICredencial Credencial)
            : base(Credencial)
        {
        }

        private IItemLancamentoFinanceiroRecebimento CrieItemLancamento(IMarcas marca)
        {
            var itemLacamentoFinanceiro =
               FabricaGenerica.GetInstancia().CrieObjeto<IItemLancamentoFinanceiroRecebimento>();
            itemLacamentoFinanceiro.Cliente = marca.Cliente;
            itemLacamentoFinanceiro.DataDoLancamento = DateTime.Now;
            itemLacamentoFinanceiro.DataDoVencimento = marca.Manutencao.DataDaProximaManutencao.Value;
            itemLacamentoFinanceiro.Situacao = Situacao.Aberta;
            itemLacamentoFinanceiro.TipoLacamento = TipoLacamentoFinanceiroRecebimento.RecebimentoDeAnuidade;
            itemLacamentoFinanceiro.Valor = marca.Manutencao.ObtenhaValorRealEmEspecie();

            return itemLacamentoFinanceiro;
        }

        private IItemLancamentoFinanceiroRecebimento CrieItemLancamento(IPatente patente)
        {
            var itemLacamentoFinanceiro =
               FabricaGenerica.GetInstancia().CrieObjeto<IItemLancamentoFinanceiroRecebimento>();
            itemLacamentoFinanceiro.Cliente = patente.Clientes[0];
            itemLacamentoFinanceiro.DataDoLancamento = DateTime.Now;
            itemLacamentoFinanceiro.DataDoVencimento = patente.Manutencao.DataDaProximaManutencao.Value;
            itemLacamentoFinanceiro.Situacao = Situacao.Aberta;
            itemLacamentoFinanceiro.TipoLacamento = TipoLacamentoFinanceiroRecebimento.RecebimentoDeAnuidade;
            itemLacamentoFinanceiro.Valor = patente.Manutencao.ObtenhaValorRealEmEspecie();

            return itemLacamentoFinanceiro;
        }

        public void ProcureEAgendeItemDeRecebimentoDeMarcasVencidasNoMes()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeInterfaceComModuloFinanceiro>();
            var mapeadorDeMarcas = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeMarcas>();
            var mapeadorItemDeLancamento =
                FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItensFinanceirosDeRecebimento>();

            var marcasComManutencaoVencida = mapeadorDeMarcas.ObtenhaMarcasComManutencaoVencendoEsteMes();

            try
            {
                foreach (var marcaVencida in marcasComManutencaoVencida)
                {
                    ServerUtils.BeginTransaction();
                    var itemDeLancamento = CrieItemLancamento(marcaVencida);
                    mapeadorItemDeLancamento.Insira(itemDeLancamento);
                    mapeador.Insira(itemDeLancamento.ID.Value, "MARCA", marcaVencida.IdMarca.Value, itemDeLancamento.DataDoVencimento);
                    ServerUtils.CommitTransaction();
                }

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

        public void ProcureEAgendeItemDeRecebimentoDePatentesVencidasNoMes()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeInterfaceComModuloFinanceiro>();
            var mapeadorDePatentes = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();
            var mapeadorItemDeLancamento =
                FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItensFinanceirosDeRecebimento>();

            var patentesVencidas = mapeadorDePatentes.ObtenhaPatentesComManutencaoVencendoEsteMes();

            try
            {
                foreach (var patenteVencida in patentesVencidas)
                {
                    ServerUtils.BeginTransaction();
                    var itemDeLancamento = CrieItemLancamento(patenteVencida);
                    mapeadorItemDeLancamento.Insira(itemDeLancamento);
                    mapeador.Insira(itemDeLancamento.ID.Value, "PATENTE", patenteVencida.Identificador, itemDeLancamento.DataDoVencimento);
                    ServerUtils.CommitTransaction();
                }

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
    }
}
