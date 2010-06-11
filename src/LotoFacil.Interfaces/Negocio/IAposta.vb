Namespace Negocio

    Public Interface IAposta

        Property ID() As Nullable(Of Long)
        ReadOnly Property Concurso() As IConcurso
        ReadOnly Property Jogos() As IList(Of IJogo)
        Sub AdicionaJogo(ByVal Jogo As IJogo)
        Property Nome() As String
    End Interface

End Namespace