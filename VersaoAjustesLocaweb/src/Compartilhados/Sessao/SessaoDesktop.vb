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

        If _Contexto.Conexao Is Nothing OrElse _Contexto.Perfil Is Nothing OrElse _Contexto.Usuario Is Nothing Then
            Dim Credencial As ICredencial = ServerUtils.getCredencial

            If Not Credencial Is Nothing Then
                _Contexto.Conexao = Credencial.Conexao
                _Contexto.Usuario = Credencial.Usuario
            End If
        End If
        
        Return _Contexto
    End Function

    Public Overrides Function SessaoEhWeb() As Boolean
        Return False
    End Function

End Class