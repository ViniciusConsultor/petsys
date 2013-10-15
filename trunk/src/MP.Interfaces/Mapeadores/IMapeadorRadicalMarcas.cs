using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorRadicalMarcas
    {
        IList<IRadicalMarcas> ObtenhaPorIdDoRadicalMarcasComoFiltro(string idRadicalMarcas, int quantidadeMaximaDeRegistros);
        IList<IRadicalMarcas> obtenhaRadicalMarcasPelaDescricaoComoFiltro(string descricaoDoRadicalMarcas, int quantidadeMaximaDeRegistros);
        IRadicalMarcas obtenhaMarcasPeloId(long idRadicalMarcas);
        void Inserir(IRadicalMarcas radicalMarcas);
        void Modificar(IRadicalMarcas radicalMarcas);
        void Excluir(long idRadicalMarcas);
    }
}
