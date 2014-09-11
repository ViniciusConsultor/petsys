Imports System.Runtime.Serialization

<Serializable()> _
Public Class BussinesException
    Inherits ApplicationException
    Implements ISerializable

    Public Sub New(ByVal Mensagem As String)
        MyBase.New(Mensagem)
    End Sub
    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

    Public Overrides Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.GetObjectData(info, context)
    End Sub

End Class