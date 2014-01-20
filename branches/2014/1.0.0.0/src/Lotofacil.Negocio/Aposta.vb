Imports Lotofacil.Negocio
Imports Lotofacil.Interfaces.Negocio

<Serializable()> _
Public Class Aposta
    Implements IAposta

    Private _Jogos As IList(Of IJogo)
    Private _Concurso As IConcurso

    Public Sub New(ByVal Concurso As IConcurso, _
                   ByVal Jogos As IList(Of IJogo))
        Me.New(Concurso)
        If Not Jogos Is Nothing Then
            _Jogos = Jogos
        End If
    End Sub

    Public Sub New(ByVal Concurso As IConcurso)
        _Jogos = New List(Of IJogo)
        _Concurso = Concurso
    End Sub

    Public Sub AdicionaJogo(ByVal Jogo As IJogo) Implements IAposta.AdicionaJogo
        _Jogos.Add(Jogo)
    End Sub

    Public ReadOnly Property Concurso() As IConcurso Implements IAposta.Concurso
        Get
            Return _Concurso
        End Get
    End Property

    Public ReadOnly Property Jogos() As IList(Of IJogo) Implements IAposta.Jogos
        Get
            Return _Jogos
        End Get
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements IAposta.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome() As String Implements IAposta.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property
End Class