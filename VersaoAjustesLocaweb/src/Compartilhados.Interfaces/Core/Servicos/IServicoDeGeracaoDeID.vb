Namespace Core.Servicos

    Public Interface IServicoDeGeracaoDeID
        Inherits IServico

        Function ObtenhaNumeroHigh() As Integer
        Sub ArmazeneNumeroHigh(ByVal NumeroHigh As Integer)

    End Interface

End Namespace