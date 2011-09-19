Imports Compartilhados
Imports Compartilhados.DBHelper
Imports Estoque.Interfaces.Mapeadores
Imports Estoque.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.Interfaces

Public MustInherit Class MapeadorDeMovimentacaoDeProduto(Of T As IMovimentacaoDeProduto)
    Implements IMapeadorDeMovimentacaoDeProduto(Of T)

    Protected MustOverride Sub EstorneEspecifico(ByVal Movimentacao As T)
    Protected MustOverride Sub InsiraEspecifico(ByVal Movimentacao As T)

    Public Sub Estornar(ByVal Movimentacao As T) Implements Interfaces.Mapeadores.IMapeadorDeMovimentacaoDeProduto(Of T).Estornar

    End Sub

    Public Sub Inserir(ByVal Movimentacao As T) Implements Interfaces.Mapeadores.IMapeadorDeMovimentacaoDeProduto(Of T).Inserir
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        Movimentacao.ID = GeradorDeID.getInstancia.getProximoID

        SQL.Append("INSERT INTO ETQ_MOVPRODUTO (ID, DATA, HISTORICO, NUMERODOCUMENTO, TIPO)")
        SQL.Append(" VALUES ( ")
        SQL.Append(String.Concat(Movimentacao.ID, ", "))
        SQL.Append(String.Concat(Movimentacao.Data.ToString("yyyyMMdd"), ", "))

        If Not String.IsNullOrEmpty(Movimentacao.Historico) Then
            SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Movimentacao.Historico), "', "))
        Else
            SQL.Append("NULL, ")
        End If

        If Not String.IsNullOrEmpty(Movimentacao.NumeroDocumento) Then
            SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Movimentacao.NumeroDocumento), "', "))
        Else
            SQL.Append("NULL, ")
        End If

        SQL.Append(String.Concat("'", Movimentacao.Tipo.ID.ToString, "')"))
        DBHelper.ExecuteNonQuery(SQL.ToString)

        InsiraProdutosMovimentados(Movimentacao)
        InsiraEspecifico(Movimentacao)
    End Sub

    Private Sub InsiraProdutosMovimentados(ByVal Movimentacao As T)
        Dim SQL As StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper
        Dim ProdutosMovimentados As IList(Of IProdutoMovimentado) = Movimentacao.ObtenhaProdutosMovimentados

        For Each ProdutoMovimentado As IProdutoMovimentado In ProdutosMovimentados
            SQL = New StringBuilder
            SQL.Append("INSERT INTO ETQ_PRODUTOMOV (IDMOVIMENTACAO, IDPRODUTO, INDICE, QUANTIDADE, PRECO)")
            SQL.Append(" VALUES ( ")
            SQL.Append(String.Concat(Movimentacao.ID.Value.ToString, ", "))
            SQL.Append(String.Concat(ProdutoMovimentado.Produto.ID.Value.ToString, ", "))
            SQL.Append(String.Concat(ProdutosMovimentados.IndexOf(ProdutoMovimentado), ", "))
            SQL.Append(String.Concat(UtilidadesDePersistencia.TPVd(ProdutoMovimentado.Quantidade), ", "))
            SQL.Append(String.Concat(UtilidadesDePersistencia.TPVd(ProdutoMovimentado.Preco), ")"))
            DBHelper.ExecuteNonQuery(SQL.ToString)
        Next
    End Sub

    Public Function ObtenhaMovimentacao(ByVal ID As Long) As T Implements IMapeadorDeMovimentacaoDeProduto(Of T).ObtenhaMovimentacao
        Return Nothing
    End Function

    Public Function ObtenhaMovimentacoes() As IList(Of T) Implements IMapeadorDeMovimentacaoDeProduto(Of T).ObtenhaMovimentacoes
        Return Nothing
    End Function

End Class