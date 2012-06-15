Imports Estoque.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeMovimentacaoDeProduto(Of T As IMovimentacaoDeProduto)

        Sub Estornar(ByVal Movimentacao As T)
        Sub Inserir(ByVal Movimentacao As T)
        Function ObtenhaMovimentacao(ByVal ID As Long) As T
        Function ObtenhaMovimentacoes() As IList(Of T)
        Function ObtenhaMovimentacoes(ByVal DataInicio As Date, ByVal DataFinal As Date) As IList(Of T)

    End Interface

End Namespace