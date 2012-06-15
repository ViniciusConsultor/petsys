Imports Compartilhados.Componentes.Web
Imports Estoque.Interfaces.Negocio
Imports Estoque.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Telerik.Web.UI
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos

Partial Public Class frmEntradaDeProduto
    Inherits SuperPagina

    Private Enum Estado As Byte
        Novo = 1
        Consulta
    End Enum

    Private CHAVE_PRODUTOS_MOVIMENTADOS_ENTRADA As String = "CHAVE_PRODUTOS_MOVIMENTADOS_ENTRADA"
    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_CDMOVIMENTACAOENTRADADEPRODUTO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        Session(CHAVE_PRODUTOS_MOVIMENTADOS_ENTRADA) = New List(Of IProdutoMovimentado)
        ExibaItensMovimentados(New List(Of IProdutoMovimentado))
    End Sub

    Protected Sub btnNovo_Click()
        ExibaTelaNovo()
    End Sub

    Private Sub ExibaTelaNovo()
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        txtDataDaMovimentacao.SelectedDate = Now
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
    End Sub

    Private Function ValidaDados() As String
        'If String.IsNullOrEmpty(cboMarca.Text) Then
        '    Return "O nome da marca do produto deve ser informado."
        'End If

        Return Nothing
    End Function

    Private Sub btnSalva_Click()
        Dim Movimentacao As IMovimentacaoDeProdutoEntrada
        Dim Mensagem As String
        Dim Inconsistencia As String

        Inconsistencia = ValidaDados()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Movimentacao = MontaObjeto()

        Try
            Using Servico As IServicoDeMovimentacaoDeProdutoEntrada = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeMovimentacaoDeProdutoEntrada)()
                Servico.InserirMovimentacaoDeEntrada(Movimentacao)
                Mensagem = "Movimentação de entrada cadastrada com sucesso."
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As IMovimentacaoDeProdutoEntrada
        Dim Movimentacao As IMovimentacaoDeProdutoEntrada

        Movimentacao = FabricaGenerica.GetInstancia.CrieObjeto(Of IMovimentacaoDeProdutoEntrada)()

        Movimentacao.Data = txtDataDaMovimentacao.SelectedDate.Value

        If Not String.IsNullOrEmpty(cboFornecedor.SelectedValue) Then
            Using ServicoDeFornecedor As IServicoDeFornecedor = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeFornecedor)()
                Movimentacao.Fornecedor = ServicoDeFornecedor.Obtenha(CLng(cboFornecedor.SelectedValue))
            End Using
        End If

        Movimentacao.Historico = txtHistorico.Text
        Movimentacao.NumeroDocumento = txtNumeroDocumento.Text
        Movimentacao.AdicioneProdutosMovimentados(CType(Session(CHAVE_PRODUTOS_MOVIMENTADOS_ENTRADA), IList(Of IProdutoMovimentado)))

        Return Movimentacao
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return ""
    End Function

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalva_Click()
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

    Protected Sub btnNovoFornecedor_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNovoFornecedor.Click
        Dim URL As String

        URL = ObtenhaURLNovoFornecedor()
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de fornecedor", 650, 450), False)
    End Sub

    Private Function ObtenhaURLNovoFornecedor() As String
        Dim URL As String

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual
        Return String.Concat(URL, "Nucleo/cdFornecedor.aspx")
    End Function

    Private Sub cboFornecedor_ItemsRequested(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboFornecedor.ItemsRequested
        Dim Fornecedores As IList(Of IFornecedor)

        Using Servico As IServicoDeFornecedor = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeFornecedor)()
            Fornecedores = Servico.ObtenhaPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Fornecedores Is Nothing Then
            For Each Fornecedor As IFornecedor In Fornecedores
                cboFornecedor.Items.Add(New RadComboBoxItem(Fornecedor.Pessoa.Nome, Fornecedor.Pessoa.ID.ToString))
            Next
        End If
    End Sub

End Class