Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class ConfiguracaoDeAgendaDoSistema
    Implements IConfiguracaoDeAgendaDoSistema

    Private _ApresentarLinhasNoCabecalhoDaAgenda As Boolean
    Public Property ApresentarLinhasNoCabecalhoDaAgenda() As Boolean Implements IConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoCabecalhoDaAgenda
        Get
            Return _ApresentarLinhasNoCabecalhoDaAgenda
        End Get
        Set(ByVal value As Boolean)
            _ApresentarLinhasNoCabecalhoDaAgenda = value
        End Set
    End Property

    Private _ApresentarLinhasNoRodapeDaAgenda As Boolean
    Public Property ApresentarLinhasNoRodapeDaAgenda() As Boolean Implements IConfiguracaoDeAgendaDoSistema.ApresentarLinhasNoRodapeDaAgenda
        Get
            Return _ApresentarLinhasNoRodapeDaAgenda
        End Get
        Set(ByVal value As Boolean)
            _ApresentarLinhasNoRodapeDaAgenda = value
        End Set
    End Property

    Private _TextoCabecalhoDaAgenda As String
    Public Property TextoCabecalhoDaAgenda() As String Implements IConfiguracaoDeAgendaDoSistema.TextoCabecalhoDaAgenda
        Get
            Return _TextoCabecalhoDaAgenda
        End Get
        Set(ByVal value As String)
            _TextoCabecalhoDaAgenda = value
        End Set
    End Property

    Private _TextoCompromissos As String
    Public Property TextoCompromissos() As String Implements IConfiguracaoDeAgendaDoSistema.TextoCompromissos
        Get
            Return _TextoCompromissos
        End Get
        Set(ByVal value As String)
            _TextoCompromissos = value
        End Set
    End Property

    Private _TextoDeLembretesEntreLinhas As Boolean
    Public Property TextoDeLembretesEntreLinhas() As Boolean Implements IConfiguracaoDeAgendaDoSistema.TextoDeLembretesEntreLinhas
        Get
            Return _TextoDeLembretesEntreLinhas
        End Get
        Set(ByVal value As Boolean)
            _TextoDeLembretesEntreLinhas = value
        End Set
    End Property

    Private _TextoDeTarefasEntreLinhas As Boolean
    Public Property TextoDeTarefasEntreLinhas() As Boolean Implements IConfiguracaoDeAgendaDoSistema.TextoDeTarefasEntreLinhas
        Get
            Return _TextoDeTarefasEntreLinhas
        End Get
        Set(ByVal value As Boolean)
            _TextoDeTarefasEntreLinhas = value
        End Set
    End Property

    Private _TextoDoCompromissoEntreLinhas As Boolean
    Public Property TextoDoCompromissoEntreLinhas() As Boolean Implements IConfiguracaoDeAgendaDoSistema.TextoDoCompromissoEntreLinhas
        Get
            Return _TextoDoCompromissoEntreLinhas
        End Get
        Set(ByVal value As Boolean)
            _TextoDoCompromissoEntreLinhas = value
        End Set
    End Property

    Private _TextoLembretes As String
    Public Property TextoLembretes() As String Implements IConfiguracaoDeAgendaDoSistema.TextoLembretes
        Get
            Return _TextoLembretes
        End Get
        Set(ByVal value As String)
            _TextoLembretes = value
        End Set
    End Property

    Private _TextoTarefas As String
    Public Property TextoTarefas() As String Implements IConfiguracaoDeAgendaDoSistema.TextoTarefas
        Get
            Return _TextoTarefas
        End Get
        Set(ByVal value As String)
            _TextoTarefas = value
        End Set
    End Property

End Class
