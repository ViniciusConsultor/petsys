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
using FN.Interfaces.Mapeadores;
using FN.Interfaces.Negocio;
using FN.Interfaces.Servicos;

namespace FN.Servicos.Local
{
    public class ServicoDeBoletoLocal : Servico, IServicoDeBoleto
    {
        public ServicoDeBoletoLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public IBoletosGeradosAux obtenhaProximasInformacoesParaGeracaoDoBoleto()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                return mapeador.obtenhaProximasInformacoesParaGeracaoDoBoleto();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void AtualizarProximasInformacoes(IBoletosGeradosAux dadosAuxBoleto)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.AtualizarProximasInformacoes(dadosAuxBoleto);
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

        public void InserirPrimeiraVez(IBoletosGeradosAux dadosAuxBoleto)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.InserirPrimeiraVez(dadosAuxBoleto);
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

        public IBoletosGerados obtenhaBoletoPeloId(long idBoleto)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                return mapeador.obtenhaBoletoPeloId(idBoleto);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IBoletosGerados obtenhaBoletoPeloNossoNumero(long numero)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                return mapeador.obtenhaBoletoPeloNossoNumero(numero);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Inserir(IBoletosGerados boletoGerado, bool gerarItemFinanceiro)
        {
            ServerUtils.setCredencial(_Credencial);
          
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();
            var mapeadorItemFinanceiroRecebimento =
                FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItensFinanceirosDeRecebimento>();

            var mapeadorDeItemFinanceiroRecebimentoComBoleto =
                    FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItemFinanceiroRecebidoComBoleto>();

            var itemLacamentoFinanceiro =
                FabricaGenerica.GetInstancia().CrieObjeto<IItemLancamentoFinanceiroRecebimento>();
            itemLacamentoFinanceiro.Cliente = boletoGerado.Cliente;
            itemLacamentoFinanceiro.DataDoLancamento = DateTime.Now;
            itemLacamentoFinanceiro.DataDoVencimento =  boletoGerado.DataVencimento.Value;
            itemLacamentoFinanceiro.Situacao = Situacao.CobrancaEmAberto;
            itemLacamentoFinanceiro.TipoLacamento = TipoLacamentoFinanceiroRecebimento.BoletoAvulso;
            itemLacamentoFinanceiro.Valor = boletoGerado.Valor;
            //if (boletoGerado.ID != null) itemLacamentoFinanceiro.IDBOLETO = boletoGerado.ID.Value;

            try
            {
                ServerUtils.BeginTransaction();

                mapeador.Inserir(boletoGerado);

                if (gerarItemFinanceiro)
                {
                    mapeadorItemFinanceiroRecebimento.Insira(itemLacamentoFinanceiro);

                    mapeadorDeItemFinanceiroRecebimentoComBoleto.Insira(itemLacamentoFinanceiro.ID.Value, boletoGerado.ID.Value);
                }

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

        public void Excluir(long idBoleto)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(idBoleto);
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

        public IConfiguracaoDeBoletoBancario ObtenhaConfiguracao()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                return mapeador.ObtenhaConfiguracao();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void SalveConfiguracao(IConfiguracaoDeBoletoBancario configuracao)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.SalveConfiguracao(configuracao);
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

        public IList<IBoletosGerados> obtenhaBoletosGerados(int quantidadeDeRegistros, int offSet)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                return mapeador.obtenhaBoletosGerados(quantidadeDeRegistros, offSet);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IBoletosGerados> obtenhaBoletosGerados(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                return mapeador.obtenhaBoletosGerados(filtro, quantidadeDeRegistros, offSet);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void AtualizarBoletoGerado(IBoletosGerados boletoGerado)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.AtualizarBoletoGerado(boletoGerado);
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
    }
}
