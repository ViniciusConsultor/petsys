Imports Compartilhados

Namespace Negocio

    Public Interface IConfiguracaoDoSistema

        Property NotificarErrosAutomaticamente() As Boolean
        Property ConfiguracaoDeEmailDoSistema() As IConfiguracaoDeEmailDoSistema

    End Interface

End Namespace