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
    public class ServicoDeProcurador : Servico, IServicoDeProcurador
    {
        public ServicoDeProcurador(ICredencial Credencial) : base(Credencial)
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

        public void Remover(IProcurador procurador)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcurador>();

            try
            {
                mapeadorProcurador.Remover(procurador);
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

        public List<IProcurador> ObtenhaTodosProcuradores()
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcurador>();

            try
            {
                return mapeadorProcurador.ObtenhaTodosProcuradores();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IProcurador ObtenhaProcurador(IProcurador procurador)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcurador>();

            try
            {
                return mapeadorProcurador.ObtenhaProcurador(procurador);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
