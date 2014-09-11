Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeGrupo

        Function ObtenhaGrupo(ByVal Id As Long) As IGrupo
        Function ObtenhaGruposPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IGrupo)
        Sub Inserir(ByVal Grupo As IGrupo)
        Sub Excluir(ByVal Id As Long)
        Sub Modificar(ByVal Grupo As IGrupo)
        Function Existe(ByVal Grupo As IGrupo) As Boolean
        Function EstaSendoUtilizado(ByVal Id As Long) As Boolean

    End Interface

End Namespace