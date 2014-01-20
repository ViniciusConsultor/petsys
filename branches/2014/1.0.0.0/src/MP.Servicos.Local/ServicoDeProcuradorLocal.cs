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
    public class ServicoDeProcuradorLocal : Servico, IServicoDeProcurador
    {
        public ServicoDeProcuradorLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public void Inserir(IProcurador procurador)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcurador>();

            try
            {
                mapeadorProcurador.Inserir(procurador);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Remover(long idProcurador)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcurador>();

            try
            {
                mapeadorProcurador.Remover(idProcurador);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Atualizar(IProcurador procurador)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcurador>();

            try
            {
                mapeadorProcurador.Atualizar(procurador);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IProcurador ObtenhaProcurador(IPessoa pessoa)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcurador>();

            try
            {
                return mapeadorProcurador.ObtenhaProcurador(pessoa);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }


        public IList<IProcurador> ObtenhaProcuradorPeloNome(string nomeDoProcurador, int quantidadeMaximaDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcurador>();

            try
            {
                return mapeadorProcurador.ObtenhaProcuradorPeloNome(nomeDoProcurador, quantidadeMaximaDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }


        public IProcurador ObtenhaProcurador(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcurador>();

            try
            {
                return mapeadorProcurador.ObtenhaProcurador(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }
    }
}
