Imports Compartilhados
Imports Estoque.Interfaces.Servicos
Imports Estoque.Interfaces.Negocio

Public Class ServicoDeMovimentacaoDeProdutoLocal
    Inherits Servico
    Implements IServicoDeMovimentacaoDeProduto

    Public Sub New(ByVal Credencial As Credencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub EstornarMovimentacaoDeEntrada(ByVal Movimentacao As IMovimentacaoDeProdutoEntrada) Implements IServicoDeMovimentacaoDeProduto.EstornarMovimentacaoDeEntrada

    End Sub

    Public Sub InserirMovimentacaoDeEntrada(ByVal Movimentacao As IMovimentacaoDeProdutoEntrada) Implements IServicoDeMovimentacaoDeProduto.InserirMovimentacaoDeEntrada

    End Sub

End Class
