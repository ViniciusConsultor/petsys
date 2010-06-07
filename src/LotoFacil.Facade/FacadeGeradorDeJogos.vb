Imports LotoFacil.Negocio

Public Class FacadeGeradorDeJogos

    Private Jogos As IList(Of Jogo)
    Private Concurso As Concurso
    Private DezenasEscolhidas As IList(Of Dezena)
    Private ResultadoCombinado() As Short
    Private TempoParaGerarOsJogos As String = "Não houve geração de jogos."

    Public Function GereJogos(ByVal Concurso As Concurso, _
                              ByVal DezenasEscolhidas As IList(Of Dezena)) As IList(Of Jogo)
        Jogos = New List(Of Jogo)

        If DezenasEscolhidas.Count < 15 Then
            Throw New Exception("A quantidade de dezenas escolhidas é insuficiente para o jogo.")
        End If

        Dim StW As Stopwatch = Stopwatch.StartNew

        Me.DezenasEscolhidas = DezenasEscolhidas
        ResultadoCombinado = New Short(14) {}
        FacaCombinacao(0, _
                       (DezenasEscolhidas.Count) - 15, _
                       0, _
                       15)
        StW.Stop()

        TempoParaGerarOsJogos = StW.Elapsed.TotalSeconds.ToString("0.000") & " seg."
        Return Jogos
    End Function

    Public Function ObtenhaTempoGastoParaGerarOsJogos() As String
        Return TempoParaGerarOsJogos
    End Function

    Private Sub FacaCombinacao(ByVal Inicio As Integer, _
                               ByVal Fim As Integer, _
                               ByVal Profundidade As Integer, _
                               ByVal QuantidadeDeDezenasDoJogo As Integer)
        If Profundidade + 1 >= QuantidadeDeDezenasDoJogo Then
            For x As Integer = Inicio To Fim
                ResultadoCombinado(Profundidade) = DezenasEscolhidas.Item(x).Numero
                Dim Jogo As Jogo

                Jogo = New Jogo(Concurso, Me.ObtenhaDezenas(ResultadoCombinado))
                Me.Jogos.Add(Jogo)
            Next
        Else
            For x As Integer = Inicio To Fim
                ResultadoCombinado(Profundidade) = DezenasEscolhidas.Item(x).Numero
                FacaCombinacao(x + 1, Fim + 1, Profundidade + 1, QuantidadeDeDezenasDoJogo)
            Next
        End If
    End Sub

    Private Function ObtenhaDezenas(ByVal ResultadoCombinacao() As Short) As IList(Of Dezena)
        Dim Dezenas As IList(Of Dezena)

        Dezenas = New List(Of Dezena)

        For Each Numero As Short In ResultadoCombinacao
            Dezenas.Add(New Dezena(Numero))
        Next

        Return Dezenas
    End Function

End Class
