﻿Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados
Imports Estoque.Interfaces.Negocio
Imports Estoque.Interfaces.Servicos
Imports Compartilhados.Fabricas

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
    Private CHAVE_ID_PRODUTO As String = "CHAVE_ID_PRODUTO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        UtilidadesWeb.LimparComponente(CType(pnlCaracteristicasDoProduto, Control))
        UtilidadesWeb.LimparComponente(CType(pnlImpostosValores, Control))
        UtilidadesWeb.LimparComponente(CType(pnlProduto, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlProduto, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlImpostosValores, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlCaracteristicasDoProduto, Control), False)
        ViewState(CHAVE_ESTADO) = Estado.Inicial
        cboProduto.EmptyMessage = "Selecione um produto"
        txtCodigo.EmptyMessage = "Informe o código"
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
        ViewState(CHAVE_ESTADO) = Estado.Novo
        UtilidadesWeb.LimparComponente(CType(pnlCaracteristicasDoProduto, Control))
        UtilidadesWeb.LimparComponente(CType(pnlImpostosValores, Control))
        UtilidadesWeb.LimparComponente(CType(pnlProduto, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlImpostosValores, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlCaracteristicasDoProduto, Control), True)
        cboProduto.EmptyMessage = ""
        txtCodigo.EmptyMessage = ""
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO) = Estado.Modifica
        UtilidadesWeb.HabilitaComponentes(CType(pnlImpostosValores, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlCaracteristicasDoProduto, Control), True)
        cboProduto.EmptyMessage = ""
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO) = Estado.Remove
        UtilidadesWeb.HabilitaComponentes(CType(pnlProduto, Control), False)
        cboProduto.EmptyMessage = ""
        txtCodigo.EmptyMessage = ""
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        cboProduto.EmptyMessage = ""
        txtCodigo.EmptyMessage = ""
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

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

    Private Sub ExibaProduto(ByVal Produto As IProduto)
        txtCodigo.Text = Produto.CodigoDeBarras
        cboProduto.Text = Produto.Nome
        cboGrupoDeProduto.SelectedValue = Produto.GrupoDeProduto.ID.Value.ToString
        cboGrupoDeProduto.Text = Produto.GrupoDeProduto.Nome
        cboMarca.SelectedValue = Produto.Marca.ID.Value.ToString
        cboMarca.Text = Produto.Marca.Nome
        ViewState(CHAVE_ID_PRODUTO) = Produto.ID
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

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                Call btnNovo_Click()
            Case "btnModificar"
                Call btnModificar_Click()
            Case "btnExcluir"
                Call btnExclui_Click()
            Case "btnSalvar"
                ' Call btnSalva_Click()
            Case "btnCancelar"
                Call btnCancela_Click()
            Case "btnSim"
                'Call btnSim_Click()
            Case "btnNao"
                Call btnNao_Click()
        End Select
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim Principal As Compartilhados.Principal

        Principal = FabricaDeContexto.GetInstancia.GetContextoAtual
        'Só verificamos se tem permissão se o botão estiver marcado para ser exibido (pela aplicação)
        If btnNovaMarca.Visible Then
            Principal.EstaAutorizado(btnNovaMarca.CommandArgument)
        End If
    End Sub

    Private Sub btnNovaMarca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNovaMarca.Click
        Dim URL As String

        URL = ObtenhaURL()
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de marcas de produtos"), False)
    End Sub

    Private Function ObtenhaURL() As String
        Dim URL As String

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual
        Return String.Concat(URL, "Estoque/cdMarca.aspx")
    End Function

    Private Sub cboProduto_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboProduto.ItemsRequested
        Dim Produtos As IList(Of IProduto)

        Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
            Produtos = Servico.ObtenhaProdutos(e.Text, 50)

            If Not Produtos Is Nothing Then
                For Each Produto As IProduto In Produtos
                    cboProduto.Items.Add(New RadComboBoxItem(Produto.Nome, Produto.ID.ToString))
                Next
            End If
        End Using
    End Sub

    Private Sub cboProduto_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboProduto.SelectedIndexChanged
        Dim Produto As IProduto
        Dim Valor As String

        Valor = DirectCast(o, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then Return

        Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
            Produto = Servico.ObtenhaProduto(CLng(Valor))
        End Using

        Me.ExibaProduto(Produto)
        Me.ExibaTelaConsultar()
    End Sub

    Private Sub txtCodigo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigo.TextChanged
        If String.IsNullOrEmpty(txtCodigo.Text) Then Exit Sub

        Dim Produto As IProduto

        Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
            Produto = Servico.ObtenhaProduto(txtCodigo.Text)
        End Using

        If Produto Is Nothing Then Exit Sub

    End Sub

    Private Sub cboGrupoDeProduto_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboGrupoDeProduto.ItemsRequested
        Dim Grupos As IList(Of IGrupoDeProduto)

        Using Servico As IServicoDeGrupoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeProduto)()
            Grupos = Servico.ObtenhaGruposDeProdutosPorNome(e.Text, 50)

            If Not Grupos Is Nothing Then
                For Each Grupo As IGrupoDeProduto In Grupos
                    cboGrupoDeProduto.Items.Add(New RadComboBoxItem(Grupo.Nome, Grupo.ID.ToString))
                Next
            End If
        End Using
    End Sub

    Private Sub cboMarca_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboMarca.ItemsRequested
        Dim Marcas As IList(Of IMarcaDeProduto)

        Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
            Marcas = Servico.ObtenhaMarcasDeProdutosPorNome(e.Text, 50)

            If Not Marcas Is Nothing Then
                For Each Marca As IMarcaDeProduto In Marcas
                    cboMarca.Items.Add(New RadComboBoxItem(Marca.Nome, Marca.ID.ToString))
                Next
            End If
        End Using
    End Sub

End Class