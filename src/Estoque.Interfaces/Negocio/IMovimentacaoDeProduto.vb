Namespace Negocio

    Public Interface IMovimentacaoDeProduto

        Property ID() As Nullable(Of Long)
        Property Data() As Date
        Property Historico() As String
        Property NumeroDocumento() As String
        Sub AdicioneProdutoMovimentado(ByVal ProdutoMovimentado As IProdutoMovimentado)
        Sub AdicioneProdutosMovimentados(ByVal ProdutosMovimentados As IList(Of IProdutoMovimentado))
        Function ObtenhaProdutosMovimentados() As IList(Of IProdutoMovimentado)
        Function ObtenhaTotalDaMovimentacao() As Double
        ReadOnly Property Tipo() As TipoMovimentacaoDeProduto

    End Interface

End Namespace