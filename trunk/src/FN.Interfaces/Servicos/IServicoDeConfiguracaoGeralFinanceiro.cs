using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FN.Interfaces.Negocio;

namespace FN.Interfaces.Servicos
{
    public interface IServicoDeConfiguracaoGeralFinanceiro : IDisposable
    {
        void Salve(IConfiguracaoGeralFinanceiro configuracaoGeral);
        IConfiguracaoGeralFinanceiro Obtenha();
    }
}
