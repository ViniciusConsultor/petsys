using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.FN.Mapeadores;
using Compartilhados.Interfaces.FN.Negocio;
using FN.Interfaces.Mapeadores;
using FN.Interfaces.Negocio;
using FN.Interfaces.Servicos;

namespace FN.Servicos.Local
{
    public class ServicoDeBoletoLocal : Servico, IServicoDeBoleto
    {
        public ServicoDeBoletoLocal(ICredencial Credencial)
            : base(Credencial)
        {
        }


        private void InserirPrimeiraVez(IBoletosGeradosAux dadosAuxBoleto)
        {
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();
            mapeador.InserirPrimeiraVez(dadosAuxBoleto);
        }
        public IBoletosGeradosAux obtenhaProximasInformacoesParaGeracaoDoBoleto(ICedente cedente)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                var proximasInformacoes = mapeador.obtenhaProximasInformacoesParaGeracaoDoBoleto(cedente.Pessoa.ID.Value);

                if (!proximasInformacoes.ID.HasValue)
                {
                    var boletosGeradosAux = FabricaGenerica.GetInstancia().CrieObjeto<IBoletosGeradosAux>();
                    boletosGeradosAux.ID = GeradorDeID.getInstancia().getProximoID();
                    // verificar se o codigo do banco é da caixa e acrescentar o 82 (somente para caixa)
                    boletosGeradosAux.ProximoNossoNumero = cedente.InicioNossoNumero > 0 ? Convert.ToInt64("82" + cedente.InicioNossoNumero) : 8210001001;
                    boletosGeradosAux.IDCEDENTE = cedente.Pessoa.ID;

                    InserirPrimeiraVez(boletosGeradosAux);

                    proximasInformacoes.ID = boletosGeradosAux.ID;
                    proximasInformacoes.ProximoNossoNumero = boletosGeradosAux.ProximoNossoNumero;
                    proximasInformacoes.IDCEDENTE = boletosGeradosAux.IDCEDENTE;

                }

                return proximasInformacoes;

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

        public void Inserir(IBoletosGerados boletoGerado, bool gerarItemFinanceiro, TipoLacamentoFinanceiroRecebimento tipoLacamento)
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
            itemLacamentoFinanceiro.DataDoVencimento = boletoGerado.DataVencimento.Value;
            itemLacamentoFinanceiro.Situacao = Situacao.CobrancaGerada;
            itemLacamentoFinanceiro.TipoLacamento = tipoLacamento;
            itemLacamentoFinanceiro.Valor = boletoGerado.Valor;
            itemLacamentoFinanceiro.NumeroBoletoGerado = boletoGerado.NossoNumero.Value.ToString();
            itemLacamentoFinanceiro.FormaDeRecebimento = FormaDeRecebimento.Boleto;

            if (!string.IsNullOrEmpty(boletoGerado.NumeroBoleto))
                itemLacamentoFinanceiro.Descricao = boletoGerado.NumeroBoleto;

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

        public int ObtenhaQuantidadeDeBoletos(IFiltro filtro)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                return mapeador.ObtenhaQuantidadeDeBoletos(filtro);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void AtualizarStatusDoBoletoGerado(long numeroDoBoletoGerado, string statusDoBoleto)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoleto>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.AtualizarStatusDoBoletoGerado(numeroDoBoletoGerado, statusDoBoleto);
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
