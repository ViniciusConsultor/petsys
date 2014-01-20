Imports Compartilhados
Imports Compartilhados.DBHelper
Imports Estoque.Interfaces.Negocio
Imports Estoque.Interfaces.Mapeadores
Imports System.Text
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Estoque.Interfaces.Negocio.LazyLoad

Public Class MapeadorDeMovimentacaoDeProdutoEntrada
    Inherits MapeadorDeMovimentacaoDeProduto(Of IMovimentacaoDeProdutoEntrada)
    Implements IMapeadorDeMovimentacaoDeProdutoEntrada

    Protected Overrides Sub EstorneEspecifico(ByVal Id As Long)
        Dim SQL As String
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL = "DELETE FROM ETQ_MOVPRODENTRADA WHERE IDMOVIMENTACAO = " & Id.ToString()
        DBHelper.ExecuteNonQuery(SQL)
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


        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Movimentacoes As IList(Of IMovimentacaoDeProdutoEntrada) = New List(Of IMovimentacaoDeProdutoEntrada)()

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            Try
                While Leitor.Read
                    Dim Movimentacao As IMovimentacaoDeProdutoEntrada

                    Movimentacao = FabricaGenerica.GetInstancia().CrieObjeto(Of IMovimentacaoDeProdutoEntrada)()

                    Movimentacao.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                    Movimentacao.Data = UtilidadesDePersistencia.getValorDate(Leitor, "DATA").Value

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "HISTORICO") Then
                        Movimentacao.Historico = UtilidadesDePersistencia.GetValorString(Leitor, "HISTORICO")
                    End If

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "NUMERODOCUMENTO") Then
                        Movimentacao.NumeroDocumento = UtilidadesDePersistencia.GetValorString(Leitor, "NUMERODOCUMENTO")
                    End If

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "IDFORNECEDOR") Then
                        Movimentacao.Fornecedor = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IFornecedorLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDFORNECEDOR"))
                    End If

                    ObtenhaProdutosMovimentados(CType(Movimentacao, IMovimentacaoDeProduto))
                    Movimentacoes.Add(Movimentacao)

                End While
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Movimentacoes
    End Function

    Protected Overrides Function ObtenhaMovimentacaoEspecifico(Id As Long) As Interfaces.Negocio.IMovimentacaoDeProdutoEntrada
        Dim SQL As String

        SQL = "SELECT ID, DATA, HISTORICO, NUMERODOCUMENTO, TIPO, IDMOVIMENTACAO, IDFORNECEDOR FROM ETQ_MOVPRODUTO, ETQ_MOVPRODENTRADA " & _
        " WHERE ID = IDMOVIMENTACAO AND ID = " & Id.ToString()

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Movimentacao As IMovimentacaoDeProdutoEntrada = Nothing

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            Try
                If Leitor.Read Then
                    Movimentacao = FabricaGenerica.GetInstancia().CrieObjeto(Of IMovimentacaoDeProdutoEntrada)()
                    PrenchaMovimentacao(Leitor, Movimentacao)

                    If Not UtilidadesDePersistencia.EhNulo(Leitor, "IDFORNECEDOR") Then
                        Movimentacao.Fornecedor = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IFornecedorLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDFORNECEDOR"))
                    End If

                    ObtenhaProdutosMovimentados(CType(Movimentacao, IMovimentacaoDeProduto))
                End If
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Movimentacao

    End Function

End Class