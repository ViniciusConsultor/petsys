Imports System.Text
Imports System.Web
Imports System.Globalization
Imports System.IO
Imports System.Xml.Xsl
Imports System.Xml.XPath
Imports System.Text.RegularExpressions
Imports System.Net

Public Class UtilidadesDeString

    Public Shared Function TransformeStringParaStringApresentavel(stringDesformatada As String) As String
        Dim transformaEmMaiusculo = True
        Dim stringFormatada = New StringBuilder

        For Each caracter In stringDesformatada
            If Char.IsWhiteSpace(caracter) Then
                transformaEmMaiusculo = True
                stringFormatada.Append(caracter)
                Continue For
            End If

            If transformaEmMaiusculo Then
                caracter = Char.ToUpper(caracter)
                transformaEmMaiusculo = False
            Else
                caracter = Char.ToLower(caracter)
            End If

            stringFormatada.Append(caracter)
        Next

        Return stringFormatada.ToString()
    End Function

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

    Public Shared Function TransfomeHtmlParaTextoPuro(TextoHtml As String, MantemQuebraDeLinha As Boolean) As String
        If String.IsNullOrEmpty(TextoHtml) Then Return String.Empty

        Dim evaluator As MatchEvaluator = Nothing

        If MantemQuebraDeLinha Then
            TextoHtml = TextoHtml.Replace("<BR />", "¬")
        End If


        Dim texto As String = TextoHtml

        texto = texto.Replace(ChrW(13), " ")
        texto = texto.Replace(ChrW(10), " ")
        texto = texto.Replace(ChrW(9), String.Empty)
        texto = Regex.Replace(texto, " +", " ")
        texto = Regex.Replace(texto, "<head[^>]*>[\s\S]*?</head>", String.Empty, RegexOptions.IgnoreCase)
        texto = Regex.Replace(texto, "<script[^>]*>[\s\S]*?</script>", String.Empty, RegexOptions.IgnoreCase)
        texto = Regex.Replace(texto, "<style[^>]*>[\s\S]*?</style>", String.Empty, RegexOptions.IgnoreCase)
        texto = Regex.Replace(texto, "<!--[\s\S]*?-->", String.Empty, RegexOptions.IgnoreCase)
        texto = Regex.Replace(texto, "<(td|th)[^>]*>", ChrW(9), RegexOptions.IgnoreCase)
        texto = Regex.Replace(texto, "<(br|li|h1|h2|h3|h4|h5|h6)[^>]*>", ChrW(10), RegexOptions.IgnoreCase)
        texto = Regex.Replace(texto, "<(div|tr|p)[^>]*>", ChrW(10) & ChrW(10), RegexOptions.IgnoreCase)
        texto = Regex.Replace(texto, "<[^>]*>", String.Empty, RegexOptions.IgnoreCase)
        If (evaluator Is Nothing) Then
            evaluator = Function(m As Match)
                            Return WebUtility.HtmlDecode(m.Value)
                        End Function
        End If
        texto = Regex.Replace(texto, "\&[^;]+;", evaluator, RegexOptions.IgnoreCase)
        texto = Regex.Replace(texto, "^[\n\s]+", String.Empty, RegexOptions.IgnoreCase)
        texto = Regex.Replace(texto, "[\n\s]+$", String.Empty, RegexOptions.IgnoreCase)
        texto = Regex.Replace(texto, "\n[\s]+\n", ChrW(10) & ChrW(10), RegexOptions.IgnoreCase)
        texto = Regex.Replace(texto, "\n\n[\n]+", ChrW(10) & ChrW(10), RegexOptions.IgnoreCase)

        Return texto.Replace("¬", Environment.NewLine)

    End Function

End Class
