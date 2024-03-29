﻿// -----------------------------------------------------------------------
// <copyright file="IServicoDeEventosDePatente.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IServicoDeEventosDePatente : IServico
    {
        IList<IEvento> ObtenhaEventos(long idDaPatente);
    }
}
