Imports Diary.Interfaces.Negocio
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Fabricas

Public Class GeradorDeSolicitacoesEmPDF

    Private _documento As Document
    Private _Fonte1 As Font
    Private _Fonte2 As Font
    Private _Fonte3 As Font
    Private _PaginaAtual As Integer = 0
    Private _Solicitacoes As IList(Of ISolicitacaoDeAudiencia)

    Public Sub New(ByVal Solicitacoes As IList(Of ISolicitacaoDeAudiencia))
        _Solicitacoes = Solicitacoes
        _Fonte1 = New Font(Font.TIMES_ROMAN, 10)
        _Fonte2 = New Font(Font.TIMES_ROMAN, 10, Font.BOLD)
        _Fonte3 = New Font(Font.TIMES_ROMAN, 10)
    End Sub

    Public Function GerePDFSolicitacoesEmAberto() As String
        Dim NomeDoPDF As String
        Dim CaminhoDoPDF As String
        Dim Escritor As PdfWriter

        NomeDoPDF = String.Concat(Now.ToString("yyyyMMddhhmmss"), ".pdf")
        CaminhoDoPDF = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_LOADS)

        _documento = New Document(PageSize.A4)
        '_documento.SetMargins(5.6692913385826769, 2.8346456692913389, 0, 0)
        Escritor = PdfWriter.GetInstance(_documento, New FileStream(Path.Combine(CaminhoDoPDF, NomeDoPDF), FileMode.Create))
        Escritor.PageEvent = New Ouvinte()
        Escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE)
        Escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE)
        _documento.Open()

        EscrevaCabecalho()
        EscrevaSolicitacoes()
        EscrevaRodape()

        _documento.Close()

        Return NomeDoPDF
    End Function

    Private Sub EscrevaCabecalho()
        'Dim NumeroDaNota As Paragraph
        'Dim DataDaEmissao As Paragraph
        'Dim NaturezaDaOperacao As Paragraph

        'NumeroDaNota = New Paragraph(14.173228346456691, _lancamentoCorrente.Numero.Value.ToString("0000"), _Fonte1)
        'NumeroDaNota.IndentationLeft = 532.86
        '_documento.Add(NumeroDaNota)

        'DataDaEmissao = New Paragraph(62.64, _lancamentoCorrente.DataDeLancamento.ToString("dd/MM/yyyy"), _Fonte1)
        'DataDaEmissao.IndentationLeft = 339.71
        '_documento.Add(DataDaEmissao)

        'Dim TextoNaturezaDaOperacao As String = String.Empty

        'If Not String.IsNullOrEmpty(_lancamentoCorrente.NaturezaDaOperacao) Then
        '    TextoNaturezaDaOperacao = _lancamentoCorrente.NaturezaDaOperacao
        'End If

        'NaturezaDaOperacao = New Paragraph(14.4, TextoNaturezaDaOperacao, _Fonte1)
        'NaturezaDaOperacao.IndentationLeft = 339.71
        '_documento.Add(NaturezaDaOperacao)
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

    Private Sub EscrevaSolicitacoes()
        Dim Tabela As Table = New Table(5)

        Tabela.Widths = New Single() {70, 10, 20}
        Tabela.Padding = 1
        Tabela.Spacing = 1

        Tabela.AddCell(Me.CrieCelula("Código", _Fonte1, Cell.ALIGN_LEFT, 13, True))
        Tabela.AddCell(Me.CrieCelula("Data", _Fonte1, Cell.ALIGN_LEFT, 13, True))
        Tabela.AddCell(Me.CrieCelula("Assunto", _Fonte1, Cell.ALIGN_LEFT, 13, True))
        Tabela.AddCell(Me.CrieCelula("Descrição", _Fonte1, Cell.ALIGN_LEFT, 13, True))
        Tabela.AddCell(Me.CrieCelula("Contato", _Fonte1, Cell.ALIGN_LEFT, 13, True))

        For Each Solicitacao As ISolicitacaoDeAudiencia In _Solicitacoes
            Tabela.AddCell(Me.CrieCelula(Solicitacao.Codigo.ToString, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(Me.CrieCelula(Solicitacao.DataDaSolicitacao.ToString("dd/MM/yyyy HH:mm:ss").ToString, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(Me.CrieCelula(Solicitacao.Assunto, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(Me.CrieCelula(Solicitacao.Descricao, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(Me.CrieCelula(Solicitacao.Contato.Pessoa.Nome, _Fonte1, Cell.ALIGN_LEFT, 13, False))
        Next

        _documento.Add(Tabela)
    End Sub

    Private Sub EscrevaRodape()

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
