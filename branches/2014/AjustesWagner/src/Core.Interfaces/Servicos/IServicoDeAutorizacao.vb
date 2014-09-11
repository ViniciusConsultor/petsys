Imports Core.Interfaces.Negocio
Imports Compartilhados

Namespace Servicos

    Public Interface IServicoDeAutorizacao
        Inherits IServico

        Function ObtenhaModulosDisponiveis() As IList(Of IModulo)
        Sub Modifique(ByVal IDGrupo As Long, ByVal Diretivas As IList(Of IDiretivaDeSeguranca))
        Function ObtenhaDiretivasDeSegurancaDoGrupo(ByVal ID As Long) As IList(Of IDiretivaDeSeguranca)

    End Interface

End Namespace