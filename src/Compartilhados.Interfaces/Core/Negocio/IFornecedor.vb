Namespace Core.Negocio

    Public Interface IFornecedor
        Inherits IPapelPessoa

        ReadOnly Property Vendedores() As IList(Of IVendedor)

    End Interface

End Namespace