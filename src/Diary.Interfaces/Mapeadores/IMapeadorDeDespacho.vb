Imports Diary.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeDespacho

        Sub Inserir(ByVal Despacho As IDespacho)
        Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long) As IList(Of IDespacho)
        Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long, ByVal DataInicial As Date, ByVal DataFinal As Nullable(Of Date)) As IList(Of IDespacho)
        Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long, ByVal Tipo As TipoDeDespacho) As IList(Of IDespacho)

    End Interface

End Namespace