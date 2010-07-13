Imports Diary.Interfaces.Negocio
Imports Compartilhados

<Serializable()> _
Public Class SolicitacaoDeConvite
    Inherits Solicitacao
    Implements ISolicitacaoDeConvite

    Private _DataEHorario As Date
    Public Property DataEHorario() As Date Implements ISolicitacaoDeConvite.DataEHorario
        Get
            Return _DataEHorario
        End Get
        Set(ByVal value As Date)
            _DataEHorario = value
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

    Public Overrides ReadOnly Property Tipo() As TipoDeSolicitacao
        Get
            Return TipoDeSolicitacao.Convite
        End Get
    End Property

End Class