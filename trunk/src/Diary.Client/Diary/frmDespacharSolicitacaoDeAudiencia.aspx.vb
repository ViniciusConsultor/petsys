Imports Compartilhados.Componentes.Web
Imports Diary.Interfaces.Negocio
Imports Telerik.Web.UI

Partial Public Class frmDespacharSolicitacaoDeAudiencia
    Inherits System.Web.UI.Page

    Private Const CHAVE_ID_SOLICITACAO As String = "CHAVE_ID_SOLICITACAO_FRMDESPACHO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ClasseEstatica.RecarreSolicitacoes, AddressOf RecarregaSolicitacoes

        If Not IsPostBack Then
            Dim Id As Nullable(Of Long)

            If Not String.IsNullOrEmpty(Request.QueryString("Id")) Then
                Id = CLng(Request.QueryString("Id"))
            End If

            If Not Id Is Nothing Then
                Me.ExibaTelaInicial()
                CarregaDados()
            End If
        End If
    End Sub

    Private Sub RecarregaSolicitacoes()

    End Sub

    Private Sub CarregaDados()
        cboDespacho.Items.Clear()

        For Each Item As TipoDeDespacho In TipoDeDespacho.ObtenhaTodos
            cboDespacho.Items.Add(New RadComboBoxItem(Item.Descricao, Item.ID.ToString))
        Next
    End Sub

    Private Sub ExibaTelaInicial()
        LimpaDados()
    End Sub

    Private Sub LimpaDados()
        UtilidadesWeb.LimparComponente(CType(pnlDadosDaSolicitacao, Control))
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
        End Select
    End Sub

   
End Class