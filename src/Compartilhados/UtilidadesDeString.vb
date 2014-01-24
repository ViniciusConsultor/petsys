Imports System.Text
Imports System.Web
Imports System.Globalization

Public Class UtilidadesDeString
    Public Shared Function RemoveAcentos(Valor As String) As String
        'If Not String.IsNullOrEmpty(Valor) Then
        '    Return HttpUtility.UrlEncode(Valor, Encoding.GetEncoding(28597)).Replace("+", " ")
        'End If

        Dim s = Valor.Normalize(NormalizationForm.FormD)

        Dim sb = New StringBuilder()

        For Each k As Char In s
            Dim uc = CharUnicodeInfo.GetUnicodeCategory(k)
            If Not uc = UnicodeCategory.NonSpacingMark Then
                sb.Append(k)
            End If
        Next

        Return sb.ToString()

    End Function
End Class
