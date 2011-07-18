Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeBancosEAgencias

        Sub InsiraBanco(ByVal Banco As IBanco)
        Sub RemovaBanco(ByVal ID As Long)
        Sub ModifiqueBanco(ByVal Banco As IBanco)
        Function ObtenhaBanco(ByVal Pessoa As IPessoa) As IBanco
        Function ObtenhaBancosPorNomeComoFiltro(ByVal Nome As String, ByVal Quantidade As Integer) As IList(Of IBanco)

        Sub InsiraAgencia(ByVal Agencia As IAgencia)
        Sub RemovaAgencia(ByVal ID As Long)
        Sub ModifiqueAgencia(ByVal Agencia As IAgencia)
        Function ObtenhaAgencia(ByVal IDBanco As Long, ByVal IDAgencia As Long) As IAgencia
        Function ObtenhaAgencias(ByVal IDBanco As Long) As IList(Of IAgencia)
        Function ObtenhaAgenciasPorNomeComoFiltro(ByVal Banco As IBanco, _
                                                  ByVal NomeDaAgencia As String, _
                                                  ByVal Quantidade As Integer) As IList(Of IAgencia)

    End Interface

End Namespace