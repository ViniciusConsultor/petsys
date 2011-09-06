Imports Compartilhados.Componentes.Web
Imports Estoque.Interfaces.Negocio
Imports Telerik.Web.UI
Imports Compartilhados.Fabricas

Partial Public Class frmEntradaDeProduto
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_FRMENTRADADEPRODUTO"
    Private CHAVE_PRODUTOS_MOVIMENTADOS_ENTRADA As String = "CHAVE_PRODUTOS_MOVIMENTADOS_ENTRADA"

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

        Session(CHAVE_ESTADO) = Estado.Inicial
        Session(CHAVE_PRODUTOS_MOVIMENTADOS_ENTRADA) = Nothing
        Session(CHAVE_PRODUTOS_MOVIMENTADOS_ENTRADA) = New List(Of IProdutoMovimentado)
        ExibaItensMovimentados(New List(Of IProdutoMovimentado))
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
        Session(CHAVE_ESTADO) = Estado.Novo

    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        Session(CHAVE_ESTADO) = Estado.Modifica

    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        Session(CHAVE_ESTADO) = Estado.Remove

    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False

    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Function ValidaDados() As String
        'If String.IsNullOrEmpty(cboMarca.Text) Then
        '    Return "O nome da marca do produto deve ser informado."
        'End If

        Return Nothing
    End Function

    Private Sub btnSalva_Click()
        '' Dim MarcaDeProduto As IMarcaDeProduto = Nothing
        'Dim Mensagem As String
        'Dim Inconsistencia As String

        'Inconsistencia = ValidaDados()

        'If Not String.IsNullOrEmpty(Inconsistencia) Then
        '    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
        '    Exit Sub
        'End If

        'MarcaDeProduto = MontaObjeto()

        'Try
        '    Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
        '        If CByte(Session(CHAVE_ESTADO)) = Estado.Novo Then
        '            Servico.InserirMarcaDeProduto(MarcaDeProduto)
        '            Mensagem = "Marca de produto cadastrado com sucesso."
        '        Else
        '            Servico.AtualizarMarcaDeProduto(MarcaDeProduto)
        '            Mensagem = "Marca de produto alterado com sucesso."
        '        End If

        '    End Using

        '    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
        '    ExibaTelaInicial()

        'Catch ex As BussinesException
        '    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        'End Try
    End Sub

    Private Function MontaObjeto() As IMarcaDeProduto
        'Dim Marca As IMarcaDeProduto

        'Marca = FabricaGenerica.GetInstancia.CrieObjeto(Of IMarcaDeProduto)()

        'If CByte(Session(CHAVE_ESTADO)) <> Estado.Novo Then
        '    Marca.ID = CLng(Session(CHAVE_ID))
        'End If

        'Marca.Nome = cboMarca.Text

        'Return Marca

        Return Nothing
    End Function

    'Private Sub ExibaMarcaDeProduto(ByVal Marca As IMarcaDeProduto)
    '    cboMarca.Text = Marca.Nome
    '    Session(CHAVE_ID) = Marca.ID
    'End Sub

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
        'Try
        '    Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
        '        Servico.RemoverMarcaDeProduto(CLng(Session(CHAVE_ID)))
        '    End Using

        '    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Marca de produto removida com sucesso."), False)
        '    ExibaTelaInicial()

        'Catch ex As BussinesException
        '    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        'End Try
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.ETQ.005"
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

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Private Sub btnAdicionar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdicionar.Click
        Dim ProdutoMovimentado As IProdutoMovimentado
        Dim ProdutosMovimentados As IList(Of IProdutoMovimentado)

        ProdutoMovimentado = FabricaGenerica.GetInstancia.CrieObjeto(Of IProdutoMovimentado)()
        ProdutoMovimentado.Preco = txtPrecoProdutoMovimentado.Value.Value
        ProdutoMovimentado.Produto = ctrlProduto1.ProdutoSelecionado
        ProdutoMovimentado.Quantidade = txtQuantidadeDeProdutoMovimentado.Value.Value

        ProdutosMovimentados = CType(Session(CHAVE_PRODUTOS_MOVIMENTADOS_ENTRADA), IList(Of IProdutoMovimentado))
        ProdutosMovimentados.Add(ProdutoMovimentado)
        ExibaItensMovimentados(ProdutosMovimentados)
        Session(CHAVE_PRODUTOS_MOVIMENTADOS_ENTRADA) = ProdutosMovimentados
        LimpaCamposProdutoMovimentado()
    End Sub

    Private Sub LimpaCamposProdutoMovimentado()
        txtQuantidadeDeProdutoMovimentado.Text = ""
        txtPrecoProdutoMovimentado.Text = ""
        ctrlProduto1.LimpaComponente()
    End Sub

    Private Sub ExibaItensMovimentados(ByVal Itens As IList(Of IProdutoMovimentado))
        grdItensLancados.DataSource = Itens
        grdItensLancados.DataBind()
    End Sub
End Class