Imports Diary.Interfaces.Negocio
Imports Compartilhados

Namespace Servicos

    Public Interface IServicoDeSolicitacaoDeVisita
        Inherits IServico

        Sub Inserir(ByVal SolicitacaoDeVisita As ISolicitacaoDeVisita)
        Sub Modificar(ByVal SolicitacaoDeVisita As ISolicitacaoDeVisita)
        Sub Finalizar(ByVal ID As Long)
        Function ObtenhaSolicitacoesDeVisita(ByVal ConsiderarSolicitacoesFinalizadas As Boolean) As IList(Of ISolicitacaoDeVisita)
        Function ObtenhaSolicitacoesDeVisita(ByVal ConsiderarSolicitacoesFinalizadas As Boolean, ByVal DataInicio As Date, ByVal DataFim As Date) As IList(Of ISolicitacaoDeVisita)
        Function ObtenhaSolicitacaoDeVisita(ByVal ID As Long) As ISolicitacaoDeVisita
        Function ObtenhaSolicitacaoPorCodigo(ByVal Codigo As Long) As ISolicitacaoDeVisita
        Function ObtenhaSolicitacoesDeVisita(ByVal ConsiderarSolicitacoesFinalizadas As Boolean, ByVal IDContato As Long) As IList(Of ISolicitacaoDeVisita)
        Sub Remover(ByVal ID As Long)

    End Interface

End Namespace