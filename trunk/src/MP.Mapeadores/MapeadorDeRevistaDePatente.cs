using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeRevistaDePatente : IMapeadorDeRevistaDePatente
    {
        public void InserirDadosRevistaXml(IList<IRevistaDePatente> listaDeProcessosExistentesNaRevista)
        {
        }

        public void Modificar(IRevistaDePatente revistaDePatentes)
        {
        }

        public IList<IRevistaDePatente> ObtenhaRevistasAProcessar(int quantidadeDeRegistros)
        {
            return new List<IRevistaDePatente>();
        }

        public IList<IRevistaDePatente> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros)
        {
            return new List<IRevistaDePatente>();
        }
    }
}
