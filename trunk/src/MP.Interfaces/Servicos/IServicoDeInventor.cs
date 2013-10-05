using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeInventor : IServico
    {
        void Inserir(IInventor inventor);
        void Remover(long ID);
        void Atualizar(IInventor procurador);
        IList<IInventor> ObtenhaPorNomeComoFiltro(string nome, int quantidadeMaxima);
        IInventor Obtenha(long ID);
        IInventor Obtenha(IPessoa pessoa);
    }
}
