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
    public class ServicoDeNaturezaPatenteLocal : Servico, IServicoDeNaturezaPatente
    {
        public ServicoDeNaturezaPatenteLocal(ICredencial credencial)
            : base(credencial)
        {
            
        }

        public IList<INaturezaPatente> obtenhaNaturezaPatentePelaDescricaoComoFiltro(string descricao, int quantidadeMaximaDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeNaturezaPatente>();

            try
            {
                return mapeador.obtenhaNaturezaPatentePelaDescricaoComoFiltro(descricao, quantidadeMaximaDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public INaturezaPatente obtenhaNaturezaPatentePeloId(long idNaturezaPatente)
        {
             ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeNaturezaPatente>();

            try
            {
                return mapeador.obtenhaNaturezaPatentePeloId(idNaturezaPatente);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public INaturezaPatente obtenhaNaturezaPatentePelaDescricaoOuSigla(string descricaoNaturezaPatente, string siglaNatureza)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeNaturezaPatente>();

            try
            {
                return mapeador.obtenhaNaturezaPatentePelaDescricaoOuSigla(descricaoNaturezaPatente, siglaNatureza);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Inserir(INaturezaPatente naturezaPatente)
        {
             ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeNaturezaPatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Inserir(naturezaPatente);
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

        public void Modificar(INaturezaPatente naturezaPatente)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeNaturezaPatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Modificar(naturezaPatente);
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

        public void Excluir(long idNaturezaPatente)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorTipoDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(idNaturezaPatente);
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
