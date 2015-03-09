using Compartilhados.Interfaces.Core.Negocio;
using PMP.Interfaces.Utilidades;

namespace PMP.Interfaces.Mapeadores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    
    public interface IMapeadorDeProcessoMarcaRevista
    {
        void GraveEmLote(IDictionary<int, IList<DTOProcessoMarcaRevista>> listaDeProcessoMarcaRevista);
        IList<DTOProcessoMarcaRevista> ObtenhaResultadoDaPesquisa(IFiltro filtro, int quantidadeDeRegistros, int offSet);
        int ObtenhaQuantidadeDeResultadoDaPesquisa(IFiltro filtro);
    }
}
