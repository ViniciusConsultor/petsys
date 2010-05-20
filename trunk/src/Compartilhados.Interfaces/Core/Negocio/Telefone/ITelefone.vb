Namespace Core.Negocio.Telefone

    Public Interface ITelefone

        Property DDD() As Short
        Property Numero() As Long
        ReadOnly Property Tipo() As TipoDeTelefone

    End Interface

End Namespace