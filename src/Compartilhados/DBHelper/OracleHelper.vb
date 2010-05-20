Imports System.Data.OracleClient
Imports System.Data.Common

Namespace DBHelper

    Public Class OracleHelper
        Inherits AbstractDBHelper

        Public Sub New(ByVal sStrConn As String)
            MyBase.New(sStrConn)
        End Sub

        Protected Overrides Function crieConexao(ByVal sStrConn As String) As System.Data.IDbConnection
            Dim cConexao As IDbConnection = New OracleConnection(sStrConn)
            cConexao.Open()
            Return cConexao
        End Function

        Protected Overrides Function crieDataAdapter(ByVal pComando As System.Data.IDbCommand) As System.Data.Common.DbDataAdapter
            Return New OracleDataAdapter(CType(pComando, OracleCommand))
        End Function

    End Class

End Namespace