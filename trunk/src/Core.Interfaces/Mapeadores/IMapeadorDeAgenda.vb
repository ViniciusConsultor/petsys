Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeAgenda

        Sub Insira(ByVal Agenda As IAgenda)
        Sub Modifique(ByVal Agenda As IAgenda)
        Sub Remova(ByVal ID As Long)

        Function ObtenhaAgenda(ByVal Pessoa As IPessoa) As IAgenda
        Function ObtenhaAgenda(ByVal IDPessoa As Long) As IAgenda

        Function InsiraCompromisso(ByVal Compromisso As ICompromisso) As Long
        Sub ModifiqueCompromisso(ByVal Compromisso As ICompromisso)
        Sub RemovaCompromisso(ByVal ID As Long)
        Function ObtenhaCompromisso(ByVal ID As Long) As ICompromisso
        Function ObtenhaCompromissos(ByVal IDProprieatario As Long) As IList(Of ICompromisso)

        Function InsiraTarefa(ByVal Tarefa As ITarefa) As Long
        Sub ModifiqueTarefa(ByVal Tarefa As ITarefa)
        Sub RemovaTarefa(ByVal ID As Long)
        Function ObtenhaTarefa(ByVal ID As Long) As ITarefa
        Function ObtenhaTarefas(ByVal IDProprietario As Long) As IList(Of ITarefa)

    End Interface

End Namespace