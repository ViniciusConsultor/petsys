Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Telefone

    <Serializable()> _
    Public Class TelefoneRecado
        Inherits Telefone
        Implements ITelefoneRecado

        Private _Contato As String

        Public Overrides ReadOnly Property Tipo() As TipoDeTelefone
            Get
                Return TipoDeTelefone.Recado
            End Get
        End Property

        Public Property Contato() As String Implements ITelefoneRecado.Contato
            Get
                Return _Contato
            End Get
            Set(ByVal value As String)
                _Contato = value
            End Set
        End Property
    End Class

End Namespace