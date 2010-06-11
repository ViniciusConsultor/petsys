Namespace Negocio

    Public Interface IJogo
        Inherits ICloneable

        Sub AdicionaDezena(ByVal Dezena As IDezena)
        Sub AdicionaDezenas(ByVal Dezenas As IList(Of IDezena))
        Function ObtenhaDezenas() As IList(Of IDezena)
        Function ObtenhaQuantidadeDeDezenasAcertadas(ByVal DezenasSorteadas As IList(Of IDezena)) As Short
        ReadOnly Property DezenasToString() As String
    End Interface

End Namespace