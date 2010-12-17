Namespace Core.Negocio

    Public Interface IConfiguracaoDeAgendaDoSistema

        Property TextoCabecalhoDaAgenda() As String
        Property ApresentarLinhasNoCabecalhoDaAgenda() As Boolean
        Property ApresentarLinhasNoRodapeDaAgenda() As Boolean

        Property TextoCompromissos() As String
        Property TextoDoCompromissoEntreLinhas() As Boolean

        Property TextoLembretes() As String
        Property TextoDeLembretesEntreLinhas() As Boolean

        Property TextoTarefas() As String
        Property TextoDeTarefasEntreLinhas() As Boolean

    End Interface

End Namespace