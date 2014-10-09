// -----------------------------------------------------------------------
// <copyright file="ServicoDeEventosDeMarca.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Compartilhados.Fabricas;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Servicos.Local
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using Compartilhados;
using MP.Interfaces.Servicos;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ServicoDeEventosDeMarcaLocal : Servico, IServicoDeEventosDeMarca
    {
        public ServicoDeEventosDeMarcaLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public IList<IEvento> ObtenhaEventos(long idDaMarca)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeEventosDeMarca>();

            try
            {
                return mapeador.ObtenhaEventos(idDaMarca);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
