Imports Compartilhados

Namespace Negocio

    Public Interface ISolicitacao

        Property ID() As Nullable(Of Long)
        Property Codigo() As Long
        Property UsuarioQueCadastrou() As Usuario
        Property DataDaSolicitacao() As Date
        Property Ativa() As Boolean
        Property Contato() As IContato
        Property Descricao() As String
        ReadOnly Property Tipo() As TipoDeSolicitacao

    End Interface

End Namespace