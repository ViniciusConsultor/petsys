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
    Private _Agenda As IAgenda

    Private _FonteNomeProprietarioCabecalho As Font
    Private _FonteHorario As Font
    Private _ConfiguracaoDeAgendaDoSistema As IConfiguracaoDeAgendaDoSistema
    Private NomeDoArquivoDeSaida As String

    Public Sub New(ByVal Agenda As IAgenda, _
                   ByVal FormatoDeSaida As TipoDeFormatoDeSaidaDoDocumento)
        _Agenda = Agenda

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
        Dim Configuracao As IConfiguracaoDoSistema

        Using Servico As IServicoDeConfiguracoesDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracoesDoSistema)()
            Configuracao = Servico.ObtenhaConfiguracaoDoSistema
        End Using

        If Configuracao Is Nothing Then Throw New BussinesException("A agenda não pode ser impressa pois a configuração de agenda do sistema ainda não foi configurada.")

        _ConfiguracaoDeAgendaDoSistema = Configuracao.ConfiguracaoDeAgendaDoSistema

        Dim DataAux As Date = _Agenda.Inicio

        _documento.Open()

        While DataAux <= _Agenda.Fim
            Dim CompromissosDaData As IList(Of ICompromisso)

            EscrevaCabecalho()
            CompromissosDaData = _Agenda.ObtenhaCompromissos(DataAux)

            If Not CompromissosDaData Is Nothing Then
                EscrevaCompromissos(CompromissosDaData, MostraAssunto, MostraLocal, MostraDescricao)
            End If

            Dim LembretesDaData As IList(Of ILembrete)

            LembretesDaData = _Agenda.ObtenhaLembretes(DataAux)

            If Not LembretesDaData Is Nothing Then
                EscrevaLembretes(LembretesDaData, MostraAssunto, MostraDescricao)
            End If

            Dim TarefasDaData As IList(Of ITarefa)

            TarefasDaData = _Agenda.ObtenhaTarefas(DataAux)

            If Not TarefasDaData Is Nothing Then
                EscrevaTarefas(TarefasDaData, MostraAssunto, MostraDescricao)
            End If

            EscrevaRodape()
            DataAux = DataAux.AddDays(1)

            If DataAux < _Agenda.Fim Then _documento.NewPage()
        End While

        _documento.Close()
        Return NomeDoArquivoDeSaida
    End Function

    Private Sub EscrevaCabecalho()
        Dim Cabecalho As HeaderFooter
        Dim Frase As Phrase

        Frase = New Phrase(_ConfiguracaoDeAgendaDoSistema.TextoCabecalhoDeCompromissos & _Agenda.Proprietario.Nome & vbLf, _FonteNomeProprietarioCabecalho)
        Frase.Add(New Phrase(UtilitarioDeData.ObtenhaDiaDaSemanaDiaDoMesMesAnoEmStr(_Agenda.Inicio), _Fonte1))

        Cabecalho = New HeaderFooter(Frase, False)
        Cabecalho.Alignment = HeaderFooter.ALIGN_RIGHT

        If Not _ConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoCabecalhoDeCompromissos Then
            Cabecalho.Border = HeaderFooter.NO_BORDER
        End If

        _documento.Header = Cabecalho
    End Sub

    Private Sub EscrevaCompromissos(ByVal Compromissos As IList(Of ICompromisso), _
                                    ByVal MostraAssunto As Boolean, _
                                    ByVal MostraLocal As Boolean, _
                                    ByVal MostraDescricao As Boolean)
        Dim TabelaCompromissos As Table = New Table(2)

        TabelaCompromissos.Width = 100%
        TabelaCompromissos.Widths = New Single() {80, 500}

        For Each Compromisso As ICompromisso In Compromissos
            Dim Texto As New StringBuilder

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
        Next

        _documento.Add(TabelaCompromissos)
    End Sub

    Private Sub EscrevaLembretes(ByVal Lembretes As IList(Of ILembrete), _
                                 ByVal MostraAssunto As Boolean, _
                                 ByVal MostraDescricao As Boolean)
        Dim TabelaLembretes As Table = New Table(2)

        TabelaLembretes.Width = 100%
        TabelaLembretes.Widths = New Single() {80, 500}

        For Each Lembrete As ILembrete In Lembretes
            Dim Texto As New StringBuilder
            Dim CelulaEmBranco = iTextSharpUtilidades.CrieCelula("", Cell.ALIGN_LEFT, Cell.NO_BORDER, False)
            'coluna de horario
            TabelaLembretes.AddCell(CelulaEmBranco)
            'coluna de descricao
            TabelaLembretes.AddCell(CelulaEmBranco)
            'Adiciona a celula com o horário
            TabelaLembretes.AddCell(iTextSharpUtilidades.CrieCelula(Lembrete.Inicio.ToString("HH") & "h" & Lembrete.Inicio.ToString("mm") & "min", _
                                                                        _FonteHorario, Cell.ALIGN_LEFT, Cell.NO_BORDER, False))
            If MostraAssunto Then
                Texto.Append(Lembrete.Assunto & "<br />")
            End If

            If MostraDescricao AndAlso Not String.IsNullOrEmpty(Lembrete.Descricao) Then
                Texto.Append(Lembrete.Descricao)
            End If

            TabelaLembretes.AddCell(iTextSharpUtilidades.CrieCelula(Texto.ToString, _
                                                                    Cell.ALIGN_LEFT, Cell.NO_BORDER, False))
        Next

        _documento.Add(TabelaLembretes)
    End Sub

    Private Sub EscrevaTarefas(ByVal Tarefas As IList(Of ITarefa), _
                               ByVal MostraAssunto As Boolean, _
                               ByVal MostraDescricao As Boolean)
        Dim TabelaTarefa As Table = New Table(2)

        TabelaTarefa.Width = 100%
        TabelaTarefa.Widths = New Single() {80, 500}

        For Each Tarefa As ITarefa In Tarefas
            Dim Texto As New StringBuilder
            Dim CelulaEmBranco = iTextSharpUtilidades.CrieCelula("", Cell.ALIGN_LEFT, Cell.NO_BORDER, False)
            'coluna de horario
            TabelaTarefa.AddCell(CelulaEmBranco)
            'coluna de descricao
            TabelaTarefa.AddCell(CelulaEmBranco)

            'Adiciona a celula com o horário
            TabelaTarefa.AddCell(iTextSharpUtilidades.CrieCelula(Tarefa.DataDeInicio.ToString("HH") & "h" & Tarefa.DataDeInicio.ToString("mm") & "min", _
                                                                 _FonteHorario, Cell.ALIGN_LEFT, Cell.NO_BORDER, False))

            If MostraAssunto Then
                Texto.Append(Tarefa.Assunto & "<br />")
            End If

            If MostraDescricao AndAlso Not String.IsNullOrEmpty(Tarefa.Descricao) Then
                Texto.Append(Tarefa.Descricao)
            End If

            TabelaTarefa.AddCell(iTextSharpUtilidades.CrieCelula(Texto.ToString, _
                                                                 Cell.ALIGN_LEFT, Cell.NO_BORDER, False))
        Next

        _documento.Add(TabelaTarefa)
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