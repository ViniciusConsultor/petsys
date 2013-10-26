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
    public class ServicoDePatenteLocal : Servico, IServicoDePatente
    {
        public ServicoDePatenteLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public void Insira(IPatente patente)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeadorProcurador.Insira(patente);
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

        public void Modificar(IPatente patente)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeadorProcurador.Modificar(patente);
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

        public void Exluir(long codigoPatente)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeadorProcurador.Exluir(codigoPatente);
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

        public IAnuidadePatente ObtenhaAnuidade(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaAnuidade(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IClassificacaoPatente ObtenhaClassificacao(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaClassificacao(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IPrioridadeUnionistaPatente ObtenhaPrioridadeUnionista(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaPrioridadeUnionista(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IInventor ObtenhaInventor(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaInventor(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IPatente ObtenhaPatente(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaPatente(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }


        public IList<IPatente> ObtenhaPatentesPeloTitulo(string titulo, int quantidadeDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaPatentesPeloTitulo(titulo, quantidadeDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }
    }
}
