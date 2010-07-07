Namespace Core.Negocio

    Public Interface ICompromisso

        Property Inicio() As DateTime
        Property Fim() As DateTime
        Property Assunto() As String
        Property Local() As String
        Property Descricao() As String
        Property Pessoa() As IPessoa

    End Interface

End Namespace