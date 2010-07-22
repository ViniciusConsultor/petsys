Imports Compartilhados

Namespace Negocio

    Public Interface ISolicitacaoDeConvite
        Inherits ISolicitacao

        Property Local() As String
        Property DataEHorario() As Date
        Property Observacao() As String
        Sub EstaConsistente()


    End Interface

End Namespace