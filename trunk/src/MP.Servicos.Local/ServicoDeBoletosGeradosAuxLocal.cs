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
    public class ServicoDeBoletosGeradosAuxLocal : Servico, IServicoDeBoletosGeradosAux
    {
        public ServicoDeBoletosGeradosAuxLocal(ICredencial Credencial) : base(Credencial)
        {
        }
        
        public IBoletosGeradosAux obtenhaProximasInformacoesParaGeracaoDoBoleto()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoletosGeradosAux>();

            try
            {
                return mapeador.obtenhaProximasInformacoesParaGeracaoDoBoleto();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void AtualizarProximasInformacoes(IBoletosGeradosAux dadosAuxBoleto)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoletosGeradosAux>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.AtualizarProximasInformacoes(dadosAuxBoleto);
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

        public void InserirPrimeiraVez(IBoletosGeradosAux dadosAuxBoleto)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeBoletosGeradosAux>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.InserirPrimeiraVez(dadosAuxBoleto);
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
