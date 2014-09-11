Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDeFornecedor
        Inherits IServico

        Function Obtenha(ByVal Pessoa As IPessoa) As IFornecedor
        Function Obtenha(ByVal ID As Long) As IFornecedor
        Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                          ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IFornecedor)
        Sub Inserir(ByVal Cliente As IFornecedor)
        Sub Modificar(ByVal Cliente As IFornecedor)
        Sub Remover(ByVal ID As Long)
    End Interface

End Namespace