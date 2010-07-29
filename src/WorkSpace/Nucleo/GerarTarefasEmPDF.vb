﻿Imports iTextSharp.text
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Componentes.Web
Imports iTextSharp.text.pdf
Imports System.IO
Imports Compartilhados

Public Class GerarTarefasEmPDF

    Private _documento As Document
    Private _Fonte1 As Font
    Private _Fonte2 As Font
    Private _Fonte3 As Font
    Private _PaginaAtual As Integer = 0
    Private _Tarefas As IList(Of ITarefa)

    Private _FonteNomeProprietarioCabecalho As Font
    Private _FonteHorario As Font
    Private _FonteDescricaoCompromissos As Font
    Private NomeDoPDF As String

    Public Sub New(ByVal Tarefas As IList(Of ITarefa))
        _Tarefas = Tarefas
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
        Dim TarefaAnterior As ITarefa = Nothing

        For Each Tarefa As ITarefa In _Tarefas
            'Primeira vez
            If TarefaAnterior Is Nothing Then
                EscrevaCabecalho(Tarefa.Proprietario)
                EscrevaRodape()
                _documento.Open()
                EscrevaCabecalhoDasTarefas(Tarefa.DataDeInicio)
                'Demais vezes testa se tarefa atual tem data maior que o anterior. 
            ElseIf CLng(TarefaAnterior.DataDeInicio.ToString("yyyyMMdd")) < CLng(TarefaAnterior.DataDeInicio.ToString("yyyyMMdd")) Then
                EscrevaCabecalhoDasTarefas(Tarefa.DataDeInicio)
            End If

            EscrevaTarefa(Tarefa, MostraAssunto, MostraDescricao)
            TarefaAnterior = Tarefa
        Next

        _documento.Close()
        Return NomeDoPDF
    End Function

    Private Sub EscrevaCabecalhoDasTarefas(ByVal DataDeInicio As Date)
        Dim Paragrafo As Paragraph

        Paragrafo = New Paragraph(UtilitarioDeData.ObtenhaDiaDaSemanaDiaDoMesMesAnoEmStr(DataDeInicio), _Fonte1)
        _documento.Add(Paragrafo)
    End Sub

    Private Sub EscrevaCabecalho(ByVal Proprietario As IPessoaFisica)
        Dim Cabecalho As HeaderFooter
        Dim Frase As Phrase

        Frase = New Phrase("Tarefas " & Proprietario.Nome & vbLf, _FonteNomeProprietarioCabecalho)

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

    Private Sub EscrevaTarefa(ByVal Tarefa As ITarefa, _
                              ByVal MostraAssunto As Boolean, _
                              ByVal MostraDescricao As Boolean)
        Dim ParagradoEmBranco As Paragraph

        ParagradoEmBranco = New Paragraph(" ")
        _documento.Add(ParagradoEmBranco)

        Dim Hora As Paragraph

        Hora = New Paragraph(Tarefa.DataDeInicio.ToString("HH:mm") & "h", _FonteHorario)
        _documento.Add(Hora)

        If MostraAssunto Then
            Dim Assunto As Paragraph

            Assunto = New Paragraph(String.Concat("Assunto: ", Tarefa.Assunto), _FonteHorario)
            Assunto.IndentationLeft = 56.7
            _documento.Add(Assunto)
        End If

        If MostraDescricao AndAlso Not String.IsNullOrEmpty(Tarefa.Descricao) Then
            Dim Descricao As Paragraph

            Descricao = New Paragraph(String.Concat("Descrição: ", Tarefa.Descricao), _FonteHorario)
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