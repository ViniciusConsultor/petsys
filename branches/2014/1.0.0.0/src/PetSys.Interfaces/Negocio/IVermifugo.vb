Namespace Negocio

    Public Interface IVermifugo

        Property ID() As Nullable(Of Long)
        Property Nome() As String
        Property Data() As Date
        Property Observacao() As String
        Property ProximaDoseEm() As Nullable(Of Date)
        Property VeterinarioQueReceitou() As IVeterinario
        Property AnimalQueRecebeu() As IAnimal

    End Interface

End Namespace