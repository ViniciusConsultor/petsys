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
                VerifiqueSeDespachoDesativaProcesso(processoDeMarca);
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
                VerifiqueSeDespachoDesativaProcesso(processoDeMarca);
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

        private void VerifiqueSeDespachoDesativaProcesso(IProcessoDeMarca processo)
        {
            if (processo.Despacho == null) return;

            processo.Ativo = !processo.Despacho.DesativaProcesso;
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

        public IList<IProcessoDeMarca> ObtenhaProcessosDeMarcas(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            try
            {
                return mapeador.ObtenhaProcessosDeMarcas(filtro, quantidadeDeRegistros, offSet);
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

        public int ObtenhaQuantidadeDeProcessosCadastrados(IFiltro filtro)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            try
            {
                return mapeador.ObtenhaQuantidadeDeProcessosCadastrados(filtro);
            }

            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IProcessoDeMarca ObtenhaProcessoDeMarcaPeloNumero(long numeroDoProcesso)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            try
            {
                return mapeador.ObtenhaProcessoDeMarcaPeloNumero(numeroDoProcesso);
            }

            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<long> ObtenhaTodosNumerosDeProcessosCadastrados()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            try
            {
                return mapeador.ObtenhaTodosNumerosDeProcessosCadastrados();
            }

            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IProcessoDeMarca> ObtenhaProcessosDeMarcas(long? IDCliente, long? IDGrupoDeAtividade, IList<string> IDsDosDespachos)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            try
            {
                return mapeador.ObtenhaProcessosDeMarcas(IDCliente, IDGrupoDeAtividade, IDsDosDespachos);
            }

            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IProcessoDeMarca> obtenhaProcessosComMarcaQueContemRadicalDadastrado()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            try
            {
                return mapeador.obtenhaProcessosComMarcaQueContemRadicalDadastrado();
            }

            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IProcessoDeMarca> ObtenhaProcessoComRadicailAdicionadoNaMarca(IList<IProcessoDeMarca> processos)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            try
            {
                return mapeador.ObtenhaProcessoComRadicailAdicionadoNaMarca(processos);
            }

            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}