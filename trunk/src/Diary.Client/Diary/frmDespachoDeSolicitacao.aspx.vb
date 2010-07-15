Imports Telerik.Web.UI
Imports Diary.Interfaces.Negocio

Partial Public Class frmDespachoDeSolicitacao
    Inherits System.Web.UI.Page

    Private Const CHAVE_ID_SOLICITACAO As String = "CHAVE_ID_SOLICITACAO_FRMDESPACHO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Private Sub CarregaDados()
        cboDespacho.Items.Clear()

        For Each Item As TipoDeDespacho In TipoDeDespacho.ObtenhaTodos
            cboDespacho.Items.Add(New RadComboBoxItem(Item.Descricao, Item.ID.ToString))
        Next
    End Sub

    Private Sub ExibaTelaInicial()
        LimpaDados()
        cboDespacho.SelectedValue = TipoDeDespacho.Agendar.ID.ToString
        pnlComponenteDespacho.Controls.Add(LoadControl("ctrlDespachoAgenda.ascx"))
    End Sub

    Private Sub LimpaDados()
        'UtilidadesWeb.LimparComponente(CType(pnlDadosDaSolicitacao, Control))
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
        End Select
    End Sub

    Private Sub cboDespacho_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDespacho.SelectedIndexChanged
        pnlComponenteDespacho.Controls.Clear()

        Select Case cboDespacho.SelectedValue
            Case TipoDeDespacho.Agendar.ID.ToString
                pnlComponenteDespacho.Controls.Add(LoadControl("ctrlDespachoAgenda.ascx"))
            Case TipoDeDespacho.Lembrente.ID.ToString
                pnlComponenteDespacho.Controls.Add(LoadControl("ctrlDespachoAgenda.ascx"))
            Case TipoDeDespacho.Mensagem.ID.ToString
                pnlComponenteDespacho.Controls.Add(LoadControl("ctrlDespachoTarefa.ascx"))
            Case TipoDeDespacho.Presente.ID.ToString
                pnlComponenteDespacho.Controls.Add(LoadControl("ctrlDespachoTarefa.ascx"))
            Case TipoDeDespacho.Remarcar.ID.ToString
                pnlComponenteDespacho.Controls.Add(LoadControl("ctrlDespachoTarefa.ascx"))
            Case TipoDeDespacho.Representante.ID.ToString
                pnlComponenteDespacho.Controls.Add(LoadControl("ctrlDespachoAgenda.ascx"))
            Case TipoDeDespacho.Telegrama.ID.ToString
                pnlComponenteDespacho.Controls.Add(LoadControl("ctrlDespachoTarefa.ascx"))
        End Select
    End Sub

End Class