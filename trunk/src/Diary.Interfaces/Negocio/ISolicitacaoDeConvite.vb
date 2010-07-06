Imports Compartilhados

Namespace Negocio

    Public Interface ISolicitacaoDeConvite

        Property Contato() As IContato
        Property Descricao() As String
        Property Local() As String
        Property DataEHorario() As Date
        Property Observacao() As String

        Property Codigo() As Long
        Property DataDaSolicitacao() As Date
        Property Ativa() As Boolean
        Property ID() As Nullable(Of Long)
        Property UsuarioQueCadastrou() As Usuario
    End Interface

End Namespace