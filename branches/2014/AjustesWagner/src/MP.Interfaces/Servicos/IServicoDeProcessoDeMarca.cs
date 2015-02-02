using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeProcessoDeMarca : IServico
    {
        void Inserir(IProcessoDeMarca processoDeMarca);
        void Modificar(IProcessoDeMarca processoDeMarca);
        void AtualizeProcessoAposLeituraDaRevista(IProcessoDeMarca processoDeMarca);
        void Excluir(IProcessoDeMarca processoDeMarca);
        IList<IProcessoDeMarca> ObtenhaProcessosDeMarcas(IFiltro filtro, int quantidadeDeRegistros, int offSet, bool considerarNaoAtivos);
        IProcessoDeMarca Obtenha(long ID);
        int ObtenhaQuantidadeDeProcessosCadastrados(IFiltro filtro, bool considerarNaoAtivos);
        IProcessoDeMarca ObtenhaProcessoDeMarcaPeloNumero(long numeroDoProcesso);
        IList<long> ObtenhaTodosNumerosDeProcessosAtivosCadastrados();

        IList<IProcessoDeMarca> ObtenhaProcessosDeMarcas(long? IDCliente, long? IDGrupoDeAtividade,
                                                         IList<string> IDsDosDespachos, ModoDePesquisaPorStatus modoDePesquisaPorStatus);

        IList<IProcessoDeMarca> obtenhaProcessosComMarcaQueContemRadicalDadastrado();
        IList<IProcessoDeMarca> ObtenhaProcessoComRadicailAdicionadoNaMarca(IList<IProcessoDeMarca> processos);

        IList<IProcessoDeMarca> ObtenhaProcessosDeMarcasComRegistroConcedido(DateTime? dataInicial, DateTime? dataFinal,
                                                                             IList<string> IDsDosDespachos);


        IProcessoDeMarca MontarProcessosDaRevistaParaListagem(IRevistaDeMarcas processo);
    }
}