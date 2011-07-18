Namespace Negocio

    Public Interface IProdutoMovimentado

        Property Produto() As IProduto
        Property Quantidade() As Double
        Property Preco() As Double
        Function ObtenhaPrecoTotal() As Double

    End Interface

End Namespace