Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class ConfiguracaoDoSistema
    Implements IConfiguracaoDoSistema

    Private _ConfiguracaoDeEmailDoSistema As IConfiguracaoDeEmailDoSistema
    Public Property ConfiguracaoDeEmailDoSistema() As IConfiguracaoDeEmailDoSistema Implements IConfiguracaoDoSistema.ConfiguracaoDeEmailDoSistema
        Get
            Return _ConfiguracaoDeEmailDoSistema
        End Get
        Set(ByVal value As IConfiguracaoDeEmailDoSistema)
            _ConfiguracaoDeEmailDoSistema = value
        End Set
    End Property

    Private _NotificarErrosAutomaticamente As Boolean
    Public Property NotificarErrosAutomaticamente() As Boolean Implements IConfiguracaoDoSistema.NotificarErrosAutomaticamente
        Get
            Return _NotificarErrosAutomaticamente
        End Get
        Set(ByVal value As Boolean)
            _NotificarErrosAutomaticamente = value
        End Set
    End Property

    Private _DestinatarioDaNotificaoDeErros As String
    Public Property DestinatarioDaNotificaoDeErros() As String Implements IConfiguracaoDoSistema.DestinatarioDaNotificaoDeErros
        Get
            Return _DestinatarioDaNotificaoDeErros
        End Get
        Set(ByVal value As String)
            _DestinatarioDaNotificaoDeErros = value
        End Set
    End Property

    Private _ConfiguracaoDeAgendaDoSistema As IConfiguracaoDeAgendaDoSistema
    Public Property ConfiguracaoDeAgendaDoSistema() As IConfiguracaoDeAgendaDoSistema Implements IConfiguracaoDoSistema.ConfiguracaoDeAgendaDoSistema
        Get
            Return _ConfiguracaoDeAgendaDoSistema
        End Get
        Set(ByVal value As IConfiguracaoDeAgendaDoSistema)
            _ConfiguracaoDeAgendaDoSistema = value
        End Set
    End Property
End Class