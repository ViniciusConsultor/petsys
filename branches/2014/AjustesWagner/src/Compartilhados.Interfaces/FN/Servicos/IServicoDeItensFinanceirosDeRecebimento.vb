Imports Compartilhados.Interfaces.FN.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

Namespace FN.Servicos

    Public Interface IServicoDeItensFinanceirosDeRecebimento
        Inherits IServico

        Sub Insira(ByVal Item As IItemLancamentoFinanceiroRecebimento)
        Sub Modifique(ByVal Item As IItemLancamentoFinanceiroRecebimento)
        Function ObtenhaQuantidadeDeItensFinanceiros(ByVal filtro As IFiltro) As Integer
        Function ObtenhaItensFinanceiros(ByVal filtro As IFiltro, ByVal quantidadeDeRegistros As Integer, ByVal offSet As Integer) As IList(Of IItemLancamentoFinanceiroRecebimento)
        Function Obtenha(ByVal ID As Long) As IItemLancamentoFinanceiroRecebimento
        Sub Excluir(ByVal IdItemLancamentoFinanceiroRecebimento As Long)
    End Interface

End Namespace
