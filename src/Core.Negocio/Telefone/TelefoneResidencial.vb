Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Telefone

    <Serializable()> _
    Public Class TelefoneResidencial
        Inherits Telefone
        Implements ITelefoneResidencial

        Public Overrides ReadOnly Property Tipo() As TipoDeTelefone
            Get
                Return TipoDeTelefone.Residencial
            End Get
        End Property

    End Class

End Namespace