Imports Compartilhados.Componentes.Web
Imports Compartilhados
Imports Core.Interfaces.Negocio
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI
Imports Compartilhados.Visual
Imports Compartilhados.Interfaces.Core.Servicos

Partial Public Class frmAtalhos
    Inherits SuperPagina

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO"
    Private CHAVE_ATALHOS_EXTERNOS As String = "CHAVE_ATALHOS_EXTERNOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        ViewState(CHAVE_ATALHOS_EXTERNOS) = New List(Of Atalho)
        ApresenteMenuParaEscolhaDosAtalhos()
        LimpaCamposDoAtalhoExterno()
        ExibaAtalhosExternos(New List(Of Atalho))

        If FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil.UsuarioTemAtalhos Then
            ExibaAtalhos()
        End If
    End Sub

    Private Sub ApresenteMenuParaEscolhaDosAtalhos()
        Dim Menu As IMenuComposto
        Dim Principal As Principal = FabricaDeContexto.GetInstancia.GetContextoAtual

        Using Servico As IServicoDeMenu = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeMenu)()
            Menu = Servico.ObtenhaMenu
        End Using

        Me.trwMenuDoUsuario.Nodes.Clear()

        For Each MenuModulo As IMenuComposto In Menu.ObtenhaItens
            If Principal.EstaAutorizado(MenuModulo.ID) Then
                Dim NodeModulo As RadTreeNode

                NodeModulo = New RadTreeNode(MenuModulo.Nome, MenuModulo.ID.ToString)
                PreenchaMenuFuncionalidadesDoModulo(MenuModulo, Principal, NodeModulo)
                Me.trwMenuDoUsuario.Nodes.Add(NodeModulo)
            End If
        Next
    End Sub

    Private Sub ExibaAtalhos()
        ExibaAtalhosExternos(FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil.ObtenhaAtalhosExternos)
        ExibaAtalhosDoSistema(FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil.ObtenhaAtalhosDoSistema)
    End Sub

    Private Sub ExibaAtalhosDoSistema(ByVal AtalhosDoSistema As IList(Of Atalho))
        For Each Atalho As Atalho In AtalhosDoSistema
            Dim No As RadTreeNode
            Dim IDAtalho As String = String.Concat(Atalho.ID.ToString, "|", Atalho.Imagem, "|", Atalho.URL)

            No = trwMenuDoUsuario.FindNodeByValue(IDAtalho)
            No.Expanded = True

            If Not No.ParentNode Is Nothing AndAlso Not No.Nodes Is Nothing AndAlso No.Nodes.Count = 0 Then
                No.Checked = True
            End If
        Next
    End Sub

    Private Sub PreenchaMenuFuncionalidadesDoModulo(ByVal MenuModulo As IMenuComposto, _
                                                    ByVal Principal As Principal, _
                                                    ByRef NodeModulo As RadTreeNode)
        For Each MenuFuncao As IMenuFolha In MenuModulo.ObtenhaItens

            If Principal.EstaAutorizado(MenuFuncao.ID) Then
                Dim NodeFuncao As RadTreeNode

                NodeFuncao = New RadTreeNode(MenuFuncao.Nome, String.Concat(MenuFuncao.ID.ToString, "|", MenuFuncao.Imagem, "|", MenuFuncao.URL))
                NodeModulo.Nodes.Add(NodeFuncao)
            End If
        Next
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalva_Click()
        End Select
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.011"
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Private Sub btnSalva_Click()
        Dim Atalhos As New List(Of Atalho)
        Dim AtalhosExternos As IList(Of Atalho)

        AtalhosExternos = DirectCast(ViewState(CHAVE_ATALHOS_EXTERNOS), IList(Of Atalho))

        If Not AtalhosExternos.Count = 0 Then
            Atalhos.AddRange(AtalhosExternos)
        End If

        Dim AtalhosDoSistema As IList(Of Atalho) = New List(Of Atalho)
        Dim ItensSelecionados As IList(Of RadTreeNode)

        ItensSelecionados = trwMenuDoUsuario.CheckedNodes

        For Each No As RadTreeNode In ItensSelecionados
            If Not No.Value.Contains("MOD") Then
                Dim Atalho As Atalho
                Dim URL As String
                Dim ID As String
                Dim Imagem As String
                Dim Chave() As String

                Chave = Split(No.Value, "|")
                ID = Chave(0)
                Imagem = Chave(1)
                URL = Chave(2)

                Atalho = New AtalhoSistema(ID, No.Text, URL, Imagem)
                AtalhosDoSistema.Add(Atalho)
            End If
        Next

        If Not AtalhosDoSistema.Count = 0 Then
            Atalhos.AddRange(AtalhosDoSistema)
        End If

        Try
            Using Servico As IServicoDePerfil = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePerfil)()
                Servico.SalveAtalhos(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario, Atalhos)
            End Using

            ExibaTelaInicial()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Atalhos modificados com sucesso."), False)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Sub btnAdicionarItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdicionarItem.Click
        Dim AtalhoASerInserido As AtalhoExterno
        Dim Inconsistencias As IList(Of String)
        Dim AtalhosExternos As IList(Of Atalho)

        Inconsistencias = ValidaInsercaoDeAtalhosExternos()

        If Not Inconsistencias.Count = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsistencias(Inconsistencias), False)
            Exit Sub
        End If

        AtalhosExternos = CType(ViewState(CHAVE_ATALHOS_EXTERNOS), IList(Of Atalho))
        AtalhoASerInserido = CriaAtalhoExterno()
        AtalhosExternos.Add(AtalhoASerInserido)
        ExibaAtalhosExternos(AtalhosExternos)
        LimpaCamposDoAtalhoExterno()
    End Sub

    Private Sub ExibaAtalhosExternos(ByVal Atalhos As IList(Of Atalho))
        ViewState(CHAVE_ATALHOS_EXTERNOS) = Atalhos
        grdAtalhosExternos.DataSource = Atalhos
        grdAtalhosExternos.DataBind()
    End Sub

    Private Function CriaAtalhoExterno() As AtalhoExterno
        Dim Atalho As AtalhoExterno

        Atalho = New AtalhoExterno(txtNomeAtalhoExterno.Text, txtURLAtalhoExterno.Text, Nothing)

        Return Atalho
    End Function

    Private Function ValidaInsercaoDeAtalhosExternos() As IList(Of String)
        Dim Inconsistencias As New List(Of String)

        If String.IsNullOrEmpty(txtNomeAtalhoExterno.Text) Then
            Inconsistencias.Add("O nome do atalho deve ser informado.")
        End If

        If String.IsNullOrEmpty(txtURLAtalhoExterno.Text) Then
            Inconsistencias.Add("A URL do atalho deve ser informada.")
        End If

        Return Inconsistencias
    End Function

    Private Sub LimpaCamposDoAtalhoExterno()
        Me.txtNomeAtalhoExterno.Text = ""
        Me.txtURLAtalhoExterno.Text = ""
    End Sub

End Class