Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeConfiguracoesDoSistema

        Function ObtenhaConfiguracaoDoSistema() As IConfiguracaoDoSistema
        Sub Salve(ByVal Configuracao As IConfiguracaoDoSistema)

    End Interface

End Namespace