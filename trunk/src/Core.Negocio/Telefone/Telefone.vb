Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Telefone

    <Serializable()> _
    Public Class Telefone
        Implements ITelefone

        Private _DDD As Short
        Private _Numero As Long

        Public Property DDD() As Short Implements ITelefone.DDD
            Get
                Return _DDD
            End Get
            Set(ByVal value As Short)
                _DDD = value
            End Set
        End Property

        Public Property Numero() As Long Implements ITelefone.Numero
            Get
                Return _Numero
            End Get
            Set(ByVal value As Long)
                _Numero = value
            End Set
        End Property

        Private _Tipo As TipoDeTelefone
        Public Property Tipo() As TipoDeTelefone Implements ITelefone.Tipo
            Get
                Return _Tipo
            End Get
            Set(ByVal value As TipoDeTelefone)
                _Tipo = value
            End Set
        End Property

    End Class

End Namespace
