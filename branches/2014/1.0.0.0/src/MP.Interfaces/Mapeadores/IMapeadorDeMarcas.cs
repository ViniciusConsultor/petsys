using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeMarcas
    {
        IMarcas obtenhaMarcasPeloId(long idMarca);
        IList<IMarcas> obtenhaMarcasPelaDescricaoComoFiltro(string descricaoDaMarca, int quantidadeMaximaDeRegistros);
        void Inserir(IMarcas marca);
        void Modificar(IMarcas marca);
        void Excluir(long idMarca);
    }
}
