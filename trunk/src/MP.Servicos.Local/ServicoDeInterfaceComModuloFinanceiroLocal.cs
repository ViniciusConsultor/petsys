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
using MP.Interfaces.Negocio.Filtros.Marcas;
using MP.Interfaces.Negocio.Filtros.Patentes;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    public class ServicoDeInterfaceComModuloFinanceiroLocal : Servico, IServicoDeInterfaceComModuloFinanceiro
    {
        public ServicoDeInterfaceComModuloFinanceiroLocal(ICredencial Credencial)
            : base(Credencial)
        {
        }

        private IItemLancamentoFinanceiroRecebimento CrieItemLancamento(IProcessoDeMarca processoDemarca)
        {
            var marca = processoDemarca.Marca;

            var itemLacamentoFinanceiro =
               FabricaGenerica.GetInstancia().CrieObjeto<IItemLancamentoFinanceiroRecebimento>();
            itemLacamentoFinanceiro.Cliente = marca.Cliente;
            itemLacamentoFinanceiro.DataDoLancamento = DateTime.Now;
            itemLacamentoFinanceiro.DataDoVencimento = marca.Manutencao.DataDaProximaManutencao.Value;
            itemLacamentoFinanceiro.Situacao = Situacao.Aberta;
            itemLacamentoFinanceiro.TipoLacamento = TipoLacamentoFinanceiroRecebimento.RecebimentoDeManutencao;
            itemLacamentoFinanceiro.Valor = marca.Manutencao.ObtenhaValorRealEmEspecie();
            itemLacamentoFinanceiro.Descricao = marca.DescricaoDaMarca;

            return itemLacamentoFinanceiro;
        }

        private IItemLancamentoFinanceiroRecebimento CrieItemLancamento(IProcessoDePatente processoDePatente)
        {
            var patente = processoDePatente.Patente;

            var itemLacamentoFinanceiro =
               FabricaGenerica.GetInstancia().CrieObjeto<IItemLancamentoFinanceiroRecebimento>();
            itemLacamentoFinanceiro.Cliente = patente.Clientes[0];
            itemLacamentoFinanceiro.DataDoLancamento = DateTime.Now;
            itemLacamentoFinanceiro.DataDoVencimento = patente.Manutencao.DataDaProximaManutencao.Value;
            itemLacamentoFinanceiro.Situacao = Situacao.Aberta;
            itemLacamentoFinanceiro.TipoLacamento = TipoLacamentoFinanceiroRecebimento.RecebimentoDeManutencao;
            itemLacamentoFinanceiro.Valor = patente.Manutencao.ObtenhaValorRealEmEspecie();
            itemLacamentoFinanceiro.Descricao = "Patente " + processoDePatente.NumeroDoProcessoFormatado;

            return itemLacamentoFinanceiro;
        }

        public void ProcureEAgendeItemDeRecebimentoDeMarcasVencidasNoMes()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeInterfaceComModuloFinanceiro>();
            var mapeadorItemDeLancamento =
                FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItensFinanceirosDeRecebimento>();
            var mapeadorDeProcessoDeMarca = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaVencidaNoMes>();
            var marcasVencidas = mapeadorDeProcessoDeMarca.ObtenhaProcessosDeMarcas(filtro, int.MaxValue, 0);
            
            try
            {
                foreach (var marcaVencida in marcasVencidas)
                {
                    ServerUtils.BeginTransaction();
                    var itemDeLancamento = CrieItemLancamento(marcaVencida);
                    mapeadorItemDeLancamento.Insira(itemDeLancamento);
                    mapeador.Insira(itemDeLancamento.ID.Value, "MARCA", marcaVencida.IdProcessoDeMarca.Value, itemDeLancamento.DataDoVencimento);
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
            var mapeadorItemDeLancamento = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItensFinanceirosDeRecebimento>();
            var mapeadorDeProcessoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();
            
            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatenteVencidaNoMes>();
            var patentesVencidas = mapeadorDeProcessoDePatente.ObtenhaProcessosDePatentes(filtro, int.MaxValue, 0);

            try
            {
                foreach (var patenteVencida in patentesVencidas)
                {
                    ServerUtils.BeginTransaction();
                    var itemDeLancamento = CrieItemLancamento(patenteVencida);
                    mapeadorItemDeLancamento.Insira(itemDeLancamento);
                    mapeador.Insira(itemDeLancamento.ID.Value, "PATENTE", patenteVencida.IdProcessoDePatente.Value, itemDeLancamento.DataDoVencimento);
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
