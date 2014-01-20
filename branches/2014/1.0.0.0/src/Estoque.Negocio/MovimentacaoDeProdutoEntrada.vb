Imports Estoque.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class MovimentacaoDeProdutoEntrada
    Inherits MovimentacaoDeProduto
    Implements IMovimentacaoDeProdutoEntrada

    Private _Fornecedor As IFornecedor
    Public Property Fornecedor() As IFornecedor Implements IMovimentacaoDeProdutoEntrada.Fornecedor
        Get
            Return _Fornecedor
        End Get
        Set(ByVal value As IFornecedor)
            _Fornecedor = value
        End Set
    End Property

    Public Overrides ReadOnly Property Tipo() As TipoMovimentacaoDeProduto
        Get
            Return TipoMovimentacaoDeProduto.Entrada
        End Get
    End Property

End Class
