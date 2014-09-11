Imports System.Runtime.Serialization

<Serializable()> _
Public Class ValidacaoException
    Inherits ApplicationException
    
    Public Sub New(ByVal Mensagem As String)
        MyBase.New(Mensagem)
    End Sub
End Class
