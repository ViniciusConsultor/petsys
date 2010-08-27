﻿Imports Diary.Interfaces.Negocio
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Fabricas
Imports Compartilhados

Public Class GeradorDeSolicitacoesDeConviteEmPDF

    Private _documento As Document
    Private _Fonte1 As Font
    Private _Fonte2 As Font
    Private _Fonte3 As Font
    Private _Fonte4 As Font
    Private _PaginaAtual As Integer = 0
    Private _Solicitacoes As IList(Of ISolicitacaoDeConvite)

    Public Sub New(ByVal Solicitacoes As IList(Of ISolicitacaoDeConvite))
        _Solicitacoes = Solicitacoes
        _Fonte1 = New Font(Font.TIMES_ROMAN, 10)
        _Fonte2 = New Font(Font.TIMES_ROMAN, 10, Font.BOLD)
        _Fonte3 = New Font(Font.TIMES_ROMAN, 14, Font.BOLDITALIC)
        _Fonte4 = New Font(Font.TIMES_ROMAN, 10, Font.BOLDITALIC)
    End Sub

    Public Function GerePDFSolicitacoesEmAberto() As String
        Dim NomeDoPDF As String
        Dim CaminhoDoPDF As String
        Dim Escritor As PdfWriter

        NomeDoPDF = String.Concat(Now.ToString("yyyyMMddhhmmss"), ".pdf")
        CaminhoDoPDF = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_LOADS)

        _documento = New Document(PageSize.A4.Rotate)

        Escritor = PdfWriter.GetInstance(_documento, New FileStream(Path.Combine(CaminhoDoPDF, NomeDoPDF), FileMode.Create))
        Escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE)
        Escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE)

        EscrevaCabecalho()
        EscrevaRodape()

        _documento.Open()
        EscrevaSolicitacoes()
        _documento.Close()

        Return NomeDoPDF
    End Function

    Private Sub EscrevaCabecalho()
        Dim Cabecalho As HeaderFooter
        Dim Frase As Phrase

        Frase = New Phrase("Solicitações de convite " & vbLf, _Fonte3)

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

    Private Sub EscrevaSolicitacoes()
        Dim Tabela As Table = New Table(8)
        Tabela.Widths = New Single() {85, 85, 120, 300, 400, 400, 200, 120}

        Tabela.Width = 100%
        Tabela.Padding = 1
        Tabela.Spacing = 1

        Tabela.AddCell(Me.CrieCelula("Parecer", _Fonte2, Cell.ALIGN_CENTER, 13, True))
        Tabela.AddCell(Me.CrieCelula("Código", _Fonte2, Cell.ALIGN_CENTER, 13, True))
        Tabela.AddCell(Me.CrieCelula("Data e hora", _Fonte2, Cell.ALIGN_CENTER, 13, True))
        Tabela.AddCell(Me.CrieCelula("Local", _Fonte2, Cell.ALIGN_CENTER, 13, True))
        Tabela.AddCell(Me.CrieCelula("Descrição", _Fonte2, Cell.ALIGN_CENTER, 13, True))
        Tabela.AddCell(Me.CrieCelula("Observação", _Fonte2, Cell.ALIGN_CENTER, 13, True))
        Tabela.AddCell(Me.CrieCelula("Contato", _Fonte2, Cell.ALIGN_CENTER, 13, True))
        Tabela.AddCell(Me.CrieCelula("Data da solicitação", _Fonte2, Cell.ALIGN_CENTER, 13, True))

        For Each Solicitacao As ISolicitacaoDeConvite In _Solicitacoes
            Tabela.AddCell(Me.CrieCelula("", _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(Me.CrieCelula(Solicitacao.Codigo.ToString, _Fonte1, Cell.ALIGN_CENTER, 13, False))
            Tabela.AddCell(Me.CrieCelula(Solicitacao.DataEHorario.ToString("dd/MM/yyyy HH:mm:ss").ToString, _Fonte1, Cell.ALIGN_CENTER, 13, False))
            Tabela.AddCell(Me.CrieCelula(Solicitacao.Local, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(Me.CrieCelula(Solicitacao.Descricao, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(Me.CrieCelula(Solicitacao.Observacao, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(Me.CrieCelula(Solicitacao.Contato.Pessoa.Nome, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(Me.CrieCelula(Solicitacao.DataDaSolicitacao.ToString("dd/MM/yyyy HH:mm:ss").ToString, _Fonte1, Cell.ALIGN_CENTER, 13, False))
        Next

        _documento.Add(Tabela)
    End Sub

    Private Sub EscrevaRodape()
        Dim Rodape As HeaderFooter

        Dim Texto As New StringBuilder

        Texto.AppendLine(String.Concat("Impressão em: ", Now.ToString("dd/MM/yyyy HH:mm:ss")))

        Rodape = New HeaderFooter(New Phrase(Texto.ToString, _Fonte4), False)

        Rodape.Alignment = HeaderFooter.ALIGN_RIGHT
        _documento.Footer = Rodape
    End Sub

End Class