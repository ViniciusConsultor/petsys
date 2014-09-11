Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados

Namespace Negocio

    Public Interface IDespacho

        Property ID() As Nullable(Of Long)
        Property Tipo() As TipoDeDespacho
        Property Solicitante() As IPessoaFisica
        Property Alvo() As IPessoaFisica
        Property Solicitacao() As ISolicitacao
        Property DataDoDespacho() As Date
        ReadOnly Property TipoDestino() As TipoDestinoDespacho

    End Interface

End Namespace