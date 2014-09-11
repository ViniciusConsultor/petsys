Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados

Public Class cdTipoDeEndereco
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO_CD_TIPO_DE_ENDERECO As String = "CHAVE_ESTADO_CD_TIPO_DE_ENDERECO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlTipoDeAtividade1.TipoFoiSelecionado, AddressOf TipoFoiSelecionado
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ctrlTipoDeAtividade1.LimparControle()
        ctrlTipoDeAtividade1.HabiliteComponente(True)
        ViewState(CHAVE_ESTADO_CD_TIPO_DE_ENDERECO) = Estado.Inicial
        ctrlTipoDeAtividade1.EnableLoadOnDemand = True
        ctrlTipoDeAtividade1.ShowDropDownOnTextboxClick = True
        ctrlTipoDeAtividade1.AutoPostBack = True
        ctrlTipoDeAtividade1.ExibeTituloParaSelecionarUmItem = True
    End Sub

    Protected Sub btnNovo_Click()
        ExibaTelaNovo()
    End Sub

    Private Sub ExibaTelaNovo()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ctrlTipoDeAtividade1.LimparControle()
        ctrlTipoDeAtividade1.HabiliteComponente(True)

        ViewState(CHAVE_ESTADO_CD_TIPO_DE_ENDERECO) = Estado.Novo
        ctrlTipoDeAtividade1.EnableLoadOnDemand = False
        ctrlTipoDeAtividade1.ShowDropDownOnTextboxClick = False
        ctrlTipoDeAtividade1.AutoPostBack = False
        ctrlTipoDeAtividade1.ExibeTituloParaSelecionarUmItem = False
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ctrlTipoDeAtividade1.HabiliteComponente(True)
        ViewState(CHAVE_ESTADO_CD_TIPO_DE_ENDERECO) = Estado.Modifica
        ctrlTipoDeAtividade1.EnableLoadOnDemand = False
        ctrlTipoDeAtividade1.ShowDropDownOnTextboxClick = False
        ctrlTipoDeAtividade1.ExibeTituloParaSelecionarUmItem = False
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO_CD_TIPO_DE_ENDERECO) = Estado.Remove
        ctrlTipoDeAtividade1.HabiliteComponente(False)
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ctrlTipoDeAtividade1.HabiliteComponente(True)
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Sub btnSalva_Click()
        Dim Tipo As ITipoDeEndereco = Nothing
        Dim Mensagem As String
        Dim Inconsistencia As String = VerifiqueCamposObrigatorios()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Tipo = MontaObjeto()

        Try
            Using Servico As IServicoDeTipoDeEndereco = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeTipoDeEndereco)()
                If CByte(ViewState(CHAVE_ESTADO_CD_TIPO_DE_ENDERECO)) = Estado.Novo Then
                    Servico.Insira(Tipo)
                    Mensagem = "Tipo de endereço cadastrado com sucesso."
                Else
                    Servico.Modificar(Tipo)
                    Mensagem = "Tipo de endereço modificado com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function VerifiqueCamposObrigatorios() As String
        If String.IsNullOrEmpty(ctrlTipoDeAtividade1.Nome) Then Return "O nome do tipo de endereço deve ser informado."

        Return Nothing
    End Function

    Private Function MontaObjeto() As ITipoDeEndereco
        Dim Tipo As ITipoDeEndereco

        Tipo = FabricaGenerica.GetInstancia.CrieObjeto(Of ITipoDeEndereco)()

        If CByte(ViewState(CHAVE_ESTADO_CD_TIPO_DE_ENDERECO)) <> Estado.Novo Then
            Tipo.ID = ctrlTipoDeAtividade1.TipoSelecionado.ID.Value
        End If

        Tipo.Nome = ctrlTipoDeAtividade1.Nome
        Return Tipo
    End Function

    Private Sub ExibaTipo(ByVal Tipo As ITipoDeEndereco)
        ctrlTipoDeAtividade1.Nome = Tipo.Nome
    End Sub

    Private Sub btnModificar_Click()
        ExibaTelaModificar()
    End Sub

    Private Sub btnExclui_Click()
        ExibaTelaExcluir()
    End Sub

    Private Sub btnNao_Click()
        Me.ExibaTelaInicial()
    End Sub

    Private Sub btnSim_Click()
        Try
            Using Servico As IServicoDeTipoDeEndereco = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeTipoDeEndereco)()
                Servico.Remover(ctrlTipoDeAtividade1.TipoSelecionado.ID.Value)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Tipo de atividade excluído com sucesso."), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.017"
    End Function

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                Call btnNovo_Click()
            Case "btnModificar"
                Call btnModificar_Click()
            Case "btnExcluir"
                Call btnExclui_Click()
            Case "btnSalvar"
                Call btnSalva_Click()
            Case "btnCancelar"
                Call btnCancela_Click()
            Case "btnSim"
                Call btnSim_Click()
            Case "btnNao"
                Call btnNao_Click()
        End Select
    End Sub

    Private Sub TipoFoiSelecionado(ByVal Tipo As ITipoDeEndereco)
        ExibaTelaConsultar()
        ExibaTipo(Tipo)
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

End Class