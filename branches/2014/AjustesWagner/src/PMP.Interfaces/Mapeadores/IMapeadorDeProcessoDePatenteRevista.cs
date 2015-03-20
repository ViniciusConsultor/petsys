// -----------------------------------------------------------------------
// <copyright file="IMapeadorDeProcessoDePatenteRevista.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Compartilhados.Interfaces.Core.Negocio;
using PMP.Interfaces.Utilidades;

namespace PMP.Interfaces.Mapeadores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IMapeadorDeProcessoDePatenteRevista
    {
        void Grave(KeyValuePair<int, IList<DTOProcessoPatenteRevista>> listaDeProcessoPatenteRevista);
        IList<DTOProcessoPatenteRevista> ObtenhaResultadoDaPesquisa(IFiltro filtro, int quantidadeDeRegistros, int offSet);
        int ObtenhaQuantidadeDeResultadoDaPesquisa(IFiltro filtro);
    }
}
