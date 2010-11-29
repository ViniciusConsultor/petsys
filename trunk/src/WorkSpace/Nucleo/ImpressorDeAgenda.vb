Imports iTextSharp.text.pdf
Imports iTextSharp.text.rtf
Imports iTextSharp.text
Imports System.IO
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports iTextSharp.text.pdf.draw

Public Class ImpressorDeAgenda

    Private _documento As Document
    Private _Fonte1 As Font
    Private _FonteRodape As Font
    Private _Compromissos As IList(Of ICompromisso)
    Private _Tarefas As IList(Of ITarefa)
    Private _Lembretes As IList(Of ILembrete)

    Private _FonteNomeProprietarioCabecalho As Font
    Private _FonteHorario As Font
    Private _ConfiguracaoDeAgendaDoSistema As IConfiguracaoDeAgendaDoSistema
    Private NomeDoArquivoDeSaida As String

    Public Sub New(ByVal Compromissos As IList(Of ICompromisso), _
                   ByVal Lembretes As IList(Of ILembrete), _
                   ByVal Tarefas As IList(Of ITarefa), _
                   ByVal FormatoDeSaida As TipoDeFormatoDeSaidaDoDocumento)
        _Compromissos = Compromissos
        _Lembretes = Lembretes
        _Tarefas = Tarefas

        _Fonte1 = New Font(Font.TIMES_ROMAN, 10)
        _FonteRodape = New Font(Font.TIMES_ROMAN, 10, Font.ITALIC)
        _FonteNomeProprietarioCabecalho = New Font(Font.TIMES_ROMAN, 12, Font.BOLDITALIC)
        _FonteHorario = New Font(Font.TIMES_ROMAN, 10, Font.BOLD)

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

    Public Function Gere(ByVal MostraAssunto As Boolean, ByVal MostraLocal As Boolean, ByVal MostraDescricao As Boolean) As String
        Dim CompromissoAnterior As ICompromisso = Nothing

        Dim Configuracao As IConfiguracaoDoSistema

        Using Servico As IServicoDeConfiguracoesDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracoesDoSistema)()
            Configuracao = Servico.ObtenhaConfiguracaoDoSistema
        End Using

        If Configuracao Is Nothing Then Throw New BussinesException("A agenda não pode ser impressa pois a configuração de agenda do sistema ainda não foi configurada.")

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
        Return NomeDoArquivoDeSaida
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
        Dim Texto As New StringBuilder
        Dim TabelaCompromissos As Table = New Table(2)

        TabelaCompromissos.Width = 100%
        TabelaCompromissos.Widths = New Single() {80, 500}

        Dim CelulaEmBranco = iTextSharpUtilidades.CrieCelula("", Cell.ALIGN_LEFT, Cell.NO_BORDER, False)
        'coluna de horario
        TabelaCompromissos.AddCell(CelulaEmBranco)
        'coluna de descricao
        TabelaCompromissos.AddCell(CelulaEmBranco)

        'Adiciona a celula com o horário
        TabelaCompromissos.AddCell(iTextSharpUtilidades.CrieCelula(Compromisso.Inicio.ToString("HH") & "h" & Compromisso.Inicio.ToString("mm") & "min", _
                                                                    _FonteHorario, Cell.ALIGN_LEFT, Cell.NO_BORDER, False))

        If MostraAssunto Then
            Texto.Append(Compromisso.Assunto & "<br />")
        End If

        If MostraLocal AndAlso Not String.IsNullOrEmpty(Compromisso.Local) Then
            Texto.Append(Compromisso.Local & "<br />")
        End If

        If MostraDescricao AndAlso Not String.IsNullOrEmpty(Compromisso.Descricao) Then
            Texto.Append(Compromisso.Descricao)
        End If

        TabelaCompromissos.AddCell(iTextSharpUtilidades.CrieCelula(Texto.ToString, _
                                                                    Cell.ALIGN_LEFT, Cell.NO_BORDER, False))
        _documento.Add(TabelaCompromissos)
    End Sub

    Private Sub EscrevaRodape()
        Dim Rodape As HeaderFooter
        Dim DataEHoraAtual As Date = Now

        Dim Texto As String

        Texto = String.Concat("Impressão em: ", DataEHoraAtual.ToString("dd/MM/yyyy"), " às ", DataEHoraAtual.ToString("HH"), "h", DataEHoraAtual.ToString("mm"), "m", DataEHoraAtual.ToString("ss"), "s")

        Rodape = New HeaderFooter(New Phrase(Texto, _FonteRodape), False)

        If Not _ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoRodapeDeCompromissos Then
            Rodape.Border = HeaderFooter.NO_BORDER
        End If

        Rodape.Alignment = HeaderFooter.ALIGN_RIGHT
        _documento.Footer = Rodape
    End Sub

End Class
