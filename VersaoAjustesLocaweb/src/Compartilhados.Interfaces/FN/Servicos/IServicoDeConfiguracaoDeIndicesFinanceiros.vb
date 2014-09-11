Imports Compartilhados.Interfaces.FN.Negocio

Namespace FN.Servicos

    Public Interface IServicoDeConfiguracaoDeIndicesFinanceiros
        Inherits IServico

        Sub Salve(Configuracao As IConfiguracaoDeIndicesFinanceiros)
        Function Obtenha() As IConfiguracaoDeIndicesFinanceiros

    End Interface

End Namespace
