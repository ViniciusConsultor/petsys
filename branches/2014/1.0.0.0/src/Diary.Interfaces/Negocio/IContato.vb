Imports Compartilhados.Interfaces.Core.Negocio

Namespace Negocio

    Public Interface IContato
        Inherits IPapelPessoa

        Property Observacoes() As String
        Property Cargo() As String

    End Interface

End Namespace