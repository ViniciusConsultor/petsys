Imports System.Web
Imports System.Runtime.Remoting.Messaging

Public Class ChamadaPorContexto

    Public Shared Function GetData(chave As String) As Object
        If Util.ExecutandoServidorWeb() Then
            Return HttpContext.Current.Items(chave)
        End If

        Return CallContext.GetData(chave)
    End Function

    Public Shared Sub SetData(chave As String, valor As Object)
        If Util.ExecutandoServidorWeb() Then

            If HttpContext.Current.Items.Contains(chave) Then
                HttpContext.Current.Items.Item(chave) = valor
                Exit Sub
            End If

            HttpContext.Current.Items.Add(chave, valor)
            Exit Sub
        End If
        
        CallContext.SetData(chave, valor)
    End Sub

    Public Shared Sub FreeNamedDataSlot(chave As String)
        If Util.ExecutandoServidorWeb() Then
            HttpContext.Current.Items.Remove(chave)
            Exit Sub
        End If

        CallContext.FreeNamedDataSlot(chave)
    End Sub

End Class