using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeRadicalMarcas
    {
        IList<IRadicalMarcas> obtenhaRadicalMarcasPelaDescricaoComoFiltro(string descricaoDoRadicalMarcas, int quantidadeMaximaDeRegistros);
        IRadicalMarcas obtenhaRadicalMarcasPeloId(long idRadicalMarcas);
        IList<IRadicalMarcas> obtenhaRadicalMarcasPeloIdDaMarcaComoFiltro(long idMarca, int quantidadeMaximaDeRegistros);
    }
}
