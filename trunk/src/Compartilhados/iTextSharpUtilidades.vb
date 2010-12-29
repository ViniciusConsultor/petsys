Imports iTextSharp.text
Imports iTextSharp.text.pdf.draw
Imports System.IO
Imports iTextSharp.text.html.simpleparser
Imports System.Text

Public Class iTextSharpUtilidades

    Public Shared Function ObtenhaCaracterQueRepresentaTAB() As Chunk
        Return New Chunk(New VerticalPositionMark, 50)
    End Function

    Public Shared Function TraduzaTextoHTMLListaDeElementos(ByVal TextoHMTL As String) As ArrayList
        Return HTMLWorker.ParseToList(New StringReader(TextoHMTL), Nothing)
    End Function

    Public Shared Function CrieCelula(ByVal Texto As String, _
                                      ByVal Fonte As Font, _
                                      ByVal AlinhamentoHorizontal As Integer, _
                                      ByVal Borda As Integer, _
                                      ByVal EhCabecalho As Boolean) As Cell
        Dim Celula As Cell

        Celula = New Cell(New Phrase(Texto, Fonte))
        Celula.HorizontalAlignment = AlinhamentoHorizontal
        Celula.Border = Borda
        Celula.Header = EhCabecalho
        Return Celula
    End Function

    Public Shared Function CrieCelulaComConteudoHTML(ByVal TextoHTML As String, _
                                                     ByVal Borda As Integer, _
                                                     ByVal EhCabecalho As Boolean) As Cell
        Dim Celula As Cell
        Dim ConteudoDaCelula As Phrase

        ConteudoDaCelula = New Phrase()

        For Each Elemento As IElement In iTextSharpUtilidades.TraduzaTextoHTMLListaDeElementos(TextoHTML)
            ConteudoDaCelula.Add(Elemento)
        Next

        Celula = New Cell(ConteudoDaCelula)
        'Celula.HorizontalAlignment = AlinhamentoHorizontal
        Celula.Border = Borda
        Celula.Header = EhCabecalho
        Return Celula
    End Function


End Class