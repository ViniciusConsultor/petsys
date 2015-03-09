// -----------------------------------------------------------------------
// <copyright file="IFiltroPMP.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Compartilhados.Interfaces.Core.Negocio;

namespace PMP.Interfaces.Negocio.Filtros
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IFiltroPMP : IFiltro
    {
        int? NumeroDaRevista { get; set; }
    }
}
