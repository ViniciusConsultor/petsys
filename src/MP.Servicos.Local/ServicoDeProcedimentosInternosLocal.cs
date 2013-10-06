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
    public class ServicoDeProcedimentosInternosLocal : Servico, IServicoDeProcedimentosInternos
    {
        public ServicoDeProcedimentosInternosLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public IList<IProcedimentosInternos> obtenhaTodosProcedimentosInternos()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcedimentosInternos>();

            try
            {
                return mapeador.obtenhaTodosProcedimentosInternos();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IProcedimentosInternos obtenhaProcedimentosInternosPeloId(long idProcedimentosInternos)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcedimentosInternos>();

            try
            {
                return mapeador.obtenhaProcedimentosInternosPeloId(idProcedimentosInternos);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IProcedimentosInternos obtenhaProcedimentosInternosPelaDescricao(string descricaoProcedimentosInternos)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcedimentosInternos>();

            try
            {
                return mapeador.obtenhaProcedimentosInternosPelaDescricao(descricaoProcedimentosInternos);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Inserir(IProcedimentosInternos procedimentosInternos)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcedimentosInternos>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Inserir(procedimentosInternos);
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

        public void Modificar(IProcedimentosInternos procedimentosInternos)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcedimentosInternos>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Modificar(procedimentosInternos);
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

        public void Excluir(long idProcedimentosInternos)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcedimentosInternos>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(idProcedimentosInternos);
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
