Namespace Negocio

    Public Interface IFuncao

        Property ID() As String
        Property Nome() As String
        Sub AdicioneOperacao(ByVal Operacao As IOperacao)
        Function ObtenhaOperacoes() As IList(Of IOperacao)

    End Interface

End Namespace