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
    public class ServicoDeBoletosGeradosLocal : Servico, IServicoDeBoletosGerados
    {
        public ServicoDeBoletosGeradosLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public IBoletosGerados obtenhaBoletoPeloId(long idBoleto)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoletosGerados>();

            try
            {
                return mapeador.obtenhaBoletoPeloId(idBoleto);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IBoletosGerados obtenhaBoletoPeloNumero(long numero)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoletosGerados>();

            try
            {
                return mapeador.obtenhaBoletoPeloNumero(numero);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Inserir(IBoletosGerados boletoGerado)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoletosGerados>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Inserir(boletoGerado);
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

        public void Excluir(long idBoleto)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoletosGerados>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(idBoleto);
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
