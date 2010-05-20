Namespace Negocio

    Public Interface IItemDeLancamento

        Property Servico() As IServicoPrestado
        Property Valor() As Double
        Property Quantidade() As Nullable(Of Short)
        Property Unidade() As String
        Property Observacao() As String
        ReadOnly Property Total() As Double
    End Interface

End Namespace