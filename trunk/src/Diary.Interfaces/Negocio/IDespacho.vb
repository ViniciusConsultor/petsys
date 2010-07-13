Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados

Namespace Negocio

    Public Interface IDespacho

        Property ID() As Nullable(Of Long)
        ReadOnly Property Tipo() As TipoDeDespacho
        Property Responsavel() As Usuario

    End Interface

End Namespace