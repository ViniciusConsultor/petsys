Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI
Imports Compartilhados

Partial Public Class cdBancosEAgencias
    Inherits SuperPagina

    Private CHAVE_ESTADO_CD_AGENCIA As String = "CHAVE_ESTADO_CD_AGENCIA"
    Private CHAVE_ID_AGENCIA As String = "CHAVE_ID_AGENCIA"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicialAgencia()
        End If
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Me.rtbToolBarAgencias
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.009"
    End Function

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Modifica
        Remove
    End Enum

    Private Sub ExibaTelaInicialAgencia()
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoAgencia, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoAgencia, Control), False)
        cboBanco.ClearSelection()
        cboBanco.Text = ""
        cboAgencia.ClearSelection()
        cboAgencia.Text = ""
        cboAgencia.AutoPostBack = True
        cboAgencia.EnableLoadOnDemand = True
        ViewState(CHAVE_ID_AGENCIA) = Nothing
        ViewState(CHAVE_ESTADO_CD_AGENCIA) = Estado.Inicial
    End Sub

    Protected Sub btnNovaAgencia_Click()
        ExibaTelaNovaAgencia()
    End Sub

    Private Sub ExibaTelaNovaAgencia()
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO_CD_AGENCIA) = Estado.Novo
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoAgencia, Control), True)
        cboBanco.ClearSelection()
        cboBanco.Text = ""
        cboAgencia.ClearSelection()
        cboAgencia.Text = ""
        cboAgencia.AutoPostBack = False
        cboAgencia.EnableLoadOnDemand = False
    End Sub

   Private Sub ExibaTelaModificarAgencia()
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO_CD_AGENCIA) = Estado.Modifica
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoAgencia, Control), True)
        cboAgencia.AutoPostBack = False
        cboAgencia.EnableLoadOnDemand = False
    End Sub

    Private Sub ExibaTelaExcluirAgencia()
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO_CD_AGENCIA) = Estado.Remove
    End Sub

   Private Sub ExibaTelaConsultarAgencia()
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoAgencia, Control), False)
    End Sub

   Protected Sub btnCancelaAgencia_Click()
        ExibaTelaInicialAgencia()
    End Sub

    Private Function MontaObjetoAgencia() As IAgencia
        Dim Banco As Banco
        Dim Agencia As IAgencia
        
        Banco = Banco.Obtenha(cboBanco.SelectedValue)
        Agencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IAgencia)()
        Agencia.Numero = txtNumeroDaAgencia.Text
        Agencia.Banco = Banco
        Agencia.Nome = cboAgencia.Text
        Agencia.ID = CLng(ViewState(CHAVE_ID_AGENCIA))

        Return Agencia
    End Function

    Private Function ConsisteDadosDaAgencia() As String
        If String.IsNullOrEmpty(cboBanco.Text) Then Return "O banco deve ser informado."
        If String.IsNullOrEmpty(cboAgencia.Text) Then Return "O nome da agência deve ser informado."
        If String.IsNullOrEmpty(txtNumeroDaAgencia.Text) Then Return "O número da agência deve ser informado."
        Return Nothing
    End Function

    Private Sub btnSalvaAgencia_Click()
        Dim Mensagem As String
        Dim Inconsistencia As String

        Inconsistencia = ConsisteDadosDaAgencia()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If
        Dim Agencia As IAgencia = MontaObjetoAgencia()

        Try
            Using Servico As IServicoDeBancosEAgencias = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeBancosEAgencias)()
                If CByte(ViewState(CHAVE_ESTADO_CD_AGENCIA)) = Estado.Novo Then
                    Servico.InsiraAgencia(Agencia)
                    Mensagem = "Agência cadastrada com sucesso."
                Else
                    Servico.ModifiqueAgencia(Agencia)
                    Mensagem = "Agência modificada com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicialAgencia()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Sub btnModificarAgencia_Click()
        ExibaTelaModificarAgencia()
    End Sub

   Private Sub btnExcluiAgencia_Click()
        ExibaTelaExcluirAgencia()
    End Sub

    Private Sub btnNaoAgencia_Click()
        Me.ExibaTelaInicialAgencia()
    End Sub

   Private Sub btnSimAgencia_Click()
        Try
            Using Servico As IServicoDeBancosEAgencias = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeBancosEAgencias)()
                Servico.RemovaAgencia(CLng(cboAgencia.SelectedValue))
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Agência excluida com sucesso."), False)
            ExibaTelaInicialAgencia()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

  Private Sub MostreAgencia(ByVal Agencia As IAgencia)
        txtNumeroDaAgencia.Text = Agencia.Numero
        ViewState(CHAVE_ID_AGENCIA) = Agencia.ID.Value
    End Sub

    Private Sub rtbToolBarAgencias_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBarAgencias.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                Call btnNovaAgencia_Click()
            Case "btnModificar"
                Call btnModificarAgencia_Click()
            Case "btnExcluir"
                Call btnExcluiAgencia_Click()
            Case "btnSalvar"
                Call btnSalvaAgencia_Click()
            Case "btnCancelar"
                Call btnCancelaAgencia_Click()
            Case "btnSim"
                Call btnSimAgencia_Click()
            Case "btnNao"
                Call btnNaoAgencia_Click()
        End Select
    End Sub

    Private Sub cboBanco_ItemsRequested(sender As Object, e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboBanco.ItemsRequested
        Dim bancos As IList(Of Banco) = Banco.ObtenhaTodos()

        cboBanco.Items.Clear()
        
        For Each banco As Banco In bancos
            Dim Item As New RadComboBoxItem(banco.Nome, banco.ID.ToString)

            Item.Attributes.Add("Numero", banco.ID)
            cboBanco.Items.Add(Item)
            Item.DataBind()
        Next

    End Sub
    
    Private Sub cboAgencia_ItemsRequested(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboAgencia.ItemsRequested
        If String.IsNullOrEmpty(cboBanco.SelectedValue) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("O banco deve ser informado para a pesquisa de agências."), False)
            Exit Sub
        End If

        cboAgencia.Items.Clear()
        
        Dim Agencias As IList(Of IAgencia)
        Dim Banco As Banco = Banco.Obtenha(cboBanco.SelectedValue)

        Using Servico As IServicoDeBancosEAgencias = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeBancosEAgencias)()
            Agencias = Servico.ObtenhaAgenciasPorNomeComoFiltro(Banco, e.Text, 50)
        End Using

        For Each Agencia As IAgencia In Agencias
            Dim Item As New RadComboBoxItem(Agencia.Nome, Agencia.ID.ToString)

            Item.Attributes.Add("Numero", Agencia.Numero)
            cboAgencia.Items.Add(Item)
            Item.DataBind()
        Next
    End Sub

    Private Sub cboAgencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboAgencia.SelectedIndexChanged
        If String.IsNullOrEmpty(cboAgencia.SelectedValue) Then Exit Sub

        Dim Agencia As IAgencia

        Using Servico As IServicoDeBancosEAgencias = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeBancosEAgencias)()
            Agencia = Servico.ObtenhaAgencia(cboBanco.SelectedValue, CLng(cboAgencia.SelectedValue))
        End Using

        MostreAgencia(Agencia)
        ExibaTelaConsultarAgencia()
    End Sub

    Private Sub cboBanco_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboBanco.SelectedIndexChanged

    End Sub

End Class