Imports Diary.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeSolicitacaoDeVisita

        Sub Inserir(ByVal SolicitacaoDeVisita As ISolicitacaoDeVisita)
        Sub Modificar(ByVal SolicitacaoDeVisita As ISolicitacaoDeVisita)
        Function ObtenhaSolicitacoesDeVisita(ByVal TrazApenasAtivas As Boolean) As IList(Of ISolicitacaoDeVisita)
        Function ObtenhaSolicitacoesDeVisita(ByVal TrazApenasAtivas As Boolean, ByVal DataInicio As Date, ByVal DataFim As Date) As IList(Of ISolicitacaoDeVisita)
        Function ObtenhaSolicitacaoDeVisita(ByVal ID As Long) As ISolicitacaoDeVisita
        Function ObtenhaSolicitacaoPorCodigo(ByVal Codigo As Long) As ISolicitacaoDeVisita
        Sub Remover(ByVal ID As Long)
        Sub Finalizar(ByVal ID As Long)
        Function ObtenhaSolicitacoesDeVisita(ByVal TrazApenasAtivas As Boolean, _
                                              ByVal IDContato As Long) As IList(Of ISolicitacaoDeVisita)
    End Interface

End Namespace