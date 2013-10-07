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
    public class ServicoDeSituacaoDoProcessoLocal : Servico, IServicoDeSituacaoDoProcesso
    {
        public ServicoDeSituacaoDoProcessoLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public IList<ISituacaoDoProcesso> obtenhaTodasSituacoesDoProcesso()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeSituacaoDoProcesso>();

            try
            {
                return mapeador.obtenhaTodasSituacoesDoProcesso();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public ISituacaoDoProcesso obtenhaSituacaoDoProcessoPeloId(long idSituacaoDoProcesso)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeSituacaoDoProcesso>();

            try
            {
                return mapeador.obtenhaSituacaoDoProcessoPeloId(idSituacaoDoProcesso);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public ISituacaoDoProcesso obtenhaSituacaoDoProcessoPelaDescricao(string descricaoSituacaoDoProcesso)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeSituacaoDoProcesso>();

            try
            {
                return mapeador.obtenhaSituacaoDoProcessoPelaDescricao(descricaoSituacaoDoProcesso);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<ISituacaoDoProcesso> ObtenhaPorDescricaoComoFiltro(string descricao, int quantidadeMaximaDeRegistros)
        {
            throw new NotImplementedException();
        }

        public void Inserir(ISituacaoDoProcesso situacaoDoProcesso)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeSituacaoDoProcesso>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Inserir(situacaoDoProcesso);
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

        public void Modificar(ISituacaoDoProcesso situacaoDoProcesso)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeSituacaoDoProcesso>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Modificar(situacaoDoProcesso);
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

        public void Excluir(long idSituacaoDoProcesso)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeSituacaoDoProcesso>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(idSituacaoDoProcesso);
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
