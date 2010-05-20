Imports Core.Interfaces.Negocio
Imports Compartilhados

<Serializable()> _
Public Class ConfiguracaoDeEmailDoSistema
    Implements IConfiguracaoDeEmailDoSistema

    Private _EmailRemetente As String
    Public Property EmailRemetente() As String Implements IConfiguracaoDeEmailDoSistema.EmailRemetente
        Get
            Return _EmailRemetente
        End Get
        Set(ByVal value As String)
            _EmailRemetente = value
        End Set
    End Property

    Private _HabilitarSSL As Boolean
    Public Property HabilitarSSL() As Boolean Implements IConfiguracaoDeEmailDoSistema.HabilitarSSL
        Get
            Return _HabilitarSSL
        End Get
        Set(ByVal value As Boolean)
            _HabilitarSSL = value
        End Set
    End Property

    Private _Porta As Integer
    Public Property Porta() As Integer Implements IConfiguracaoDeEmailDoSistema.Porta
        Get
            Return _Porta
        End Get
        Set(ByVal value As Integer)
            _Porta = value
        End Set
    End Property

    Private _RequerAutenticacao As Boolean
    Public Property RequerAutenticacao() As Boolean Implements IConfiguracaoDeEmailDoSistema.RequerAutenticacao
        Get
            Return _RequerAutenticacao
        End Get
        Set(ByVal value As Boolean)
            _RequerAutenticacao = value
        End Set
    End Property

    Private _SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida As String
    Public Property SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida() As String Implements IConfiguracaoDeEmailDoSistema.SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida
        Get
            Return _SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida
        End Get
        Set(ByVal value As String)
            _SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida = value
        End Set
    End Property

    Private _ServidorDeSaidaDeEmail As String
    Public Property ServidorDeSaidaDeEmail() As String Implements IConfiguracaoDeEmailDoSistema.ServidorDeSaidaDeEmail
        Get
            Return _ServidorDeSaidaDeEmail
        End Get
        Set(ByVal value As String)
            _ServidorDeSaidaDeEmail = value
        End Set
    End Property

    Private _TipoDoServidor As TipoDeServidorDeEmail
    Public Property TipoDoServidor() As TipoDeServidorDeEmail Implements IConfiguracaoDeEmailDoSistema.TipoDoServidor
        Get
            Return _TipoDoServidor
        End Get
        Set(ByVal value As Compartilhados.TipoDeServidorDeEmail)
            _TipoDoServidor = value
        End Set
    End Property

    Private _UsuarioDeAutenticacaoDoServidorDeSaida As String
    Public Property UsuarioDeAutenticacaoDoServidorDeSaida() As String Implements IConfiguracaoDeEmailDoSistema.UsuarioDeAutenticacaoDoServidorDeSaida
        Get
            Return _UsuarioDeAutenticacaoDoServidorDeSaida
        End Get
        Set(ByVal value As String)
            _UsuarioDeAutenticacaoDoServidorDeSaida = value
        End Set
    End Property

End Class