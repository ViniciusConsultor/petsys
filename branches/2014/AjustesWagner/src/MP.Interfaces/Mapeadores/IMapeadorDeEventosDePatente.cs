// -----------------------------------------------------------------------
// <copyright file="IMapeadorDeEventosDePatente.cs" company="Microsoft">
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
    public interface IMapeadorDeEventosDePatente
    {
        IList<IEvento> ObtenhaEventos(long idDaPatente);
        void AtualizeEventos(IPatente patente);
        void RemovaEventos(long idDaPatente);
    }
}
