Imports Core.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeAutorizacao

        Function ObtenhaModulosDisponiveis() As IList(Of IModulo)
        Sub Modifique(ByVal IDGrupo As Long, ByVal Diretivas As IList(Of IDiretivaDeSeguranca))
        Function ObtenhaDiretivasDeSegurancaDoGrupo(ByVal ID As Long) As IList(Of IDiretivaDeSeguranca)

    End Interface

End Namespace