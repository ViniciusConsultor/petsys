Namespace Core.Negocio

    Public Interface IFiltro

        Function ObtenhaQuery() As String
        Function ObtenhaQueryParaQuantidade() As String
        Property Operacao As OperacaoDeFiltro
        Property ValorDoFiltro As String
        Function ObtenhaFiltroMontado(campo As String, colocaAspas As Boolean) As String
        
    End Interface

End Namespace