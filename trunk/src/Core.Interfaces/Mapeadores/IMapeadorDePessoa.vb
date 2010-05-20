Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDePessoa(Of T As IPessoa)

        Function ObtenhaPessoasPorNomeComoFiltro(ByVal Nome As String, _
                                                 ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of T)
        
        Function Obtenha(ByVal ID As Long) As T
        Function Inserir(ByVal Pessoa As T) As Long
        Sub Atualizar(ByVal Pessoa As T)
        Sub Remover(ByVal Pessoa As T)

    End Interface

End Namespace