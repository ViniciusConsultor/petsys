Imports Diary.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeSolicitacaoDeAudiencia

        Sub Inserir(ByVal SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia)
        Sub Modificar(ByVal SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia)
        Function ObtenhaSolicitacoesDeAudiencia(ByVal ConsiderarSolicitacoesFinalizadas As Boolean) As IList(Of ISolicitacaoDeAudiencia)
        Function ObtenhaSolicitacoesDeAudiencia(ByVal ConsiderarSolicitacoesFinalizadas As Boolean, ByVal DataInicio As Date, ByVal DataFim As Date) As IList(Of ISolicitacaoDeAudiencia)
        Function ObtenhaSolicitacaoDeAudiencia(ByVal ID As Long) As ISolicitacaoDeAudiencia
        Function ObtenhaSolicitacaoPorCodigo(ByVal Codigo As Long) As ISolicitacaoDeAudiencia
        Sub Remover(ByVal ID As Long)
        Sub Finalizar(ByVal ID As Long)
        Function ObtenhaSolicitacoesDeAudiencia(ByVal ConsiderarSolicitacoesFinalizadas As Boolean, _
                                                ByVal IDContato As Long) As IList(Of ISolicitacaoDeAudiencia)
    End Interface

End Namespace