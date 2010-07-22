﻿Imports Diary.Interfaces.Negocio
Imports Compartilhados

<Serializable()> _
Public MustInherit Class Solicitacao
    Implements ISolicitacao

    Private _Ativa As Boolean
    Public Property Ativa() As Boolean Implements ISolicitacao.Ativa
        Get
            Return _Ativa
        End Get
        Set(ByVal value As Boolean)
            _Ativa = value
        End Set
    End Property

    Private _Codigo As Long
    Public Property Codigo() As Long Implements ISolicitacao.Codigo
        Get
            Return _Codigo
        End Get
        Set(ByVal value As Long)
            _Codigo = value
        End Set
    End Property

    Private _Contato As IContato
    Public Property Contato() As IContato Implements ISolicitacao.Contato
        Get
            Return _Contato
        End Get
        Set(ByVal value As IContato)
            _Contato = value
        End Set
    End Property

    Private _DataDaSolicitacao As Date
    Public Property DataDaSolicitacao() As Date Implements ISolicitacao.DataDaSolicitacao
        Get
            Return _DataDaSolicitacao
        End Get
        Set(ByVal value As Date)
            _DataDaSolicitacao = value
        End Set
    End Property

    Private _Descricao As String
    Public Property Descricao() As String Implements ISolicitacao.Descricao
        Get
            Return _Descricao
        End Get
        Set(ByVal value As String)
            _Descricao = value
        End Set
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements ISolicitacao.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _UsuarioQueCadastrou As Usuario
    Public Property UsuarioQueCadastrou() As Usuario Implements ISolicitacao.UsuarioQueCadastrou
        Get
            Return _UsuarioQueCadastrou
        End Get
        Set(ByVal value As Compartilhados.Usuario)
            _UsuarioQueCadastrou = value
        End Set
    End Property

    Public MustOverride ReadOnly Property Tipo() As TipoDeSolicitacao Implements ISolicitacao.Tipo

End Class