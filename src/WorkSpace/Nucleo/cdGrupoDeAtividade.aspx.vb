Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados

Public Class cdGrupoDeAtividade
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO_CD_GRUPO As String = "CHAVE_ESTADO_CD_GRUPO_DE_ATIVIDADE"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlGrupo1.GrupoFoiSelecionado, AddressOf GrupoFoiSelecionado
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
        ctrlGrupo1.LimparControle()
        ctrlGrupo1.HabiliteComponente(True)
        ViewState(CHAVE_ESTADO_CD_GRUPO) = Estado.Inicial
        ctrlGrupo1.EnableLoadOnDemand = True
        ctrlGrupo1.ShowDropDownOnTextboxClick = True
        ctrlGrupo1.AutoPostBack = True
        ctrlGrupo1.ExibeTituloParaSelecionarUmItem = True
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
        ctrlGrupo1.LimparControle()
        ctrlGrupo1.HabiliteComponente(True)

        ViewState(CHAVE_ESTADO_CD_GRUPO) = Estado.Novo
        ctrlGrupo1.EnableLoadOnDemand = False
        ctrlGrupo1.ShowDropDownOnTextboxClick = False
        ctrlGrupo1.AutoPostBack = False
        ctrlGrupo1.ExibeTituloParaSelecionarUmItem = False
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ctrlGrupo1.HabiliteComponente(True)
        ViewState(CHAVE_ESTADO_CD_GRUPO) = Estado.Modifica
        ctrlGrupo1.EnableLoadOnDemand = False
        ctrlGrupo1.ShowDropDownOnTextboxClick = False
        ctrlGrupo1.ExibeTituloParaSelecionarUmItem = False
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO_CD_GRUPO) = Estado.Remove
        ctrlGrupo1.HabiliteComponente(False)
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ctrlGrupo1.HabiliteComponente(True)
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Sub btnSalva_Click()
        Dim Grupo As IGrupoDeAtividade = Nothing
        Dim Mensagem As String

        Grupo = MontaObjeto()

        Try
            Using Servico As IServicoDeGrupoDeAtividade = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeAtividade)()
                If CByte(ViewState(CHAVE_ESTADO_CD_GRUPO)) = Estado.Novo Then
                    Servico.Insira(Grupo)
                    Mensagem = "Grupo de atividade cadastrado com sucesso."
                Else
                    Servico.Modificar(Grupo)
                    Mensagem = "Grupo de atividade modificado com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As IGrupoDeAtividade
        Dim Grupo As IGrupoDeAtividade

        Grupo = FabricaGenerica.GetInstancia.CrieObjeto(Of IGrupoDeAtividade)()

        If CByte(ViewState(CHAVE_ESTADO_CD_GRUPO)) <> Estado.Novo Then
            Grupo.ID = ctrlGrupo1.GrupoSelecionado.ID.Value
        End If

        Grupo.Nome = ctrlGrupo1.NomeDoGrupo
        Return Grupo
    End Function

    Private Sub ExibaGrupo(ByVal Grupo As IGrupoDeAtividade)
        ctrlGrupo1.NomeDoGrupo = Grupo.Nome
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
            Using Servico As IServicoDeGrupoDeAtividade = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeAtividade)()
                Servico.Remover(ctrlGrupo1.GrupoSelecionado.ID.Value)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Grupo de atividade excluído com sucesso."), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.016"
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

    Private Sub GrupoFoiSelecionado(ByVal Grupo As IGrupoDeAtividade)
        ExibaTelaConsultar()
        ExibaGrupo(Grupo)
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

End Class