Namespace Core.Negocio

    Public Interface IAgencia
        
        Property ID As Nullable(Of Long)
        Property Numero() As String
        Property Banco() As Banco
        Property Nome As String

    End Interface

End Namespace