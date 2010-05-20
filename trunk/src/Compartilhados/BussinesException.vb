Public Class BussinesException
    Inherits Exception

    Public Sub New(ByVal Mensagem As String)
        MyBase.New(Mensagem)
    End Sub

End Class