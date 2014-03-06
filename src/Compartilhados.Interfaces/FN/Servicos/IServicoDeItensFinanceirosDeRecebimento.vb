Imports Compartilhados.Interfaces.FN.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

Namespace FN.Servicos

    Public Interface IServicoDeItensFinanceirosDeRecebimento
        Inherits IServico

        Sub Insira(ByVal Item As IItemLancamentoFinanceiroRecebimento)
        Sub Modifique(ByVal Item As IItemLancamentoFinanceiroRecebimento)
        Function ObtenhaQuantidadeDeProcessosCadastrados(ByVal filtro As IFiltro) As Integer
        Function ObtenhaProcessosDeMarcas(ByVal filtro As IFiltro, ByVal quantidadeDeRegistros As Integer, ByVal offSet As Integer) As IList(Of IItemLancamentoFinanceiroRecebimento)

    End Interface

End Namespace
