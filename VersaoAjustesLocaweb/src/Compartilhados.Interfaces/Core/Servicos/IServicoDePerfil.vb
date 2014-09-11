Imports Compartilhados
Imports Compartilhados.Visual

Namespace Core.Servicos

    Public Interface IServicoDePerfil
        Inherits IServico

        Function Obtenha(ByVal Usuario As Usuario) As Perfil
        Sub Salve(ByVal Usuario As Usuario, ByVal Perfil As Perfil)
        Sub Remova(ByVal Usuario As Usuario)
        Sub SalveAtalhos(ByVal Usuario As Usuario, ByVal Atalhos As IList(Of Atalho))

    End Interface

End Namespace