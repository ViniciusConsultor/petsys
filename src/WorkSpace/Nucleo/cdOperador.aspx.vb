Imports Compartilhados.Componentes.Web
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Servicos
Imports Telerik.Web.UI
Imports Core.Interfaces.Negocio
Imports Core.Interfaces.Servicos

Partial Public Class cdOperador
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO_CD_OPERADOR As String = "CHAVE_ESTADO_CD_OPERADOR"
    Private CHAVE_GRUPOS As String = "CD_OPERADOR_CHAVE_GRUPOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlPessoa1.PessoaFoiSelecionada, AddressOf ObtenhaOperador
        AddHandler ctrlGrupo1.GrupoFoiSelecionado, AddressOf GrupoFoiSelecionado

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
        UtilidadesWeb.LimparComponente(CType(pnlSenha, Control))
        UtilidadesWeb.LimparComponente(CType(pnlGruposDoOperador, Control))
        UtilidadesWeb.LimparComponente(CType(grdGrupos, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoOperador, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlGruposDoOperador, Control), False)
        DokSenha.Visible = False
        ctrlPessoa1.Inicializa()
        ctrlPessoa1.BotaoDetalharEhVisivel = False
        ctrlPessoa1.BotaoNovoEhVisivel = True
        ViewState(CHAVE_ESTADO_CD_OPERADOR) = Estado.Inicial
        MostraGrupos(New List(Of IGrupo))
        CarregaStatus()
        rblStatus.SelectedValue = StatusDoOperador.Ativo.ID.ToString
        ' grdGrupos.Columns(0).Visible = True
    End Sub

    Private Sub CarregaStatus()
        Me.rblStatus.Items.Clear()

        For Each Status As StatusDoOperador In StatusDoOperador.ObtenhaTodosStatus
            rblStatus.Items.Add(New ListItem(Status.Descricao, Status.ID))
        Next
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
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoOperador, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlGruposDoOperador, Control), True)
        ViewState(CHAVE_ESTADO_CD_OPERADOR) = Estado.Novo
        DokSenha.Visible = True
        MostraGrupos(New List(Of IGrupo))
        'grdGrupos.Columns(0).Visible = True
    End Sub

    Private Sub ExibaTelaModificar()
        grdGrupos.Columns(0).Display = True
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoOperador, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlGruposDoOperador, Control), True)
        ViewState(CHAVE_ESTADO_CD_OPERADOR) = Estado.Modifica
        DokSenha.Visible = False
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO_CD_OPERADOR) = Estado.Remove
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoOperador, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlGruposDoOperador, Control), False)
        DokSenha.Visible = False
        'grdGrupos.Columns(0).Visible = False
    End Sub

    Private Sub ExibaTelaConsultar()
        grdGrupos.Columns(0).Display = False
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        DokSenha.Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoOperador, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlGruposDoOperador, Control), False)
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Function ObtenhaSenha() As ISenha
        Dim Senha As ISenha
        Dim SenhaTXTCript As String

        SenhaTXTCript = AjudanteDeCriptografia.CriptografeMaoUnicao(txtSenha.Text)

        Senha = FabricaGenerica.GetInstancia.CrieObjeto(Of ISenha)(New Object() {SenhaTXTCript, Now})
        Return Senha
    End Function

    Private Sub btnSalva_Click()
        Dim Operador As IOperador = Nothing
        Dim Mensagem As String

        Operador = MontaObjeto()

        Try
            Using Servico As IServicoDeOperador = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeOperador)()
                If CByte(ViewState(CHAVE_ESTADO_CD_OPERADOR)) = Estado.Novo Then
                    Servico.Inserir(Operador, ObtenhaSenha)
                    Mensagem = "Operador cadastrado com sucesso."
                Else
                    Servico.Modificar(Operador)
                    Mensagem = "Operador modificado com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As IOperador
        Dim Operador As IOperador
        Dim Pessoa As IPessoa

        Pessoa = ctrlPessoa1.PessoaSelecionada
        Operador = FabricaGenerica.GetInstancia.CrieObjeto(Of IOperador)(New Object() {Pessoa})
        Operador.Login = txtLogin.Text
        Operador.Status = StatusDoOperador.ObtenhaStatus(CChar(rblStatus.SelectedValue))
        Operador.AdicioneGrupos(CType(ViewState(CHAVE_GRUPOS), IList(Of IGrupo)))

        Return Operador
    End Function

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
            Using Servico As IServicoDeOperador = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeOperador)()
                Servico.Remover(ctrlPessoa1.PessoaSelecionada.ID.Value)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Operador excluído com sucesso."), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.005"
    End Function

    Private Sub GrupoFoiSelecionado(ByVal Grupo As IGrupo)
        Dim Grupos As IList(Of IGrupo)

        Grupos = CType(ViewState(CHAVE_GRUPOS), IList(Of IGrupo))

        If Grupos Is Nothing Then Grupos = New List(Of IGrupo)

        If Not Grupos.Contains(Grupo) Then
            Grupos.Add(Grupo)
            MostraGrupos(Grupos)
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("Operador já possui este grupo."), False)
        End If
    End Sub

    Private Sub MostraGrupos(ByVal Grupos As IList(Of IGrupo))
        Me.grdGrupos.MasterTableView.DataSource = Grupos
        Me.grdGrupos.DataBind()
        ViewState.Add(CHAVE_GRUPOS, Grupos)
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

    Private Sub ObtenhaOperador(ByVal Pessoa As IPessoa)
        Dim Operador As IOperador

        ctrlPessoa1.BotaoDetalharEhVisivel = True

        Using Servico As IServicoDeOperador = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeOperador)()
            Operador = Servico.ObtenhaOperador(Pessoa)
        End Using

        If Operador Is Nothing Then
            ExibaTelaPreparaDadosParaNovoOperador()
            Exit Sub
        End If

        MostreOperador(Operador)
        ExibaTelaConsultar()
    End Sub

    Private Sub MostreOperador(ByVal Operador As IOperador)
        Me.txtLogin.Text = Operador.Login
        Me.rblStatus.SelectedValue = Operador.Status.ID

        If Operador.ObtenhaGrupos.Count <> 0 Then
            MostraGrupos(Operador.ObtenhaGrupos)
        End If
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Private Sub ExibaTelaPreparaDadosParaNovoOperador()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoOperador, Control))
        UtilidadesWeb.LimparComponente(CType(pnlSenha, Control))
        UtilidadesWeb.LimparComponente(CType(pnlGruposDoOperador, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoOperador, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlGruposDoOperador, Control), False)
        DokSenha.Visible = False
        ctrlPessoa1.BotaoDetalharEhVisivel = False
        ctrlPessoa1.BotaoNovoEhVisivel = True
        ViewState(CHAVE_ESTADO_CD_OPERADOR) = Estado.Inicial
        CarregaStatus()
        rblStatus.SelectedValue = StatusDoOperador.Ativo.ID.ToString
        MostraGrupos(New List(Of IGrupo))
    End Sub

    Private Sub grdGrupos_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdGrupos.ItemCommand
        Dim IndiceSelecionado As Integer

        If Not e.CommandName = "Page" AndAlso Not e.CommandName = "ChangePageSize" Then
            IndiceSelecionado = e.Item().ItemIndex
        End If

        If e.CommandName = "Excluir" Then
            Dim Grupos As IList(Of IGrupo)
            Grupos = CType(ViewState(CHAVE_GRUPOS), IList(Of IGrupo))
            Grupos.RemoveAt(IndiceSelecionado)
            MostraGrupos(Grupos)
        End If
    End Sub

    Private Sub grdGrupos_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdGrupos.ItemCreated
        If (TypeOf e.Item Is GridDataItem) Then
            Dim gridItem As GridDataItem = CType(e.Item, GridDataItem)

            For Each column As GridColumn In grdGrupos.MasterTableView.RenderColumns
                If (TypeOf column Is GridButtonColumn) Then
                    gridItem(column.UniqueName).ToolTip = column.HeaderTooltip
                End If
            Next
        End If
    End Sub

    Private Sub grdGrupos_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdGrupos.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdGrupos, ViewState(CHAVE_GRUPOS), e)
    End Sub

End Class