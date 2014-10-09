// -----------------------------------------------------------------------
// <copyright file="ServicoDeEventosDePatenteLocal.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Compartilhados;
using Compartilhados.Fabricas;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ServicoDeEventosDePatenteLocal : Servico, IServicoDeEventosDePatente
    {
        public ServicoDeEventosDePatenteLocal(ICredencial Credencial)
            : base(Credencial)
        {
        }

        public IList<IEvento> ObtenhaEventos(long idDaPatente)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeEventosDePatente>();

            try
            {
                return mapeador.ObtenhaEventos(idDaPatente);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
