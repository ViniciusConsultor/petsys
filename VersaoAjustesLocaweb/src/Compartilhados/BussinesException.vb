<Serializable()> _
Public Class BussinesException
    Inherits ApplicationException

    Public Sub New(ByVal Mensagem As String)
        MyBase.New(Mensagem)
    End Sub

End Class