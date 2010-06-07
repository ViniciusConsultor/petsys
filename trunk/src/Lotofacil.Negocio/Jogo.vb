Public Class Jogo

    Private _Dezenas As IList(Of Dezena)

    Private _ID As String
    Public ReadOnly Property ID() As String
        Get
            Return _ID
        End Get
    End Property

    Public Sub New(ByVal Concurso As Concurso)
        _ID = Guid.NewGuid.ToString
        _Dezenas = New List(Of Dezena)
    End Sub

    Public Sub New(ByVal Concurso As Concurso, _
                   ByVal Dezenas As IList(Of Dezena))
        _Concurso = Concurso
        _Dezenas = Dezenas
    End Sub

    Public Sub AdicionaDezena(ByVal Dezena As Dezena)
        If _Dezenas.Contains(Dezena) Then
            Throw New Exception("Dezena já adicionada no jogo")
        End If
        _Dezenas.Add(Dezena)
    End Sub

    Private _Concurso As Concurso
    Public ReadOnly Property Concurso() As Concurso
        Get
            Return _Concurso
        End Get
    End Property

    Public Function ObtenhaDezenas() As IList(Of Dezena)
        Return _Dezenas
    End Function

    Public Shared Function QuantidadeDeDezenasASeremPreenchidas() As Integer
        Return 15
    End Function

End Class