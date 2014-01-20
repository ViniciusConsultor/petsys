Namespace Negocio

    Public Interface IVacina

        Property ID() As Nullable(Of Long)
        Property Nome() As String
        Property DataDaVacinacao() As Date
        Property Observacao() As String
        Property RevacinarEm() As Nullable(Of Date)
        Property VeterinarioQueAplicou() As IVeterinario
        Property AnimalQueRecebeu() As IAnimal

    End Interface

End Namespace