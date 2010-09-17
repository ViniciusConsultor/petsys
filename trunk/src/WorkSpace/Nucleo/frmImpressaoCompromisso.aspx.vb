﻿Imports Compartilhados
Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas

Partial Public Class frmImpressaoCompromisso
    Inherits SuperPagina

    Private Const CHAVE_ID_PROPRIETARIO_AGENDA_IMPRESSAO As String = "CHAVE_ID_PROPRIETARIO_AGENDA_IMPRESSAO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
            If Not String.IsNullOrEmpty(Request.QueryString("IdProprietario")) Then
                ViewState(CHAVE_ID_PROPRIETARIO_AGENDA_IMPRESSAO) = CLng(Request.QueryString("IdProprietario"))
            End If
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        UtilidadesWeb.LimparComponente(CType(pnlFiltro, Control))
        CarregaOpcoesDeImpressao()
        CarregaOpcoesDeFormatoDeSaida()
    End Sub

    Private Sub CarregaOpcoesDeImpressao()
        cboOpcoesDeImpressao.Items.Clear()
        cboOpcoesDeImpressao.Items.Add(New RadComboBoxItem("Assunto + Local + Descrição", "1"))
        cboOpcoesDeImpressao.Items.Add(New RadComboBoxItem("Assunto + Local", "2"))
        cboOpcoesDeImpressao.Items.Add(New RadComboBoxItem("Assunto", "3"))
        cboOpcoesDeImpressao.Items.Add(New RadComboBoxItem("Local", "4"))
    End Sub

    Private Sub CarregaOpcoesDeFormatoDeSaida()
        rblFormato.Items.Clear()
        rblFormato.Items.Add(New ListItem(TipoDeFormatoDeSaidaDoDocumento.RTF.Descricao, TipoDeFormatoDeSaidaDoDocumento.RTF.ID))
        rblFormato.Items.Add(New ListItem(TipoDeFormatoDeSaidaDoDocumento.PDF.Descricao, TipoDeFormatoDeSaidaDoDocumento.PDF.ID))
    End Sub

    Private Sub btnPesquisar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisar.Click
        Dim Compromissos As IList(Of ICompromisso)
        Dim PossiveisInconsistencias As String

        PossiveisInconsistencias = ValidaDados()

        If Not String.IsNullOrEmpty(PossiveisInconsistencias) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(PossiveisInconsistencias), False)
            Exit Sub
        End If

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Compromissos = Servico.ObtenhaCompromissos(CLng(ViewState(CHAVE_ID_PROPRIETARIO_AGENDA_IMPRESSAO)), _
                                                       txtDataInicial.SelectedDate.Value, _
                                                       txtDataFinal.SelectedDate)
        End Using

        If Compromissos Is Nothing OrElse Compromissos.Count = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Nenhum compromisso foi encontrado com o filtro que foi informado."), False)
            Exit Sub
        End If

        Dim Gerador As ImpressorDeCompromissos
        Dim NomeDoArquivo As String = Nothing
        Dim URL As String
        Dim FormatoDeSaida As TipoDeFormatoDeSaidaDoDocumento

        FormatoDeSaida = TipoDeFormatoDeSaidaDoDocumento.Obtenha(CChar(rblFormato.SelectedValue))

        Gerador = New ImpressorDeCompromissos(Compromissos, FormatoDeSaida)

        Try
            If cboOpcoesDeImpressao.SelectedValue = "1" Then
                NomeDoArquivo = Gerador.Gere(True, True, True)
            ElseIf cboOpcoesDeImpressao.SelectedValue = "2" Then
                NomeDoArquivo = Gerador.Gere(True, True, False)
            ElseIf cboOpcoesDeImpressao.SelectedValue = "3" Then
                NomeDoArquivo = Gerador.Gere(True, False, False)
            ElseIf cboOpcoesDeImpressao.SelectedValue = "4" Then
                NomeDoArquivo = Gerador.Gere(False, True, False)
            End If

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
            Exit Sub
        End Try

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual & UtilidadesWeb.PASTA_LOADS & "/" & NomeDoArquivo
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Imprimir"), False)
    End Sub

    Private Function ValidaDados() As String
        If Not txtDataInicial.SelectedDate.HasValue Then Return "A data de início do(s) compromisso(s) deve ser informada."

        Return Nothing
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Nothing
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return Nothing
    End Function

End Class