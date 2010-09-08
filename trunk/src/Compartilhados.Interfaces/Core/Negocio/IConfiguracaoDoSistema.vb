Imports Compartilhados

Namespace Core.Negocio

    Public Interface IConfiguracaoDoSistema

        Property NotificarErrosAutomaticamente() As Boolean
        Property RemetenteDaNotificaoDeErros() As String
        Property ConfiguracaoDeEmailDoSistema() As IConfiguracaoDeEmailDoSistema
        Property ConfiguracaoDeAgendaDoSistema() As IConfiguracaoDeAgendaDoSistema

    End Interface

End Namespace