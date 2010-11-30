Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Agenda
    Implements IAgenda

    Private _Compromissos As IList(Of ICompromisso)
    Private _Tarefas As IList(Of ITarefa)
    Private _Lembretes As IList(Of ILembrete)
    Private _Proprietario As IPessoaFisica

    Public Sub New(ByVal Inicio As Date, _
                   ByVal Fim As Date, _
                   ByVal Compromissos As IList(Of ICompromisso), _
                   ByVal Lembretes As IList(Of ILembrete), _
                   ByVal Tarefas As IList(Of ITarefa), _
                   ByVal Proprietario As IPessoaFisica)
        _Inicio = Inicio
        _Fim = Fim
        _Compromissos = Compromissos
        _Lembretes = Lembretes
        _Tarefas = Tarefas
        _Proprietario = Proprietario
    End Sub

    Private _Fim As Date
    Public ReadOnly Property Fim() As Date Implements IAgenda.Fim
        Get
            Return _Fim
        End Get
    End Property

    Private _Inicio As Date
    Public ReadOnly Property Inicio() As Date Implements IAgenda.Inicio
        Get
            Return _Inicio
        End Get
    End Property

    Private _DicionarioDeCompromissos As Dictionary(Of String, IList(Of ICompromisso))
    Public Function ObtenhaCompromissos(ByVal Data As Date) As IList(Of ICompromisso) Implements IAgenda.ObtenhaCompromissos
        Dim Compromissos As IList(Of ICompromisso) = Nothing

        If Not _DicionarioDeCompromissos Is Nothing Then
            _DicionarioDeCompromissos.TryGetValue(Data.ToString("yyyyMMdd"), Compromissos)
        End If

        Return Compromissos
    End Function

    Private _DicionarioDeLembretes As Dictionary(Of String, IList(Of ILembrete))
    Public Function ObtenhaLembretes(ByVal Data As Date) As IList(Of ILembrete) Implements IAgenda.ObtenhaLembretes
        Dim Lembretes As IList(Of ILembrete) = Nothing

        If Not _DicionarioDeCompromissos Is Nothing Then
            _DicionarioDeLembretes.TryGetValue(Data.ToString("yyyyMMdd"), Lembretes)
        End If

        Return Lembretes
    End Function

    Private _DicionarioDeTarefas As Dictionary(Of String, IList(Of ITarefa))
    Public Function ObtenhaTarefas(ByVal Data As Date) As IList(Of ITarefa) Implements IAgenda.ObtenhaTarefas
        Dim Tarefas As IList(Of ITarefa) = Nothing

        If Not _DicionarioDeTarefas Is Nothing Then
            _DicionarioDeTarefas.TryGetValue(Data.ToString("yyyyMMdd"), Tarefas)
        End If

        Return Tarefas
    End Function

    Public Sub Organize() Implements IAgenda.Organize
        OrganizeCompromissos()
        OrganizeTarefas()
        OrganizeLembretes()
    End Sub

    Private Sub OrganizeCompromissos()
        _DicionarioDeCompromissos = New Dictionary(Of String, IList(Of ICompromisso))

        If _Compromissos Is Nothing Then Exit Sub

        Dim DataAnterior As String = Nothing

        For Each Compromisso As ICompromisso In _Compromissos
            If DataAnterior Is Nothing OrElse (CLng(DataAnterior) < CLng(Compromisso.Inicio.ToString("yyyyMMdd"))) Then
                _DicionarioDeCompromissos.Add(Compromisso.Inicio.ToString("yyyyMMdd"), New List(Of ICompromisso))
            End If

            _DicionarioDeCompromissos(Compromisso.Inicio.ToString("yyyyMMdd")).Add(Compromisso)
            DataAnterior = Compromisso.Inicio.ToString("yyyyMMdd")
        Next
    End Sub

    Private Sub OrganizeTarefas()
        _DicionarioDeTarefas = New Dictionary(Of String, IList(Of ITarefa))

        If _Tarefas Is Nothing Then Exit Sub

        Dim DataAnterior As String = Nothing

        For Each Tarefa As ITarefa In _Tarefas
            If DataAnterior Is Nothing OrElse (CLng(DataAnterior) < CLng(Tarefa.DataDeInicio.ToString("yyyyMMdd"))) Then
                _DicionarioDeTarefas.Add(Tarefa.DataDeInicio.ToString("yyyyMMdd"), New List(Of ITarefa))
            End If

            _DicionarioDeTarefas(Tarefa.DataDeInicio.ToString("yyyyMMdd")).Add(Tarefa)
            DataAnterior = Tarefa.DataDeInicio.ToString("yyyyMMdd")
        Next
    End Sub

    Private Sub OrganizeLembretes()
        _DicionarioDeLembretes = New Dictionary(Of String, IList(Of ILembrete))

        If _Lembretes Is Nothing Then Exit Sub

        Dim DataAnterior As String = Nothing

        For Each Lembrete As ILembrete In _Lembretes
            If DataAnterior Is Nothing OrElse (CLng(DataAnterior) < CLng(Lembrete.Inicio.ToString("yyyyMMdd"))) Then
                _DicionarioDeLembretes.Add(Lembrete.Inicio.ToString("yyyyMMdd"), New List(Of ILembrete))
            End If

            _DicionarioDeLembretes(Lembrete.Inicio.ToString("yyyyMMdd")).Add(Lembrete)
            DataAnterior = Lembrete.Inicio.ToString("yyyyMMdd")
        Next

    End Sub

    Public ReadOnly Property Proprietario() As IPessoaFisica Implements IAgenda.Proprietario
        Get
            Return _Proprietario
        End Get
    End Property

End Class