Namespace Core.Negocio

    Public Interface IAgenda

        Property Inicio() As Date
        Property Fim() As Date
        Sub Organize()
        Function ObtenhaCompromissos(ByVal Data As Date) As IList(Of ICompromisso)
        Function ObtenhaLembretes(ByVal Data As Date) As IList(Of ILembrete)
        Function ObtenhaTarefas(ByVal Data As Date) As IList(Of ITarefa)

    End Interface

End Namespace