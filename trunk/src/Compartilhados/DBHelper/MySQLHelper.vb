Imports MySql.Data.MySqlClient
Imports System.Data.Common

Namespace DBHelper

    Friend Class MySQLHelper
        Inherits AbstractDBHelper

        Public Sub New(ByVal sStrConn As String)
            MyBase.New(sStrConn)
        End Sub

        Protected Overrides Function CrieConexao(ByVal StringDeConexao As String) As IDbConnection
            Dim Conexao As IDbConnection = New MySqlConnection(StringDeConexao)
            Conexao.Open()
            Return Conexao
        End Function

        Protected Overrides Function CrieDataAdapter(ByVal Comando As IDbCommand) As DbDataAdapter
            Return New MySqlDataAdapter(CType(Comando, MySqlCommand))
        End Function

    End Class

End Namespace