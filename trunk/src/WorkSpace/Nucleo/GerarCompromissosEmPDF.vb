Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos

Public Class GerarCompromissosEmPDF

    Private _documento As Document
    Private _Fonte1 As Font
    Private _FonteRodape As Font
    Private _Compromissos As IList(Of ICompromisso)

    Private _FonteNomeProprietarioCabecalho As Font
    Private _FonteHorario As Font
    Private _FonteDescricaoCompromissos As Font
    Private _ConfiguracaoDeAgendaDoSistema As IConfiguracaoDeAgendaDoSistema
    Private NomeDoPDF As String

    Public Sub New(ByVal Compromissos As IList(Of ICompromisso))
        _Compromissos = Compromissos
        _Fonte1 = New Font(Font.TIMES_ROMAN, 10)
        _FonteRodape = New Font(Font.TIMES_ROMAN, 10, Font.ITALIC)

        _FonteNomeProprietarioCabecalho = New Font(Font.TIMES_ROMAN, 12, Font.BOLDITALIC)
        _FonteHorario = New Font(Font.TIMES_ROMAN, 10, Font.BOLD)
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

    Public Function GerePDF(ByVal MostraAssunto As Boolean, ByVal MostraLocal As Boolean, ByVal MostraDescricao As Boolean) As String
        Dim CompromissoAnterior As ICompromisso = Nothing

        Dim Configuracao As IConfiguracaoDoSistema

        Using Servico As IServicoDeConfiguracoesDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracoesDoSistema)()
            Configuracao = Servico.ObtenhaConfiguracaoDoSistema
        End Using

        If Configuracao Is Nothing Then Throw New BussinesException("Os compromissos não podem ser impressos pois a configuração de agenda do sistema ainda não foi configurada.")

        _ConfiguracaoDeAgendaDoSistema = Configuracao.ConfiguracaoDeAgendaDoSistema

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

        Frase = New Phrase(_ConfiguracaoDeAgendaDoSistema.TextoCabecalhoDeCompromissos & Compromisso.Proprietario.Nome & vbLf, _FonteNomeProprietarioCabecalho)
        Frase.Add(New Phrase(UtilitarioDeData.ObtenhaDiaDaSemanaDiaDoMesMesAnoEmStr(Compromisso.Inicio), _Fonte1))

        Cabecalho = New HeaderFooter(Frase, False)
        Cabecalho.Alignment = HeaderFooter.ALIGN_RIGHT

        If Not _ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoCabecalhoDeCompromissos Then
            Cabecalho.Border = HeaderFooter.NO_BORDER
        End If

        _documento.Header = Cabecalho
    End Sub

    Private Sub EscrevaCompromisso(ByVal Compromisso As ICompromisso, _
                                   ByVal MostraAssunto As Boolean, _
                                   ByVal MostraLocal As Boolean, _
                                   ByVal MostraDescricao As Boolean)
        Dim ParagradoEmBranco As Paragraph
        Dim Flag As Boolean = False

        ParagradoEmBranco = New Paragraph(" ")
        _documento.Add(ParagradoEmBranco)

        Dim Hora As Paragraph

        Hora = New Paragraph(Compromisso.Inicio.ToString("HH:mm") & "h", _FonteHorario)
        _documento.Add(Hora)

        If MostraAssunto Then
            Dim Assunto As Paragraph

            Assunto = New Paragraph(0, String.Concat("Assunto: ", Compromisso.Assunto), _FonteDescricaoCompromissos)
            Assunto.IndentationLeft = 56.7
            _documento.Add(Assunto)
            Flag = True
        End If

        If MostraLocal AndAlso Not String.IsNullOrEmpty(Compromisso.Local) Then
            Dim Local As Paragraph

            If Not Flag Then
                Local = New Paragraph(0, String.Concat("Local: ", Compromisso.Local), _FonteDescricaoCompromissos)
            Else
                Local = New Paragraph(String.Concat("Local: ", Compromisso.Local), _FonteDescricaoCompromissos)
            End If

            Local.IndentationLeft = 56.7
            _documento.Add(Local)
            Flag = True
        End If

        If MostraDescricao AndAlso Not String.IsNullOrEmpty(Compromisso.Descricao) Then
            Dim Descricao As Paragraph

            If Not Flag Then
                Descricao = New Paragraph(0, String.Concat("Descrição: ", Compromisso.Descricao), _FonteDescricaoCompromissos)
            Else
                Descricao = New Paragraph(String.Concat("Descrição: ", Compromisso.Descricao), _FonteDescricaoCompromissos)
            End If

            Descricao.IndentationLeft = 56.7
            _documento.Add(Descricao)
        End If
    End Sub

    Private Sub EscrevaRodape()
        Dim Rodape As HeaderFooter

        Dim Texto As New StringBuilder

        Texto.AppendLine(String.Concat("Impressão em: ", Now.ToString("dd/MM/yyyy HH:mm:ss")))

        Rodape = New HeaderFooter(New Phrase(Texto.ToString, _FonteRodape), False)

        If Not _ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoRodapeDeCompromissos Then
            Rodape.Border = HeaderFooter.NO_BORDER
        End If

        Rodape.Alignment = HeaderFooter.ALIGN_RIGHT
        _documento.Footer = Rodape
    End Sub

End Class
