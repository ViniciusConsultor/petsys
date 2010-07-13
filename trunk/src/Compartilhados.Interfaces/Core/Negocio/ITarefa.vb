Namespace Core.Negocio

    Public Interface ITarefa

        Property Assunto() As String
        Property DataDeInicio() As DateTime
        Property DataDeConclusao() As DateTime
        Property Prioridade() As PrioridadeDaTarefa
        Property Descricao() As String
        Property Proprietario() As IPessoaFisica

    End Interface

End Namespace