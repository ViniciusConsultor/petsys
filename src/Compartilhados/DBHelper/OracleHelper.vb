Imports System.Data.OracleClient
Imports System.Data.Common
Imports System.Text

Namespace DBHelper

    Public Class OracleHelper
        Inherits AbstractDBHelper

        Public Sub New(ByVal sStrConn As String, ByVal SistemaUtilizaSQLUpperCase As Boolean)
            MyBase.New(sStrConn, SistemaUtilizaSQLUpperCase)
        End Sub

        Protected Overrides Function crieConexao(ByVal sStrConn As String) As IDbConnection
            Dim cConexao As IDbConnection = New OracleConnection(sStrConn)
            cConexao.Open()
            Return cConexao
        End Function

        Protected Overrides Function crieDataAdapter(ByVal pComando As IDbCommand) As DbDataAdapter
            Return New OracleDataAdapter(CType(pComando, OracleCommand))
        End Function

        Public Overrides Function SuporteALimite() As Boolean
            Return True
        End Function

        Public Overrides Function ObtenhaQueryComLimite(ByVal QueryOriginal As String, _
                                                        ByVal QuantidadeDeRegistros As Integer) As String
            Dim QueryComLimite As New StringBuilder

            QueryComLimite.Append("SELECT * FROM ( ")
            QueryComLimite.Append(QueryOriginal)
            QueryComLimite.Append(" ) WHERE ROWNUM <= ")
            QueryComLimite.Append(QuantidadeDeRegistros.ToString)

            Return QueryComLimite.ToString
        End Function

        Public Overrides Function ObtenhaMensagemDaExcecaoLancada(ByVal Ex As System.Exception) As String
            If TypeOf (Ex) Is OracleException Then
                Return MensagensOracle.GetInstancia.ObtenhaMensagemDeErro(CType(Ex, OracleException).Code.ToString)
            End If

            Return Nothing
        End Function
    End Class

End Namespace