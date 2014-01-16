Imports Compartilhados

Namespace Core.Negocio

    Public Interface IConfiguracaoDoSistema

        Property NotificarErrosAutomaticamente() As Boolean
        Property DestinatarioDaNotificaoDeErros() As String
        Property ConfiguracaoDeEmailDoSistema() As IConfiguracaoDeEmailDoSistema
        Property ConfiguracaoDeAgendaDoSistema() As IConfiguracaoDeAgendaDoSistema

    End Interface

End Namespace