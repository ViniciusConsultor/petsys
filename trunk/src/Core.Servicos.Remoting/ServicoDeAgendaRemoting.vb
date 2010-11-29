Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Servicos.Local

Public Class ServicoDeAgendaRemoting
    Inherits ServicoRemoto
    Implements IServicoDeAgenda

    Private _ServicoLocal As ServicoDeAgendaLocal

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDeAgendaLocal(Credencial)
    End Sub

    Public Function InsiraCompromisso(ByVal Compromisso As ICompromisso) As Long Implements IServicoDeAgenda.InsiraCompromisso
        Return _ServicoLocal.InsiraCompromisso(Compromisso)
    End Function

    Public Function InsiraLembrete(ByVal Lembrete As ILembrete) As Long Implements IServicoDeAgenda.InsiraLembrete
        Return _ServicoLocal.InsiraLembrete(Lembrete)
    End Function

    Public Function InsiraTarefa(ByVal Tarefa As ITarefa) As Long Implements IServicoDeAgenda.InsiraTarefa
        Return _ServicoLocal.InsiraTarefa(Tarefa)
    End Function

    Public Sub ModifiqueCompromisso(ByVal Compromisso As ICompromisso) Implements IServicoDeAgenda.ModifiqueCompromisso
        _ServicoLocal.ModifiqueCompromisso(Compromisso)
    End Sub

    Public Sub ModifiqueLembrete(ByVal Lembrete As ILembrete) Implements IServicoDeAgenda.ModifiqueLembrete
        _ServicoLocal.ModifiqueLembrete(Lembrete)
    End Sub

    Public Sub ModifiqueTarefa(ByVal Tarefa As ITarefa) Implements IServicoDeAgenda.ModifiqueTarefa
        _ServicoLocal.ModifiqueTarefa(Tarefa)
    End Sub

    Public Function ObtenhaCompromisso(ByVal ID As Long) As ICompromisso Implements IServicoDeAgenda.ObtenhaCompromisso
        Return _ServicoLocal.ObtenhaCompromisso(ID)
    End Function

    Public Function ObtenhaCompromissos(ByVal IDProprietario As Long) As IList(Of ICompromisso) Implements IServicoDeAgenda.ObtenhaCompromissos
        Return _ServicoLocal.ObtenhaCompromissos(IDProprietario)
    End Function

    Public Function ObtenhaCompromissos(ByVal IDProprietario As Long, ByVal DataInicio As Date, ByVal DataFim As Date?) As IList(Of ICompromisso) Implements IServicoDeAgenda.ObtenhaCompromissos
        Return _ServicoLocal.ObtenhaCompromissos(IDProprietario, DataInicio, DataFim)
    End Function

    Public Function ObtenhaLembrete(ByVal ID As Long) As ILembrete Implements IServicoDeAgenda.ObtenhaLembrete
        Return _ServicoLocal.ObtenhaLembrete(ID)
    End Function

    Public Function ObtenhaLembretes(ByVal IDProprietario As Long) As IList(Of ILembrete) Implements IServicoDeAgenda.ObtenhaLembretes
        Return _ServicoLocal.ObtenhaLembretes(IDProprietario)
    End Function

    Public Function ObtenhaLembretes(ByVal IDProprietario As Long, ByVal DataInicio As Date, ByVal DataFim As Date?) As IList(Of ILembrete) Implements IServicoDeAgenda.ObtenhaLembretes
        Return _ServicoLocal.ObtenhaLembretes(IDProprietario, DataInicio, DataFim)
    End Function

    Public Function ObtenhaTarefa(ByVal ID As Long) As ITarefa Implements IServicoDeAgenda.ObtenhaTarefa
        Return _ServicoLocal.ObtenhaTarefa(ID)
    End Function

    Public Function ObtenhaTarefas(ByVal IDProprietario As Long) As IList(Of ITarefa) Implements IServicoDeAgenda.ObtenhaTarefas
        Return _ServicoLocal.ObtenhaTarefas(IDProprietario)
    End Function

    Public Function ObtenhaTarefas(ByVal IDProprietario As Long, ByVal DataInicio As Date, ByVal DataFim As Date?) As IList(Of ITarefa) Implements IServicoDeAgenda.ObtenhaTarefas
        Return _ServicoLocal.ObtenhaTarefas(IDProprietario, DataInicio, DataFim)
    End Function

    Public Sub RemovaCompromisso(ByVal ID As Long) Implements IServicoDeAgenda.RemovaCompromisso
        _ServicoLocal.RemovaCompromisso(ID)
    End Sub

    Public Sub RemovaLembrete(ByVal ID As Long) Implements IServicoDeAgenda.RemovaLembrete
        _ServicoLocal.RemovaLembrete(ID)
    End Sub

    Public Sub RemovaTarefa(ByVal ID As Long) Implements IServicoDeAgenda.RemovaTarefa
        _ServicoLocal.RemovaTarefa(ID)
    End Sub

    Public Sub ModifiqueConfiguracao(ByVal ConfiguracaoDaAgenda As IConfiguracaoDeAgendaDoUsuario) Implements IServicoDeAgenda.ModifiqueConfiguracao
        _ServicoLocal.ModifiqueConfiguracao(ConfiguracaoDaAgenda)
    End Sub

    Public Function ObtenhaConfiguracao(ByVal Pessoa As IPessoa) As IConfiguracaoDeAgendaDoUsuario Implements IServicoDeAgenda.ObtenhaConfiguracao
        Return _ServicoLocal.ObtenhaConfiguracao(Pessoa)
    End Function

    Public Function ObtenhaConfiguracao(ByVal IDPessoa As Long) As IConfiguracaoDeAgendaDoUsuario Implements IServicoDeAgenda.ObtenhaConfiguracao
        Return _ServicoLocal.ObtenhaConfiguracao(IDPessoa)
    End Function

    Public Sub RemovaConfiguracao(ByVal ID As Long) Implements IServicoDeAgenda.RemovaConfiguracao
        _ServicoLocal.RemovaConfiguracao(ID)
    End Sub

End Class