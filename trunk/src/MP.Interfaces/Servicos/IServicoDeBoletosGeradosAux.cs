using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeBoletosGeradosAux : IServico
    {
        IBoletosGeradosAux obtenhaProximasInformacoesParaGeracaoDoBoleto();

        void AtualizarProximasInformacoes(IBoletosGeradosAux dadosAuxBoleto);

        void InserirPrimeiraVez(IBoletosGeradosAux dadosAuxBoleto);
    }
}
