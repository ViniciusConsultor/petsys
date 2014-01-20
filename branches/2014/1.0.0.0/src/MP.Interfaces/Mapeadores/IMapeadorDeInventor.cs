using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeInventor
    {
        void Inserir(IInventor inventor);
        void Remover(long ID);
        void Atualizar(IInventor inventor);
        IList<IInventor> ObtenhaPorNomeComoFiltro(string nome, int quantidadeMaxima);
        IInventor Obtenha(long ID);
        IInventor Obtenha(IPessoa pessoa);
    }
}
