Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Text

Namespace DBHelper

    Friend Class SQLServerHelper
        Inherits AbstractDBHelper

        Public Sub New(ByVal sStrConn As String, ByVal SistemaUtilizaSQLUpperCase As Boolean)
            MyBase.New(sStrConn, SistemaUtilizaSQLUpperCase)
        End Sub

        Protected Overrides Function CrieConexao(ByVal StringDeConexao As String) As IDbConnection
            Dim Conexao As IDbConnection = New SqlConnection(StringDeConexao)
            Conexao.Open()
            Return Conexao
        End Function

        Protected Overrides Function CrieDataAdapter(ByVal Comando As IDbCommand) As DbDataAdapter
            Return New SqlDataAdapter(CType(Comando, SqlCommand))
        End Function

        Public Overrides Function SuporteALimite() As Boolean
            Return True
        End Function

        Public Overrides Function ObtenhaQueryComLimite(ByVal QueryOriginal As String, _
                                                        ByVal QuantidadeDeRegistros As Integer) As String
            Dim QueryComLimite As StringBuilder = New StringBuilder

            QueryComLimite.Append("SELECT TOP ")
            QueryComLimite.Append(QuantidadeDeRegistros.ToString)
            QueryComLimite.Append(" * FROM ( ")
            QueryComLimite.Append(ObtenhaParteSelectFromWhere(QueryOriginal))
            QueryComLimite.Append(") TABELAAUX ")
            QueryComLimite.Append(ObtenhaParteOrderBy(QueryOriginal))

            Return QueryComLimite.ToString
        End Function

        Private Function ObtenhaParteOrderBy(ByVal querySqlString As String) As String
            Dim IndiceOrderBy As Integer = ObtenhaIndiceParteOrderBy(querySqlString)

            If IndiceOrderBy > 0 Then
                Return querySqlString.ToString().Remove(0, IndiceOrderBy)
            End If

            Return String.Empty
        End Function

        Private Function ObtenhaIndiceParteOrderBy(ByVal querySqlString As String) As Integer
            Dim IndiceOrderBy As Integer

            IndiceOrderBy = querySqlString.LastIndexOf("order", StringComparison.InvariantCultureIgnoreCase)
            Return IndiceOrderBy
        End Function

        Private Function ObtenhaParteSelectFromWhere(ByVal querySqlString As String) As String
            Dim IndiceOrderBy As Integer = ObtenhaIndiceParteOrderBy(querySqlString)

            If IndiceOrderBy > 0 Then
                Return querySqlString.ToString().Remove(IndiceOrderBy)
            End If

            Return querySqlString
        End Function

    End Class

End Namespace