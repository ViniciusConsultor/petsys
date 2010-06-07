Imports System.IO
Imports Lotofacil.Negocio
Imports System.Text

Public Class Form1

    Private Sub btnExibir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExibir.Click
        Dim Diretorio As String = Environment.CurrentDirectory

        Dim Arquivo As New StreamReader(Diretorio & "\D_LOTFAC.txt")
        Dim JogosSorteados As IList(Of Jogo) = New List(Of Jogo)

        While Not Arquivo.EndOfStream
            Dim Linha As String

            Linha = Arquivo.ReadLine()
            JogosSorteados.Add(CriaJogo(Linha))
        End While

        Dim DicionarioDeNumerosMaisSorteados As IDictionary(Of Short, Integer)

        DicionarioDeNumerosMaisSorteados = CriaDicionarioDeNumerosMaisSorteados()

        For Each JogoSorteado As Jogo In JogosSorteados
            For Each Dezena As Dezena In JogoSorteado.ObtenhaDezenas
                DicionarioDeNumerosMaisSorteados(Dezena.Numero) += 1
            Next
        Next

        Dim Texto As New StringBuilder
        Dim QuantidadeDeSorteios As Integer = JogosSorteados.Count

        lblQuantidadeDeJogosSorteados.Text = "Quantidade de jogos já realizados : " & QuantidadeDeSorteios.ToString

        Texto.AppendLine("Número" & vbTab & "Quantidade de vezes sorteadas" & vbTab & "Porcentagem de chances de ser sorteado")

        For Each Item As KeyValuePair(Of Short, Integer) In DicionarioDeNumerosMaisSorteados
            Texto.AppendLine(Item.Key & vbTab & Item.Value & vbTab & vbTab & vbTab & vbTab & Math.Round(ObtenhaPortecentagem(QuantidadeDeSorteios, Item.Value), 2).ToString & "%")
        Next

        txtResultado.Text = Texto.ToString
    End Sub

    Private Function ObtenhaPortecentagem(ByVal QuantidadeDeSorteios As Integer, ByVal NumeroDeVezesSorteados As Integer) As Double
        Return (NumeroDeVezesSorteados * 100) / QuantidadeDeSorteios
    End Function

    Private Function CriaDicionarioDeNumerosMaisSorteados() As IDictionary(Of Short, Integer)
        Dim Dicionario As IDictionary(Of Short, Integer)

        Dicionario = New Dictionary(Of Short, Integer)

        Dicionario.Add(1, 0)
        Dicionario.Add(2, 0)
        Dicionario.Add(3, 0)
        Dicionario.Add(4, 0)
        Dicionario.Add(5, 0)
        Dicionario.Add(6, 0)
        Dicionario.Add(7, 0)
        Dicionario.Add(8, 0)
        Dicionario.Add(9, 0)
        Dicionario.Add(10, 0)
        Dicionario.Add(11, 0)
        Dicionario.Add(12, 0)
        Dicionario.Add(13, 0)
        Dicionario.Add(14, 0)
        Dicionario.Add(15, 0)
        Dicionario.Add(16, 0)
        Dicionario.Add(17, 0)
        Dicionario.Add(18, 0)
        Dicionario.Add(19, 0)
        Dicionario.Add(20, 0)
        Dicionario.Add(21, 0)
        Dicionario.Add(22, 0)
        Dicionario.Add(23, 0)
        Dicionario.Add(24, 0)
        Dicionario.Add(25, 0)

        Return Dicionario
    End Function

    Private Function CriaJogo(ByVal Linha As String) As Jogo
        Dim Jogo As Jogo = Nothing
        Dim VetorA() As String

        VetorA = Split(Linha, vbTab)

        Jogo = New Jogo(Nothing)

        For Each Numero As String In VetorA
            Dim Dezena As Dezena

            Dezena = New Dezena(CShort(Numero))
            Jogo.AdicionaDezena(Dezena)
        Next

        Return Jogo
    End Function

End Class
