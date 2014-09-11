Namespace Core.Negocio.Telefone

    Public Interface ITelefone

        Property DDD() As Short
        Property Numero() As Long
        Property Tipo() As TipoDeTelefone
        Property Contato() As String

    End Interface

End Namespace