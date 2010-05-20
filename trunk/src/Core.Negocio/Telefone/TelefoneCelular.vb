Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Telefone

    <Serializable()> _
    Public Class TelefoneCelular
        Inherits Telefone
        Implements ITelefoneCelular

        Public Overrides ReadOnly Property Tipo() As TipoDeTelefone
            Get
                Return TipoDeTelefone.Celular
            End Get
        End Property

    End Class

End Namespace