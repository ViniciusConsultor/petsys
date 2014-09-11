Namespace Core.Negocio.Documento

    Public Interface IDocumento

        Property Numero() As String
        ReadOnly Property Tipo() As TipoDeDocumento
        Function ToString() As String
        Function EhValido() As Boolean

    End Interface

End Namespace