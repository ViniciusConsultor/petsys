using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FN.Interfaces.Negocio;

namespace FN.Interfaces.Mapeadores
{
    public interface IMapeadorDeConfiguracaoGeralFinanceiro
    {
        void Salve(IConfiguracaoGeralFinanceiro configuracaoGeral);
        IConfiguracaoGeralFinanceiro Obtenha();
    }
}
