Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class TipoDeEndereco
    Implements ITipoDeEndereco

    Private _ID As Nullable(Of Long)
    Public Property ID As Long? Implements ITipoDeEndereco.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome As String Implements ITipoDeEndereco.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If obj Is Nothing Then Return (False)

        Return CType(obj, ITipoDeEndereco).ID.Value.Equals(Me._ID)
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return _ID.GetHashCode
    End Function


End Class