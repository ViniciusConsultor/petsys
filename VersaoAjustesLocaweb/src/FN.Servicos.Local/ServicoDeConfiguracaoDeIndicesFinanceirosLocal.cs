using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.FN.Negocio;
using Compartilhados.Interfaces.FN.Servicos;
using FN.Interfaces.Mapeadores;

namespace FN.Servicos.Local
{
    public class ServicoDeConfiguracaoDeIndicesFinanceirosLocal : Servico,  IServicoDeConfiguracaoDeIndicesFinanceiros
    {
        public ServicoDeConfiguracaoDeIndicesFinanceirosLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public void Salve(IConfiguracaoDeIndicesFinanceiros Configuracao)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorConfiguracaoDeIndicesFinanceiros>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Salve(Configuracao);
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

        public IConfiguracaoDeIndicesFinanceiros Obtenha()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorConfiguracaoDeIndicesFinanceiros>();

            try
            {
                return mapeador.ObtenhaConfiguracao();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
