Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeBancosEAgencias

        Sub InsiraAgencia(ByVal Agencia As IAgencia)
        Sub RemovaAgencia(ByVal ID As Long)
        Sub ModifiqueAgencia(ByVal Agencia As IAgencia)
        Function ObtenhaAgencia(ByVal IDBanco As String, ByVal IDAgencia As Long) As IAgencia
        Function ObtenhaAgencias(ByVal IDBanco As String) As IList(Of IAgencia)
        Function ObtenhaAgenciasPorNomeComoFiltro(ByVal Banco As Banco, _
                                                  ByVal NomeDaAgencia As String, _
                                                  ByVal Quantidade As Integer) As IList(Of IAgencia)

    End Interface

End Namespace