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
    public class ServicoDeTipoDeProcedimentoInternoLocal : Servico, IServicoDeTipoDeProcedimentoInterno
    {
        public ServicoDeTipoDeProcedimentoInternoLocal(ICredencial Credencial)
            : base(Credencial)
        {
        }

        public IList<ITipoDeProcedimentoInterno> obtenhaTodosTiposDeProcedimentoInterno()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeTipoDeProcedimentoInterno>();

            try
            {
                return mapeador.obtenhaTodosTiposDeProcedimentoInterno();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public ITipoDeProcedimentoInterno obtenhaTipoProcedimentoInternoPeloId(long idTipoProcedimentosInternos)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeTipoDeProcedimentoInterno>();

            try
            {
                return mapeador.obtenhaTipoProcedimentoInternoPeloId(idTipoProcedimentosInternos);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public ITipoDeProcedimentoInterno obtenhaTipoProcedimentoInternoPelaDescricao(string descricao)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeTipoDeProcedimentoInterno>();

            try
            {
                return mapeador.obtenhaTipoProcedimentoInternoPelaDescricao(descricao);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Inserir(ITipoDeProcedimentoInterno procedimentosInternos)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeTipoDeProcedimentoInterno>();

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

        public void Modificar(ITipoDeProcedimentoInterno procedimentosInternos)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeTipoDeProcedimentoInterno>();

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

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeTipoDeProcedimentoInterno>();

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
