<Serializable()> _
Public Class TipoDeServidorDeEmail

    Private _ID As Char
    Private _Descricao As String

    Public Shared SMTP As TipoDeServidorDeEmail = New TipoDeServidorDeEmail("0"c, "SMTP")

    Private Shared Lista As TipoDeServidorDeEmail() = {SMTP}

    Private Sub New(ByVal ID As Char, _
                    ByVal Descricao As String)
        _ID = ID
        _Descricao = Descricao
    End Sub

    Public ReadOnly Property ID() As Char
        Get
            Return _ID
        End Get
    End Property

    Public ReadOnly Property Descricao() As String
        Get
            Return _Descricao
        End Get
    End Property

    Public Shared Function Obtenha(ByVal ID As Char) As TipoDeServidorDeEmail
        For Each Tipo As TipoDeServidorDeEmail In Lista
            If Tipo.ID = ID Then
                Return Tipo
            End If
        Next

        Return Nothing
    End Function

    Public Shared Function ObtenhaTodos() As IList(Of TipoDeServidorDeEmail)
        Return New List(Of TipoDeServidorDeEmail)(Lista)
    End Function

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Return CType(obj, TipoDeServidorDeEmail).ID = Me.ID
    End Function

End Class
