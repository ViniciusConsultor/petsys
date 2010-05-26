Imports Compartilhados
Imports Estoque.Interfaces.Negocio

Namespace Servicos

    Public Interface IServicoDeProduto
        Inherits IServico

        Sub InserirMarcaDeProduto(ByVal Marca As IMarcaDeProduto)
        Function ObtenhaMarcaDeProduto(ByVal ID As Long) As IMarcaDeProduto
        Function ObtenhaMarcasDeProdutosPorNome(ByVal Nome As String, ByVal QuantidadeDeRegistros As Integer) As IList(Of IMarcaDeProduto)
        Sub AtualizarMarcaDeProduto(ByVal Marca As IMarcaDeProduto)
        Sub RemoverMarcaDeProduto(ByVal ID As Long)
        Function ObtenhaProduto(ByVal ID As Long) As IProduto
        Function ObtenhaProduto(ByVal CodigoDeBarras As String) As IProduto
        Function ObtenhaProdutos(ByVal Nome As String, ByVal QuantidadeDeRegistros As Integer) As IList(Of IProduto)
        Sub InserirProduto(ByVal Produto As IProduto)
        Sub AtualizarProduto(ByVal Produto As IProduto)
        Sub RemoverProduto(ByVal ID As Long)

    End Interface

End Namespace