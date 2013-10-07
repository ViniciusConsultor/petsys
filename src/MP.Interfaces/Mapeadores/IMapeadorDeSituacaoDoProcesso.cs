using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeSituacaoDoProcesso
    {
        IList<ISituacaoDoProcesso> obtenhaTodasSituacoesDoProcesso();
        ISituacaoDoProcesso obtenhaSituacaoDoProcessoPeloId(long idSituacaoDoProcesso);
        ISituacaoDoProcesso obtenhaSituacaoDoProcessoPelaDescricao(string descricaoSituacaoDoProcesso);
        IList<ISituacaoDoProcesso> ObtenhaPorDescricaoComoFiltro(string descricao, int quantidadeMaximaDeRegistros);
        void Inserir(ISituacaoDoProcesso situacaoDoProcesso);
        void Modificar(ISituacaoDoProcesso situacaoDoProcesso);
        void Excluir(long idSituacaoDoProcesso);
    }
}
