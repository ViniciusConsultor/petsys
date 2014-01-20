Imports Lotofacil.Interfaces.Negocio

<Serializable()> _
Public Class Concurso
    Implements IConcurso

    Private _Data As Date
    Private _Numero As Integer

    Public Sub New(ByVal Numero As Integer, _
                   ByVal Data As Date)
        _Numero = Numero
        _Data = Data
    End Sub

    Public ReadOnly Property Data() As Date Implements IConcurso.Data
        Get
            Return _Data
        End Get
    End Property

    Public ReadOnly Property Numero() As Integer Implements IConcurso.Numero
        Get
            Return _Numero
        End Get
    End Property

End Class