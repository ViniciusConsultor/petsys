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
        IList<IProcessoDeMarca> ObtenhaProcessosDeMarcas(IFiltro filtro, int quantidadeDeRegistros, int offSet);
        IProcessoDeMarca Obtenha(long ID);
        int ObtenhaQuantidadeDeProcessosCadastrados(IFiltro filtro);
        IProcessoDeMarca ObtenhaProcessoDeMarcaPeloNumero(long numeroDoProcesso);
        IList<long> ObtenhaTodosNumerosDeProcessosCadastrados();

        IList<IProcessoDeMarca> ObtenhaProcessosDeMarcas(long? IDCliente, long? IDGrupoDeAtividade,
                                                         IList<string> IDsDosDespachos);

        IList<IProcessoDeMarca> obtenhaProcessosComMarcaQueContemRadicalDadastrado();
        IList<IProcessoDeMarca> ObtenhaProcessoComRadicailAdicionadoNaMarca(IList<IProcessoDeMarca> processos);

        IList<IProcessoDeMarca> ObtenhaProcessosDeMarcasComRegistroConcedido(DateTime? dataInicial, DateTime? dataFinal,
                                                                             IList<string> IDsDosDespachos);
    }
}