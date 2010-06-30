Imports Diary.Interfaces.Negocio
Imports Compartilhados

<Serializable()> _
Public Class SolicitacaoDeAudiencia
    Implements ISolicitacaoDeAudiencia

    Private _Assunto As String
    Public Property Assunto() As String Implements ISolicitacaoDeAudiencia.Assunto
        Get
            Return _Assunto
        End Get
        Set(ByVal value As String)
            _Assunto = value
        End Set
    End Property

    Private _Ativa As Boolean
    Public Property Ativa() As Boolean Implements ISolicitacaoDeAudiencia.Ativa
        Get
            Return _Ativa
        End Get
        Set(ByVal value As Boolean)
            _Ativa = value
        End Set
    End Property

    Private _Contato As IContato
    Public Property Contato() As IContato Implements ISolicitacaoDeAudiencia.Contato
        Get
            Return _Contato
        End Get
        Set(ByVal value As IContato)
            _Contato = value
        End Set
    End Property

    Private _DataDaSolicitacao As Date
    Public Property DataDaSolicitacao() As Date Implements ISolicitacaoDeAudiencia.DataDaSolicitacao
        Get
            Return _DataDaSolicitacao
        End Get
        Set(ByVal value As Date)
            _DataDaSolicitacao = value
        End Set
    End Property

    Private _Descricao As String
    Public Property Descricao() As String Implements ISolicitacaoDeAudiencia.Descricao
        Get
            Return _Descricao
        End Get
        Set(ByVal value As String)
            _Descricao = value
        End Set
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements ISolicitacaoDeAudiencia.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _UsuarioQueCadastro As Usuario
    Public Property UsuarioQueCadastrou() As Usuario Implements ISolicitacaoDeAudiencia.UsuarioQueCadastrou
        Get
            Return _UsuarioQueCadastro
        End Get
        Set(ByVal value As Compartilhados.Usuario)
            _UsuarioQueCadastro = value
        End Set
    End Property

    Private _Codigo As Long
    Public Property Codigo() As Long Implements ISolicitacaoDeAudiencia.Codigo
        Get
            Return _Codigo
        End Get
        Set(ByVal value As Long)
            _Codigo = value
        End Set
    End Property

End Class