using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeRadicalMarcas
    {
        IList<IRadicalMarcas> ObtenhaPorIdDoRadicalMarcasComoFiltro(string idRadicalMarcas, int quantidadeMaximaDeRegistros);
        IList<IRadicalMarcas> obtenhaRadicalMarcasPelaDescricaoComoFiltro(string descricaoDoRadicalMarcas, int quantidadeMaximaDeRegistros);
        IRadicalMarcas obtenhaRadicalMarcasPeloId(long idRadicalMarcas);
        void Inserir(IRadicalMarcas radicalMarcas);
        void Modificar(IRadicalMarcas radicalMarcas);
        void Excluir(long idRadicalMarcas);
    }
}
