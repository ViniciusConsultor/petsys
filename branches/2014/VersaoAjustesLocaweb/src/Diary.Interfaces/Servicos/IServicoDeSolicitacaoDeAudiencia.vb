Imports Diary.Interfaces.Negocio
Imports Compartilhados

Namespace Servicos

    Public Interface IServicoDeSolicitacaoDeAudiencia
        Inherits IServico

        Sub Inserir(ByVal SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia)
        Sub Modificar(ByVal SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia)
        Sub Finalizar(ByVal ID As Long)
        Function ObtenhaSolicitacoesDeAudiencia(ByVal ConsiderarSolicitacoesFinalizadas As Boolean) As IList(Of ISolicitacaoDeAudiencia)
        Function ObtenhaSolicitacoesDeAudiencia(ByVal ConsiderarSolicitacoesFinalizadas As Boolean, ByVal DataInicio As Date, ByVal DataFim As Date) As IList(Of ISolicitacaoDeAudiencia)
        Function ObtenhaSolicitacaoDeAudiencia(ByVal ID As Long) As ISolicitacaoDeAudiencia
        Function ObtenhaSolicitacaoPorCodigo(ByVal Codigo As Long) As ISolicitacaoDeAudiencia
        Function ObtenhaSolicitacoesDeAudiencia(ByVal ConsiderarSolicitacoesFinalizadas As Boolean, ByVal IDContato As Long) As IList(Of ISolicitacaoDeAudiencia)
        Sub Remover(ByVal ID As Long)

    End Interface

End Namespace