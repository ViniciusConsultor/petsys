Imports Core.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeConfiguracaoDeAgenda

        Function ObtenhaConfiguracoesPorNomeComoFiltro(ByVal Nome As String, _
                                                       ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IConfiguracaoDeAgenda)
        Function ObtenhaConfiguracao(ByVal ID As Long) As IConfiguracaoDeAgenda
        Sub Inserir(ByVal ConfiguracaoDeAgenda As IConfiguracaoDeAgenda)
        Sub Modificar(ByVal ConfiguracaoDeAgenda As IConfiguracaoDeAgenda)
        Sub Excluir(ByVal ID As Long)

    End Interface

End Namespace