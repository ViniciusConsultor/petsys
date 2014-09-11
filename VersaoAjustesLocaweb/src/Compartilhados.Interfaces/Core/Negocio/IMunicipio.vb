Namespace Core.Negocio

    Public Interface IMunicipio

        Property ID() As Nullable(Of Long)
        Property Nome() As String
        Property UF() As UF
        Property CEP() As CEP

    End Interface

End Namespace