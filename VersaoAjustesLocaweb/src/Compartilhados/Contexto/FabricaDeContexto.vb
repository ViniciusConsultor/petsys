Public Class FabricaDeContexto

    Private Shared InstanciaFabrica As FabricaDeContexto
    Private Shared ObjLock As New Object

    Public Shared Function GetInstancia() As FabricaDeContexto
        If InstanciaFabrica Is Nothing Then
            SyncLock ObjLock
                If InstanciaFabrica Is Nothing Then
                    InstanciaFabrica = New FabricaDeContexto
                End If
            End SyncLock
        End If

        Return InstanciaFabrica
    End Function

    Public Function GetContextoAtual() As Principal
        Return Sessao.GetInstancia.RecupereContexto
    End Function

End Class