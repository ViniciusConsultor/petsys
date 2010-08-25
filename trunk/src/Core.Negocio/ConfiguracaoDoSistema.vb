﻿Imports Core.Interfaces.Negocio
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

    Private _RemetenteDaNotificaoDeErros As String
    Public Property RemetenteDaNotificaoDeErros() As String Implements IConfiguracaoDoSistema.RemetenteDaNotificaoDeErros
        Get
            Return _RemetenteDaNotificaoDeErros
        End Get
        Set(ByVal value As String)
            _RemetenteDaNotificaoDeErros = value
        End Set
    End Property

End Class