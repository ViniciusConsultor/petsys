Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeTipoDeEndereco

        Sub Insira(ByVal Tipo As ITipoDeEndereco)
        Function Obtenha(ByVal ID As Long) As ITipoDeEndereco
        Sub Modificar(ByVal Tipo As ITipoDeEndereco)
        Sub Remover(ByVal ID As Long)
        Function ObtenhaPorNome(ByVal Filtro As String, ByVal Quantidade As Integer) As IList(Of ITipoDeEndereco)

    End Interface

End Namespace