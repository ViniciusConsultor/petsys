Imports Compartilhados
Imports Estoque.Interfaces.Negocio

Namespace Servicos

    Public Interface IServicoDeMovimentacaoDeProdutoEntrada
        Inherits IServico

        Sub InserirMovimentacaoDeEntrada(ByVal Movimentacao As IMovimentacaoDeProdutoEntrada)
        Sub EstornarMovimentacaoDeEntrada(Id As Long)
        Function ObtenhaMovimentacoes(ByVal DataInicio As Date, ByVal DataFinal As Date) As IList(Of IMovimentacaoDeProdutoEntrada)
        Function ObtenhaMovimentacao(ID As Long) As IMovimentacaoDeProdutoEntrada

    End Interface

End Namespace