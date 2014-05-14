Imports Compartilhados
Imports Core.Interfaces.Mapeadores
Imports Core.Interfaces.Negocio
Imports Compartilhados.DBHelper
Imports System.Text
Imports Compartilhados.Interfaces
Imports System.IO

Public Class MapeadorDeHistoricoDeEmail
    Implements IMapeadorDeHistoricoDeEmail
    
    Public Sub GravaAnexos(ByVal IdHistorico As Long, ByVal Anexos As IDictionary(Of String,Stream)) Implements IMapeadorDeHistoricoDeEmail.GravaAnexos
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper = Nothing
        DBHelper = ServerUtils.getDBHelper

        Dim CaracterComandoPreparado As String = DBHelper.ObtenhaCaracterDoComandoPreparado()
        
        Sql.Append("INSERT INTO NCL_HISTORICOEMAILANEXO (IDHISTORICO, NOMEANEXO, INDICE, BINARIOANEXO) VALUES (")
        Sql.Append(CaracterComandoPreparado & "IDHISTORICO, " & CaracterComandoPreparado & "NOMEANEXO, " & CaracterComandoPreparado & "INDICE, " & CaracterComandoPreparado & "BINARIOANEXO)")

        Dim Indice As Integer = 0

        For Each Anexo As KeyValuePair(Of String, Stream) In Anexos
            Using Comando As IDbCommand = DBHelper.ObtenhaConexaoPadrao().CreateCommand()
                Comando.CommandText = Sql.ToString()

                CrieParametro(Comando, "IDHISTORICO", DbType.Int64, IdHistorico)
                CrieParametro(Comando, "NOMEANEXO", DbType.AnsiString, Anexo.Key)
                CrieParametro(Comando, "INDICE", DbType.Int32, Indice)
                CrieParametro(Comando, "BINARIOANEXO", DbType.Binary, UtilidadesDeStream.TransformaStreamEmArrayDeBytes(Anexo.Value))

                Comando.ExecuteNonQuery()
                Indice += 1
            End Using
        Next

    End Sub

    Private Sub CrieParametro(Comando As IDbCommand, Nome As String, TipoDeDado As DbType, Valor As Object)
        Dim Parametro As IDbDataParameter = Comando.CreateParameter()

        Parametro.DbType = TipoDeDado
        Parametro.ParameterName = Nome
        Parametro.Value = Valor
        Comando.Parameters.Add(Parametro)
    End Sub

    Public Sub Exclua(IdHistorico As Long) Implements IMapeadorDeHistoricoDeEmail.Exclua
        Dim DBHelper As IDBHelper = Nothing

        DBHelper = ServerUtils.getDBHelper

        DBHelper.ExecuteNonQuery("DELETE FROM NCL_HISTORICOEMAILANEXO WHERE IDHISTORICO = " & IdHistorico)
        DBHelper.ExecuteNonQuery("DELETE FROM NCL_HISTORICOEMAIL WHERE ID = " & IdHistorico)
    End Sub

    Public Sub Grave(Historico As IHistoricoDeEmail) Implements IMapeadorDeHistoricoDeEmail.Grave
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper = Nothing

        DBHelper = ServerUtils.getDBHelper
        Historico.ID = GeradorDeID.ProximoID()

        Sql.Append("INSERT INTO NCL_HISTORICOEMAIL (ID, ASSUNTO, REMETENTE, MENSAGEM, DESTINATARIOSCC, DESTINATARIOSCCO, CONTEXTO) ")
        Sql.Append("VALUES (")
        Sql.Append(Historico.ID.Value.ToString() & ", ")

        If String.IsNullOrEmpty(Historico.Assunto) Then
            Sql.Append("NULL , ")
        Else
            Sql.Append("'" & UtilidadesDePersistencia.FiltraApostrofe(Historico.Assunto) & "', ")
        End If

        Sql.Append("'" & UtilidadesDePersistencia.FiltraApostrofe(Historico.Remetente) & "', ")

        If String.IsNullOrEmpty(Historico.Mensagem) Then
            Sql.Append("NULL , ")
        Else
            Sql.Append("'" & UtilidadesDePersistencia.FiltraApostrofe(Historico.Mensagem) & "', ")
        End If

        Sql.Append("'" & UtilidadesDePersistencia.ObtenhaStringMapeadaDeListaDeString(Historico.DestinatariosEmCopia, "|"c) & "', ")

        If Historico.DestinatariosEmCopiaOculta Is Nothing OrElse Historico.DestinatariosEmCopiaOculta.Count = 0 Then
            Sql.Append("NULL , ")
        Else
            Sql.Append("'" & UtilidadesDePersistencia.ObtenhaStringMapeadaDeListaDeString(Historico.DestinatariosEmCopiaOculta, "|"c) & "', ")
        End If

        If String.IsNullOrEmpty(Historico.Contexto) Then
            Sql.Append("NULL")
        Else
            Sql.Append("'" & UtilidadesDePersistencia.FiltraApostrofe(Historico.Contexto) & "'")
        End If

        Sql.Append(")")

        DBHelper.ExecuteNonQuery(Sql.ToString())
    End Sub

    Public Function ObtenhaAnexos(IdHistorico As Long) As IDictionary(Of String, IO.Stream) Implements IMapeadorDeHistoricoDeEmail.ObtenhaAnexos
        Return Nothing
    End Function

End Class
