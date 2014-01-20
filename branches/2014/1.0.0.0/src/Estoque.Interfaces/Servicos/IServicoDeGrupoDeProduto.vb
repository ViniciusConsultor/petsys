Imports Compartilhados
Imports Estoque.Interfaces.Negocio

Namespace Servicos

    Public Interface IServicoDeGrupoDeProduto
        Inherits IServico

        Function ObtenhaGrupoDeProdutos(ByVal ID As Long) As IGrupoDeProduto
        Function ObtenhaGruposDeProdutosPorNome(ByVal Nome As String, ByVal QuantidadeDeRegistros As Integer) As IList(Of IGrupoDeProduto)
        Sub Inserir(ByVal GrupoDeProdutos As IGrupoDeProduto)
        Sub Atualizar(ByVal GrupoDeProdutos As IGrupoDeProduto)
        Sub Remover(ByVal ID As Long)

    End Interface

End Namespace