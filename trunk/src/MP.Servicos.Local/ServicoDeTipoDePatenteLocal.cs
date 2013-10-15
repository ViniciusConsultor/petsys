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
    public class ServicoDeTipoDePatenteLocal : Servico, IServicoDeTipoDePatente
    {
        public ServicoDeTipoDePatenteLocal(ICredencial credencial)
            : base(credencial)
        {
            
        }

        public IList<ITipoDePatente> obtenhaTipoDePatentePelaDescricaoComoFiltro(string descricao, int quantidadeMaximaDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorTipoDePatente>();

            try
            {
                return mapeador.obtenhaTipoDePatentePelaDescricaoComoFiltro(descricao,quantidadeMaximaDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public ITipoDePatente obtenhaTipoDePatentePeloId(long idTipoPatente)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorTipoDePatente>();

            try
            {
                return mapeador.obtenhaTipoDePatentePeloId(idTipoPatente);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public ITipoDePatente obtenhaTipoDePatentePelaDescricaoOuSigla(string descricaoTipoDePatente, string siglaTipo)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorTipoDePatente>();

            try
            {
                return mapeador.obtenhaTipoDePatentePelaDescricaoOuSigla(descricaoTipoDePatente, siglaTipo);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Inserir(ITipoDePatente tipoPatente)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorTipoDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Inserir(tipoPatente);
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

        public void Modificar(ITipoDePatente tipoPatente)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorTipoDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Modificar(tipoPatente);
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

        public void Excluir(long idTipoPatente)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorTipoDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(idTipoPatente);
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
