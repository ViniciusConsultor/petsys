Imports Core.Interfaces.Negocio

<Serializable()> _
Public Class DiretivaDeSeguranca
    Implements IDiretivaDeSeguranca

    Private _ID As String

    Public Property ID() As String Implements IDiretivaDeSeguranca.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property

End Class