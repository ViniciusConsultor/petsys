Imports Compartilhados.Interfaces.Core.Negocio

Namespace Negocio

    Public Interface IMovimentacaoDeProdutoEntrada
        Inherits IMovimentacaoDeProduto

        Property Fornecedor() As IFornecedor

    End Interface

End Namespace