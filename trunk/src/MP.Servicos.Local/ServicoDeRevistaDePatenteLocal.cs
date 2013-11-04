using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    public class ServicoDeRevistaDePatenteLocal : Servico, IServicoDeRevistaDePatente
    {
        public ServicoDeRevistaDePatenteLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public void Inserir(IList<IRevistaDePatente> listaDeObjetoRevistaDeMarcas)
        {
        }

        public void Modificar(IRevistaDePatente revistaDeMarcas)
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
