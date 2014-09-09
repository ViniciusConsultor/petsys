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
    public class ServicoDeDespachoDePatentesLocal : Servico, IServicoDeDespachoDePatentes
    {
        public ServicoDeDespachoDePatentesLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public IDespachoDePatentes obtenhaDespachoDePatentesPeloId(long idDespachoDePatentes)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeDespachoDePatentes>();

            try
            {
                return mapeador.obtenhaDespachoDePatentesPeloId(idDespachoDePatentes);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IDespachoDePatentes> ObtenhaPorDescricao(string descricaoParcial, int quantidadeMaximaDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeDespachoDePatentes>();

            try
            {
                return mapeador.ObtenhaPorDescricao(descricaoParcial, quantidadeMaximaDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Inserir(IDespachoDePatentes despachoDePatentes)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeDespachoDePatentes>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Inserir(despachoDePatentes);
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

        public void Modificar(IDespachoDePatentes despachoDePatentes)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeDespachoDePatentes>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Modificar(despachoDePatentes);
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

        public void Excluir(long idDespachoDePatentes)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeDespachoDePatentes>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(idDespachoDePatentes);
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


        public IDespachoDePatentes ObtenhaDespachoPeloCodigo(string codigo)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeDespachoDePatentes>();

            try
            {
                return mapeador.ObtenhaDespachoPeloCodigo(codigo);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
