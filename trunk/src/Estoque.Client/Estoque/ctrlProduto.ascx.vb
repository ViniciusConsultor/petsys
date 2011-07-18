Imports Estoque.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports Estoque.Interfaces.Servicos
Imports Telerik.Web.UI

Partial Public Class ctrlProduto
    Inherits System.Web.UI.UserControl

    Public Event ProdutoFoiSelecionado(ByVal Produto As IProduto)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

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

        ProdutoSelecionado = Produto
        RaiseEvent ProdutoFoiSelecionado(Produto)
    End Sub

    Public Property ProdutoSelecionado() As IProduto
        Get
            Return CType(ViewState(Me.ClientID), IProduto)
        End Get
        Set(ByVal value As IProduto)
            ViewState.Add(Me.ClientID, value)
        End Set
    End Property

    Private Sub txtCodigo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigo.TextChanged
        If String.IsNullOrEmpty(txtCodigo.Text) Then Exit Sub

        Dim Produto As IProduto

        Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
            Produto = Servico.ObtenhaProduto(txtCodigo.Text)
        End Using

        If Produto Is Nothing Then Exit Sub

        ProdutoSelecionado = Produto
        RaiseEvent ProdutoFoiSelecionado(Produto)
    End Sub

End Class