Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text

<Serializable()> _
Public MustInherit Class Filtro
    Implements IFiltro

    Public MustOverride Function ObtenhaQuery() As String Implements IFiltro.ObtenhaQuery

    Private _operacao As OperacaoDeFiltro
    Public Property Operacao As OperacaoDeFiltro Implements IFiltro.Operacao
        Get
            Return _operacao
        End Get
        Set(ByVal value As OperacaoDeFiltro)
            _operacao = value
        End Set
    End Property

    Private _valorDoFiltro As String
    Public Property ValorDoFiltro As String Implements IFiltro.ValorDoFiltro
        Get
            Return _valorDoFiltro
        End Get
        Set(ByVal value As String)
            _valorDoFiltro = value
        End Set
    End Property

    Private _valorDoFiltro1 As String
    Private _ValorDoFiltro2 As String
    Public Sub AdicioneValoresDoFiltroParaEntre(ByVal valorDoFiltro1 As String, ByVal valorDoFiltro2 As String) Implements IFiltro.AdicioneValoresDoFiltroParaEntre
        _valorDoFiltro1 = valorDoFiltro1
        _ValorDoFiltro2 = valorDoFiltro2
    End Sub

    Public Function ObtenhaFiltroMontado(ByVal campo As String, ByVal colocaAspas As Boolean) As String Implements IFiltro.ObtenhaFiltroMontado
        Dim caracter As String = ""

        If colocaAspas Then caracter = "'"

        If Operacao.Equals(OperacaoDeFiltro.ComecaCom) Then
            Return campo & " LIKE '" & ValorDoFiltro & "%' "
        ElseIf Operacao.Equals(OperacaoDeFiltro.EmQualquerParte) Then
            Return campo & " LIKE '%" & ValorDoFiltro & "%' "
        ElseIf Operacao.Equals(OperacaoDeFiltro.IgualA) Then
            Return campo & " = " & caracter & ValorDoFiltro & caracter & " "
        ElseIf Operacao.Equals(OperacaoDeFiltro.Diferente) Then
            Return campo & " <> " & caracter & ValorDoFiltro & caracter & " "
        ElseIf Operacao.Equals(OperacaoDeFiltro.MaiorIgualA) Then
            Return campo & " >= " & caracter & ValorDoFiltro & caracter & " "
        ElseIf Operacao.Equals(OperacaoDeFiltro.MaiorQue) Then
            Return campo & " > " & caracter & ValorDoFiltro & caracter & " "
        ElseIf Operacao.Equals(OperacaoDeFiltro.MenorIgualA) Then
            Return campo & " <= " & caracter & ValorDoFiltro & caracter & " "
        ElseIf Operacao.Equals(OperacaoDeFiltro.MenorQue) Then
            Return campo & " < " & caracter & ValorDoFiltro & caracter & " "
        ElseIf Operacao.Equals(OperacaoDeFiltro.Intervalo) Then
            If String.IsNullOrEmpty(_valorDoFiltro1) OrElse String.IsNullOrEmpty(_ValorDoFiltro2) Then Throw New BussinesException("A operação de filtro Intervalo requer que seja passado 2 valores para a comparação.")

            Return campo & " <= " & caracter & _ValorDoFiltro2 & caracter & " AND " & campo & " >= " & caracter & _valorDoFiltro1 & caracter & " "
        End If
        Return ""
    End Function

    Public Function ObtenhaQueryParaQuantidade() As String Implements IFiltro.ObtenhaQueryParaQuantidade
        Dim sqlCount = New StringBuilder()

        Dim query = ObtenhaQuery()

        sqlCount.AppendLine("SELECT COUNT(*) QUANTIDADE ")
        sqlCount.AppendLine(query.Substring(query.IndexOf("from", StringComparison.InvariantCultureIgnoreCase)))

        Return sqlCount.ToString()

    End Function
End Class
