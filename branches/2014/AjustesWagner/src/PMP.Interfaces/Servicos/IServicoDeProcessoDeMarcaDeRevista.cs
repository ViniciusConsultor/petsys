using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Interfaces.Core.Negocio;
using PMP.Interfaces.Utilidades;

namespace PMP.Interfaces.Servicos
{
    public interface IServicoDeProcessoDeMarcaDeRevista : IServico
    {
        void ProcesseEmLote(string pastaDeArmazenamentoDasRevistas);
        IList<DTOProcessoMarcaRevista> ObtenhaResultadoDaPesquisa(IFiltro filtro, int quantidadeDeRegistros, int offSet);
        int ObtenhaQuantidadeDeResultadoDaPesquisa(IFiltro filtro);
    }
}
