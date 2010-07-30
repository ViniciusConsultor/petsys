Imports iTextSharp.text
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Componentes.Web
Imports iTextSharp.text.pdf
Imports System.IO
Imports Compartilhados

Public Class GerarLembretesEmPDF

    Private _documento As Document
    Private _Fonte1 As Font
    Private _Fonte2 As Font
    Private _Fonte3 As Font
    Private _PaginaAtual As Integer = 0
    Private _Lembretes As IList(Of ILembrete)

    Private _FonteNomeProprietarioCabecalho As Font
    Private _FonteHorario As Font
    Private _FonteDescricaoCompromissos As Font
    Private NomeDoPDF As String

    Public Sub New(ByVal Lembretes As IList(Of ILembrete))
        _Lembretes = Lembretes
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
        Escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE)
        Escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE)
    End Sub

    Public Function GerePDF(ByVal MostraAssunto As Boolean, ByVal MostraDescricao As Boolean) As String
        Dim LembreteAnterior As ILembrete = Nothing

        For Each Lembrete As ILembrete In _Lembretes
            'Primeira vez
            If LembreteAnterior Is Nothing Then
                EscrevaCabecalho(Lembrete)
                EscrevaRodape()
                _documento.Open()
                'Demais vezes testa se tarefa atual tem data maior que o anterior. 
            ElseIf CLng(LembreteAnterior.Inicio.ToString("yyyyMMdd")) < CLng(Lembrete.Inicio.ToString("yyyyMMdd")) Then
                EscrevaCabecalho(Lembrete)
                _documento.NewPage()
            End If

            EscrevaLembretes(Lembrete, MostraAssunto, MostraDescricao)
            LembreteAnterior = Lembrete
        Next

        _documento.Close()
        Return NomeDoPDF
    End Function

    Private Sub EscrevaCabecalho(ByVal Lembrete As ILembrete)
        Dim Cabecalho As HeaderFooter
        Dim Frase As Phrase

        Frase = New Phrase("Lembretes " & Lembrete.Proprietario.Nome & vbLf, _FonteNomeProprietarioCabecalho)
        Frase.Add(New Phrase(UtilitarioDeData.ObtenhaDiaDaSemanaDiaDoMesMesAnoEmStr(Lembrete.Inicio), _Fonte1))

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

    Private Sub EscrevaLembretes(ByVal Lembrete As ILembrete, _
                                 ByVal MostraAssunto As Boolean, _
                                 ByVal MostraDescricao As Boolean)
        Dim ParagradoEmBranco As Paragraph

        ParagradoEmBranco = New Paragraph(" ")
        _documento.Add(ParagradoEmBranco)

        Dim Hora As Paragraph

        Hora = New Paragraph(Lembrete.Inicio.ToString("HH:mm") & "h", _FonteHorario)
        _documento.Add(Hora)

        If MostraAssunto Then
            Dim Assunto As Paragraph

            Assunto = New Paragraph(String.Concat("Assunto: ", Lembrete.Assunto), _FonteHorario)
            Assunto.IndentationLeft = 56.7
            _documento.Add(Assunto)
        End If

        If MostraDescricao AndAlso Not String.IsNullOrEmpty(Lembrete.Descricao) Then
            Dim Descricao As Paragraph

            Descricao = New Paragraph(String.Concat("Descrição: ", Lembrete.Descricao), _FonteHorario)
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

End Class
