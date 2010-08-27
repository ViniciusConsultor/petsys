﻿Imports System.Data.Odbc

Namespace DBHelper

    Friend Class ODBCHelper
        Inherits AbstractDBHelper

        Public Sub New(ByVal sStrConn As String)
            MyBase.New(sStrConn)
        End Sub

        Protected Overrides Function crieConexao(ByVal sStrConn As String) As System.Data.IDbConnection
            Dim cConexao As IDbConnection = New OdbcConnection(sStrConn)
            cConexao.Open()
            Return cConexao
        End Function

        Protected Overrides Function crieDataAdapter(ByVal pComando As System.Data.IDbCommand) As System.Data.Common.DbDataAdapter
            Return New OdbcDataAdapter(CType(pComando, OdbcCommand))
        End Function

    End Class

End Namespace