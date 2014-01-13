using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeTitular : IMapeadorDeTitular
    {
        public void Inserir(ITitular inventor)
        {
        }

        public void Remover(long ID)
        {
        }

        public void Atualizar(ITitular inventor)
        {
        }

        public IList<ITitular> ObtenhaPorNomeComoFiltro(string nome, int quantidadeMaxima)
        {
            throw new NotImplementedException();
        }

        public ITitular Obtenha(long ID)
        {
            throw new NotImplementedException();
        }

        public ITitular Obtenha(IPessoa pessoa)
        {
            throw new NotImplementedException();
        }
    }
}
