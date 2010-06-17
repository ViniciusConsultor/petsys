Imports LotoFacil.Interfaces.Mapeadores
Imports LotoFacil.Interfaces.Negocio
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports System.Text
Imports Compartilhados.Interfaces

Public Class MapeadorDeAposta
    Implements IMapeadorDeAposta

    Public Sub GraveAposta(ByVal Aposta As IAposta) Implements IMapeadorDeAposta.GraveAposta
        Dim DBHelper As IDBHelper
        Dim Sql As New StringBuilder

        DBHelper = ServerUtils.getDBHelper
        Aposta.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO LTF_APOSTA (")
        Sql.Append("ID, NOME, NUMCONCURSO, DTCONCURSO)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Aposta.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("'", Aposta.Nome, "', "))
        Sql.Append(String.Concat(Aposta.Concurso.Numero, ", "))
        Sql.Append(String.Concat(Aposta.Concurso.Data.ToString("yyyyMMdd"), ")"))

        DBHelper.ExecuteNonQuery(Sql.ToString)

        For Each Jogo As IJogo In Aposta.Jogos
            Sql = New StringBuilder

            Sql.Append("INSERT INTO LTF_JOGO (")
            Sql.Append("IDAPOSTA, INDICE, DEZENAS)")
            Sql.Append(" VALUES (")
            Sql.Append(String.Concat(Aposta.ID.Value.ToString, ", "))
            Sql.Append(String.Concat(Aposta.Jogos.IndexOf(Jogo), ", "))
            Sql.Append(String.Concat("'", Jogo.DezenasToString, "')"))
            DBHelper.ExecuteNonQuery(Sql.ToString)
        Next
    End Sub

End Class