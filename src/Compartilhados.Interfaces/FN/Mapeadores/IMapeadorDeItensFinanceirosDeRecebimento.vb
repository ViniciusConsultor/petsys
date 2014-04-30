Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.FN.Negocio

Namespace FN.Mapeadores

    Public Interface IMapeadorDeItensFinanceirosDeRecebimento

        Sub Insira(ByVal Item As IItemLancamentoFinanceiroRecebimento)
        Sub Modifique(ByVal Item As IItemLancamentoFinanceiroRecebimento)
        Function ObtenhaQuantidadeDeItensFinanceiros(ByVal filtro As IFiltro) As Integer
        Function ObtenhaItensFinanceiros(ByVal filtro As IFiltro, ByVal quantidadeDeRegistros As Integer, ByVal offSet As Integer) As IList(Of IItemLancamentoFinanceiroRecebimento)
        Function Obtenha(ByVal ID As Long) As IItemLancamentoFinanceiroRecebimento
        Sub Excluir(ByVal IdItemLancamentoFinanceiroRecebimento As Long)

    End Interface

End Namespace
