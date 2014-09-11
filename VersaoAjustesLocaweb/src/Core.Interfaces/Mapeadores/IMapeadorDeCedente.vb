Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeCedente

        Function Obtenha(ByVal Pessoa As IPessoa) As ICedente
        Function Obtenha(ByVal ID As Long) As ICedente
        Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                          ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of ICedente)
        Sub Inserir(ByVal Cedente As ICedente)
        Sub Remover(ByVal ID As Long)
        Sub Modificar(ByVal Cedente As ICedente)
        Function ObtenhaCedentePadrao() As Long

    End Interface

End Namespace
