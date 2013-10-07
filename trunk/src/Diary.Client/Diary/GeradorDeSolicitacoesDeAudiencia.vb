﻿Imports Diary.Interfaces.Negocio
Imports iTextSharp.text
Imports System.IO
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports iTextSharp.text.rtf

Public Class GeradorDeSolicitacoesDeAudiencia

    Private _documento As Document
    Private _Fonte1 As Font
    Private _Fonte2 As Font
    Private _Fonte3 As Font
    Private _Fonte4 As Font
    Private _Solicitacoes As IList(Of ISolicitacaoDeAudiencia)

    Public Sub New(ByVal Solicitacoes As IList(Of ISolicitacaoDeAudiencia))
        _Solicitacoes = Solicitacoes
        _Fonte1 = New Font(Font.TIMES_ROMAN, 10)
        _Fonte2 = New Font(Font.TIMES_ROMAN, 10, Font.BOLD)
        _Fonte3 = New Font(Font.TIMES_ROMAN, 14, Font.BOLDITALIC)
        _Fonte4 = New Font(Font.TIMES_ROMAN, 10, Font.BOLDITALIC)
    End Sub

    Public Function GereRelatorioDeSolicitacoes() As String
        Dim Escritor As RtfWriter2
        Dim Caminho As String
        Dim NomeDoArquivoDeSaida As String

        NomeDoArquivoDeSaida = String.Concat(Now.ToString("yyyyMMddhhmmss"), ".rtf")
        Caminho = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_LOADS)
        _documento = New Document(PageSize.A4.Rotate)
        Escritor = RtfWriter2.GetInstance(_documento, New FileStream(Path.Combine(Caminho, NomeDoArquivoDeSaida), FileMode.Create))
        EscrevaCabecalho()
        EscrevaRodape()
        _documento.Open()
        EscrevaSolicitacoes()
        _documento.Close()
        Return NomeDoArquivoDeSaida
    End Function

    Private Sub EscrevaCabecalho()
        Dim Cabecalho As HeaderFooter
        Dim Frase As Phrase

        Frase = New Phrase("Solicitações de audiência " & vbLf, _Fonte3)

        Cabecalho = New HeaderFooter(Frase, False)
        Cabecalho.Border = HeaderFooter.NO_BORDER
        Cabecalho.Alignment = HeaderFooter.ALIGN_RIGHT
        _documento.Header = Cabecalho
    End Sub

    Private Sub EscrevaSolicitacoes()
        Dim Tabela As Table = New Table(7)

        Tabela.Widths = New Single() {85, 120, 400, 300, 400, 120, 85}

        Tabela.Padding = 1
        Tabela.Spacing = 1
        Tabela.Width = 100%

        Tabela.AddCell(iTextSharpUtilidades.CrieCelula("Código", _Fonte2, Cell.ALIGN_CENTER, 13, True))
        Tabela.AddCell(iTextSharpUtilidades.CrieCelula("Data e hora do cadastro", _Fonte2, Cell.ALIGN_CENTER, 13, True))
        Tabela.AddCell(iTextSharpUtilidades.CrieCelula("Contato", _Fonte2, Cell.ALIGN_CENTER, 13, True))
        Tabela.AddCell(iTextSharpUtilidades.CrieCelula("Pauta", _Fonte2, Cell.ALIGN_CENTER, 13, True))
        Tabela.AddCell(iTextSharpUtilidades.CrieCelula("Descrição", _Fonte2, Cell.ALIGN_CENTER, 13, True))
        Tabela.AddCell(iTextSharpUtilidades.CrieCelula("Possui despacho?", _Fonte2, Cell.ALIGN_CENTER, 13, True))
        Tabela.AddCell(iTextSharpUtilidades.CrieCelula("Parecer", _Fonte2, Cell.ALIGN_CENTER, 13, True))

        For Each Solicitacao As ISolicitacaoDeAudiencia In _Solicitacoes
            Tabela.AddCell(iTextSharpUtilidades.CrieCelula(Solicitacao.Codigo.ToString, _Fonte1, Cell.ALIGN_CENTER, 13, False))
            Tabela.AddCell(iTextSharpUtilidades.CrieCelula(Solicitacao.DataDaSolicitacao.ToString("dd/MM/yyyy HH:mm:ss").ToString, _Fonte1, Cell.ALIGN_CENTER, 13, False))
            Tabela.AddCell(iTextSharpUtilidades.CrieCelula(Solicitacao.Contato.Pessoa.Nome, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(iTextSharpUtilidades.CrieCelula(Solicitacao.Assunto, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(iTextSharpUtilidades.CrieCelula(Solicitacao.Descricao, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(iTextSharpUtilidades.CrieCelula(IIf(Solicitacao.TemDespacho, "SIM", "NÃO").ToString, _Fonte1, Cell.ALIGN_CENTER, 13, False))
            Tabela.AddCell(iTextSharpUtilidades.CrieCelula(" ", _Fonte1, Cell.ALIGN_LEFT, 13, False))
        Next

        _documento.Add(Tabela)
        Dim LinhaQuantidadeDeItens As New Chunk(String.Concat("Quantidade de solicitações : ", _Solicitacoes.Count), _Fonte4)
        _documento.Add(LinhaQuantidadeDeItens)
    End Sub

    Private Sub EscrevaRodape()
        Dim Rodape As HeaderFooter
        Dim Texto As New StringBuilder

        Texto.AppendLine(String.Concat("Impressão em: ", Now.ToString("dd/MM/yyyy HH:mm:ss")))

        Rodape = New HeaderFooter(New Phrase(Texto.ToString, _Fonte4), False)
        Rodape.Border = HeaderFooter.NO_BORDER
        Rodape.Alignment = HeaderFooter.ALIGN_RIGHT
        _documento.Footer = Rodape
    End Sub

End Class