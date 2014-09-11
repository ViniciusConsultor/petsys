Namespace Negocio

    Public Interface IModulo

        Property ID() As String
        Property Nome() As String
        Sub AdicioneFuncao(ByVal Funcao As IFuncao)
        Function ObtenhaFuncoes() As IList(Of IFuncao)

    End Interface

End Namespace