Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeAgenda

        Sub ModifiqueConfiguracao(ByVal ConfiguracaoDaAgenda As IConfiguracaoDeAgendaDoUsuario)
        Sub RemovaConfiguracao(ByVal ID As Long)
        Function ObtenhaConfiguracao(ByVal Pessoa As IPessoa) As IConfiguracaoDeAgendaDoUsuario
        Function ObtenhaConfiguracao(ByVal IDPessoa As Long) As IConfiguracaoDeAgendaDoUsuario

        Function InsiraCompromisso(ByVal Compromisso As ICompromisso) As Long
        Sub ModifiqueCompromisso(ByVal Compromisso As ICompromisso)
        Sub RemovaCompromisso(ByVal ID As Long)
        Function ObtenhaCompromisso(ByVal ID As Long) As ICompromisso
        Function ObtenhaCompromissos(ByVal IDProprieatario As Long) As IList(Of ICompromisso)
        Function ObtenhaCompromissos(ByVal IDProprietario As Long, _
                                     ByVal DataInicio As Date, _
                                     ByVal DataFim As Nullable(Of Date)) As IList(Of ICompromisso)

        Function InsiraTarefa(ByVal Tarefa As ITarefa) As Long
        Sub ModifiqueTarefa(ByVal Tarefa As ITarefa)
        Sub RemovaTarefa(ByVal ID As Long)
        Function ObtenhaTarefa(ByVal ID As Long) As ITarefa
        Function ObtenhaTarefas(ByVal IDProprietario As Long) As IList(Of ITarefa)
        Function ObtenhaTarefas(ByVal IDProprietario As Long, _
                                   ByVal DataInicio As Date, _
                                   ByVal DataFim As Date?) As IList(Of ITarefa)


        Function InsiraLembrete(ByVal Lembrete As ILembrete) As Long
        Sub ModifiqueLembrete(ByVal Lembrete As ILembrete)
        Sub RemovaLembrete(ByVal ID As Long)
        Function ObtenhaLembrete(ByVal ID As Long) As ILembrete
        Function ObtenhaLembretes(ByVal IDProprietario As Long) As IList(Of ILembrete)
        Function ObtenhaLembretes(ByVal IDProprietario As Long, ByVal DataInicio As Date, ByVal DataFim As Nullable(Of Date)) As IList(Of ILembrete)

    End Interface

End Namespace