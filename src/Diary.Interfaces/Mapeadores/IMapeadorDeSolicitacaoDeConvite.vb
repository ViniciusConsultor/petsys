Imports Diary.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeSolicitacaoDeConvite

        Sub Inserir(ByVal SolicitacaoDeConvite As ISolicitacaoDeConvite)
        Sub Modificar(ByVal SolicitacaoDeConvite As ISolicitacaoDeConvite)
        Function ObtenhaSolicitacoesDeConvite(ByVal TrazApenasAtivas As Boolean) As IList(Of ISolicitacaoDeConvite)
        Function ObtenhaSolicitacoesDeConvite(ByVal TrazApenasAtivas As Boolean, ByVal DataInicio As Date, ByVal DataFim As Date) As IList(Of ISolicitacaoDeConvite)
        Function ObtenhaSolicitacaoDeConvite(ByVal ID As Long) As ISolicitacaoDeConvite
        Function ObtenhaSolicitacaoPorCodigo(ByVal Codigo As Long) As ISolicitacaoDeConvite
        Sub Remover(ByVal ID As Long)
        Sub Finalizar(ByVal ID As Long)

    End Interface

End Namespace