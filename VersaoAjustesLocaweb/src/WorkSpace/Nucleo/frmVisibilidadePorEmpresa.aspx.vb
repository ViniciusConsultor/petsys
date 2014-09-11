Imports Compartilhados
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI

Public Class frmVisibilidadePorEmpresa
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO_FRM_VISIBILIDADE_EMPRESA As String = "CHAVE_ESTADO_FRM_VISIBILIDADE_EMPRESA"
    Private CHAVE_EMPRESAS_VISIVEIS As String = "CHAVE_FRM_VISIBILIDADE_EMPRESA"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler _ctrlEmpresa1.EmpresaFoiSelecionada, AddressOf EmpresaFoiSelecionada

        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoOperador, Control))
        UtilidadesWeb.LimparComponente(CType(pnlEmpresasVisiveis, Control))
        UtilidadesWeb.LimparComponente(CType(grdEmpresasVisiveis, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoOperador, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlEmpresasVisiveis, Control), False)
        ViewState(CHAVE_ESTADO_FRM_VISIBILIDADE_EMPRESA) = Estado.Inicial
        cboOperador.ClearSelection()
        MostraEmpresasVisiveis(New List(Of IEmpresa))
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
        UtilidadesWeb.HabilitaComponentes(CType(pnlEmpresasVisiveis, Control), True)
        ViewState(CHAVE_ESTADO_FRM_VISIBILIDADE_EMPRESA) = Estado.Novo
        MostraEmpresasVisiveis(New List(Of IEmpresa))
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoOperador, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlEmpresasVisiveis, Control), True)
        grdEmpresasVisiveis.Columns(0).Display = True
        ViewState(CHAVE_ESTADO_FRM_VISIBILIDADE_EMPRESA) = Estado.Modifica
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO_FRM_VISIBILIDADE_EMPRESA) = Estado.Remove
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoOperador, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlEmpresasVisiveis, Control), False)

    End Sub

    Private Sub ExibaTelaConsultar()
        grdEmpresasVisiveis.Columns(0).Display = False
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoOperador, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlEmpresasVisiveis, Control), False)
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Sub btnSalva_Click()
       Dim Mensagem As String

        Try
            Using Servico As IServicoDeVisibilidadePorEmpresa = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVisibilidadePorEmpresa)()
                If CByte(ViewState(CHAVE_ESTADO_FRM_VISIBILIDADE_EMPRESA)) = Estado.Novo Then
                    Servico.Inserir(CLng(cboOperador.SelectedValue), CType(ViewState(CHAVE_EMPRESAS_VISIVEIS), IList(Of IEmpresa)))
                    Mensagem = "Visibilidade cadastrada com sucesso."
                Else
                    Servico.Modifique(CLng(cboOperador.SelectedValue), CType(ViewState(CHAVE_EMPRESAS_VISIVEIS), IList(Of IEmpresa)))
                    Mensagem = "Visibilidade modificada com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
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

            Using Servico As IServicoDeVisibilidadePorEmpresa = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVisibilidadePorEmpresa)()
                Servico.Remova(CLng(cboOperador.SelectedValue))
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Visibilidade excluída com sucesso."), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.015"
    End Function

    Private Sub EmpresaFoiSelecionada(ByVal Empresa As IEmpresa)
        Dim Empresas As IList(Of IEmpresa)

        Empresas = CType(ViewState(CHAVE_EMPRESAS_VISIVEIS), IList(Of IEmpresa))

        If Empresas Is Nothing Then Empresas = New List(Of IEmpresa)

        If Not Empresas.Contains(Empresa) Then
            Empresas.Add(Empresa)
            MostraEmpresasVisiveis(Empresas)
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("Operador já possui visibilidade para esta empresa."), False)
        End If
    End Sub

    Private Sub MostraEmpresasVisiveis(ByVal Empresas As IList(Of IEmpresa))
        Me.grdEmpresasVisiveis.MasterTableView.DataSource = Empresas
        Me.grdEmpresasVisiveis.DataBind()
        ViewState.Add(CHAVE_EMPRESAS_VISIVEIS, Empresas)
    End Sub

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

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Private Sub grdGrupos_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdEmpresasVisiveis.ItemCommand
        Dim IndiceSelecionado As Integer

        If e.CommandName = "Excluir" Then
            Dim Empresas As IList(Of IEmpresa)
            Empresas = CType(ViewState(CHAVE_EMPRESAS_VISIVEIS), IList(Of IEmpresa))
            Empresas.RemoveAt(IndiceSelecionado)
            MostraEmpresasVisiveis(Empresas)
            Exit Sub
        End If
    End Sub

    Private Sub grdGrupos_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdEmpresasVisiveis.ItemCreated
        If (TypeOf e.Item Is GridDataItem) Then
            Dim gridItem As GridDataItem = CType(e.Item, GridDataItem)

            For Each column As GridColumn In grdEmpresasVisiveis.MasterTableView.RenderColumns
                If (TypeOf column Is GridButtonColumn) Then
                    gridItem(column.UniqueName).ToolTip = column.HeaderTooltip
                End If
            Next
        End If
    End Sub

    Private Sub grdEmpresasVisiveis_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdEmpresasVisiveis.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdEmpresasVisiveis, ViewState(CHAVE_EMPRESAS_VISIVEIS), e)
    End Sub

    Private Sub cboOperador_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboOperador.ItemsRequested
        Dim Operadores As IList(Of IOperador)

        Using Servico As IServicoDeOperador = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeOperador)()
            Operadores = Servico.ObtenhaOperadores(e.Text, 50)
        End Using

        If Not Operadores Is Nothing Then
            For Each Operador As IOperador In Operadores
                Dim Item As New RadComboBoxItem(Operador.Pessoa.Nome.Trim, Operador.Pessoa.ID.Value.ToString)

                Item.Attributes.Add("Login", Operador.Login)
                cboOperador.Items.Add(Item)
                Item.DataBind()
            Next
        End If
    End Sub

    Private Sub cboOperador_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboOperador.SelectedIndexChanged
        Dim EmpresasVisiveis As IList(Of IEmpresa)

        Using Servico As IServicoDeVisibilidadePorEmpresa = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVisibilidadePorEmpresa)()
            EmpresasVisiveis = Servico.Obtenha(CLng(e.Value))
        End Using

        If EmpresasVisiveis Is Nothing OrElse EmpresasVisiveis.Count = 0 Then
            ExibaTelaPreparaDadosParaNovaVisibilidade()
            Exit Sub
        End If

        ExibaTelaConsultar()
        MostraEmpresasVisiveis(EmpresasVisiveis)
    End Sub

    Private Sub ExibaTelaPreparaDadosParaNovaVisibilidade()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(pnlEmpresasVisiveis, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlEmpresasVisiveis, Control), False)
        ViewState(CHAVE_ESTADO_FRM_VISIBILIDADE_EMPRESA) = Estado.Inicial
        MostraEmpresasVisiveis(New List(Of IEmpresa))
    End Sub

End Class