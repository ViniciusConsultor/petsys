﻿Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDeAgenda
        Inherits IServico

        'Cadastro de agenda
        Sub Modifique(ByVal Agenda As IAgenda)
        Sub Remova(ByVal ID As Long)
        Function ObtenhaAgenda(ByVal Pessoa As IPessoa) As IAgenda
        Function ObtenhaAgenda(ByVal IDPessoa As Long) As IAgenda

        'Compromissos
        Function InsiraCompromisso(ByVal Compromisso As ICompromisso) As Long
        Sub ModifiqueCompromisso(ByVal Compromisso As ICompromisso)
        Sub RemovaCompromisso(ByVal ID As Long)
        Function ObtenhaCompromisso(ByVal ID As Long) As ICompromisso
        Function ObtenhaCompromissos(ByVal IDProprietario As Long) As IList(Of ICompromisso)
        Function ObtenhaCompromissos(ByVal IDProprietario As Long, ByVal DataInicio As Date, ByVal DataFim As Nullable(Of Date)) As IList(Of ICompromisso)

        'Tarefa
        Function InsiraTarefa(ByVal Tarefa As ITarefa) As Long
        Sub ModifiqueTarefa(ByVal Tarefa As ITarefa)
        Sub RemovaTarefa(ByVal ID As Long)
        Function ObtenhaTarefa(ByVal ID As Long) As ITarefa
        Function ObtenhaTarefas(ByVal IDProprietario As Long) As IList(Of ITarefa)
        Function ObtenhaTarefas(ByVal IDProprietario As Long, ByVal DataInicio As Date, ByVal DataFim As Nullable(Of Date)) As IList(Of ITarefa)

        'Lembretes
        Function InsiraLembrete(ByVal Lembrete As ILembrete) As Long
        Sub ModifiqueLembrete(ByVal Lembrete As ILembrete)
        Sub RemovaLembrete(ByVal ID As Long)
        Function ObtenhaLembrete(ByVal ID As Long) As ILembrete
        Function ObtenhaLembretes(ByVal IDProprietario As Long) As IList(Of ILembrete)
        Function ObtenhaLembretes(ByVal IDProprietario As Long, ByVal DataInicio As Date, ByVal DataFim As Nullable(Of Date)) As IList(Of ILembrete)

    End Interface

End Namespace