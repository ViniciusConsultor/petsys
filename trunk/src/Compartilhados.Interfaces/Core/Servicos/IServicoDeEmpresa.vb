Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDeEmpresa
        Inherits IServico

        Function Obtenha() As IEmpresa
        Sub Insira(ByVal Empresa As IEmpresa)

    End Interface

End Namespace