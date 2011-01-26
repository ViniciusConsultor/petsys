Imports PetSys.Interfaces.Negocio

<Serializable()> _
Public Class Vacina
    Implements IVacina


    Private _AnimalQueRecebeu As IAnimal
    Public Property AnimalQueRecebeu() As IAnimal Implements IVacina.AnimalQueRecebeu
        Get
            Return _AnimalQueRecebeu
        End Get
        Set(ByVal value As IAnimal)
            _AnimalQueRecebeu = value
        End Set
    End Property

    Private _DataDaVacinacao As Date
    Public Property DataDaVacinacao() As Date Implements IVacina.DataDaVacinacao
        Get
            Return _DataDaVacinacao
        End Get
        Set(ByVal value As Date)
            _DataDaVacinacao = value
        End Set
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements IVacina.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome() As String Implements IVacina.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Private _Observacao As String
    Public Property Observacao() As String Implements IVacina.Observacao
        Get
            Return _Observacao
        End Get
        Set(ByVal value As String)
            _Observacao = value
        End Set
    End Property

    Private _RevacinarEm As Nullable(Of Date)
    Public Property RevacinarEm() As Date? Implements IVacina.RevacinarEm
        Get
            Return _RevacinarEm
        End Get
        Set(ByVal value As Date?)
            _RevacinarEm = value
        End Set
    End Property

    Private _VeterinarioQueAplicou As IVeterinario
    Public Property VeterinarioQueAplicou() As IVeterinario Implements IVacina.VeterinarioQueAplicou
        Get
            Return _VeterinarioQueAplicou
        End Get
        Set(ByVal value As IVeterinario)
            _VeterinarioQueAplicou = value
        End Set
    End Property

End Class
