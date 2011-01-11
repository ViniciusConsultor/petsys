Namespace Negocio

    Public Interface IVacina

        Property Nome() As String
        Property DataDaVacinacao() As Date
        Property Observacao() As String
        Property RevacinarEm() As Nullable(Of Date)

    End Interface

End Namespace