Imports Telerik.Web
Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI

Partial Public Class frmAutorizacao
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlGrupo1.GrupoFoiSelecionado, AddressOf GrupoFoiSelecionado

        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        ctrlGrupo1.LimparControle()
        ctrlGrupo1.HabiliteComponente(True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlAutorizacao, Control), False)
        PreenchaModulos()
    End Sub

    Private Sub ExibaTelaModificar()
        ctrlGrupo1.HabiliteComponente(False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlAutorizacao, Control), True)
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        pnlAutorizacao.Visible = True
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        ctrlGrupo1.HabiliteComponente(True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlAutorizacao, Control), False)
    End Sub

    Private Sub PreenchaModulos()
        Dim Modulos As IList(Of IModulo)

        Using Servico As IServicoDeAutorizacao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAutorizacao)()
            Modulos = Servico.ObtenhaModulosDisponiveis
        End Using

        Me.trwModulos.Nodes.Clear()

        For Each Modulo As IModulo In Modulos
            Dim NodeModulo As RadTreeNode

            NodeModulo = New RadTreeNode(Modulo.Nome, Modulo.ID.ToString)

            For Each Funcao As IFuncao In Modulo.ObtenhaFuncoes
                Dim NodeFuncao As RadTreeNode

                NodeFuncao = New RadTreeNode(Funcao.Nome, Funcao.ID.ToString)

                For Each Operacao As IOperacao In Funcao.ObtenhaOperacoes
                    Dim NodeOperacao As RadTreeNode

                    NodeOperacao = New RadTreeNode(Operacao.Nome, Operacao.ID.ToString)
                    NodeFuncao.Nodes.Add(NodeOperacao)
                Next

                NodeModulo.Nodes.Add(NodeFuncao)
            Next

            Me.trwModulos.Nodes.Add(NodeModulo)
        Next

    End Sub

    Private Sub GrupoFoiSelecionado(ByVal Grupo As IGrupo)
        Dim DiretivasDoGrupo As IList(Of IDiretivaDeSeguranca)

        Using Servico As IServicoDeAutorizacao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAutorizacao)()
            DiretivasDoGrupo = Servico.ObtenhaDiretivasDeSegurancaDoGrupo(Grupo.ID.Value)
        End Using

        If Not DiretivasDoGrupo Is Nothing AndAlso Not DiretivasDoGrupo.Count = 0 Then
            MarqueServicosComDiretivas(DiretivasDoGrupo)
        End If

        ExibaTelaConsultar()
    End Sub

    Private Sub MarqueServicosComDiretivas(ByVal Diretivas As IList(Of IDiretivaDeSeguranca))
        For Each Diretiva As IDiretivaDeSeguranca In Diretivas
            Dim No As RadTreeNode

            No = trwModulos.FindNodeByValue(Diretiva.ID)
            No.Expanded = True

            If Not No.ParentNode Is Nothing AndAlso Not No.Nodes Is Nothing AndAlso No.Nodes.Count = 0 Then
                No.Checked = True
            End If
        Next
    End Sub

    Private Sub btnModificar_Click()
        If String.IsNullOrEmpty(ctrlGrupo1.NomeDoGrupo) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("O grupo deve ser informado."), False)
            Exit Sub
        End If

        Me.ExibaTelaModificar()
    End Sub

    Private Sub btnSalva_Click()

        Try
            Dim ItensSelecionados As IList(Of RadTreeNode)
            Dim Diretivas As IList(Of IDiretivaDeSeguranca)
            Dim Diretiva As IDiretivaDeSeguranca

            ItensSelecionados = trwModulos.CheckedNodes
            Diretivas = New List(Of IDiretivaDeSeguranca)

            For Each No As RadTreeNode In ItensSelecionados
                Diretiva = FabricaGenerica.GetInstancia.CrieObjeto(Of IDiretivaDeSeguranca)()
                Diretiva.ID = No.Value
                Diretivas.Add(Diretiva)
            Next

            Using Servico As IServicoDeAutorizacao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAutorizacao)()
                Servico.Modifique(ctrlGrupo1.GrupoSelecionado.ID.Value, Diretivas)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Autorização modificada com sucesso."), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try

    End Sub

    Private Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnModificar"
                Call btnModificar_Click()
            Case "btnSalvar"
                Call btnSalva_Click()
            Case "btnCancelar"
                Call btnCancela_Click()
        End Select
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.003"
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

End Class