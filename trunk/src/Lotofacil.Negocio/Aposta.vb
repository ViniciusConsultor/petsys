Imports Lotofacil.Negocio

Public Class Aposta

    Private _Jogos As IList(Of Jogo)
    Private _Concurso As Concurso
    Public ReadOnly Property Concurso() As Concurso
        Get
            Return _Concurso
        End Get
    End Property

    Public Sub New(ByVal Concurso As Concurso, _
                   ByVal Jogos As IList(Of Jogo))
        Me.New(Concurso)
        If Not Jogos Is Nothing Then
            _Jogos = Jogos
        End If
    End Sub

    Public Sub New(ByVal Concurso As Concurso)
        _Jogos = New List(Of Jogo)
        _Concurso = Concurso
    End Sub

    Public ReadOnly Property Jogos() As IList(Of Jogo)
        Get
            Return _Jogos
        End Get
    End Property

    Public Sub AdicionaJogo(ByVal Jogo As Jogo)
        _Jogos.Add(Jogo)
    End Sub

End Class