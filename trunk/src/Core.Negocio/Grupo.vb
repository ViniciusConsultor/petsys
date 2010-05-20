Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio

<Serializable()> _
Public Class Grupo
    Implements IGrupo

    Private _Nome As String
    Private _ID As Nullable(Of Long)
    Private _Status As StatusDoGrupo

    Public Property Nome() As String Implements IGrupo.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Public Property Status() As StatusDoGrupo Implements IGrupo.Status
        Get
            Return _Status
        End Get
        Set(ByVal value As StatusDoGrupo)
            _Status = value
        End Set
    End Property

    Public Property ID() As Long? Implements IGrupo.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If Not obj Is Nothing AndAlso TypeOf (obj) Is IGrupo Then
            Return CType(obj, IGrupo).ID.Value = Me.ID.Value
        End If
        Return False
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return MyBase.GetHashCode()
    End Function

End Class