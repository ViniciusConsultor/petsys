Imports PetSys.Interfaces.Negocio

<Serializable()> _
Public Class Vermifugo
    Implements IVermifugo

    Private _AnimalQueRecebeu As IAnimal
    Public Property AnimalQueRecebeu() As IAnimal Implements IVermifugo.AnimalQueRecebeu
        Get
            Return _AnimalQueRecebeu
        End Get
        Set(ByVal value As IAnimal)
            _AnimalQueRecebeu = value
        End Set
    End Property

    Private _Data As Date
    Public Property Data() As Date Implements IVermifugo.Data
        Get
            Return _Data
        End Get
        Set(ByVal value As Date)
            _Data = value
        End Set
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements IVermifugo.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome() As String Implements IVermifugo.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Private _Observacao As String
    Public Property Observacao() As String Implements IVermifugo.Observacao
        Get
            Return _Observacao
        End Get
        Set(ByVal value As String)
            _Observacao = value
        End Set
    End Property

    Private _ProximaDoseEm As Nullable(Of Date)
    Public Property ProximaDoseEm() As Date? Implements IVermifugo.ProximaDoseEm
        Get
            Return _ProximaDoseEm
        End Get
        Set(ByVal value As Date?)
            _ProximaDoseEm = value
        End Set
    End Property

    Private _VeterinarioQueReceitou As IVeterinario
    Public Property VeterinarioQueReceitou() As IVeterinario Implements IVermifugo.VeterinarioQueReceitou
        Get
            Return _VeterinarioQueReceitou
        End Get
        Set(ByVal value As IVeterinario)
            _VeterinarioQueReceitou = value
        End Set
    End Property
End Class
