using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeProcessoDeMarca : IMapeadorDeProcessoDeMarca
    {
        public void Inserir(IProcessoDeMarca processoDeMarca)
        {
            throw new NotImplementedException();
        }

        public void Modificar(IProcessoDeMarca processoDeMarca)
        {
            throw new NotImplementedException();
        }

        public void Excluir(long ID)
        {
            throw new NotImplementedException();
        }

        public IList<IProcessoDeMarca> ObtenhaProcessosDeMarcas(int quantidadeDeRegistros, int offSet)
        {
            throw new NotImplementedException();
        }

        public IProcessoDeMarca Obtenha(long ID)
        {
            throw new NotImplementedException();
        }
    }
}
