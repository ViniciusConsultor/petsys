Imports iTextSharp.text
Imports iTextSharp.text.pdf.draw
Imports System.IO
Imports iTextSharp.text.html.simpleparser

Public Class iTextSharpUtilidades

    Public Shared Function ObtenhaCaracterQueRepresentaTAB() As Chunk
        Return New Chunk(New VerticalPositionMark, 50)
    End Function

    Public Shared Function TraduzaTextoHTMLListaDeElementos(ByVal TextoHMTL As String) As ArrayList
        Return HTMLWorker.ParseToList(New StringReader(TextoHMTL), Nothing)
    End Function

End Class