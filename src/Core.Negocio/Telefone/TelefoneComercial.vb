Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Telefone

    <Serializable()> _
    Public Class TelefoneComercial
        Inherits Telefone
        Implements ITelefoneComercial

        Private _Ramal As String

        Public Overrides ReadOnly Property Tipo() As TipoDeTelefone
            Get
                Return TipoDeTelefone.Comercial
            End Get
        End Property

        Public Property Ramal() As String Implements ITelefoneComercial.Ramal
            Get
                Return _Ramal
            End Get
            Set(ByVal value As String)
                _Ramal = value
            End Set
        End Property

    End Class

End Namespace