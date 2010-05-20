Namespace Core.Negocio

    Public Interface IGrupo

        Property ID() As Nullable(Of Long)
        Property Nome() As String
        Property Status() As StatusDoGrupo

    End Interface

End Namespace