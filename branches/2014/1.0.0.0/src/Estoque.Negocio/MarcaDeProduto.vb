Imports Estoque.Interfaces.Negocio

<Serializable()> _
Public Class MarcaDeProduto
    Implements IMarcaDeProduto

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements IMarcaDeProduto.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome() As String Implements IMarcaDeProduto.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

End Class
