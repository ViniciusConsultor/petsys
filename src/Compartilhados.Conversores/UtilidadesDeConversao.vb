Public Class UtilidadesDeConversao

    Public Shared Function FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(ByVal Texto As String, _
                                                                                 ByVal LowerCaseSelecionado As Boolean) As String
        If String.IsNullOrEmpty(Texto) Then Return ""


        If Not LowerCaseSelecionado Then Return Texto.ToUpper

        Dim Retorno As String

        Retorno = Mid(Texto, 1, 1) & Mid(LCase(Texto), 2)
        Return Retorno
    End Function

    Public Shared Function FormataTextoTudoMinusculo(ByVal Texto As String, _
                                                     ByVal LowerCaseSelecionado As Boolean) As String
        If String.IsNullOrEmpty(Texto) Then Return ""

        If Not LowerCaseSelecionado Then Return Texto.ToUpper

        Return LCase(Texto)
    End Function

    Public Shared Function FormataTextoEmMaisculoMinusculoIntercaladoPorEspaco(ByVal Texto As String, _
                                                                               ByVal LowerCaseSelecinado As Boolean) As String
        If String.IsNullOrEmpty(Texto) Then Return ""

        Dim Vetor As String() = Texto.Split(CChar(" "))
        Dim Retorno As String = ""

        For Each Pedaco As String In Vetor
            If Not String.IsNullOrEmpty(Pedaco) Then

                If Pedaco.Equals("DA", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("DE", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("DI", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("D", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("DAS", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("DOS", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("NO", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("NA", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("E", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("A", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("AS", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("À", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("ÀS", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("E", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("É", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("O", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("OS", StringComparison.InvariantCultureIgnoreCase) OrElse _
                   Pedaco.Equals("DO", StringComparison.InvariantCultureIgnoreCase) Then
                    Retorno &= LCase(Pedaco) & " "
                Else
                    Retorno &= FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(Pedaco, True) & " "
                End If
            End If
        Next

        If Not LowerCaseSelecinado Then Return Retorno.Trim.ToUpper

        Return Retorno.TrimEnd
    End Function

End Class
