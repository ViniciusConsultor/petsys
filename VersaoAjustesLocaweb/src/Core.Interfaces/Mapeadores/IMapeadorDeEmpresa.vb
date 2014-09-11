Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeEmpresa

        Sub Insira(ByVal Empresa As IEmpresa)
        Function Obtenha(ByVal ID As Long) As IEmpresa
        Sub Modificar(ByVal Empresa As IEmpresa)
        Sub Remover(ByVal ID As Long)
        Function ObtenhaPorNome(Filtro As String, Quantidade As Integer) As IList(Of IEmpresa)


    End Interface

End Namespace