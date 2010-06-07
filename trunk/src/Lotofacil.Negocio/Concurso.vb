Public Class Concurso

    Public Sub New(ByVal Numero As Integer, _
                   ByVal Data As Date)
        _Numero = Numero
        _Data = Data
    End Sub

    Private _Data As Date
    Public ReadOnly Property Data() As Date
        Get
            Return _Data
        End Get
    End Property

    Private _Numero As Integer
    Public ReadOnly Property Numero() As Integer
        Get
            Return _numero
        End Get
    End Property

End Class
