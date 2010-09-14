Imports System.Data.SQLite
Imports System.Data.Common
Imports System.Text

Namespace DBHelper

    Public Class SQLiteHelper
        Inherits AbstractDBHelper

        Public Sub New(ByVal sStrConn As String)
            MyBase.New(sStrConn)
        End Sub

        Protected Overrides Function CrieConexao(ByVal StringDeConexao As String) As IDbConnection
            Dim cConexao As IDbConnection = New SQLiteConnection(StringDeConexao)
            cConexao.Open()
            Return cConexao
            Return Nothing
        End Function

        Protected Overrides Function CrieDataAdapter(ByVal Comando As IDbCommand) As DbDataAdapter
            Return New SQLiteDataAdapter(CType(Comando, SQLiteCommand))
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

    End Class

End Namespace