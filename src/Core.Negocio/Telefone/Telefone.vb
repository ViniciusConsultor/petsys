Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Telefone

    <Serializable()> _
    Public MustInherit Class Telefone
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

        Public MustOverride ReadOnly Property Tipo() As TipoDeTelefone Implements ITelefone.Tipo

    End Class

End Namespace
