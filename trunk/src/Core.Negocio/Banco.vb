Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Banco
    Implements IBanco

    Public ReadOnly Property Agencias() As IList(Of IAgencia) Implements IBanco.Agencias
        Get
            Return Nothing
        End Get
    End Property

    Private _Numero As Integer
    Public Property Numero() As Integer Implements IBanco.Numero
        Get
            Return _Numero
        End Get
        Set(ByVal value As Integer)
            _Numero = value
        End Set
    End Property

    Private _ID As Long?
    Public Property ID As Long? Implements IBanco.ID
        Get
            Return _ID
        End Get
        Set(value As Long?)
            _ID = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome As String Implements IBanco.Nome
        Get
            Return _Nome
        End Get
        Set(value As String)
            _Nome = value
        End Set
    End Property

End Class