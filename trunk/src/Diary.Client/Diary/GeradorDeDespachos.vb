Imports Diary.Interfaces.Negocio
Imports iTextSharp.text
Imports System.IO
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports iTextSharp.text.rtf

Public Class GeradorDeDespachos

    Private _documento As Document
    Private _Fonte1 As Font
    Private _Fonte2 As Font
    Private _Fonte3 As Font
    Private _Despachos As IList(Of IDespacho)

    Public Sub New(ByVal Despachos As IList(Of IDespacho))
        _Despachos = Despachos
        _Fonte1 = New Font(Font.TIMES_ROMAN, 10)
        _Fonte2 = New Font(Font.TIMES_ROMAN, 12, Font.BOLD)
        _Fonte3 = New Font(Font.TIMES_ROMAN, 14, Font.BOLDITALIC)
    End Sub

    Public Function GereRelatorioDeDespachos() As String
        Dim Escritor As RtfWriter2
        Dim Caminho As String
        Dim NomeDoArquivoDeSaida As String

        _documento = New Document(PageSize.A4.Rotate)
        NomeDoArquivoDeSaida = String.Concat(Now.ToString("yyyyMMddhhmmss"), ".rtf")
        Caminho = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_LOADS)
        Escritor = RtfWriter2.GetInstance(_documento, New FileStream(Path.Combine(Caminho, NomeDoArquivoDeSaida), FileMode.Create))
        EscrevaDespachos()
        EscrevaRodape()
        _documento.Close()

        Return NomeDoArquivoDeSaida
    End Function

    Private Sub EscrevaDespachos()
        Dim Tabela As Table = New Table(4)

        Tabela.Padding = 1
        Tabela.Spacing = 1
        Tabela.Width = 100%

        Tabela.AddCell(iTextSharpUtilidades.CrieCelula("Data e hora", _Fonte2, Cell.ALIGN_LEFT, 13, True))
        Tabela.AddCell(iTextSharpUtilidades.CrieCelula("Alvo do despacho", _Fonte2, Cell.ALIGN_LEFT, 13, True))
        Tabela.AddCell(iTextSharpUtilidades.CrieCelula("Tipo do despacho", _Fonte2, Cell.ALIGN_LEFT, 13, True))
        Tabela.AddCell(iTextSharpUtilidades.CrieCelula("Solicitante", _Fonte2, Cell.ALIGN_LEFT, 13, True))

        Dim Solicitacao As ISolicitacao = Nothing

        For Each Despacho As IDespacho In _Despachos
            Tabela.AddCell(iTextSharpUtilidades.CrieCelula(Despacho.DataDoDespacho.ToString("dd/MM/yyyy HH:mm:ss").ToString, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(iTextSharpUtilidades.CrieCelula(Despacho.Alvo.Nome, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(iTextSharpUtilidades.CrieCelula(Despacho.Tipo.Descricao, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Tabela.AddCell(iTextSharpUtilidades.CrieCelula(Despacho.Solicitante.Nome, _Fonte1, Cell.ALIGN_LEFT, 13, False))
            Solicitacao = Despacho.Solicitacao
        Next

        Dim Cabecalho As HeaderFooter
        Dim Frase As Phrase

        Frase = New Phrase("Despachos da solicitação " & Solicitacao.Codigo.ToString & vbLf, _Fonte3)

        Cabecalho = New HeaderFooter(Frase, False)
        Cabecalho.Alignment = HeaderFooter.ALIGN_RIGHT
        _documento.Header = Cabecalho
        _documento.Open()
        _documento.Add(Tabela)
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
