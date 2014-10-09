// -----------------------------------------------------------------------
// <copyright file="IMapeadorDeEventosDeMarca.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IMapeadorDeEventosDeMarca
    {
        IList<IEvento> ObtenhaEventos(long idDaMarca);
        void AtualizeEventos(IMarcas marca);
        void RemovaEventos(long idDaMarca);
    }
}
