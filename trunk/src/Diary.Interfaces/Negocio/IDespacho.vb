Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados

Namespace Negocio

    Public Interface IDespacho

        Property ID() As Nullable(Of Long)
        Property Tipo() As TipoDeDespacho
        Property Responsavel() As IPessoaFisica
        Property Solicitacao() As ISolicitacao
        Property DataDoDespacho() As Date
        ReadOnly Property TipoDestinoDespacho() As TipoDestinoDespacho

    End Interface

End Namespace