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
    public class ServicoDeConfiguracoesDoModuloLocal :  Servico,  IServicoDeConfiguracoesDoModulo
    {
        public ServicoDeConfiguracoesDoModuloLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public IConfiguracaoDeModulo ObtenhaConfiguracao()
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeConfiguracoesDoModulo>();

            try
            {
                return mapeador.ObtenhaConfiguracao();
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public void Salve(IConfiguracaoDeModulo configuracao)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeConfiguracoesDoModulo>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Salve(configuracao);
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
