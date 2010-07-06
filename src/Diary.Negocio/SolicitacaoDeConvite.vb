Imports Diary.Interfaces.Negocio
Imports Compartilhados

<Serializable()> _
Public Class SolicitacaoDeConvite
    Implements ISolicitacaoDeConvite

    Private _Ativa As Boolean
    Public Property Ativa() As Boolean Implements ISolicitacaoDeConvite.Ativa
        Get
            Return _Ativa
        End Get
        Set(ByVal value As Boolean)
            _Ativa = value
        End Set
    End Property

    Private _Codigo As Long
    Public Property Codigo() As Long Implements ISolicitacaoDeConvite.Codigo
        Get
            Return _Codigo
        End Get
        Set(ByVal value As Long)
            _Codigo = value
        End Set
    End Property

    Private _Contato As IContato
    Public Property Contato() As IContato Implements ISolicitacaoDeConvite.Contato
        Get
            Return _Contato
        End Get
        Set(ByVal value As IContato)
            _Contato = value
        End Set
    End Property

    Private _DataDaSolicitacao As Date
    Public Property DataDaSolicitacao() As Date Implements ISolicitacaoDeConvite.DataDaSolicitacao
        Get
            Return _DataDaSolicitacao
        End Get
        Set(ByVal value As Date)
            _DataDaSolicitacao = value
        End Set
    End Property

    Private _DataEHorario As Date
    Public Property DataEHorario() As Date Implements ISolicitacaoDeConvite.DataEHorario
        Get
            Return _DataEHorario
        End Get
        Set(ByVal value As Date)
            _DataEHorario = value
        End Set
    End Property

    Private _Descricao As String
    Public Property Descricao() As String Implements ISolicitacaoDeConvite.Descricao
        Get
            Return _Descricao
        End Get
        Set(ByVal value As String)
            _Descricao = value
        End Set
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements ISolicitacaoDeConvite.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Local As String
    Public Property Local() As String Implements ISolicitacaoDeConvite.Local
        Get
            Return _Local
        End Get
        Set(ByVal value As String)
            _Local = value
        End Set
    End Property

    Private _Observacao As String
    Public Property Observacao() As String Implements ISolicitacaoDeConvite.Observacao
        Get
            Return _Observacao
        End Get
        Set(ByVal value As String)
            _Observacao = value
        End Set
    End Property

    Private _UsuarioQueCadastrou As Usuario
    Public Property UsuarioQueCadastrou() As Usuario Implements ISolicitacaoDeConvite.UsuarioQueCadastrou
        Get
            Return _UsuarioQueCadastrou
        End Get
        Set(ByVal value As Usuario)
            _UsuarioQueCadastrou = value
        End Set
    End Property

End Class