Imports Estoque.Interfaces.Negocio

<Serializable()> _
Public Class ProdutoMovimentado
    Implements IProdutoMovimentado

    Private _Preco As Double
    Public Property Preco() As Double Implements IProdutoMovimentado.Preco
        Get
            Return _Preco
        End Get
        Set(ByVal value As Double)
            _Preco = value
        End Set
    End Property

    Private _Produto As IProduto
    Public Property Produto() As IProduto Implements IProdutoMovimentado.Produto
        Get
            Return _Produto
        End Get
        Set(ByVal value As IProduto)
            _Produto = value
        End Set
    End Property

    Private _Quantidade As Double
    Public Property Quantidade() As Double Implements IProdutoMovimentado.Quantidade
        Get
            Return _Quantidade
        End Get
        Set(ByVal value As Double)
            _Quantidade = value
        End Set
    End Property

    Public ReadOnly Property PrecoTotal() As Double Implements IProdutoMovimentado.PrecoTotal
        Get
            Return Preco * Quantidade
        End Get
    End Property

End Class