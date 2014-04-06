Imports Core.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeMenu

        Function ObtenhaMenu() As IMenuComposto
        Function ObtenhaFuncoesComCaminhoDoMenu(ByVal NomeDaFuncao As String) As HashSet(Of DTOAjudanteDePesquisaDeMenu)
    End Interface

End Namespace