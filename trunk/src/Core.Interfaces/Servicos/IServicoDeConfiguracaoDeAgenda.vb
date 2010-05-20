Imports Core.Interfaces.Negocio
Imports Compartilhados

Namespace Servicos

    Public Interface IServicoDeConfiguracaoDeAgenda
        Inherits IServico

        Function ObtenhaConfiguracoesPorNomeComoFiltro(ByVal Nome As String, _
                                                       ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IConfiguracaoDeAgenda)
        Function ObtenhaConfiguracao(ByVal ID As Long) As IConfiguracaoDeAgenda
        Sub Inserir(ByVal ConfiguracaoDeAgenda As IConfiguracaoDeAgenda)
        Sub Modificar(ByVal ConfiguracaoDeAgenda As IConfiguracaoDeAgenda)
        Sub Excluir(ByVal ID As Long)
    End Interface

End Namespace