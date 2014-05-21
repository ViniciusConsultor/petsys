Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Negocio.Filtros.HistoricoDeEmail
Imports Compartilhados.Interfaces.Core.Servicos
Imports Telerik.Web.UI
Imports Compartilhados.Componentes.Web

Public Class frmHistoricoDeEmails
    Inherits SuperPagina

    Private Const CHAVE_FILTRO_APLICADO As String = "CHAVE_FILTRO_APLICADO_HISTORICO_EMAIL"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        UtilidadesWeb.LimparComponente(CType(pnlFiltro, Control))
        UtilidadesWeb.LimparComponente(CType(rdkHistorico, Control))
        CarregaOpcoesDeFiltro()
        EscondaTodosOsPanelsDeFiltro()
        pnlData.Visible = True
        cboTipoDeFiltro.SelectedValue = "1"

        ctrlOperacaoFiltro1.Codigo = OperacaoDeFiltro.IgualA.ID.ToString()
        Dim filtro As IFiltro = FabricaGenerica.GetInstancia().CrieObjeto(Of IFiltroHistoricoDeEmailSemFiltro)()
        FiltroAplicado = filtro
        MostraHistorico(filtro, grdHistoricoDeEmails.PageSize, 0)
    End Sub

    Private Sub MostraHistorico(Filtro As IFiltro, QuantidadeDeItens As Integer, OffSet As Integer)
        Using Servico As IServicoDeEnvioDeEmail = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeEnvioDeEmail)()
            grdHistoricoDeEmails.VirtualItemCount = Servico.ObtenhaQuantidadeDeHistoricoDeEmails(Filtro)
            grdHistoricoDeEmails.DataSource = Servico.ObtenhaHistoricos(Filtro, QuantidadeDeItens, OffSet)
            grdHistoricoDeEmails.DataBind()
        End Using
    End Sub

    Private Sub CarregaOpcoesDeFiltro()
        cboTipoDeFiltro.Items.Clear()
        cboTipoDeFiltro.Items.Add(New RadComboBoxItem("Data de envio", "1"))
        cboTipoDeFiltro.Items.Add(New RadComboBoxItem("Assunto", "2"))
        cboTipoDeFiltro.Items.Add(New RadComboBoxItem("Destinatário", "3"))
        cboTipoDeFiltro.Items.Add(New RadComboBoxItem("Mensagem", "4"))
        cboTipoDeFiltro.Items.Add(New RadComboBoxItem("Contexto", "5"))
    End Sub

    Private Sub EscondaTodosOsPanelsDeFiltro()
        pnlData.Visible = False
        pnlAssunto.Visible = False
        pnlDestinario.Visible = False
        pnlMensagem.Visible = False
        pnlContexto.Visible = False
    End Sub

    Private Property FiltroAplicado() As IFiltro
        Get
            Return DirectCast(ViewState(CHAVE_FILTRO_APLICADO), IFiltro)
        End Get
        Set(ByVal value As IFiltro)
            ViewState(CHAVE_FILTRO_APLICADO) = value
        End Set
    End Property

    Protected Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub cboTipoDeFiltro_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        EscondaTodosOsPanelsDeFiltro()

        Select Case cboTipoDeFiltro.SelectedValue

            Case "1"
                pnlData.Visible = True
            Case "2"
                pnlAssunto.Visible = True
            Case "3"
                pnlDestinario.Visible = True
            Case "4"
                pnlMensagem.Visible = True
            Case "5"
                pnlContexto.Visible = True
        End Select
    End Sub

    Protected Sub btnPesquisarPorAssunto_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)

    End Sub

    Protected Sub btnPesquisarPorDestinatario_OnClick_(ByVal sender As Object, ByVal e As ImageClickEventArgs)

    End Sub

    Protected Sub btnPesquisarPorDestinatario_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)

    End Sub

    Protected Sub btnPesquisarPorDataDeEnvio_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)

    End Sub

    Protected Sub btnPesquisarPorContexto_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)

    End Sub

    Protected Sub btnPesquisarPorMensagem_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)

    End Sub

    Protected Sub grdHistoricoDeEmails_OnPageIndexChanged(ByVal sender As Object, ByVal e As GridPageChangedEventArgs)
        If e.NewPageIndex >= 0 Then

            Dim offSet As Integer = 0

            If e.NewPageIndex > 0 Then offSet = e.NewPageIndex * grdHistoricoDeEmails.PageSize

            MostraHistorico(FiltroAplicado, grdHistoricoDeEmails.PageSize, offSet)
        End If
    End Sub

    Protected Sub grdHistoricoDeEmails_OnItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs)
        If e.Item.GetType().Equals(GetType(GridDataItem)) Then

            Dim item As GridDataItem = CType(e.Item, GridDataItem)
            item("column12").Text = ""


        End If
    End Sub

    Protected Sub grdHistoricoDeEmails_OnItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs)

    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.020"
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

End Class