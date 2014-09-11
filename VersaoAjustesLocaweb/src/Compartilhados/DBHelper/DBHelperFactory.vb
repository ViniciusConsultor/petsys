Namespace DBHelper

    Public Class DBHelperFactory

        Public Shared Function Create(ByVal Conexao As IConexao) As IDBHelper
            Dim DB As IDBHelper = Nothing

            If Conexao.Provider.Equals(TipoDeProviderConexao.OLEBD) Then
                DB = New OleDbHelper(Conexao.StringDeConexao, Conexao.SistemaUtilizaSQLUpperCase)
            ElseIf Conexao.Provider.Equals(TipoDeProviderConexao.ODBC) Then
                DB = New ODBCHelper(Conexao.StringDeConexao, Conexao.SistemaUtilizaSQLUpperCase)
            ElseIf Conexao.Provider.Equals(TipoDeProviderConexao.SQLSERVER) Then
                DB = New SQLServerHelper(Conexao.StringDeConexao, Conexao.SistemaUtilizaSQLUpperCase)
            ElseIf Conexao.Provider.Equals(TipoDeProviderConexao.ORACLE) Then
                DB = New OracleHelper(Conexao.StringDeConexao, Conexao.SistemaUtilizaSQLUpperCase)
            ElseIf Conexao.Provider.Equals(TipoDeProviderConexao.SQLITE) Then
                DB = New SQLiteHelper(Conexao.StringDeConexao, Conexao.SistemaUtilizaSQLUpperCase)
            ElseIf Conexao.Provider.Equals(TipoDeProviderConexao.MYSQL) Then
                DB = New MySQLHelper(Conexao.StringDeConexao, Conexao.SistemaUtilizaSQLUpperCase)
            End If

            If DB Is Nothing Then Throw New Exception("Não existe provider helper implementado para o tipo " & Conexao.Provider.Descricao)
            Return DB
        End Function

    End Class

End Namespace