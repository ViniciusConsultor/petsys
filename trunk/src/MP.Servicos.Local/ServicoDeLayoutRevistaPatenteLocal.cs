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
    public class ServicoDeLayoutRevistaPatenteLocal : Servico, IServicoDeLayoutRevistaPatente
    {
        public ServicoDeLayoutRevistaPatenteLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public void Inserir(ILayoutRevistaPatente layoutRevistaPatente)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorLayoutRevistaPatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorLayoutRevistaPatente>();

            try
            {
                mapeadorLayoutRevistaPatente.Inserir(layoutRevistaPatente);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Excluir(long codigo)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorLayoutRevistaPatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorLayoutRevistaPatente>();

            try
            {
                mapeadorLayoutRevistaPatente.Excluir(codigo);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Modificar(ILayoutRevistaPatente layoutRevistaPatente)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorLayoutRevistaPatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorLayoutRevistaPatente>();

            try
            {
                mapeadorLayoutRevistaPatente.Modificar(layoutRevistaPatente);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }


        public IList<ILayoutRevistaPatente> ObtenhaTodos()
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorLayoutRevistaPatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorLayoutRevistaPatente>();

            try
            {
                return mapeadorLayoutRevistaPatente.ObtenhaTodos();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }


        public IList<ILayoutRevistaPatente> SelecioneLayoutPeloNomeDoCampo(string nomeDoCampo, int quantidadeMaximaDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorLayoutRevistaPatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorLayoutRevistaPatente>();

            try
            {
                return mapeadorLayoutRevistaPatente.SelecioneLayoutPeloNomeDoCampo(nomeDoCampo, quantidadeMaximaDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }
    }
}
