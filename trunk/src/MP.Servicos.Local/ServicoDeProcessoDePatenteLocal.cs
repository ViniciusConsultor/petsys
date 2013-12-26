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
    public class ServicoDeProcessoDePatenteLocal : Servico, IServicoDeProcessoDePatente
    {
        public ServicoDeProcessoDePatenteLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public void Inserir(IProcessoDePatente processoDePatente)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Inserir(processoDePatente);
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

        public void Modificar(IProcessoDePatente processoDePatente)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Modificar(processoDePatente);
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

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

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

        public IList<IProcessoDePatente> ObtenhaProcessosDePatentes(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                return mapeador.ObtenhaProcessosDePatentes(filtro,quantidadeDeRegistros, offSet);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IProcessoDePatente Obtenha(long ID)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                return mapeador.Obtenha(ID);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
         
        }

        public int ObtenhaQuantidadeDeProcessosCadastrados(IFiltro filtro)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                return mapeador.ObtenhaQuantidadeDeProcessosCadastrados(filtro);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void AtivaDesativaProcessoDePatente(long idProcessoDePatente, bool ativo)
        {
            throw new NotImplementedException();
        }

        public IList<string> ObtenhaTodosNumerosDeProcessosCadastrados()
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                return mapeador.ObtenhaTodosNumerosDeProcessosCadastrados();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public DateTime? ObtenhaDataDepositoDoProcessoVinvuladoAPatente(long idPatente)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                return mapeador.ObtenhaDataDepositoDoProcessoVinvuladoAPatente(idPatente);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }


        public IProcessoDePatente ObtenhaPeloNumeroDoProcesso(string numeroDoProcesso)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                return mapeador.ObtenhaPeloNumeroDoProcesso(numeroDoProcesso);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IProcessoDePatente> obtenhaProcessosComPatenteQueContemRadicalCadastrado()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                return mapeador.obtenhaProcessosComPatenteQueContemRadicalCadastrado();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
