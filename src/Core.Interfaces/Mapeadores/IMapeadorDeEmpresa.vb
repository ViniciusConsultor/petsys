Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeEmpresa

        Function Obtenha() As IEmpresa
        Sub Insira(ByVal Empresa As IEmpresa)

    End Interface

End Namespace