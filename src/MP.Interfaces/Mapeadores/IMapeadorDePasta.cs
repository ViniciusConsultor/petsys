using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDePasta
    {
        IPasta obtenha(long id);
        IList<IPasta> obtenhaPeloNome(string nome, int quantidadeDeItens);
        void Inserir(IPasta pasta);
        void Modificar(IPasta pasta);
        void Excluir(long id);
    }
}
