Imports System.Data.Common
Imports System.Text
Imports System.Reflection
Imports System.IO

Namespace DBHelper

    Public Class SQLiteHelper
        Inherits AbstractDBHelper
        
        Private _DllSQLLite As Assembly

        Public Sub New(ByVal sStrConn As String, ByVal SistemaUtilizaSQLUpperCase As Boolean)
            MyBase.New(sStrConn, SistemaUtilizaSQLUpperCase)
            CarregueAssemblyCorreto()
        End Sub

        Private Sub CarregueAssemblyCorreto()

            If Not _DllSQLLite Is Nothing Then Exit Sub

            Dim CaminhoBase = Path.Combine(Util.ObtenhaCaminhoDaPastaDoServidorDeAplicacao(), "SQLite")

            If IntPtr.Size = 8 Then
                ' 64 bits 
                CaminhoBase = Path.Combine(CaminhoBase, "x64")
            ElseIf IntPtr.Size = 4 Then
                '32 bits
                CaminhoBase = Path.Combine(CaminhoBase, "x86")
            Else
                Throw New Exception("Não foi possível encontrar o arquivo SQLite.dll para a plataforma corrente.")
            End If
            _DllSQLLite = Assembly.LoadFile(Path.Combine(CaminhoBase, "System.Data.SQLite.DLL"))
        End Sub


        Protected Overrides Function CrieConexao(ByVal StringDeConexao As String) As IDbConnection
            Dim cConexao As IDbConnection = DirectCast(_DllSQLLite.CreateInstance("System.Data.SQLite.SQLiteConnection", True), IDbConnection)
            cConexao.ConnectionString = StringDeConexao
            cConexao.Open()
            Return cConexao
        End Function

        Private Function ObtenhaSQLiteFactory() As DbProviderFactory
            Return DirectCast(_DllSQLLite.CreateInstance("System.Data.SQLite.SQLiteFactory", True), DbProviderFactory)
        End Function


        Protected Overrides Function CrieDataAdapter(ByVal Comando As IDbCommand) As DbDataAdapter
            Dim adaptador As DbDataAdapter = ObtenhaSQLiteFactory().CreateDataAdapter()

            adaptador.SelectCommand = CType(Comando, DbCommand)
            Return adaptador
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

        Public Overrides Function ObtenhaMensagemDaExcecaoLancada(ByVal Ex As System.Exception) As String
            Return Nothing
        End Function

        Public Overrides Function ObtenhaCaracterDoComandoPreparado() As String
            Throw New NotImplementedException()
        End Function

        Public Overrides Function SuporteAOffSet() As Boolean
            Return False
        End Function

    End Class

End Namespace