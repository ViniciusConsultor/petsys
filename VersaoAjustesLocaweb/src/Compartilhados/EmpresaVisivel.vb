<Serializable()> _
Public Class EmpresaVisivel

    Public Sub New(id As Long, nome As String)
        _ID = id
        _Nome = nome
    End Sub

    Private _ID As Long
    Public ReadOnly Property ID As Long
        Get
            Return _ID
        End Get
    End Property

    Private _Nome As String
    Public ReadOnly Property Nome As String
        Get
            Return _Nome
        End Get
    End Property

End Class