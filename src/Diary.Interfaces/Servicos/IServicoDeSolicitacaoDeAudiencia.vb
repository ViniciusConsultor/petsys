Imports Diary.Interfaces.Negocio

Namespace Servicos

    Public Interface IServicoDeSolicitacaoDeAudiencia

        Sub Inserir(ByVal SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia)
        Sub Modificar(ByVal SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia)
        Function ObtenhaSolicitacoesDeAudiencia(ByVal ApenasSolicitacoesAtivas As Boolean) As IList(Of ISolicitacaoDeAudiencia)
        Function ObtenhaSolicitacoesDeAudienciaEntreData(ByVal DataInicial As Date, ByVal DataFinal As Date, ByVal ApenasSolicitacoesAtivas As Boolean) As IList(Of ISolicitacaoDeAudiencia)
        Function ObtenhaSolicitacaoDeAudiencia(ByVal ID As Long) As ISolicitacaoDeAudiencia
        Sub Remover(ByVal ID As Long)

    End Interface

End Namespace