Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados

Public Class GerarCompromissosEmPDF

    Private _documento As Document
    Private _Fonte1 As Font
    Private _Fonte2 As Font
    Private _Fonte3 As Font
    Private _PaginaAtual As Integer = 0
    Private _Compromissos As IList(Of ICompromisso)

    Private _FonteNomeProprietarioCabecalho As Font
    Private _FonteHorario As Font
    Private _FonteDescricaoCompromissos As Font
    Private NomeDoPDF As String

    Public Sub New(ByVal Compromissos As IList(Of ICompromisso))
        _Compromissos = Compromissos
        _Fonte1 = New Font(Font.TIMES_ROMAN, 10)
        _Fonte2 = New Font(Font.TIMES_ROMAN, 10, Font.BOLD)
        _Fonte3 = New Font(Font.TIMES_ROMAN, 10)
        _FonteNomeProprietarioCabecalho = New Font(Font.TIMES_ROMAN, 10)
        _FonteHorario = New Font(Font.TIMES_ROMAN, 10)
        _FonteDescricaoCompromissos = New Font(Font.TIMES_ROMAN, 10)

        Dim CaminhoDoPDF As String
        Dim Escritor As PdfWriter

        NomeDoPDF = String.Concat(Now.ToString("yyyyMMddhhmmss"), ".pdf")
        CaminhoDoPDF = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_LOADS)

        _documento = New Document(PageSize.A4)
        Escritor = PdfWriter.GetInstance(_documento, New FileStream(Path.Combine(CaminhoDoPDF, NomeDoPDF), FileMode.Create))
        Escritor.PageEvent = New Ouvinte()
        Escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE)
        Escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE)
    End Sub

    Public Function GerePDF(ByVal MostraAssunto As Boolean, ByVal MostraLocal As Boolean, ByVal MostraDescricao As Boolean) As String
        Dim CompromissoAnterior As ICompromisso = Nothing

        For Each Compromisso As ICompromisso In _Compromissos
            'Primeira vez
            If CompromissoAnterior Is Nothing Then
                EscrevaCabecalho(Compromisso)
                EscrevaRodape()
                _documento.Open()
                'Demais vezes testa se o compromisso atual tem data maior que o anterio. Caso tenha atualizamos a data do cabeçalho
            ElseIf CLng(CompromissoAnterior.Inicio.ToString("yyyyMMdd")) < CLng(Compromisso.Inicio.ToString("yyyyMMdd")) Then
                EscrevaCabecalho(Compromisso)
                _documento.NewPage()
            End If

            EscrevaCompromisso(Compromisso, MostraAssunto, MostraLocal, MostraDescricao)
            CompromissoAnterior = Compromisso
        Next

        _documento.Close()
        Return NomeDoPDF
    End Function

    Private Sub EscrevaCabecalho(ByVal Compromisso As ICompromisso)
        Dim Cabecalho As HeaderFooter
        Dim Frase As Phrase

        Frase = New Phrase("Compromissos " & Compromisso.Proprietario.Nome & vbLf, _FonteNomeProprietarioCabecalho)
        Frase.Add(New Phrase(UtilitarioDeData.ObtenhaDiaDaSemanaDiaDoMesMesAnoEmStr(Compromisso.Inicio), _Fonte1))

        Cabecalho = New HeaderFooter(Frase, False)
        Cabecalho.Alignment = HeaderFooter.ALIGN_RIGHT
        _documento.Header = Cabecalho
    End Sub

    Private Function CrieCelula(ByVal Texto As String, _
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

    Private Sub EscrevaCompromisso(ByVal Compromisso As ICompromisso, _
                                   ByVal MostraAssunto As Boolean, _
                                   ByVal MostraLocal As Boolean, _
                                   ByVal MostraDescricao As Boolean)
        Dim ParagradoEmBranco As Paragraph

        ParagradoEmBranco = New Paragraph(" ")
        _documento.Add(ParagradoEmBranco)

        Dim Hora As Paragraph

        Hora = New Paragraph(Compromisso.Inicio.ToString("HH:mm") & "h", _FonteHorario)
        _documento.Add(Hora)

        If MostraAssunto Then
            Dim Assunto As Paragraph

            Assunto = New Paragraph(String.Concat("Assunto: ", Compromisso.Assunto), _FonteHorario)
            Assunto.IndentationLeft = 56.7
            _documento.Add(Assunto)
        End If

        If MostraLocal AndAlso Not String.IsNullOrEmpty(Compromisso.Local) Then
            Dim Local As Paragraph

            Local = New Paragraph(String.Concat("Local: ", Compromisso.Local), _FonteHorario)
            Local.IndentationLeft = 56.7
            _documento.Add(Local)
        End If

        If MostraDescricao AndAlso Not String.IsNullOrEmpty(Compromisso.Descricao) Then
            Dim Descricao As Paragraph

            Descricao = New Paragraph(String.Concat("Descrição: ", Compromisso.Descricao), _FonteHorario)
            Descricao.IndentationLeft = 56.7
            _documento.Add(Descricao)
        End If
    End Sub

    Private Sub EscrevaRodape()
        Dim Rodape As HeaderFooter

        Dim Texto As New StringBuilder

        Texto.AppendLine(String.Concat("Impressão em: ", Now.ToString("dd/MM/yyyy HH:mm:ss")))

        Rodape = New HeaderFooter(New Phrase(Texto.ToString, _Fonte1), False)

        Rodape.Alignment = HeaderFooter.ALIGN_RIGHT
        _documento.Footer = Rodape
    End Sub

    Private Class Ouvinte
        Implements IPdfPageEvent

        Public Sub OnChapter(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single, ByVal title As iTextSharp.text.Paragraph) Implements iTextSharp.text.pdf.IPdfPageEvent.OnChapter

        End Sub

        Public Sub OnChapterEnd(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single) Implements iTextSharp.text.pdf.IPdfPageEvent.OnChapterEnd

        End Sub

        Public Sub OnCloseDocument(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document) Implements iTextSharp.text.pdf.IPdfPageEvent.OnCloseDocument

        End Sub

        Public Sub OnEndPage(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document) Implements iTextSharp.text.pdf.IPdfPageEvent.OnEndPage

        End Sub

        Public Sub OnGenericTag(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal rect As iTextSharp.text.Rectangle, ByVal text As String) Implements iTextSharp.text.pdf.IPdfPageEvent.OnGenericTag

        End Sub

        Public Sub OnOpenDocument(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document) Implements iTextSharp.text.pdf.IPdfPageEvent.OnOpenDocument

        End Sub

        Public Sub OnParagraph(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single) Implements iTextSharp.text.pdf.IPdfPageEvent.OnParagraph

        End Sub

        Public Sub OnParagraphEnd(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single) Implements iTextSharp.text.pdf.IPdfPageEvent.OnParagraphEnd

        End Sub

        Public Sub OnSection(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single, ByVal depth As Integer, ByVal title As iTextSharp.text.Paragraph) Implements iTextSharp.text.pdf.IPdfPageEvent.OnSection

        End Sub

        Public Sub OnSectionEnd(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single) Implements iTextSharp.text.pdf.IPdfPageEvent.OnSectionEnd

        End Sub

        Public Sub OnStartPage(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document) Implements iTextSharp.text.pdf.IPdfPageEvent.OnStartPage

        End Sub

    End Class

End Class
