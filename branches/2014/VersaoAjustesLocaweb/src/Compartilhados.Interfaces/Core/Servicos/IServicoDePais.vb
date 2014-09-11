Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDePais
        Inherits IServico

        Function ObtenhaPais(ByVal Id As Long) As IPais
        Function ObtenhaPaisesPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPais)
        Sub Inserir(ByVal Pais As IPais)
        Sub Excluir(ByVal Id As Long)
        Sub Modificar(ByVal Pais As IPais)

    End Interface

End Namespace