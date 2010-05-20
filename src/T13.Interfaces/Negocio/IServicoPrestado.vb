Namespace Negocio

    Public Interface IServicoPrestado

        Property ID() As Nullable(Of Long)
        Property Nome() As String
        Property Valor() As Nullable(Of Double)
        Property CaracterizaDesconto() As Boolean

    End Interface

End Namespace