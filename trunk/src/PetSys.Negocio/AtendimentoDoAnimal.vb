Imports PetSys.Interfaces.Negocio

<Serializable()> _
Public Class AtendimentoDoAnimal
    Implements IAtendimentoDoAnimal

    Private _Animal As IAnimal
    Private _DataEHoraDoAtendimento As Date
    Private _ID As Nullable(Of Long)
    Private _Veterinario As IVeterinario

    Public Property Animal() As IAnimal Implements IAtendimentoDoAnimal.Animal
        Get
            Return _Animal
        End Get
        Set(ByVal value As IAnimal)
            _Animal = value
        End Set
    End Property

    Public Property DataEHoraDoAtendimento() As Date Implements IAtendimentoDoAnimal.DataEHoraDoAtendimento
        Get
            Return _DataEHoraDoAtendimento
        End Get
        Set(ByVal value As Date)
            _DataEHoraDoAtendimento = value
        End Set
    End Property

    Public Property ID() As Long? Implements IAtendimentoDoAnimal.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Public Property Veterinario() As IVeterinario Implements IAtendimentoDoAnimal.Veterinario
        Get
            Return _Veterinario
        End Get
        Set(ByVal value As IVeterinario)
            _Veterinario = value
        End Set
    End Property

End Class