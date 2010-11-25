Imports iTextSharp.text
Imports iTextSharp.text.pdf.draw

Public Class iTextSharpUtilidades

    Private Shared DeParaDeCores As IDictionary(Of String, iTextSharp.text.Color)

    Private Sub New()
        If DeParaDeCores Is Nothing Then
            CarregaDicionariosDePara()
        End If
    End Sub

    Private Shared Sub CarregaDicionariosDePara()
        DeParaDeCores = New Dictionary(Of String, iTextSharp.text.Color)

        'Preto
        DeParaDeCores.Add("#000000", Color.BLACK)
        'Cinza
        DeParaDeCores.Add("#808080", Color.GRAY)
        'Vermelho
        DeParaDeCores.Add("#ff0000", Color.RED)
        'Rosa
        DeParaDeCores.Add("#ffc0cb", Color.PINK)
        'Amarelo
        DeParaDeCores.Add("#ffff00", Color.YELLOW)
        'Azul
        DeParaDeCores.Add("#0000ff", Color.BLUE)
        'Laranja
        DeParaDeCores.Add("#ffa500", Color.ORANGE)
        'Verde
        DeParaDeCores.Add("#008000", Color.GREEN)
    End Sub

    Public Shared Function TransformeHTMLEmPhrase(ByVal TextoHTML As String, _
                                                  ByVal TipoFonte As Font, _
                                                  ByVal TamanhoDaFonte As Single) As Phrase
        Dim LinhasDoHTML() As String

        TextoHTML = TextoHTML.Replace("<br />", "§")
        LinhasDoHTML = TextoHTML.Split(CChar("§"))

        Dim Frase As New Phrase
        Dim EstaEmNegrito As Boolean = False
        Dim EstaComCorAlterada As Boolean = False

        'For Each Linha As String In LinhasDoHTML
        '    If Linha.Contains("<strong"> then

        'Next

        Return Nothing
    End Function

    Public Shared Function ObtenhaCaracterQueRepresentaTAB() As Chunk
        Return New Chunk(New VerticalPositionMark, 50)
    End Function

End Class