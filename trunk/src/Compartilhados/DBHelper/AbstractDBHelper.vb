Imports System.Data
Imports System.Data.Common

Namespace DBHelper

    Public MustInherit Class AbstractDBHelper
        Implements IDBHelper

        Protected ConexaoPadrao As IDbConnection
        Protected Transacao As IDbTransaction
        Protected _SistemaUtilizaSQLUpperCase As Boolean

        Protected MustOverride Function CrieConexao(ByVal StringDeConexao As String) As IDbConnection
        Protected MustOverride Function CrieDataAdapter(ByVal Comando As IDbCommand) As DbDataAdapter

        Public Sub New(ByVal StringDeConexao As String, ByVal SistemaUtilizaSQLUpperCase As Boolean)
            ConexaoPadrao = CrieConexao(StringDeConexao)
            _SistemaUtilizaSQLUpperCase = SistemaUtilizaSQLUpperCase
        End Sub

        Public Sub BeginTransaction(ByVal TipoTransacao As IsolationLevel) Implements IDBHelper.BeginTransaction
            Transacao = ConexaoPadrao.BeginTransaction(TipoTransacao)
        End Sub

        Public Sub BeginTransaction() Implements IDBHelper.BeginTransaction
            Transacao = ConexaoPadrao.BeginTransaction(IsolationLevel.ReadCommitted)
        End Sub

        Public Sub CommitTransaction() Implements IDBHelper.CommitTransaction
            Transacao.Commit()
        End Sub

        Public Sub Dispose() Implements IDBHelper.Dispose
            ConexaoPadrao.Close()
            ConexaoPadrao.Dispose()
        End Sub

        Public Function ExecuteNonQuery(ByVal SQL As String) As Integer Implements IDBHelper.ExecuteNonQuery
            Return ExecuteNonQuery(SQL, _SistemaUtilizaSQLUpperCase)
        End Function

        Public Function ExecuteNonQuery(ByVal SQL As String, _
                                        ByVal SaveUpperCase As Boolean) As Integer Implements IDBHelper.ExecuteNonQuery
            Dim Comando As IDbCommand = Nothing

            Try
                Comando = ConexaoPadrao.CreateCommand
                Comando.Transaction = Transacao

                ' Verifica se será necessário salvar os dados em Caixa Alta ou como o usuário informou
                If SaveUpperCase Then SQL = SQL.ToUpper
                Comando.CommandText = SQL

                Return Comando.ExecuteNonQuery()

            Catch ex As Exception
                Dim Mensagem As String = Nothing

                Mensagem = ObtenhaMensagemDaExcecaoLancada(ex)

                If String.IsNullOrEmpty(Mensagem) Then
                    Throw New Exception(ex.Message & vbCrLf _
                                      & "ORIGEM: DBHelper.ExecuteNonQuery " & vbCrLf _
                                      & "SQL: " & SQL)
                Else
                    Throw New BussinesException(Mensagem)
                End If
            Finally
                Comando.Dispose()
            End Try
        End Function

        Public Sub RollbackTransaction() Implements IDBHelper.RollbackTransaction
            Transacao.Rollback()
        End Sub

        Public Function existeTransacaoAberta() As Boolean Implements IDBHelper.existeTransacaoAberta
            Return Not Transacao Is Nothing
        End Function

        Public Function obtenhaReader(ByVal sQuery As String) As IDataReader Implements IDBHelper.obtenhaReader
            Dim Comando As IDbCommand = Nothing
            Dim Leitor As IDataReader

            Try
                Comando = Me.ConexaoPadrao.CreateCommand
                Comando.CommandText = sQuery

                Leitor = Comando.ExecuteReader
                Return Leitor
            Finally
                If Not Comando Is Nothing Then Comando.Dispose()
            End Try

        End Function

        Public Function obtenhaReader(ByVal Query As String, _
                                      ByVal QuantidadeDeRegistros As Integer) As IDataReader Implements IDBHelper.obtenhaReader
            If SuporteALimite() Then
                Return obtenhaReader(ObtenhaQueryComLimite(Query, QuantidadeDeRegistros))
            End If

            Return obtenhaReader(Query)
        End Function

        Public MustOverride Function SuporteALimite() As Boolean Implements IDBHelper.SuporteALimite

        Public Overridable Function ObtenhaQueryComLimite(ByVal QueryOriginal As String, _
                                                          ByVal QuantidadeDeRegistros As Integer) As String Implements IDBHelper.ObtenhaQueryComLimite
            Return QueryOriginal
        End Function

        Public MustOverride Function ObtenhaMensagemDaExcecaoLancada(ByVal Ex As Exception) As String Implements IDBHelper.ObtenhaMensagemDaExcecaoLancada

        Public Overridable Function ObtenhaQueryComLimiteEOffset(QueryOriginal As String, QuantidadeDeRegistros As Integer, OffSet As Integer) As String Implements IDBHelper.ObtenhaQueryComLimiteEOffset
            Return QueryOriginal
        End Function

        Public Function obtenhaReader(Query As String, _
                                      QuantidadeDeRegistros As Integer, _
                                      OffSet As Integer) As IDataReader Implements IDBHelper.obtenhaReader

            If SuporteALimite() AndAlso SuporteAOffSet() Then
                Return obtenhaReader(ObtenhaQueryComLimiteEOffset(Query, QuantidadeDeRegistros, OffSet))
            End If

            Return obtenhaReader(Query)
        End Function

        Public MustOverride Function SuporteAOffSet() As Boolean Implements IDBHelper.SuporteAOffSet

    End Class

End Namespace