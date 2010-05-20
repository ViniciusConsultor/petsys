Imports System.Data.SqlClient
Imports System.Data.Common

Namespace DBHelper

    Friend Class SQLServerHelper
        Inherits AbstractDBHelper

        Public Sub New(ByVal StringDeConexao As String)
            MyBase.New(StringDeConexao)
        End Sub

        Protected Overrides Function CrieConexao(ByVal StringDeConexao As String) As IDbConnection
            Dim Conexao As IDbConnection = New SqlConnection(StringDeConexao)
            Conexao.Open()
            Return Conexao
        End Function

        Protected Overrides Function CrieDataAdapter(ByVal Comando As IDbCommand) As DbDataAdapter
            Return New SqlDataAdapter(CType(Comando, SqlCommand))
        End Function

    End Class

End Namespace