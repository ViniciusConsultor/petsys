Imports Estoque.Interfaces.Negocio
Imports Estoque.Interfaces.Mapeadores

Public Class MapeadorDeMovimentacaoDeProdutoEntrada
    Inherits MapeadorDeMovimentacaoDeProduto(Of IMovimentacaoDeProdutoEntrada)
    Implements IMapeadorDeMovimentacaoDeProdutoEntrada

    Protected Overrides Sub EstorneEspecifico(ByVal Movimentacao As IMovimentacaoDeProdutoEntrada)

    End Sub

    Protected Overrides Sub InsiraEspecifico(ByVal Movimentacao As IMovimentacaoDeProdutoEntrada)

    End Sub

End Class