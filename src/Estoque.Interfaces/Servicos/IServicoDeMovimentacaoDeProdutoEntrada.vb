Imports Compartilhados
Imports Estoque.Interfaces.Negocio

Namespace Servicos

    Public Interface IServicoDeMovimentacaoDeProdutoEntrada
        Inherits IServico

        Sub InserirMovimentacaoDeEntrada(ByVal Movimentacao As IMovimentacaoDeProdutoEntrada)
        Sub EstornarMovimentacaoDeEntrada(ByVal Movimentacao As IMovimentacaoDeProdutoEntrada)
        Function ObtenhaMovimentacoes(ByVal DataInicio As Date, ByVal DataFinal As Date) As IList(Of IMovimentacaoDeProdutoEntrada)

    End Interface

End Namespace