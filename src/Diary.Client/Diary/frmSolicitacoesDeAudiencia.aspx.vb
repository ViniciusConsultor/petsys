﻿Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Diary.Interfaces.Negocio
Imports Compartilhados

Partial Public Class frmSolicitacoesDeAudiencia
    Inherits SuperPagina

    Private Const CHAVE_SOLICITACOES As String = "CHAVE_SOLICITACOES_AUDIENCIA"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Me.rtbToolBar
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.DRY.002"
    End Function

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True

        UtilidadesWeb.LimparComponente(CType(pnlFiltro, Control))
        UtilidadesWeb.LimparComponente(CType(rdkLancamentos, Control))

        CarregaOpcoesDeFiltro()
        pnlCodigoDaSolicitacao.Visible = False
        pnlEntreDadas.Visible = True
        pnlContato.Visible = False
        chkConsiderarSolicitacoesFinalizadas.Checked = False
        CarregaSolicitacoesSemFiltro()
    End Sub

    Private Sub CarregaSolicitacoesSemFiltro()
        Dim Solicitacoes As IList(Of ISolicitacaoDeAudiencia)

        Using Servico As IServicoDeSolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeAudiencia)()
            Solicitacoes = Servico.ObtenhaSolicitacoesDeAudiencia(chkConsiderarSolicitacoesFinalizadas.Checked)
        End Using

        ExibaSolicitacoes(Solicitacoes)
    End Sub

    Private Sub CarregaOpcoesDeFiltro()
        cboTipoDeFiltro.Items.Clear()

        cboTipoDeFiltro.Items.Add(New RadComboBoxItem("Por código", "1"))
        cboTipoDeFiltro.Items.Add(New RadComboBoxItem("Entre datas", "2"))
        cboTipoDeFiltro.Items.Add(New RadComboBoxItem("Por contato", "3"))

        'Seta o valor entre datas como inicial
        cboTipoDeFiltro.SelectedValue = "2"
    End Sub

    Private Sub ExibaSolicitacoes(ByVal Solicitacoes As IList(Of ISolicitacaoDeAudiencia))
        ViewState(CHAVE_SOLICITACOES) = Solicitacoes
        Me.grdItensLancados.DataSource = Solicitacoes
        Me.grdItensLancados.DataBind()
    End Sub

    Protected Sub btnNovo_Click()
        Dim URL As String

        URL = ObtenhaURL()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanela(URL, "Nova solicitação de audiência", 800, 600, "Diary_cdSolicitacaoDeAudiencia_aspx"), False)
    End Sub

    Private Function ObtenhaURL() As String
        Return String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Diary/cdSolicitacaoDeAudiencia.aspx")
    End Function

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                btnNovo_Click()
            Case "btnImprimir"
                btnImprir_Click()
            Case "btnRecarregar"
                btnRecarregar_Click()
        End Select
    End Sub

    Protected Sub btnRecarregar_Click()
        CarregaSolicitacoesSemFiltro()
    End Sub

    Private Sub grdItensLancados_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdItensLancados.ItemCommand
        Dim ID As Long
        Dim IndiceSelecionado As Integer

        If Not e.CommandName = "Page" AndAlso Not e.CommandName = "ChangePageSize" Then
            ID = CLng(e.Item.Cells(4).Text)
            IndiceSelecionado = e.Item().ItemIndex
        End If

        If e.CommandName = "Excluir" Then
            Dim Solicitacoes As IList(Of ISolicitacaoDeAudiencia)
            Solicitacoes = CType(ViewState(CHAVE_SOLICITACOES), IList(Of ISolicitacaoDeAudiencia))
            Solicitacoes.RemoveAt(IndiceSelecionado)
            ExibaSolicitacoes(Solicitacoes)

            Using Servico As IServicoDeSolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeAudiencia)()
                Servico.Remover(ID)
            End Using
        ElseIf e.CommandName = "Modificar" Then
            Dim URL As String

            URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Diary/cdSolicitacaoDeAudiencia.aspx", "?Id=", ID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanela(URL, "Cadastrar solicitação de audiência", 800, 600, "Diary_cdSolicitacaoDeAudiencia_aspx"), False)

        ElseIf e.CommandName = "Finalizar" Then
            Using Servico As IServicoDeSolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeAudiencia)()
                Servico.Finalizar(ID)
                btnRecarregar_Click()
            End Using
        ElseIf e.CommandName = "Despachar" Then
            Dim URL As String

            URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Diary/frmDespachoDeSolicitacao.aspx", "?Id=", ID, "&Tipo=", TipoDeSolicitacao.Audiencia.ID.ToString)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanela(URL, "Despachar solicitação de audiência", 800, 600, "Diary_frmDespachoDeSolicitacao_aspx"), False)
        End If
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim Principal As Compartilhados.Principal

        Principal = FabricaDeContexto.GetInstancia.GetContextoAtual

        'Coluna com botão modificar
        grdItensLancados.Columns(0).Visible = Principal.EstaAutorizado("OPE.DRY.002.0002")

        'Coluna com botão excluir
        grdItensLancados.Columns(1).Visible = Principal.EstaAutorizado("OPE.DRY.002.0003")

        'Coluna com botão despachar
        grdItensLancados.Columns(8).Visible = Principal.EstaAutorizado("OPE.DRY.002.0004")

        'Coluna com botão finalizar
        grdItensLancados.Columns(9).Visible = Principal.EstaAutorizado("OPE.DRY.002.0005")

    End Sub

    Private Sub grdItensLancados_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdItensLancados.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdItensLancados, ViewState(CHAVE_SOLICITACOES), e)
    End Sub

    Protected Sub btnPesquisar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisar.Click
        If txtDataInicial.IsEmpty Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("A data inicial da solicitação de audiência deve ser informada."), False)
            Exit Sub
        End If

        If txtDataFinal.IsEmpty Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("A data final da solicitação de audiência deve ser informada."), False)
            Exit Sub
        End If

        Dim Solicitacoes As IList(Of ISolicitacaoDeAudiencia)

        Using Servico As IServicoDeSolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeAudiencia)()
            Solicitacoes = Servico.ObtenhaSolicitacoesDeAudiencia(chkConsiderarSolicitacoesFinalizadas.Checked, _
                                                                  txtDataInicial.SelectedDate.Value, txtDataFinal.SelectedDate.Value)
        End Using

        ExibaSolicitacoes(Solicitacoes)
    End Sub

    Protected Sub btnPesquisarPorCodigo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisarPorCodigo.Click
        If String.IsNullOrEmpty(txtCodigoDaSolicitacao.Text) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("O código da solicitação de audiência deve ser informado."), False)
            Exit Sub
        End If

        Dim Solicitacoes As IList(Of ISolicitacaoDeAudiencia) = New List(Of ISolicitacaoDeAudiencia)

        Using Servico As IServicoDeSolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeAudiencia)()
            Dim Solicitacao As ISolicitacaoDeAudiencia

            Solicitacao = Servico.ObtenhaSolicitacaoPorCodigo(CLng(txtCodigoDaSolicitacao.Value))
            If Not Solicitacao Is Nothing Then Solicitacoes.Add(Solicitacao)
        End Using

        ExibaSolicitacoes(Solicitacoes)
    End Sub

    Private Sub btnImprir_Click()
        Dim NomeDoArquivo As String
        Dim Gerador As GeradorDeSolicitacoesDeAudiencia
        Dim Solicitacoes As IList(Of ISolicitacaoDeAudiencia)
        Dim URL As String

        Solicitacoes = CType(ViewState(CHAVE_SOLICITACOES), IList(Of ISolicitacaoDeAudiencia))

        If Solicitacoes Is Nothing AndAlso Solicitacoes.Count = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Não existem solicitações de audiência para ser impressas."), False)
        End If

        Gerador = New GeradorDeSolicitacoesDeAudiencia(Solicitacoes)
        NomeDoArquivo = Gerador.GereRelatorioDeSolicitacoes
        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual & UtilidadesWeb.PASTA_LOADS & "/" & NomeDoArquivo
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraArquivoParaDownload(URL, "Imprimir"), False)
    End Sub

    Private Sub btnPesquisarPorContato_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisarPorContato.Click
        If ctrlContato1.ContatoSelecionado Is Nothing Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("O contato da solicitação de audiência deve ser informado."), False)
            Exit Sub
        End If

        Dim Solicitacoes As IList(Of ISolicitacaoDeAudiencia)

        Using Servico As IServicoDeSolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeAudiencia)()
            Solicitacoes = Servico.ObtenhaSolicitacoesDeAudiencia(chkConsiderarSolicitacoesFinalizadas.Checked, ctrlContato1.ContatoSelecionado.Pessoa.ID.Value)
        End Using

        ExibaSolicitacoes(Solicitacoes)
    End Sub

    Private Sub grdItensLancados_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdItensLancados.ItemCreated
        If (TypeOf e.Item Is GridDataItem) Then
            Dim gridItem As GridDataItem = CType(e.Item, GridDataItem)

            For Each column As GridColumn In grdItensLancados.MasterTableView.RenderColumns
                If (TypeOf column Is GridButtonColumn) Then
                    gridItem(column.UniqueName).ToolTip = column.HeaderTooltip
                End If
            Next
        End If
    End Sub

    Private Sub cboTipoDeFiltro_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTipoDeFiltro.SelectedIndexChanged
        Dim ValorSelecionado As String

        ValorSelecionado = cboTipoDeFiltro.SelectedValue

        If ValorSelecionado = "1" Then
            pnlCodigoDaSolicitacao.Visible = True
            pnlEntreDadas.Visible = False
            pnlContato.Visible = False
        ElseIf ValorSelecionado = "2" Then
            pnlCodigoDaSolicitacao.Visible = False
            pnlEntreDadas.Visible = True
            pnlContato.Visible = False
        ElseIf ValorSelecionado = "3" Then
            pnlCodigoDaSolicitacao.Visible = False
            pnlEntreDadas.Visible = False
            pnlContato.Visible = True
        End If
    End Sub

End Class