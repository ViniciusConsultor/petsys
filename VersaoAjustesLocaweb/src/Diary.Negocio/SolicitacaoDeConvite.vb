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

    Public Sub EstaConsistente() Implements ISolicitacaoDeConvite.EstaConsistente
        'If CLng(DataEHorario.ToString("yyyyMMddHHmm")) < CLng(Now.ToString("yyyyMMddHHmm")) Then
        '    Throw New BussinesException("A data e horário da solicitação deve ser maior que a data e horário atual.")
        'End If
    End Sub

End Class