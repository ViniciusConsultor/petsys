using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using FN.Interfaces.Mapeadores;
using FN.Interfaces.Negocio;
using FN.Interfaces.Servicos;

namespace FN.Servicos.Local
{
    public class ServicoDeItemFinanceiroRecebidoComBoletoLocal : Servico, IServicoDeItemFinanceiroRecebidoComBoleto
    {
        public ServicoDeItemFinanceiroRecebidoComBoletoLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public void Insira(long idItemFinanRecebimento, long idBoleto)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItemFinanceiroRecebidoComBoleto>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Insira(idItemFinanRecebimento, idBoleto);
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

        public long ObtenhaItemFinanRecebimentoPorIdBoleto(long idBoleto)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItemFinanceiroRecebidoComBoleto>();

            try
            {
                return mapeador.ObtenhaItemFinanRecebimentoPorIdBoleto(idBoleto);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public long ObtenhaBoletoPorIdItemFinanRecebimento(long idItemFinanRecebimento)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItemFinanceiroRecebidoComBoleto>();

            try
            {
                return mapeador.ObtenhaBoletoPorIdItemFinanRecebimento(idItemFinanRecebimento);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Excluir(long idItemFinanRecebimento)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeItemFinanceiroRecebidoComBoleto>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(idItemFinanRecebimento);
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
