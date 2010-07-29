Namespace Core.Negocio

    Public Interface ICompromisso

        Property Inicio() As DateTime
        Property Fim() As DateTime
        Property Assunto() As String
        Property Local() As String
        Property Descricao() As String
        Property Proprietario() As IPessoaFisica
        Property ID() As Nullable(Of Long)
        Sub EstaConsistente()
        Property Status() As StatusDoCompromisso

    End Interface

End Namespace