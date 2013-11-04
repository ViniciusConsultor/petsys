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
        void Modificar(IRevistaDePatente revistaDePatentes);
        IList<IRevistaDePatente> ObtenhaRevistasAProcessar(int quantidadeDeRegistros);
        IList<IRevistaDePatente> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros);
    }
}
