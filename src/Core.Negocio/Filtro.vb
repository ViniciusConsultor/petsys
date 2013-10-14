Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text

Public MustInherit Class Filtro
    Implements IFiltro
    
    Public MustOverride Function ObtenhaQuery() As String Implements IFiltro.ObtenhaQuery

    Private _operacao As OperacaoDeFiltro
    Public Property Operacao As OperacaoDeFiltro Implements IFiltro.Operacao
        Get
            Return _operacao
        End Get
        Set(value As OperacaoDeFiltro)
            _operacao = value
        End Set
    End Property

    Private _valorDoFiltro As String
    Public Property ValorDoFiltro As String Implements IFiltro.ValorDoFiltro
        Get
            Return _valorDoFiltro
        End Get
        Set(value As String)
            _valorDoFiltro = value
        End Set
    End Property

    Public Function ObtenhaFiltroMontado(campo As String, colocaAspas As Boolean) As String Implements IFiltro.ObtenhaFiltroMontado
        Dim caracter As String = ""

        If colocaAspas Then caracter = "'"
        
        If Operacao.Equals(OperacaoDeFiltro.ComecaCom) Then
            Return campo & " LIKE '" & ValorDoFiltro & "%' "
        ElseIf Operacao.Equals(OperacaoDeFiltro.EmQualquerParte) Then
            Return campo & " LIKE '%" & ValorDoFiltro & "%' "
        ElseIf Operacao.Equals(OperacaoDeFiltro.IgualA) Then
            Return campo & " = " & caracter & ValorDoFiltro & caracter & " "
        ElseIf Operacao.Equals(OperacaoDeFiltro.MaiorIgualA) Then
            Return campo & " >= " & caracter & ValorDoFiltro & caracter & " "
        ElseIf Operacao.Equals(OperacaoDeFiltro.MaiorQue) Then
            Return campo & " > " & caracter & ValorDoFiltro & caracter & " "
        ElseIf Operacao.Equals(OperacaoDeFiltro.MenorIgualA) Then
            Return campo & " <= " & caracter & ValorDoFiltro & caracter & " "
        ElseIf Operacao.Equals(OperacaoDeFiltro.MenorQue) Then
            Return campo & " < " & caracter & ValorDoFiltro & caracter & " "
        End If
        Return ""
    End Function

End Class
