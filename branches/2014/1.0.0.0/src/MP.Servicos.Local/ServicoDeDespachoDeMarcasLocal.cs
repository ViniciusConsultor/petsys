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
    public class ServicoDeDespachoDeMarcasLocal : Servico, IServicoDeDespachoDeMarcas
    {
        public ServicoDeDespachoDeMarcasLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public IDespachoDeMarcas obtenhaDespachoDeMarcasPeloId(long idDespachoDeMarcas)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeDespachoDeMarcas>();

            try
            {
                return mapeador.obtenhaDespachoDeMarcasPeloId(idDespachoDeMarcas);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IDespachoDeMarcas> ObtenhaPorCodigoDoDespachoComoFiltro(string codigo, int quantidadeMaximaDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeDespachoDeMarcas>();

            try
            {
                return mapeador.ObtenhaPorCodigoDoDespachoComoFiltro(codigo, quantidadeMaximaDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IDespachoDeMarcas ObtenhaDespachoPorCodigo(string codigo)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeDespachoDeMarcas>();

            try
            {
                return mapeador.ObtenhaDespachoPorCodigo(codigo);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Inserir(IDespachoDeMarcas despachoDeMarcas)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeDespachoDeMarcas>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Inserir(despachoDeMarcas);
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

        public void Modificar(IDespachoDeMarcas despachoDeMarcas)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeDespachoDeMarcas>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Modificar(despachoDeMarcas);
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

        public void Excluir(long idDespachoDeMarcas)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeDespachoDeMarcas>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(idDespachoDeMarcas);
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
