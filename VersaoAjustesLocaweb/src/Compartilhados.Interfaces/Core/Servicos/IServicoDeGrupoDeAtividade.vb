Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDeGrupoDeAtividade
        Inherits IServico

        Sub Insira(ByVal GrupoDeAtividade As IGrupoDeAtividade)
        Function Obtenha(ByVal ID As Long) As IGrupoDeAtividade
        Sub Modificar(ByVal GrupoDeAtividade As IGrupoDeAtividade)
        Sub Remover(ByVal ID As Long)
        Function ObtenhaPorNome(ByVal Filtro As String, ByVal Quantidade As Integer) As IList(Of IGrupoDeAtividade)

    End Interface

End Namespace