Imports Compartilhados

Namespace Negocio

    Public Interface ISolicitacaoDeAudiencia

        Property Codigo() As Long
        Property Contato() As IContato
        Property Assunto() As String
        Property Descricao() As String
        Property DataDaSolicitacao() As Date
        Property Ativa() As Boolean
        Property ID() As Nullable(Of Long)
        Property UsuarioQueCadastrou() As Usuario

    End Interface

End Namespace