using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.FN.Negocio;

namespace FN.Interfaces.Mapeadores
{
    public interface IMapeadorConfiguracaoDeIndicesFinanceiros
    {
        IConfiguracaoDeIndicesFinanceiros ObtenhaConfiguracao();
        void Salve(IConfiguracaoDeIndicesFinanceiros configuracao);
    }
}
