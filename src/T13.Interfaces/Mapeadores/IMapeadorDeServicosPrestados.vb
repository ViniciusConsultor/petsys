Imports T13.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeServicosPrestados

        Function ObtenhaServicoPorNomeComoFiltro(ByVal Nome As String, _
                                                ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IServicoPrestado)
        Function ObtenhaServico(ByVal ID As Long) As IServicoPrestado
        Sub Inserir(ByVal Servico As IServicoPrestado)
        Sub Modificar(ByVal Sevico As IServicoPrestado)
        Sub Excluir(ByVal ID As Long)

    End Interface

End Namespace