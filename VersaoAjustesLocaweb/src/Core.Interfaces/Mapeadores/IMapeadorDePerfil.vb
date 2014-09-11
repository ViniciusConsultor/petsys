Imports Core.Interfaces.Negocio
Imports Compartilhados
Imports Compartilhados.Visual

Namespace Mapeadores

    Public Interface IMapeadorDePerfil

        Function Obtenha(ByVal Usuario As Usuario) As Perfil
        Sub Salve(ByVal Usuario As Usuario, ByVal Perfil As Perfil)
        Sub Remova(ByVal Usuario As Usuario)
        Sub SalveAtalhos(ByVal Usuario As Usuario, ByVal Atalhos As IList(Of Atalho))
        Function ObtenhaAtalhos(ByVal Usuario As Usuario) As IList(Of Atalho)

    End Interface

End Namespace