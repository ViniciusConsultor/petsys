Imports MySql.Data.MySqlClient
Imports System.Data.Common
Imports System.Text

Namespace DBHelper

    Friend Class MySQLHelper
        Inherits AbstractDBHelper
        
        Public Sub New(ByVal sStrConn As String, ByVal SistemaUtilizaSQLUpperCase As Boolean)
            MyBase.New(sStrConn, SistemaUtilizaSQLUpperCase)
        End Sub

        Protected Overrides Function CrieConexao(ByVal StringDeConexao As String) As IDbConnection
            Dim Conexao As IDbConnection = New MySqlConnection(StringDeConexao)
            Conexao.Open()
            Return Conexao
        End Function

        Protected Overrides Function CrieDataAdapter(ByVal Comando As IDbCommand) As DbDataAdapter
            Return New MySqlDataAdapter(CType(Comando, MySqlCommand))
        End Function

        Public Overrides Function SuporteALimite() As Boolean
            Return True
        End Function

        Public Overrides Function ObtenhaQueryComLimite(ByVal QueryOriginal As String, ByVal QuantidadeDeRegistros As Integer) As String
            Dim QueryComLimite As New StringBuilder

            QueryComLimite.Append(QueryOriginal)
            QueryComLimite.Append(" limit ")
            QueryComLimite.Append(QuantidadeDeRegistros.ToString)

            Return QueryComLimite.ToString
        End Function

        'TODO: fazer
        Public Overrides Function ObtenhaMensagemDaExcecaoLancada(ByVal Ex As System.Exception) As String
            Return Nothing
        End Function

        Public Overrides Function SuporteAOffSet() As Boolean
            Return False
        End Function

    End Class

End Namespace