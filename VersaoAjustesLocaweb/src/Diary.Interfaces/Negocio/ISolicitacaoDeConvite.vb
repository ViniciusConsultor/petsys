Imports Compartilhados

Namespace Negocio

    Public Interface ISolicitacaoDeConvite
        Inherits ISolicitacao

        Property DataEHorario() As Date
        Property Observacao() As String
        Sub EstaConsistente()

    End Interface

End Namespace