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
    public class ServicoDeProcessoDeMarcaLocal : Servico, IServicoDeProcessoDeMarca
    {
        public ServicoDeProcessoDeMarcaLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public void Inserir(IProcessoDeMarca processoDeMarca)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Inserir(processoDeMarca);
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

        public void Modificar(IProcessoDeMarca processoDeMarca)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Modificar(processoDeMarca);
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

        public void Excluir(long ID)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(ID);
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

        public IList<IProcessoDeMarca> ObtenhaProcessosDeMarcas(int quantidadeDeRegistros, int offSet)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            try
            {
                return mapeador.ObtenhaProcessosDeMarcas(quantidadeDeRegistros, offSet);
            }

            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IProcessoDeMarca Obtenha(long ID)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            try
            {
                return mapeador.Obtenha(ID);
            }
           
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
