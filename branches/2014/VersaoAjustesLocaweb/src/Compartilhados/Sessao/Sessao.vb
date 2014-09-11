<Serializable()> _
Friend MustInherit Class Sessao

    MustOverride Property Contexto() As Principal
    MustOverride Sub AtualizeContexto(ByVal ContextoAtual As Principal)
    MustOverride Function RecupereContexto() As Principal
    MustOverride Function SessaoEhWeb() As Boolean

    Private Shared InstanciaSessao As Sessao
    Private Shared ObjLock As New Object

    Public Shared Function GetInstancia() As Sessao
        If InstanciaSessao Is Nothing Then
            SyncLock ObjLock
                If InstanciaSessao Is Nothing Then
                    If Not Util.ExecutandoServidorWeb() Then
                        InstanciaSessao = New SessaoDesktop
                    Else
                        InstanciaSessao = New SessaoWeb
                    End If
                End If
            End SyncLock
        End If

        Return InstanciaSessao
    End Function

End Class