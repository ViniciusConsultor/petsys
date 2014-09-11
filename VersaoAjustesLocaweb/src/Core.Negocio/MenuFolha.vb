Imports Core.Interfaces.Negocio

<Serializable()> _
Public Class MenuFolha
    Inherits MenuAbstrato
    Implements IMenuFolha

    Private _URL As String

    Public Property URL() As String Implements IMenuFolha.URL
        Get
            Return _URL
        End Get
        Set(ByVal value As String)
            _URL = value
        End Set
    End Property

End Class
