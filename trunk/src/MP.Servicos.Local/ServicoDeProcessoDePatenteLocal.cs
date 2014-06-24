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

            var mapeadorPatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();
            var mapeadorDeProcessoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeadorPatente.Insira(processoDePatente.Patente);
                mapeadorDeProcessoDePatente.Inserir(processoDePatente);
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

        private void VerifiqueSeDespachoDesativaProcesso(IProcessoDePatente processo)
        {
            if (processo.Despacho == null) return;

            if (processo.Despacho.DesativaProcesso)
                processo.Ativo = false;
        }

        public void Modificar(IProcessoDePatente processoDePatente)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeadorPatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();
            var mapeadorDeProcessoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                VerifiqueSeDespachoDesativaProcesso(processoDePatente);
                mapeadorPatente.Modificar(processoDePatente.Patente);
                mapeadorDeProcessoDePatente.Modificar(processoDePatente);
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

        public void AtualizeProcessoAposLeituraDaRevista(IProcessoDePatente processoDePatente)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeadorDeProcessoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                VerifiqueSeDespachoDesativaProcesso(processoDePatente);
                mapeadorDeProcessoDePatente.Modificar(processoDePatente);
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

        public void Excluir(IProcessoDePatente processoDePatente)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeadorPatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();
            var mapeadorDeProcessoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeadorPatente.Exluir(processoDePatente.Patente.Identificador);
                mapeadorDeProcessoDePatente.Excluir(processoDePatente.IdProcessoDePatente.Value);
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

        public IList<IProcessoDePatente> ObtenhaTodosProcessosCadastrados()
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();

            try
            {
                return mapeador.ObtenhaTodosProcessosCadastrados();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
