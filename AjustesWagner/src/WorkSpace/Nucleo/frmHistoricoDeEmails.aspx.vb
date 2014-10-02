Imports Compartilhados
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
        ctrlOperacaoFiltro1.Inicializa()
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

    Private Function OpcaoDeOperacaodeFiltroEstaSelecionada() As Boolean
        Return Not String.IsNullOrEmpty(ctrlOperacaoFiltro1.Codigo)
    End Function

    Private Sub ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro()
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(), _
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione uma opção de filtro."), False)
    End Sub

    Protected Sub btnPesquisarPorAssunto_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        If Not OpcaoDeOperacaodeFiltroEstaSelecionada() Then 
            ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro()
            Exit Sub
        End If

        Dim operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo))

        If operacao.Equals(OperacaoDeFiltro.Intervalo) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.MostraMensagemDeInconsitencia("Para o filtro selecionado essa opção de filtro não está disponível."), False)
            Exit Sub
        End If

        If String.IsNullOrEmpty(txtAssunto.Text) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.MostraMensagemDeInconsitencia("Informe o assunto."), False)
            Exit Sub
        End If

        Dim filtro = FabricaGenerica.GetInstancia().CrieObjeto(Of IFiltroHistoricoDeEmailPorAssunto)()
        filtro.Operacao = operacao
        filtro.ValorDoFiltro = txtAssunto.Text
        FiltroAplicado = filtro
        MostraHistorico(filtro, grdHistoricoDeEmails.PageSize, 0)
    End Sub

    Protected Sub btnPesquisarPorDestinatario_OnClick_(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        If Not OpcaoDeOperacaodeFiltroEstaSelecionada() Then
            ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro()
            Exit Sub
        End If

        Dim operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo))

        If operacao.Equals(OperacaoDeFiltro.Intervalo) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.MostraMensagemDeInconsitencia("Para o filtro selecionado essa opção de filtro não está disponível."), False)
            Exit Sub
        End If

        If String.IsNullOrEmpty(txtAssunto.Text) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.MostraMensagemDeInconsitencia("Informe o destinatário."), False)
            Exit Sub
        End If

        Dim filtro = FabricaGenerica.GetInstancia().CrieObjeto(Of IFiltroHistoricoDeEmailPorDestinatario)()
        filtro.Operacao = operacao
        filtro.ValorDoFiltro = txtDestinario.Text
        FiltroAplicado = filtro
        MostraHistorico(filtro, grdHistoricoDeEmails.PageSize, 0)
    End Sub

    Protected Sub btnPesquisarPorDataDeEnvio_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        If Not OpcaoDeOperacaodeFiltroEstaSelecionada() Then
            ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro()
            Exit Sub
        End If

        Dim operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo))

        If String.IsNullOrEmpty(txtDestinario.Text) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.MostraMensagemDeInconsitencia("Informe a data de envio."), False)
            Exit Sub
        End If

        Dim filtro = FabricaGenerica.GetInstancia().CrieObjeto(Of IFiltroHistoricoDeEmailPorDataDeEnvio)()
        filtro.Operacao = operacao
        filtro.ValorDoFiltro = txtDataDeEnvio.SelectedDate.Value.ToString("yyyyMdd")
        FiltroAplicado = filtro
        MostraHistorico(filtro, grdHistoricoDeEmails.PageSize, 0)
    End Sub

    Protected Sub btnPesquisarPorContexto_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        If Not OpcaoDeOperacaodeFiltroEstaSelecionada() Then
            ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro()
            Exit Sub
        End If

        Dim operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo))

        If operacao.Equals(OperacaoDeFiltro.Intervalo) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.MostraMensagemDeInconsitencia("Para o filtro selecionado essa opção de filtro não está disponível."), False)
            Exit Sub
        End If

        If String.IsNullOrEmpty(txtContexto.Text) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.MostraMensagemDeInconsitencia("Informe o contexto do envio do e-mail."), False)
            Exit Sub
        End If

        Dim filtro = FabricaGenerica.GetInstancia().CrieObjeto(Of IFiltroHistoricoDeEmailPorContexto)()
        filtro.Operacao = operacao
        filtro.ValorDoFiltro = txtContexto.Text
        FiltroAplicado = filtro
        MostraHistorico(filtro, grdHistoricoDeEmails.PageSize, 0)
    End Sub

    Protected Sub btnPesquisarPorMensagem_OnClick(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        If Not OpcaoDeOperacaodeFiltroEstaSelecionada() Then
            ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro()
            Exit Sub
        End If

        Dim operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo))

        If operacao.Equals(OperacaoDeFiltro.Intervalo) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.MostraMensagemDeInconsitencia("Para o filtro selecionado essa opção de filtro não está disponível."), False)
            Exit Sub
        End If

        If String.IsNullOrEmpty(txtMensagem.Text) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.MostraMensagemDeInconsitencia("Informe a menagem do e-mail."), False)
            Exit Sub
        End If

        Dim filtro = FabricaGenerica.GetInstancia().CrieObjeto(Of IFiltroHistoricoDeEmailPorMensagem)()
        filtro.Operacao = operacao
        filtro.ValorDoFiltro = txtMensagem.Text
        FiltroAplicado = filtro
        MostraHistorico(filtro, grdHistoricoDeEmails.PageSize, 0)
    End Sub

    Protected Sub grdHistoricoDeEmails_OnPageIndexChanged(ByVal sender As Object, ByVal e As GridPageChangedEventArgs)
        If e.NewPageIndex >= 0 Then

            Dim offSet As Integer = 0

            If e.NewPageIndex > 0 Then offSet = e.NewPageIndex * grdHistoricoDeEmails.PageSize

            MostraHistorico(FiltroAplicado, grdHistoricoDeEmails.PageSize, offSet)
        End If
    End Sub

    Protected Sub grdHistoricoDeEmails_OnItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs)
        Dim id As Long = 0

        If e.CommandName <> "Page" AndAlso e.CommandName <> "ChangePageSize" Then id = Convert.ToInt64((e.Item.Cells(4).Text))

        Select Case e.CommandName
            Case "ReenviarEmail"

                Try
                    Using ServicoDeConfiguracao As IServicoDeConfiguracoesDoSistema = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeConfiguracoesDoSistema)()

                        Dim Configuracao = ServicoDeConfiguracao.ObtenhaConfiguracaoDoSistema()

                        Using ServicoDeEmail As IServicoDeEnvioDeEmail = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeEnvioDeEmail)()
                            ServicoDeEmail.ReenvieEmail(Configuracao, id)
                        End Using
                    End Using

                Catch ex As BussinesException
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
                End Try
             
        End Select

    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.020"
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Protected Function MontaListaDeDestinatarios(ByVal Historico As IHistoricoDeEmail) As String
        Return UtilidadesWeb.ObtenhaListaDeStringComQuebraDeLinhaWeb(Historico.Destinatarios)
    End Function

    Protected Function MontaListaDeDestinatariosCC(ByVal Historico As IHistoricoDeEmail) As String
        Return UtilidadesWeb.ObtenhaListaDeStringComQuebraDeLinhaWeb(Historico.DestinatariosEmCopia)
    End Function

    Protected Function MontaListaDeDestinatariosCCo(ByVal Historico As IHistoricoDeEmail) As String
        Return UtilidadesWeb.ObtenhaListaDeStringComQuebraDeLinhaWeb(Historico.DestinatariosEmCopiaOculta)
    End Function

End Class