﻿Imports System.Data.Odbc
Imports System.Data.Common

Namespace DBHelper

    Friend Class ODBCHelper
        Inherits AbstractDBHelper
        
        Public Sub New(ByVal sStrConn As String, ByVal SistemaUtilizaSQLUpperCase As Boolean)
            MyBase.New(sStrConn, SistemaUtilizaSQLUpperCase)
        End Sub

        Protected Overrides Function crieConexao(ByVal sStrConn As String) As IDbConnection
            Dim cConexao As IDbConnection = New OdbcConnection(sStrConn)
            cConexao.Open()
            Return cConexao
        End Function

        Protected Overrides Function crieDataAdapter(ByVal pComando As System.Data.IDbCommand) As DbDataAdapter
            Return New OdbcDataAdapter(CType(pComando, OdbcCommand))
        End Function

        Public Overrides Function SuporteALimite() As Boolean
            Return False
        End Function

        'TODO: fazer
        Public Overrides Function ObtenhaMensagemDaExcecaoLancada(ByVal Ex As Exception) As String
            Return Nothing
        End Function

        Public Overrides Function ObtenhaCaracterDoComandoPreparado() As String
            Return "?"
        End Function

        Public Overrides Function SuporteAOffSet() As Boolean
            Return False
        End Function

    End Class

End Namespace