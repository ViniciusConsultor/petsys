using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;

namespace PMP.Interfaces.Servicos
{
    public interface IServicoDeProcessoDeMarcaDeRevista : IServico
    {
        void ProcesseEmLote(string pastaDeArmazenamentoDasRevistas);
    }
}
