using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    public class ServicoDePastaLocal : Servico, IServicoDePasta
    {
        public ServicoDePastaLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public IPasta obtenha(long id)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePasta>();

            try
            {
                return mapeador.obtenha(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IPasta> obtenhaPeloNome(string nome, int quantidadeDeItens)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePasta>();

            try
            {
                return mapeador.obtenhaPeloNome(nome, quantidadeDeItens);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Inserir(IPasta pasta)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePasta>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Inserir(pasta);
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

        public void Modificar(IPasta pasta)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePasta>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Modificar(pasta);
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

        public void Excluir(long id)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePasta>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(id);
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
