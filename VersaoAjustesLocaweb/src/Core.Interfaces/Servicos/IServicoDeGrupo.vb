Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Servicos

    Public Interface IServicoDeGrupo
        Inherits IServico

        Function ObtenhaGruposPorNomeComoFiltro(ByVal Nome As String, _
                                                ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IGrupo)
        Function ObtenhaGrupo(ByVal ID As Long) As IGrupo
        Sub Inserir(ByVal Grupo As IGrupo)
        Sub Modificar(ByVal Grupo As IGrupo)
        Sub Remover(ByVal ID As Long)
    End Interface

End Namespace