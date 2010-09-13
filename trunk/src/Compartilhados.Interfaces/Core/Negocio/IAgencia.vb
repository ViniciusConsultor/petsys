Namespace Core.Negocio

    Public Interface IAgencia
        Inherits IPapelPessoa

        Property Numero() As String
        Property Banco() As IBanco

    End Interface

End Namespace