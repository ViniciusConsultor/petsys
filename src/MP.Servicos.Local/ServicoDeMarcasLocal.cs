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
    public class ServicoDeMarcasLocal : Servico, IServicoDeMarcas
    {
        public ServicoDeMarcasLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public IList<IMarcas> obtenhaTodasMarcasCadastradas()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeMarcas>();

            try
            {
                return mapeador.obtenhaTodasMarcasCadastradas();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IMarcas obtenhaMarcasPeloId(long idMarca)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeMarcas>();

            try
            {
                return mapeador.obtenhaMarcasPeloId(idMarca);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IMarcas> obtenhaMarcasPelaDescricaoComoFiltro(string descricaoDaMarca, int quantidadeMaximaDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeMarcas>();

            try
            {
                return mapeador.obtenhaMarcasPelaDescricaoComoFiltro(descricaoDaMarca, quantidadeMaximaDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IMarcas> ObtenhaPorIdDaMarcaComoFiltro(string idMarca, int quantidadeMaximaDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeMarcas>();

            try
            {
                return mapeador.ObtenhaPorIdDaMarcaComoFiltro(idMarca, quantidadeMaximaDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Inserir(IMarcas marca)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeMarcas>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Inserir(marca);
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

        public void Modificar(IMarcas marca)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeMarcas>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Modificar(marca);
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

        public void Excluir(long idMarca)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeMarcas>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(idMarca);
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
