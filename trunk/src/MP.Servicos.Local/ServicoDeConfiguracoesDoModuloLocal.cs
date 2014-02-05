using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
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
            return null;
        }

        public void Salve(IConfiguracaoDeModulo configuracao)
        {
            
        }
    }
}
