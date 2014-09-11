Imports System.Data.Common
Imports System.Text

Namespace DBHelper

    Public Class OracleHelper
        Inherits AbstractDBHelper
        
        Public Sub New(ByVal sStrConn As String, ByVal SistemaUtilizaSQLUpperCase As Boolean)
            MyBase.New(sStrConn, SistemaUtilizaSQLUpperCase)
        End Sub

        Protected Overrides Function crieConexao(ByVal sStrConn As String) As IDbConnection
            Dim Fabrica As DbProviderFactory = ObtenhaFabricaProviderOracle()
            Dim cConexao As IDbConnection = Fabrica.CreateConnection()
            cConexao.ConnectionString = sStrConn
            cConexao.Open()
            Return cConexao
        End Function

        Protected Overrides Function crieDataAdapter(ByVal pComando As IDbCommand) As DbDataAdapter
            Dim Fabrica As DbProviderFactory = ObtenhaFabricaProviderOracle()
            Dim Adaptador As DbDataAdapter = Fabrica.CreateDataAdapter
            Adaptador.SelectCommand = CType(pComando, DbCommand)
            Return Adaptador
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
            'If TypeOf (Ex) Is OracleException Then
            '    Return MensagensOracle.GetInstancia.ObtenhaMensagemDeErro(CType(Ex, OracleException).Code.ToString)
            'End If

            Return Nothing
        End Function

        Public Overrides Function ObtenhaCaracterDoComandoPreparado() As String
            Return ":"
        End Function

        Private Function ObtenhaFabricaProviderOracle() As DbProviderFactory
            Try
                Return DbProviderFactories.GetFactory("Oracle.DataAccess.Client")
            Catch ex As Exception
                Throw New ApplicationException("ODP.NET Oracle Data Provider for .Net não encontrado.")
            End Try

        End Function

        Public Overrides Function SuporteAOffSet() As Boolean
            Return True
        End Function

        Public Overrides Function ObtenhaQueryComLimiteEOffset(QueryOriginal As String, QuantidadeDeRegistros As Integer, OffSet As Integer) As String
            Dim QueryComLimite As StringBuilder = New StringBuilder

            If QueryOriginal.ToLower().IndexOf("order") <= 0 Then
                Throw New Exception("Para utilizar offset é necessário ter uma ordenação na query.")
            End If
            
            With QueryComLimite
                .Append("SELECT * FROM (")
                .Append("SELECT a.*, ROWNUM rnum FROM (")
                .Append(QueryOriginal)
                .Append(") a WHERE ROWNUM <= " & QuantidadeDeRegistros)
                .Append(") WHERE rnum >= " & OffSet)
            End With

            Return QueryComLimite.ToString()
        End Function

    End Class

End Namespace