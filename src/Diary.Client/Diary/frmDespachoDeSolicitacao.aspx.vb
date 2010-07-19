Imports Telerik.Web.UI
Imports Diary.Interfaces.Negocio
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Componentes.Web

Partial Public Class frmDespachoDeSolicitacao
    Inherits System.Web.UI.Page

    Private Const CHAVE_ID_SOLICITACAO As String = "CHAVE_ID_SOLICITACAO_FRMDESPACHO"
    Private Const CHAVE_DESPACHOS_DA_SOLICITACAO As String = "CHAVE_DESPACHOS_DA_SOLICITACAO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlDespachoAgenda1.SolicitacaoFoiDespachada, AddressOf SolicitacaoFoiDespachada
        AddHandler ctrlDespachoTarefa1.SolicitacaoFoiDespachada, AddressOf SolicitacaoFoiDespachada
        AddHandler ctrlPessoa1.PessoaSelecionada, AddressOf 

        If Not IsPostBack Then
            Dim Id As Nullable(Of Long)

            If Not String.IsNullOrEmpty(Request.QueryString("Id")) Then
                Id = CLng(Request.QueryString("Id"))
            End If

            Me.ExibaTelaInicial()

            If Not Id Is Nothing Then
                Dim DespachosDaSolicitacao As IList(Of IDespacho)

                Using Servico As IServicoDeDespacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeDespacho)()
                    DespachosDaSolicitacao = Servico.ObtenhaDespachosDaSolicitacao(CLng(Id))
                    ExibaDespachos(DespachosDaSolicitacao)
                End Using
            End If
        End If
    End Sub

    Private Sub 

    Private Sub SolicitacaoFoiDespachada(ByVal Despacho As IDespacho)
        Dim Despachos As IList(Of IDespacho)

        Despachos = CType(Session(CHAVE_DESPACHOS_DA_SOLICITACAO), IList(Of IDespacho))
        Despachos.Add(Despacho)
        ExibaDespachos(Despachos)
    End Sub

    Private Sub ExibaDespachos(ByVal Despachos As IList(Of IDespacho))
        grdDespachos.DataSource = Despachos
        grdDespachos.DataBind()
        Session(CHAVE_DESPACHOS_DA_SOLICITACAO) = Despachos
    End Sub

    Private Sub CarregaDados()
        cboDespacho.Items.Clear()

        For Each Item As TipoDeDespacho In TipoDeDespacho.ObtenhaTodos
            cboDespacho.Items.Add(New RadComboBoxItem(Item.Descricao, Item.ID.ToString))
        Next
    End Sub

    Private Sub ExibaTelaInicial()
        LimpaDados()
        CarregaDados()
        cboDespacho.SelectedValue = TipoDeDespacho.Agendar.ID.ToString
        pnlComponenteDespachoAgenda.Visible = True
        pnlComponenteDespachoTarefa.Visible = False

    End Sub

    Private Sub LimpaDados()
        UtilidadesWeb.LimparComponente(CType(pnlComponenteDespachoAgenda, Control))
        UtilidadesWeb.LimparComponente(CType(pnlComponenteDespachoTarefa, Control))
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
        End Select
    End Sub

    Private Sub cboDespacho_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDespacho.SelectedIndexChanged
        Select Case cboDespacho.SelectedValue
            Case TipoDeDespacho.Agendar.ID.ToString
                pnlComponenteDespachoAgenda.Visible = True
                pnlComponenteDespachoTarefa.Visible = False
            Case TipoDeDespacho.Lembrente.ID.ToString
                pnlComponenteDespachoAgenda.Visible = True
                pnlComponenteDespachoTarefa.Visible = False
            Case TipoDeDespacho.Mensagem.ID.ToString
                pnlComponenteDespachoAgenda.Visible = False
                pnlComponenteDespachoTarefa.Visible = True
            Case TipoDeDespacho.Presente.ID.ToString
                pnlComponenteDespachoAgenda.Visible = False
                pnlComponenteDespachoTarefa.Visible = True
            Case TipoDeDespacho.Remarcar.ID.ToString
                pnlComponenteDespachoAgenda.Visible = False
                pnlComponenteDespachoTarefa.Visible = True
            Case TipoDeDespacho.Representante.ID.ToString
                pnlComponenteDespachoAgenda.Visible = True
                pnlComponenteDespachoTarefa.Visible = False
            Case TipoDeDespacho.Telegrama.ID.ToString
                pnlComponenteDespachoAgenda.Visible = False
                pnlComponenteDespachoTarefa.Visible = True
        End Select

        ctrlDespachoAgenda1.IDProprietario = 
    End Sub

End Class