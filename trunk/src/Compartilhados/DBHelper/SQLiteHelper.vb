'Imports System.Data.SQLite
Imports System.Data.Common

Namespace DBHelper

    Public Class SQLiteHelper
        Inherits AbstractDBHelper

        Public Sub New(ByVal sStrConn As String)
            MyBase.New(sStrConn)
        End Sub

        Protected Overrides Function CrieConexao(ByVal StringDeConexao As String) As IDbConnection
            '    Dim cConexao As IDbConnection = New SQLiteConnection(StringDeConexao)
            '    cConexao.Open()
            '    Return cConexao
            Return Nothing
        End Function

        Protected Overrides Function CrieDataAdapter(ByVal Comando As IDbCommand) As DbDataAdapter
            'Return New SQLiteDataAdapter(CType(Comando, SQLiteCommand))
            Return Nothing
        End Function

    End Class

End Namespace