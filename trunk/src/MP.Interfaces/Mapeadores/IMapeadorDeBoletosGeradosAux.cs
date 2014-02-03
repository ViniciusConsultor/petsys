using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeBoletosGeradosAux
    {
        IBoletosGeradosAux obtenhaProximasInformacoesParaGeracaoDoBoleto();

        void AtualizarProximasInformacoes(IBoletosGeradosAux dadosAuxBoleto);

        void InserirPrimeiraVez(IBoletosGeradosAux dadosAuxBoleto);
    }
}
