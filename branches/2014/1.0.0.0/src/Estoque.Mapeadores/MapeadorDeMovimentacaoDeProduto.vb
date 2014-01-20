Imports Compartilhados
Imports Compartilhados.DBHelper
Imports Estoque.Interfaces.Mapeadores
Imports Estoque.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.Interfaces
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Estoque.Interfaces.Negocio.LazyLoad
Imports Compartilhados.Fabricas

Public MustInherit Class MapeadorDeMovimentacaoDeProduto(Of T As IMovimentacaoDeProduto)
    Implements IMapeadorDeMovimentacaoDeProduto(Of T)

    Protected MustOverride Sub EstorneEspecifico(ByVal Id As Long)
    Protected MustOverride Sub InsiraEspecifico(ByVal Movimentacao As T)
    Protected MustOverride Function ObtenhaMovimentacoesEspecifico(ByVal DataInicio As Date, _
                                                                   ByVal DataFinal As Date) As IList(Of T)
    Protected MustOverride Function ObtenhaMovimentacaoEspecifico(Id As Long) As T

    Public Sub Estornar(ByVal Id As Long) Implements IMapeadorDeMovimentacaoDeProduto(Of T).Estornar
        EstorneEspecifico(Id)

        Dim SQL As String
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL = "DELETE FROM ETQ_PRODUTOMOV WHERE IDMOVIMENTACAO = " & Id.ToString()
        DBHelper.ExecuteNonQuery(SQL)

        SQL = "DELETE FROM ETQ_MOVPRODUTO WHERE ID = " & Id.ToString()
        DBHelper.ExecuteNonQuery(SQL)
    End Sub

    Public Sub Inserir(ByVal Movimentacao As T) Implements IMapeadorDeMovimentacaoDeProduto(Of T).Inserir
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
        Return ObtenhaMovimentacaoEspecifico(ID)
    End Function

    Public Function ObtenhaMovimentacoes() As IList(Of T) Implements IMapeadorDeMovimentacaoDeProduto(Of T).ObtenhaMovimentacoes
        Return Nothing
    End Function

    Public Function ObtenhaMovimentacoes(ByVal DataInicio As Date, _
                                         ByVal DataFinal As Date) As IList(Of T) Implements IMapeadorDeMovimentacaoDeProduto(Of T).ObtenhaMovimentacoes
        Return ObtenhaMovimentacoesEspecifico(DataInicio, DataFinal)
    End Function

    Protected Sub PrenchaMovimentacao(ByVal Leitor As IDataReader, ByRef Movimentacao As T)
        Movimentacao.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
        Movimentacao.Data = UtilidadesDePersistencia.getValorDate(Leitor, "DATA").Value

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "HISTORICO") Then
            Movimentacao.Historico = UtilidadesDePersistencia.GetValorString(Leitor, "HISTORICO")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "NUMERODOCUMENTO") Then
            Movimentacao.NumeroDocumento = UtilidadesDePersistencia.GetValorString(Leitor, "NUMERODOCUMENTO")
        End If
    End Sub

    Protected Sub ObtenhaProdutosMovimentados(ByRef Movimentacao As IMovimentacaoDeProduto)
        Dim SQL As String

        SQL = "SELECT IDMOVIMENTACAO, IDPRODUTO, INDICE, QUANTIDADE, PRECO FROM ETQ_PRODUTOMOV WHERE IDMOVIMENTACAO = " & Movimentacao.ID.Value.ToString() & " ORDER BY INDICE"

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            Try
                While Leitor.Read
                    Dim ProdutoMovimentado As IProdutoMovimentado

                    ProdutoMovimentado = FabricaGenerica.GetInstancia().CrieObjeto(Of IProdutoMovimentado)()
                    ProdutoMovimentado.Produto = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IProdutoLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPRODUTO"))
                    ProdutoMovimentado.Preco = UtilidadesDePersistencia.getValorDouble(Leitor, "PRECO")
                    ProdutoMovimentado.Quantidade = UtilidadesDePersistencia.getValorDouble(Leitor, "QUANTIDADE")

                    Movimentacao.AdicioneProdutoMovimentado(ProdutoMovimentado)
                End While
            Finally
                Leitor.Close()
            End Try
        End Using
    End Sub

End Class