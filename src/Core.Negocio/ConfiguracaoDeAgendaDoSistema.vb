Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class ConfiguracaoDeAgendaDoSistema
    Implements IConfiguracaoDeAgendaDoSistema


    Private _ApresentarLinhasNoCabecalhoDeCompromissos As Boolean
    Public Property ApresentarLinhasNoCabecalhoDeCompromissos() As Boolean Implements IConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoCabecalhoDeCompromissos
        Get
            Return _ApresentarLinhasNoCabecalhoDeCompromissos
        End Get
        Set(ByVal value As Boolean)
            _ApresentarLinhasNoCabecalhoDeCompromissos = value
        End Set
    End Property

    Private _ApresentarLinhasNoCabecalhoDeLembretes As Boolean
    Public Property ApresentarLinhasNoCabecalhoDeLembretes() As Boolean Implements IConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoCabecalhoDeLembretes
        Get
            Return _ApresentarLinhasNoCabecalhoDeLembretes
        End Get
        Set(ByVal value As Boolean)
            _ApresentarLinhasNoCabecalhoDeLembretes = value
        End Set
    End Property

    Private _ApresentarLinhasNoCabecalhoDeTarefas As Boolean
    Public Property ApresentarLinhasNoCabecalhoDeTarefas() As Boolean Implements IConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoCabecalhoDeTarefas
        Get
            Return _ApresentarLinhasNoCabecalhoDeTarefas
        End Get
        Set(ByVal value As Boolean)
            _ApresentarLinhasNoCabecalhoDeTarefas = value
        End Set
    End Property

    Private _ApresentarLinhasNoRodapeDeCompromissos As Boolean
    Public Property ApresentarLinhasNoRodapeDeCompromissos() As Boolean Implements IConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoRodapeDeCompromissos
        Get
            Return _ApresentarLinhasNoRodapeDeCompromissos
        End Get
        Set(ByVal value As Boolean)
            _ApresentarLinhasNoRodapeDeCompromissos = value
        End Set
    End Property

    Private _ApresentarLinhasNoRodapeDeLembretes As Boolean
    Public Property ApresentarLinhasNoRodapeDeLembretes() As Boolean Implements IConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoRodapeDeLembretes
        Get
            Return _ApresentarLinhasNoRodapeDeLembretes
        End Get
        Set(ByVal value As Boolean)
            _ApresentarLinhasNoRodapeDeLembretes = value
        End Set
    End Property

    Private _ApresentarLinhasNoRodapeDeTarefas As Boolean
    Public Property ApresentarLinhasNoRodapeDeTarefas() As Boolean Implements IConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoRodapeDeTarefas
        Get
            Return _ApresentarLinhasNoRodapeDeTarefas
        End Get
        Set(ByVal value As Boolean)
            _ApresentarLinhasNoRodapeDeTarefas = value
        End Set
    End Property

    Private _TextoCabecalhoDeCompromissos As String
    Public Property TextoCabecalhoDeCompromissos() As String Implements IConfiguracaoDeAgendaDoSistema.TextoCabecalhoDeCompromissos
        Get
            Return _TextoCabecalhoDeCompromissos
        End Get
        Set(ByVal value As String)
            _TextoCabecalhoDeCompromissos = value
        End Set
    End Property

    Private _TextoCabecalhoDeTarefas As String
    Public Property TextoCabecalhoDeTarefas() As String Implements IConfiguracaoDeAgendaDoSistema.TextoCabecalhoDeTarefas
        Get
            Return _TextoCabecalhoDeTarefas
        End Get
        Set(ByVal value As String)
            _TextoCabecalhoDeTarefas = value
        End Set
    End Property

    Private _TextoCabelhoDeLembretes As String
    Public Property TextoCabelhoDeLembretes() As String Implements IConfiguracaoDeAgendaDoSistema.TextoCabelhoDeLembretes
        Get
            Return _TextoCabelhoDeLembretes
        End Get
        Set(ByVal value As String)
            _TextoCabelhoDeLembretes = value
        End Set
    End Property
End Class
