// -----------------------------------------------------------------------
// <copyright file="IServicoDeProcessoDePatenteDeRevista.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Compartilhados.Interfaces.Core.Negocio;
using PMP.Interfaces.Utilidades;

namespace PMP.Interfaces.Servicos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IServicoDeProcessoDePatenteDeRevista
    {
        IList<string> ProcesseEmLote(string pastaDeArmazenamentoDasRevistas);
        IList<DTOProcessoPatenteRevista> ObtenhaResultadoDaPesquisa(IFiltro filtro, int quantidadeDeRegistros, int offSet);
        int ObtenhaQuantidadeDeResultadoDaPesquisa(IFiltro filtro);
    }
}
