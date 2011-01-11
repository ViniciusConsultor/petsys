Imports PetSys.Interfaces.Negocio

<Serializable()> _
Public Class Atendimento
    Implements IAtendimento

    Private _Animal As IAnimal
    Public Property Animal() As IAnimal Implements IAtendimento.Animal
        Get
            Return _Animal
        End Get
        Set(ByVal value As IAnimal)
            _Animal = value
        End Set
    End Property

    Private _DataEHoraDoAtendimento As Date
    Public Property DataEHoraDoAtendimento() As Date Implements IAtendimento.DataEHoraDoAtendimento
        Get
            Return _DataEHoraDoAtendimento
        End Get
        Set(ByVal value As Date)
            _DataEHoraDoAtendimento = value
        End Set
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements IAtendimento.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Veterinario As IVeterinario
    Public Property Veterinario() As IVeterinario Implements IAtendimento.Veterinario
        Get
            Return _Veterinario
        End Get
        Set(ByVal value As IVeterinario)
            _Veterinario = value
        End Set
    End Property

    Private _Vacinas As IList(Of IVacina)
    Public Property Vacinas() As IList(Of IVacina) Implements IAtendimento.Vacinas
        Get
            Return _Vacinas
        End Get
        Set(ByVal value As IList(Of IVacina))
            _Vacinas = value
        End Set
    End Property

End Class
