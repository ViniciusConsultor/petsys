Imports System.Data.OleDb

Namespace DBHelper

    Friend Class OleDbHelper
        Inherits AbstractDBHelper

        Public Sub New(ByVal sStrConn As String)
            MyBase.New(sStrConn)
        End Sub

        Protected Overrides Function crieConexao(ByVal sStrConn As String) As System.Data.IDbConnection
            Dim cConexao As IDbConnection = New OleDbConnection(sStrConn)
            cConexao.Open()
            Return cConexao
        End Function

        Protected Overrides Function crieDataAdapter(ByVal pComando As System.Data.IDbCommand) As System.Data.Common.DbDataAdapter
            Return New OleDbDataAdapter(CType(pComando, OleDbCommand))
        End Function

    End Class

End Namespace