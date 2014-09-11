Imports Compartilhados

Namespace Core.Negocio

    Public Interface IConfiguracaoDeEmailDoSistema

        Property TipoDoServidor() As TipoDeServidorDeEmail
        Property ServidorDeSaidaDeEmail() As String
        Property Porta() As Integer
        Property HabilitarSSL() As Boolean
        Property RequerAutenticacao() As Boolean
        Property UsuarioDeAutenticacaoDoServidorDeSaida() As String
        Property SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida() As String
        Property EmailRemetente() As String

    End Interface

End Namespace