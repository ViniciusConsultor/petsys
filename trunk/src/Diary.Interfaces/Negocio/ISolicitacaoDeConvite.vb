Imports Compartilhados

Namespace Negocio

    Public Interface ISolicitacaoDeConvite
        Inherits ISolicitacao

        Property Local() As String
        Property DataEHorario() As Date
        Property Observacao() As String

    End Interface

End Namespace