Imports Estoque.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeGrupoDeProduto

        Function ObtenhaGrupoDeProdutos(ByVal ID As Long) As IGrupoDeProduto
        Function ObtenhaGruposDeProdutosPorNome(ByVal Nome As String, ByVal QuantidadeDeRegistros As Integer) As IList(Of IGrupoDeProduto)
        Sub Inserir(ByVal GrupoDeProdutos As IGrupoDeProduto)
        Sub Atualizar(ByVal GrupoDeProdutos As IGrupoDeProduto)
        Sub Remover(ByVal ID As Long)

    End Interface

End Namespace