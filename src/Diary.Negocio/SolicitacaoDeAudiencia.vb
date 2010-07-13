Imports Diary.Interfaces.Negocio
Imports Compartilhados

<Serializable()> _
Public Class SolicitacaoDeAudiencia
    Inherits Solicitacao
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

    Public Overrides ReadOnly Property Tipo() As TipoDeSolicitacao
        Get
            Return TipoDeSolicitacao.Audiencia
        End Get
    End Property

End Class