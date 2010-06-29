Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Diary.Interfaces.Negocio
Imports Compartilhados

Partial Public Class frmSolicitacoesDeAudiencia
    Inherits SuperPagina

    Private Const CHAVE_SOLICITACOES As String = "CHAVE_SOLICITACOES"

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

        Dim Solicitacoes As IList(Of ISolicitacaoDeAudiencia)

        UtilidadesWeb.LimparComponente(CType(pnlFiltro, Control))
        UtilidadesWeb.LimparComponente(CType(rdkLancamentos, Control))

        Using Servico As IServicoDeSolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeAudiencia)()
            Solicitacoes = Servico.ObtenhaSolicitacoesDeAudiencia(True)
        End Using

        ExibaSolicitacoes(Solicitacoes)
    End Sub

    Private Sub ExibaSolicitacoes(ByVal Solicitacoes As IList(Of ISolicitacaoDeAudiencia))
        Session(CHAVE_SOLICITACOES) = Solicitacoes
        Me.grdItensLancados.DataSource = Solicitacoes
        Me.grdItensLancados.DataBind()
    End Sub

    Protected Sub btnNovo_Click()
        Dim URL As String

        URL = ObtenhaURL()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Nova solicitação de audiência"), False)
    End Sub

    Private Function ObtenhaURL() As String
        Return String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Diary/cdSolicitacaoDeAudiencia.aspx")
    End Function

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                Call btnNovo_Click()
        End Select
    End Sub

    Private Sub grdItensLancados_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdItensLancados.ItemCommand
        Dim ID As Long = CLng(e.Item.Cells(4).Text)
        Dim IndiceSelecionado As Integer = e.Item().ItemIndex

        If e.CommandName = "Excluir" Then
            Dim Solicitacoes As IList(Of ISolicitacaoDeAudiencia)
            Solicitacoes = CType(Session(CHAVE_SOLICITACOES), IList(Of ISolicitacaoDeAudiencia))
            Solicitacoes.RemoveAt(IndiceSelecionado)
            ExibaSolicitacoes(Solicitacoes)

            Using Servico As IServicoDeSolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeAudiencia)()
                Servico.Remover(ID)
            End Using
        ElseIf e.CommandName = "Modificar" Then
            Dim URL As String

            URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Diary/cdSolicitacaoDeAudiencia.aspx", "?Id=", ID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Despachar solicitação de audiência"), False)

        ElseIf e.CommandName = "Finalizar" Then
            Using Servico As IServicoDeSolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeAudiencia)()
                Servico.Finalizar(ID)
            End Using
        ElseIf e.CommandName = "Despachar" Then
            Dim URL As String

            URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Diary/frmDespacharSolicitacaoDeAudiencia.aspx", "?Id=", ID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Despachar solicitação de audiência"), False)
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
        grdItensLancados.Columns(7).Visible = Principal.EstaAutorizado("OPE.DRY.002.0004")

        'Coluna com botão finalizar
        grdItensLancados.Columns(8).Visible = Principal.EstaAutorizado("OPE.DRY.002.0005")

    End Sub

    Private Sub grdItensLancados_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdItensLancados.PageIndexChanged
    End Sub
End Class