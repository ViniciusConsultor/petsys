// -----------------------------------------------------------------------
// <copyright file="IFiltroPorNumeroDeProcesso.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Compartilhados.Interfaces.Core.Negocio;

namespace PMP.Interfaces.Negocio.Filtros.Marca
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IFiltroPorNumeroDeProcesso : IFiltro
    {
        int? NumeroDaRevista { get; set; }
    }
}
