using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeRevistaDePatente
    {
        void InserirDadosRevistaXml(IList<IRevistaDePatente> listaDeProcessosExistentesNaRevista);
        IList<IRevistaDePatente> ObtenhaRevistasAProcessar(int quantidadeDeRegistros);
        IList<IRevistaDePatente> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros);
        void Excluir(int numeroDaRevistaDePatente);
        IList<IRevistaDePatente> ObtenhaRevistasProcessadas(int numeroDaRevistaDePatente);
    }
}
