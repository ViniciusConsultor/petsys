Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDeTipoDeEndereco
        Inherits IServico

        Sub Insira(ByVal TipoDeEndereco As ITipoDeEndereco)
        Function Obtenha(ByVal ID As Long) As ITipoDeEndereco
        Sub Modificar(ByVal TipoDeEndereco As ITipoDeEndereco)
        Sub Remover(ByVal ID As Long)
        Function ObtenhaPorNome(ByVal Filtro As String, ByVal Quantidade As Integer) As IList(Of ITipoDeEndereco)

    End Interface

End Namespace