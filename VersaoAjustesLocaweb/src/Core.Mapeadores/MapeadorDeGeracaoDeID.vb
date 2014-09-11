Imports Core.Interfaces.Mapeadores
Imports Compartilhados
Imports Compartilhados.DBHelper

Public Class MapeadorDeGeracaoDeID
    Implements IMapeadorDeGeracaoDeID

    Public Sub ArmazeneNumeroHigh(ByVal NumeroHigh As Integer) Implements IMapeadorDeGeracaoDeID.ArmazeneNumeroHigh
        Dim Sql As String
        Dim DBHelper As IDBHelper = Nothing

        DBHelper = ServerUtils.getDBHelper

        Sql = "UPDATE NCL_IDENTIFICADOR SET NUMHIGH = " & NumeroHigh.ToString

        DBHelper.ExecuteNonQuery(Sql)
    End Sub

    Public Function ObtenhaNumeroHigh() As Integer Implements IMapeadorDeGeracaoDeID.ObtenhaNumeroHigh
        Dim Sql As String
        Dim DBHelper As IDBHelper
        Dim NumeroHigh As Integer = 0

        Sql = "SELECT NUMHIGH FROM NCL_IDENTIFICADOR"

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql)
            Try
                If Leitor.Read Then
                    NumeroHigh = UtilidadesDePersistencia.getValorInteger(Leitor, "NUMHIGH")
                Else
                    Me.CrieNumeroHigh()
                End If
            Finally
                Leitor.Close()
            End Try
        End Using

        Return NumeroHigh
    End Function

    Private Sub CrieNumeroHigh()
        Dim Sql As String
        Dim DBHelper As IDBHelper = Nothing

        Sql = "INSERT INTO NCL_IDENTIFICADOR (NUMHIGH) VALUES (0) "

        DBHelper = ServerUtils.criarNovoDbHelper
        DBHelper.ExecuteNonQuery(Sql)
    End Sub

End Class