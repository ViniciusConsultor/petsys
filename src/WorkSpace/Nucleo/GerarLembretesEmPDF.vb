Imports iTextSharp.text
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Componentes.Web
Imports iTextSharp.text.pdf
Imports System.IO
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas

Public Class GerarLembretesEmPDF

    Private _documento As Document
    Private _Fonte1 As Font
    Private _FonteRodape As Font
    Private _Lembretes As IList(Of ILembrete)
    Private _FonteNomeProprietarioCabecalho As Font
    Private _FonteHorario As Font
    Private _FonteDescricaoCompromissos As Font
    Private NomeDoPDF As String
    Private _ConfiguracaoDeAgendaDoSistema As IConfiguracaoDeAgendaDoSistema

    Public Sub New(ByVal Lembretes As IList(Of ILembrete))
        _Lembretes = Lembretes
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

    Public Function GerePDF(ByVal MostraAssunto As Boolean, ByVal MostraDescricao As Boolean) As String
        Dim LembreteAnterior As ILembrete = Nothing

        Dim Configuracao As IConfiguracaoDoSistema

        Using Servico As IServicoDeConfiguracoesDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracoesDoSistema)()
            Configuracao = Servico.ObtenhaConfiguracaoDoSistema
        End Using

        If Configuracao Is Nothing Then Throw New BussinesException("Os lembretes não podem ser impressos pois a configuração de agenda do sistema ainda não foi configurada.")

        _ConfiguracaoDeAgendaDoSistema = Configuracao.ConfiguracaoDeAgendaDoSistema

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

        Frase = New Phrase(_ConfiguracaoDeAgendaDoSistema.TextoCabelhoDeLembretes & Lembrete.Proprietario.Nome & vbLf, _FonteNomeProprietarioCabecalho)
        Frase.Add(New Phrase(UtilitarioDeData.ObtenhaDiaDaSemanaDiaDoMesMesAnoEmStr(Lembrete.Inicio), _Fonte1))

        Cabecalho = New HeaderFooter(Frase, False)
        Cabecalho.Alignment = HeaderFooter.ALIGN_RIGHT

        If Not _ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoCabecalhoDeLembretes Then
            Cabecalho.Border = HeaderFooter.NO_BORDER
        End If

        _documento.Header = Cabecalho
    End Sub

    Private Sub EscrevaLembretes(ByVal Lembrete As ILembrete, _
                                 ByVal MostraAssunto As Boolean, _
                                 ByVal MostraDescricao As Boolean)
        Dim ParagradoEmBranco As Paragraph
        Dim Flag As Boolean = False

        ParagradoEmBranco = New Paragraph(" ")
        _documento.Add(ParagradoEmBranco)

        Dim Hora As Paragraph

        Hora = New Paragraph(Lembrete.Inicio.ToString("HH:mm") & "h", _FonteHorario)
        _documento.Add(Hora)

        If MostraAssunto Then
            Dim Assunto As Paragraph

            Assunto = New Paragraph(0, String.Concat("Assunto: ", Lembrete.Assunto), _FonteDescricaoCompromissos)
            Assunto.IndentationLeft = 56.7
            _documento.Add(Assunto)
            Flag = True
        End If

        If MostraDescricao AndAlso Not String.IsNullOrEmpty(Lembrete.Descricao) Then
            Dim Descricao As Paragraph

            If Not Flag Then
                Descricao = New Paragraph(0, String.Concat("Descrição: ", Lembrete.Descricao), _FonteDescricaoCompromissos)
            Else
                Descricao = New Paragraph(String.Concat("Descrição: ", Lembrete.Descricao), _FonteDescricaoCompromissos)
            End If

            Descricao.IndentationLeft = 56.7
            _documento.Add(Descricao)
            Flag = False
        End If
    End Sub

    Private Sub EscrevaRodape()
        Dim Rodape As HeaderFooter

        Dim Texto As New StringBuilder

        Texto.AppendLine(String.Concat("Impressão em: ", Now.ToString("dd/MM/yyyy HH:mm:ss")))

        Rodape = New HeaderFooter(New Phrase(Texto.ToString, _FonteRodape), False)
        Rodape.Alignment = HeaderFooter.ALIGN_RIGHT

        If Not _ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoCabecalhoDeLembretes Then
            Rodape.Border = HeaderFooter.NO_BORDER
        End If

        _documento.Footer = Rodape
    End Sub

End Class
