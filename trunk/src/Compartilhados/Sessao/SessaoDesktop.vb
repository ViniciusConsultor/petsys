Friend Class SessaoDesktop
    Inherits Sessao

    Private Shared _Contexto As Principal

    Public Sub New()
        Me.Contexto = New Principal
    End Sub

    Public Overrides Sub AtualizeContexto(ByVal ContextoAtual As Principal)
        _Contexto = ContextoAtual
    End Sub

    Public Overrides Property Contexto() As Principal
        Get
            Return Me.RecupereContexto()
        End Get
        Set(ByVal value As Principal)
            Me.AtualizeContexto(value)
        End Set
    End Property

    Public Overrides Function RecupereContexto() As Principal
        Return _Contexto
    End Function

End Class