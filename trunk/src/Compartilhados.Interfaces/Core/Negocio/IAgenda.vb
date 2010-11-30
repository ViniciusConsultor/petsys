Namespace Core.Negocio

    Public Interface IAgenda

        ReadOnly Property Inicio() As Date
        ReadOnly Property Fim() As Date
        Sub Organize()
        Function ObtenhaCompromissos(ByVal Data As Date) As IList(Of ICompromisso)
        Function ObtenhaLembretes(ByVal Data As Date) As IList(Of ILembrete)
        Function ObtenhaTarefas(ByVal Data As Date) As IList(Of ITarefa)
        ReadOnly Property Proprietario() As IPessoaFisica

    End Interface

End Namespace