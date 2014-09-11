using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeTitular : IServico
    {
        void Inserir(ITitular titular);
        void Remover(long ID);
        void Atualizar(ITitular titular);
        IList<ITitular> ObtenhaPorNomeComoFiltro(string nome, int quantidadeMaxima);
        ITitular Obtenha(long ID);
        ITitular Obtenha(IPessoa pessoa);
    }
}
