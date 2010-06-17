Imports Compartilhados
Imports LotoFacil.Interfaces.Servicos
Imports LotoFacil.Interfaces.Negocio
Imports LotoFacil.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeApostaLocal
    Inherits Servico
    Implements IServicoDeAposta

    Private Jogos As IList(Of IJogo)
    Private Concurso As IConcurso
    Private DezenasEscolhidas As IList(Of IDezena)
    Private ResultadoCombinado() As IDezena
    Private TempoParaGerarOsJogos As String = "Não houve geração de jogos."
    Private JogoClonavel As IJogo

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Function ConfiraAposta(ByVal Aposta As IAposta, _
                                  ByVal DezenasSorteadas As IList(Of IDezena)) As IDictionary(Of IJogo, Short) Implements IServicoDeAposta.ConfiraAposta
        Dim DicionarioDeAcertos As IDictionary(Of IJogo, Short)

        DicionarioDeAcertos = New Dictionary(Of IJogo, Short)

        For Each Jogo As IJogo In Aposta.Jogos
            DicionarioDeAcertos.Add(Jogo, Jogo.ObtenhaQuantidadeDeDezenasAcertadas(DezenasSorteadas))
        Next

        Return DicionarioDeAcertos
    End Function

    Public Function ConfiraApostas(ByVal Apostas As IList(Of IAposta), _
                                   ByVal DezenasSorteadas As IList(Of IDezena)) As IDictionary(Of IAposta,  _
                                                                                               IDictionary(Of IJogo, Short)) Implements IServicoDeAposta.ConfiraApostas
        Dim DicionarioDeAcertos As IDictionary(Of IAposta, IDictionary(Of IJogo, Short))

        DicionarioDeAcertos = New Dictionary(Of IAposta, IDictionary(Of IJogo, Short))

        For Each Aposta As IAposta In Apostas
            DicionarioDeAcertos.Add(Aposta, Me.ConfiraAposta(Aposta, DezenasSorteadas))
        Next

        Return DicionarioDeAcertos
    End Function

    Public Function GereJogos(ByVal DezenasEscolhidas As IList(Of IDezena)) As IList(Of IJogo) Implements IServicoDeAposta.GereJogos
        Jogos = New List(Of IJogo)

        JogoClonavel = FabricaDeJogo.CrieObjeto()

        If DezenasEscolhidas.Count < 15 Then
            Throw New Exception("A quantidade de dezenas escolhidas é insuficiente para o jogo.")
        End If

        Dim StW As Stopwatch = Stopwatch.StartNew

        Me.DezenasEscolhidas = DezenasEscolhidas
        ResultadoCombinado = CType(Array.CreateInstance(GetType(IDezena), 15), IDezena())
        FacaCombinacao(0, _
                       (DezenasEscolhidas.Count) - 15, _
                       0, _
                       15)
        StW.Stop()

        TempoParaGerarOsJogos = StW.Elapsed.TotalSeconds.ToString("0.000") & " seg."

        Return Jogos
    End Function

    Private Sub FacaCombinacao(ByVal Inicio As Integer, _
                               ByVal Fim As Integer, _
                               ByVal Profundidade As Integer, _
                               ByVal QuantidadeDeDezenasDoJogo As Integer)
        If Profundidade + 1 >= QuantidadeDeDezenasDoJogo Then
            For x As Integer = Inicio To Fim
                ResultadoCombinado(Profundidade) = DezenasEscolhidas.Item(x)
                Dim Jogo As IJogo = DirectCast(JogoClonavel.Clone, IJogo)

                Jogo.AdicionaDezenas(ResultadoCombinado)
                Me.Jogos.Add(Jogo)
            Next
        Else
            For x As Integer = Inicio To Fim
                ResultadoCombinado(Profundidade) = DezenasEscolhidas.Item(x)
                FacaCombinacao(x + 1, Fim + 1, Profundidade + 1, QuantidadeDeDezenasDoJogo)
            Next
        End If
    End Sub

    Private Function ObtenhaDezenas(ByVal ResultadoCombinacao() As Byte) As IList(Of IDezena)
        Dim Dezenas As IList(Of IDezena)

        Dezenas = New List(Of IDezena)

        For Each Numero As Byte In ResultadoCombinacao
            Dezenas.Add(FabricaDeDezena.CrieObjeto(Numero))
        Next

        Return Dezenas
    End Function

    Public Function ObtenhaTempoGastoParaGerarOsJogos() As String Implements IServicoDeAposta.ObtenhaTempoGastoParaGerarOsJogos
        Return TempoParaGerarOsJogos
    End Function

    Public Sub GraveAposta(ByVal Aposta As IAposta) Implements IServicoDeAposta.GraveAposta
        Dim Mapeador As IMapeadorDeAposta

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAposta)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.GraveAposta(Aposta)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

End Class