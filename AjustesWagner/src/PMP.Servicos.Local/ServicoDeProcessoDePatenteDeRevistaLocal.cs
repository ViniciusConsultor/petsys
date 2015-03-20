// -----------------------------------------------------------------------
// <copyright file="ServicoDeProcessoDePatenteDeRevistaLocal.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using PMP.Interfaces.Mapeadores;
using PMP.Interfaces.Servicos;
using PMP.Interfaces.Utilidades;

namespace PMP.Servicos.Local
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ServicoDeProcessoDePatenteDeRevistaLocal : Servico, IServicoDeProcessoDePatenteDeRevista
    {
        public ServicoDeProcessoDePatenteDeRevistaLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public IList<string> ProcesseEmLote(string pastaDeArmazenamentoDasRevistas)
        {
            throw new NotImplementedException();
        }

        public IList<DTOProcessoPatenteRevista> ObtenhaResultadoDaPesquisa(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatenteRevista>();

            try
            {
                return mapeador.ObtenhaResultadoDaPesquisa(filtro, quantidadeDeRegistros, offSet);
            }
            finally
            {
                ServerUtils.libereRecursos();
            } 
        }

        public int ObtenhaQuantidadeDeResultadoDaPesquisa(IFiltro filtro)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatenteRevista>();

            try
            {
                return mapeador.ObtenhaQuantidadeDeResultadoDaPesquisa(filtro);
            }
            finally
            {
                ServerUtils.libereRecursos();
            } 
        }
    }
}
