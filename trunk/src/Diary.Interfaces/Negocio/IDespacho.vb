Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados

Namespace Negocio

    Public Interface IDespacho

        Property ID() As Nullable(Of Long)
        ReadOnly Property Tipo() As TipoDeDespacho
        Property Responsavel() As IPessoaFisica
        Property Solicitacao() As ISolicitacao

    End Interface

End Namespace