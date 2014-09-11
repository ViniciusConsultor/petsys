Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Mapeadores

    Public Interface IMapeadorDeCliente

        Function Obtenha(ByVal Pessoa As IPessoa) As ICliente
        Function Obtenha(ByVal ID As Long) As ICliente
        Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                          ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of ICliente)
        Sub Inserir(ByVal Cliente As ICliente)
        Sub Remover(ByVal ID As Long)
        Sub Modificar(ByVal Cliente As ICliente)

    End Interface

End Namespace