Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados

Partial Public Class cdProduto
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO"
    Private CHAVE_ID_GRUPOS_DE_PRODUTO As String = "CHAVE_ID_GRUPOS_DE_PRODUTO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsPostBack Then
        '    ExibaTelaInicial()
        'End If
    End Sub

    'Private Sub ExibaTelaInicial()
    '    CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
    '    CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
    '    UtilidadesWeb.LimparComponente(CType(pnlCaracteristicasDosGruposDeProduto, Control))
    '    UtilidadesWeb.LimparComponente(CType(pnlGruposDeProduto, Control))
    '    UtilidadesWeb.HabilitaComponentes(CType(pnlGruposDeProduto, Control), True)
    '    UtilidadesWeb.HabilitaComponentes(CType(pnlCaracteristicasDosGruposDeProduto, Control), False)
    '    ViewState(CHAVE_ESTADO) = Estado.Inicial
    '    cboGruposDeProduto.EmptyMessage = "Selecione um grupo de produto"
    'End Sub

    'Protected Sub btnNovo_Click()
    '    ExibaTelaNovo()
    'End Sub

    'Private Sub ExibaTelaNovo()
    '    CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
    '    CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
    '    CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
    '    ViewState(CHAVE_ESTADO) = Estado.Novo
    '    UtilidadesWeb.LimparComponente(CType(pnlCaracteristicasDosGruposDeProduto, Control))
    '    UtilidadesWeb.LimparComponente(CType(pnlGruposDeProduto, Control))
    '    UtilidadesWeb.HabilitaComponentes(CType(pnlCaracteristicasDosGruposDeProduto, Control), True)
    '    cboGruposDeProduto.EmptyMessage = ""
    'End Sub

    'Private Sub ExibaTelaModificar()
    '    CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
    '    CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
    '    CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
    '    ViewState(CHAVE_ESTADO) = Estado.Modifica
    '    UtilidadesWeb.HabilitaComponentes(CType(pnlCaracteristicasDosGruposDeProduto, Control), True)
    '    cboGruposDeProduto.EmptyMessage = ""
    'End Sub

    'Private Sub ExibaTelaExcluir()
    '    CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
    '    CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
    '    ViewState(CHAVE_ESTADO) = Estado.Remove
    '    UtilidadesWeb.HabilitaComponentes(CType(pnlGruposDeProduto, Control), False)
    '    cboGruposDeProduto.EmptyMessage = ""
    'End Sub

    'Private Sub ExibaTelaConsultar()
    '    CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
    '    CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
    '    CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
    '    CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
    '    CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
    '    cboGruposDeProduto.EmptyMessage = ""
    'End Sub

    'Protected Sub btnCancela_Click()
    '    ExibaTelaInicial()
    'End Sub

    'Private Sub btnSalva_Click()
    '    Dim GrupoDeProduto As IGrupoDeProduto = Nothing
    '    Dim Mensagem As String

    '    GrupoDeProduto = MontaObjeto()

    '    Try
    '        Using Servico As IServicoDeGrupoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeProduto)()
    '            If CByte(ViewState(CHAVE_ESTADO)) = Estado.Novo Then
    '                Servico.Inserir(GrupoDeProduto)
    '                Mensagem = "Grupo de produto cadastrado com sucesso."
    '            Else
    '                Servico.Atualizar(GrupoDeProduto)
    '                Mensagem = "Grupo de produto modificado com sucesso."
    '            End If

    '        End Using

    '        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
    '        ExibaTelaInicial()

    '    Catch ex As BussinesException
    '        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
    '    End Try
    'End Sub

    'Private Function MontaObjeto() As IGrupoDeProduto
    '    Dim Grupo As IGrupoDeProduto

    '    Grupo = FabricaGenerica.GetInstancia.CrieObjeto(Of IGrupoDeProduto)()

    '    If CByte(ViewState(CHAVE_ESTADO)) <> Estado.Novo Then
    '        Grupo.ID = CLng(ViewState(CHAVE_ID_GRUPOS_DE_PRODUTO))
    '    End If

    '    Grupo.Nome = cboGruposDeProduto.Text
    '    Grupo.PorcentagemDeComissao = CDbl(txtPorcentagemDeComissao.Value)

    '    Return Grupo
    'End Function

    'Private Sub ExibaGrupoDeProduto(ByVal Grupo As IGrupoDeProduto)
    '    cboGruposDeProduto.Text = Grupo.Nome
    '    ViewState(CHAVE_ID_GRUPOS_DE_PRODUTO) = Grupo.ID
    '    txtPorcentagemDeComissao.Value = Grupo.PorcentagemDeComissao
    'End Sub

    'Private Sub btnModificar_Click()
    '    ExibaTelaModificar()
    'End Sub

    'Private Sub btnExclui_Click()
    '    ExibaTelaExcluir()
    'End Sub

    'Private Sub btnNao_Click()
    '    Me.ExibaTelaInicial()
    'End Sub

    'Private Sub btnSim_Click()
    '    Try
    '        Using Servico As IServicoDeGrupoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeProduto)()
    '            Servico.Remover(CLng(ViewState(CHAVE_ID_GRUPOS_DE_PRODUTO)))
    '        End Using

    '        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Grupo de produto excluído com sucesso."), False)
    '        ExibaTelaInicial()

    '    Catch ex As BussinesException
    '        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
    '    End Try
    'End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.ETQ.002"
    End Function

    'Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
    '    Select Case CType(e.Item, RadToolBarButton).CommandName
    '        Case "btnNovo"
    '            Call btnNovo_Click()
    '        Case "btnModificar"
    '            Call btnModificar_Click()
    '        Case "btnExcluir"
    '            Call btnExclui_Click()
    '        Case "btnSalvar"
    '            Call btnSalva_Click()
    '        Case "btnCancelar"
    '            Call btnCancela_Click()
    '        Case "btnSim"
    '            Call btnSim_Click()
    '        Case "btnNao"
    '            Call btnNao_Click()
    '    End Select
    'End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    'Private Sub cboGruposDeProduto_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboGruposDeProduto.ItemsRequested
    '    Dim Grupos As IList(Of IGrupoDeProduto)

    '    Using Servico As IServicoDeGrupoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeProduto)()
    '        Grupos = Servico.ObtenhaGruposDeProdutosPorNome(e.Text, 50)
    '    End Using

    '    If Not Grupos Is Nothing Then
    '        For Each Grupo As IGrupoDeProduto In Grupos
    '            cboGruposDeProduto.Items.Add(New RadComboBoxItem(Grupo.Nome, Grupo.ID.ToString))
    '        Next
    '    End If
    'End Sub

    'Private Sub cboGruposDeProduto_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboGruposDeProduto.SelectedIndexChanged
    '    Dim Grupo As IGrupoDeProduto
    '    Dim Valor As String

    '    Valor = DirectCast(o, RadComboBox).SelectedValue
    '    If String.IsNullOrEmpty(Valor) Then Return

    '    Using Servico As IServicoDeGrupoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeProduto)()
    '        Grupo = Servico.ObtenhaGrupoDeProdutos(CLng(Valor))
    '    End Using

    '    Me.ExibaGrupoDeProduto(Grupo)
    '    Me.ExibaTelaConsultar()
    'End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim Principal As Compartilhados.Principal

        Principal = FabricaDeContexto.GetInstancia.GetContextoAtual
        'Só verificamos se tem permissão se o botão estiver marcado para ser exibido (pela aplicação)
        If btnNovaMarca.Visible Then
            Principal.EstaAutorizado(btnNovaMarca.CommandArgument)
        End If
    End Sub

End Class