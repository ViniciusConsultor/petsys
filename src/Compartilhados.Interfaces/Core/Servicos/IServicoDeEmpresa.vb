Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDeEmpresa
        Inherits IServico

        Sub Insira(ByVal Empresa As IEmpresa)
        Function Obtenha(ByVal Pessoa As IPessoa) As IEmpresa
        Sub Modificar(ByVal Empresa As IEmpresa)
        Sub Remover(ByVal ID As Long)

    End Interface

End Namespace