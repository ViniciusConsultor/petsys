Imports iTextSharp.text
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Componentes.Web
Imports iTextSharp.text.pdf
Imports System.IO
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports iTextSharp.text.rtf
Imports iTextSharp.text.pdf.draw

Public Class ImpressorDeLembretes

    Private _documento As Document
    Private _Fonte1 As Font
    Private _FonteRodape As Font
    Private _Lembretes As IList(Of ILembrete)
    Private _FonteNomeProprietarioCabecalho As Font
    Private _FonteHorario As Font
    Private _FonteDescricaoCompromissos As Font
    Private NomeDoArquivoDeSaida As String
    Private _ConfiguracaoDeAgendaDoSistema As IConfiguracaoDeAgendaDoSistema

    Public Sub New(ByVal Lembretes As IList(Of ILembrete), _
                   ByVal FormatoDeSaida As TipoDeFormatoDeSaidaDoDocumento)
        _Lembretes = Lembretes
        _Fonte1 = New Font(Font.TIMES_ROMAN, 10)
        _FonteRodape = New Font(Font.TIMES_ROMAN, 10, Font.ITALIC)

        _FonteNomeProprietarioCabecalho = New Font(Font.TIMES_ROMAN, 12, Font.BOLDITALIC)
        _FonteHorario = New Font(Font.TIMES_ROMAN, 10, Font.BOLD)
        _FonteDescricaoCompromissos = New Font(Font.TIMES_ROMAN, 10)

        _documento = New Document(PageSize.A4)
        CriaEscritor(FormatoDeSaida)
    End Sub

    Private Sub CriaEscritor(ByVal FormatoDeSaida As TipoDeFormatoDeSaidaDoDocumento)
        If FormatoDeSaida.Equals(TipoDeFormatoDeSaidaDoDocumento.PDF) Then
            CriaEscritorPDF()
        ElseIf FormatoDeSaida.Equals(TipoDeFormatoDeSaidaDoDocumento.RTF) Then
            CriaEscritorRTF()
        End If
    End Sub

    Private Sub CriaEscritorPDF()
        Dim Escritor As PdfWriter
        Dim Caminho As String

        NomeDoArquivoDeSaida = String.Concat(Now.ToString("yyyyMMddhhmmss"), ".pdf")
        Caminho = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_LOADS)

        Escritor = PdfWriter.GetInstance(_documento, New FileStream(Path.Combine(Caminho, NomeDoArquivoDeSaida), FileMode.Create))
        Escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE)
        Escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE)
    End Sub

    Private Sub CriaEscritorRTF()
        Dim Escritor As RtfWriter2
        Dim Caminho As String

        NomeDoArquivoDeSaida = String.Concat(Now.ToString("yyyyMMddhhmmss"), ".rtf")
        Caminho = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_LOADS)

        Escritor = RtfWriter2.GetInstance(_documento, New FileStream(Path.Combine(Caminho, NomeDoArquivoDeSaida), FileMode.Create))
    End Sub

    Public Function Gere(ByVal MostraAssunto As Boolean, ByVal MostraDescricao As Boolean) As String
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
        Return NomeDoArquivoDeSaida
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

        Dim CaracterTAB = New Chunk(New VerticalPositionMark(), 50)
        Dim CorpoLembrete As Phrase

        CorpoLembrete = New Phrase

        CorpoLembrete.Add(New Chunk(Lembrete.Inicio.ToString("HH") & "h" & Lembrete.Inicio.ToString("mm") & "min", _FonteHorario))
        CorpoLembrete.Add(CaracterTAB)

        If MostraAssunto Then
            CorpoLembrete.Add(New Chunk(String.Concat("Assunto: ", Lembrete.Assunto), _FonteDescricaoCompromissos))
            CorpoLembrete.Add(Chunk.NEWLINE)
            Flag = True
        End If

        If MostraDescricao AndAlso Not String.IsNullOrEmpty(Lembrete.Descricao) Then
            If Flag Then
                CorpoLembrete.Add(CaracterTAB)
            End If

            CorpoLembrete.Add(New Chunk(String.Concat("Descrição: ", Lembrete.Descricao), _FonteDescricaoCompromissos))
        End If

        _documento.Add(CorpoLembrete)
    End Sub

    Private Sub EscrevaRodape()
        Dim Rodape As HeaderFooter
        Dim DataEHoraAtual As Date = Now

        Dim Texto As String

        Texto = String.Concat("Impressão em: ", DataEHoraAtual.ToString("dd/MM/yyyy"), " às ", DataEHoraAtual.ToString("HH"), "h", DataEHoraAtual.ToString("mm"), "m", DataEHoraAtual.ToString("ss"), "s")

        Rodape = New HeaderFooter(New Phrase(Texto, _FonteRodape), False)
        Rodape.Alignment = HeaderFooter.ALIGN_RIGHT

        If Not _ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoCabecalhoDeLembretes Then
            Rodape.Border = HeaderFooter.NO_BORDER
        End If

        _documento.Footer = Rodape
    End Sub

End Class
