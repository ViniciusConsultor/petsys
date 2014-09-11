using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    public class ServicoDeTitularLocal : Servico, IServicoDeTitular
    {
        public ServicoDeTitularLocal(ICredencial credencial) : base(credencial) { }

        public void Inserir(ITitular titular)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeTitular>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Inserir(titular);
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

        public void Remover(long ID)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeTitular>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Remover(ID);
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

        public void Atualizar(ITitular titular)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeTitular>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Atualizar(titular);
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

        public IList<ITitular> ObtenhaPorNomeComoFiltro(string nome, int quantidadeMaxima)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeTitular>();

            try
            {
                return mapeador.ObtenhaPorNomeComoFiltro(nome, quantidadeMaxima);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public ITitular Obtenha(long ID)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeTitular>();

            try
            {
                return mapeador.Obtenha(ID);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public ITitular Obtenha(IPessoa pessoa)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeTitular>();

            try
            {
                return mapeador.Obtenha(pessoa);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
