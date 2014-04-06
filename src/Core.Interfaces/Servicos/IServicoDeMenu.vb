Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio

Namespace Servicos

    Public Interface IServicoDeMenu
        Inherits IServico

        Function ObtenhaMenu() As IMenuComposto
        Function ObtenhaFuncoesComCaminhoDoMenu(ByVal NomeDaFuncao As String) As HashSet(Of DTOAjudanteDePesquisaDeMenu)

    End Interface

End Namespace