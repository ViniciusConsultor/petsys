using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using FN.Interfaces.Mapeadores;
using FN.Interfaces.Negocio;
using FN.Interfaces.Servicos;

namespace FN.Servicos.Local
{
    public class ServicoDeConfiguracaoGeralFinanceiroLocal : Servico, IServicoDeConfiguracaoGeralFinanceiro
    {
        public ServicoDeConfiguracaoGeralFinanceiroLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public void Salve(IConfiguracaoGeralFinanceiro configuracaoGeral)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeConfiguracaoGeralFinanceiro>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Salve(configuracaoGeral);
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

        public IConfiguracaoGeralFinanceiro Obtenha()
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeConfiguracaoGeralFinanceiro>();

            try
            {
                return mapeador.Obtenha();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
