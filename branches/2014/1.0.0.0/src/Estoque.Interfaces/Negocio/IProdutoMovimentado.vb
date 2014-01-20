Namespace Negocio

    Public Interface IProdutoMovimentado

        Property Produto() As IProduto
        Property Quantidade() As Double
        Property Preco() As Double
        ReadOnly Property PrecoTotal() As Double

    End Interface

End Namespace