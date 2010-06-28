Namespace Negocio

    Public Interface ISolicitacaoDeAudiencia

        Property Contato() As IContato
        Property Assunto() As String
        Property Descricao() As String
        Property DataDaSolicitacao() As Date
        Property Ativa() As Boolean
        Property ID() As Nullable(Of Long)

    End Interface

End Namespace