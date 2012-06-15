Imports Compartilhados
Imports Compartilhados.DBHelper
Imports Estoque.Interfaces.Negocio
Imports Estoque.Interfaces.Mapeadores
Imports System.Text

Public Class MapeadorDeMovimentacaoDeProdutoEntrada
    Inherits MapeadorDeMovimentacaoDeProduto(Of IMovimentacaoDeProdutoEntrada)
    Implements IMapeadorDeMovimentacaoDeProdutoEntrada

    Protected Overrides Sub EstorneEspecifico(ByVal Movimentacao As IMovimentacaoDeProdutoEntrada)

    End Sub

    Protected Overrides Sub InsiraEspecifico(ByVal Movimentacao As IMovimentacaoDeProdutoEntrada)
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("INSERT INTO ETQ_MOVPRODENTRADA (IDMOVIMENTACAO, IDFORNECEDOR)")
        SQL.Append(" VALUES ( ")
        SQL.Append(String.Concat(Movimentacao.ID.Value.ToString, ", "))

        If Movimentacao.Fornecedor Is Nothing Then
            SQL.Append("NULL)")
        Else
            SQL.Append(String.Concat(Movimentacao.Fornecedor.Pessoa.ID.Value.ToString, ")"))
        End If

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Protected Overrides Function ObtenhaMovimentacoesEspecifico(ByVal DataInicio As Date, _
                                                                ByVal DataFinal As Date) As IList(Of IMovimentacaoDeProdutoEntrada)
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ID, DATA, HISTORICO, NUMERODOCUMENTO, TIPO, IDMOVIMENTACAO, IDFORNECEDOR FROM ETQ_MOVPRODUTO, ETQ_MOVPRODENTRADA ")
        SQL.Append(" WHERE ID = IDMOVIMENTACAO AND DATA BETWEEN '" & DataInicio.ToString("yyyyMMdd") & "' AND '" & DataFinal.ToString("yyyyMMdd") & "'")


        Return Nothing
    End Function

End Class