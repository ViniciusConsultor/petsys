Namespace Core.Negocio

    Public Interface IConfiguracaoDeAgendaDoSistema

        Property TextoCabecalhoDeCompromissos() As String
        Property ApresentarLinhasNoCabecalhoDeCompromissos() As Boolean
        Property ApresentarLinhasNoRodapeDeCompromissos() As Boolean
        Property TextoCabelhoDeLembretes() As String
        Property ApresentarLinhasNoCabecalhoDeLembretes() As Boolean
        Property ApresentarLinhasNoRodapeDeLembretes() As Boolean
        Property TextoCabecalhoDeTarefas() As String
        Property ApresentarLinhasNoCabecalhoDeTarefas() As Boolean
        Property ApresentarLinhasNoRodapeDeTarefas() As Boolean

    End Interface

End Namespace