Imports System.Text
Imports System.Web

Public Class UtilidadesDeString
    Public Shared Function RemoveAcentos(Valor As String) As String
        If Not String.IsNullOrEmpty(Valor) Then
            Return HttpUtility.UrlEncode(Valor, Encoding.GetEncoding(28597)).Replace("+", " ")
        End If

        Return Valor
    End Function
End Class
