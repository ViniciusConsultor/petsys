Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeEmpresa

        Sub Insira(ByVal Empresa As IEmpresa)
        Function Obtenha(ByVal Pessoa As IPessoa) As IEmpresa
        Sub Modificar(ByVal Empresa As IEmpresa)
        Sub Remover(ByVal ID As Long)

    End Interface

End Namespace