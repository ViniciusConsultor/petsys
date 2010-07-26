﻿Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas

Partial Public Class frmImpressaoTarefa
    Inherits SuperPagina

    Private Const CHAVE_ID_PROPRIETARIO_AGENDA_IMPRESSAO As String = "CHAVE_ID_PROPRIETARIO_TAREFA_IMPRESSAO"

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
    End Sub

    Private Sub CarregaOpcoesDeImpressao()
        cboOpcoesDeImpressao.Items.Clear()
        cboOpcoesDeImpressao.Items.Add(New RadComboBoxItem("Assunto + Descrição", "1"))
        cboOpcoesDeImpressao.Items.Add(New RadComboBoxItem("Assunto", "2"))
        cboOpcoesDeImpressao.Items.Add(New RadComboBoxItem("Descrição", "3"))
    End Sub

    Private Sub btnPesquisar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisar.Click
        Dim Tarefas As IList(Of ITarefa)
        Dim PossiveisInconsistencias As String

        PossiveisInconsistencias = ValidaDados()

        If Not String.IsNullOrEmpty(PossiveisInconsistencias) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(PossiveisInconsistencias), False)
            Exit Sub
        End If

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Tarefas = Servico.ObtenhaTarefas(CLng(ViewState(CHAVE_ID_PROPRIETARIO_AGENDA_IMPRESSAO)), _
                                             txtDataInicial.SelectedDate.Value, _
                                             txtDataFinal.SelectedDate)
        End Using

        If Tarefas Is Nothing OrElse Tarefas.Count = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Nenhuma tarefa foi encontrada com o filtro que foi informado."), False)
            Exit Sub
        End If

        Dim GeradorDePDF As GerarTarefasEmPDF
        Dim NomeDoPDFGerado As String = Nothing
        Dim URL As String

        GeradorDePDF = New GerarTarefasEmPDF(Tarefas)

        If cboOpcoesDeImpressao.SelectedValue = "1" Then
            NomeDoPDFGerado = GeradorDePDF.GerePDF(True, True)
        ElseIf cboOpcoesDeImpressao.SelectedValue = "2" Then
            NomeDoPDFGerado = GeradorDePDF.GerePDF(True, False)
        ElseIf cboOpcoesDeImpressao.SelectedValue = "3" Then
            NomeDoPDFGerado = GeradorDePDF.GerePDF(False, True)
        End If

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual & UtilidadesWeb.PASTA_LOADS & "/" & NomeDoPDFGerado
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Imprimir"), False)
    End Sub

    Private Function ValidaDados() As String
        If Not txtDataInicial.SelectedDate.HasValue Then Return "A data de início da(s) tarefa(s) deve ser informada."

        Return Nothing
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Nothing
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return Nothing
    End Function


End Class