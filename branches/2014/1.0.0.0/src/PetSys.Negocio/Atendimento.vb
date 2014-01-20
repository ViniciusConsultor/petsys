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

    Private _DataEHoraDoRetorno As Nullable(Of Date)
    Public Property DataEHoraDoRetorno() As Date? Implements IAtendimento.DataEHoraDoRetorno
        Get
            Return _DataEHoraDoRetorno
        End Get
        Set(ByVal value As Date?)
            _DataEHoraDoRetorno = value
        End Set
    End Property

    Private _Prognostico As String
    Public Property Prognostico() As String Implements IAtendimento.Prognostico
        Get
            Return _Prognostico
        End Get
        Set(ByVal value As String)
            _Prognostico = value
        End Set
    End Property

    Private _Queixa As String
    Public Property Queixa() As String Implements IAtendimento.Queixa
        Get
            Return _Queixa
        End Get
        Set(ByVal value As String)
            _Queixa = value
        End Set
    End Property

    Private _Tratamento As String
    Public Property Tratamento() As String Implements IAtendimento.Tratamento
        Get
            Return _Tratamento
        End Get
        Set(ByVal value As String)
            _Tratamento = value
        End Set
    End Property

    Private _Vermifugos As IList(Of IVermifugo)
    Public Property Vermifugos() As IList(Of IVermifugo) Implements IAtendimento.Vermifugos
        Get
            Return _Vermifugos
        End Get
        Set(ByVal value As IList(Of IVermifugo))
            _Vermifugos = value
        End Set
    End Property

    Private _Peso As Nullable(Of Double)
    Public Property Peso() As Double? Implements IAtendimento.Peso
        Get
            Return _Peso
        End Get
        Set(ByVal value As Double?)
            _Peso = value
        End Set
    End Property

    Private _SinaisClinicos As String
    Public Property SinaisClinicos() As String Implements IAtendimento.SinaisClinicos
        Get
            Return _SinaisClinicos
        End Get
        Set(ByVal value As String)
            _SinaisClinicos = value
        End Set
    End Property

End Class