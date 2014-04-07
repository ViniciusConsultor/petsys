Namespace Core.Negocio

    Public Interface IBanco

        Property ID() As Nullable(Of Long)
        Property Nome() As String
        Property Numero() As Integer
        ReadOnly Property Agencias() As IList(Of IAgencia)

    End Interface

End Namespace