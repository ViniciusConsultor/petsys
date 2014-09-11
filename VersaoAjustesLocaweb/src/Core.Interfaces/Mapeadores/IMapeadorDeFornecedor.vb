Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeFornecedor

        Function Obtenha(ByVal Pessoa As IPessoa) As IFornecedor
        Function Obtenha(ByVal ID As Long) As IFornecedor
        Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                          ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IFornecedor)
        Sub Inserir(ByVal Fornecedor As IFornecedor)
        Sub Remover(ByVal ID As Long)
        Sub Modificar(ByVal Fornecedor As IFornecedor)

    End Interface

End Namespace