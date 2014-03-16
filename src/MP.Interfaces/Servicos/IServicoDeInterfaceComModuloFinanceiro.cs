using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeInterfaceComModuloFinanceiro: IServico
    {
        void ProcureEAgendeItemDeRecebimentoDeMarcasVencidasNoMes();
        void ProcureEAgendeItemDeRecebimentoDePatentesVencidasNoMes();
    }
}
