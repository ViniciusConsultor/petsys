using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeConfiguracoesDoModulo
    {
        IConfiguracaoDeModulo ObtenhaConfiguracao();
        void Salve(IConfiguracaoDeModulo configuracao);
    
    }
}
