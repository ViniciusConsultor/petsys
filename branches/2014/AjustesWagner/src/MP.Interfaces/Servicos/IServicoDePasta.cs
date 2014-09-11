using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDePasta : IServico
    {
        IPasta obtenha(long id);
        IList<IPasta> obtenhaPeloCodigo(string codigo, int quantidadeDeItens);
        void Inserir(IPasta pasta);
        void Modificar(IPasta pasta);
        void Excluir(long id);
    }
}
