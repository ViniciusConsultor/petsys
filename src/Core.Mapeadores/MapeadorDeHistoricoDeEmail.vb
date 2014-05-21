Imports Compartilhados
Imports Core.Interfaces.Mapeadores
Imports Core.Interfaces.Negocio
Imports Compartilhados.DBHelper
Imports System.Text
Imports Compartilhados.Interfaces
Imports System.IO
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas

Public Class MapeadorDeHistoricoDeEmail
    Implements IMapeadorDeHistoricoDeEmail
    
    Private Sub GravaAnexos(ByVal IdHistorico As Long, ByVal Anexos As IDictionary(Of String, Stream)) 
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

    Public Sub Grave(Historico As IHistoricoDeEmail, Anexos As IDictionary(Of String, Stream)) Implements IMapeadorDeHistoricoDeEmail.Grave
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper = Nothing
        Dim PossuiAnexo As Boolean = False

        If Not Anexos Is Nothing AndAlso Not Anexos.Count = 0 Then PossuiAnexo = True

        DBHelper = ServerUtils.getDBHelper
        Historico.ID = GeradorDeID.ProximoID()

        Sql.Append("INSERT INTO NCL_HISTORICOEMAIL (ID, DATA, POSSUIANEXO, ASSUNTO, REMETENTE, MENSAGEM, DESTINATARIOSCC, DESTINATARIOSCCO, CONTEXTO) ")
        Sql.Append("VALUES (")
        Sql.Append(Historico.ID.Value.ToString() & ", ")
        Sql.Append("'" & Historico.Data.ToString("yyyyMMddHHmmss") & "', ")
        Sql.Append("'" & IIf(PossuiAnexo, "S", "N").ToString() & "', ")
        
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

        If PossuiAnexo Then GravaAnexos(Historico.ID.Value, Anexos)
    End Sub

    Public Function ObtenhaAnexos(IdHistorico As Long) As IDictionary(Of String, StreamReader) Implements IMapeadorDeHistoricoDeEmail.ObtenhaAnexos
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.criarNovoDbHelper()
        Dim sql = New StringBuilder()

        sql.Append("SELECT IDHISTORICO, NOMEANEXO, INDICE, BINARIOANEXO ")
        sql.Append("FROM NCL_HISTORICOEMAILANEXO ")
        sql.Append("WHERE IDHISTORICO = " & IdHistorico)
        sql.AppendLine(" ORDER BY INDICE")

        Dim Anexos As IDictionary(Of String, StreamReader) = New Dictionary(Of String, StreamReader)()

        Using Leitor As IDataReader = DBHelper.obtenhaReader(sql.ToString())
            While Leitor.Read()
                Anexos.Add(UtilidadesDePersistencia.GetValorString(Leitor, "NOMEANEXO"),
                           UtilidadesDeStream.TransformeArrayBytesEmStream(UtilidadesDePersistencia.GetValorArrayBytes(Leitor, "BINARIOANEXO")))
            End While

        End Using

        Return Anexos
    End Function

    Public Function ObtenhaHistoricos(ByVal Filtro As IFiltro, ByVal QuantidadeDeItens As Integer, ByVal OffSet As Integer) As IList(Of IHistoricoDeEmail) Implements IMapeadorDeHistoricoDeEmail.ObtenhaHistoricos
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.criarNovoDbHelper()
        Dim sql = New StringBuilder()

        sql.Append(Filtro.ObtenhaQuery())
        sql.AppendLine(" ORDER BY DATA DESC")

        Dim Historicos As IList(Of IHistoricoDeEmail) = New List(Of IHistoricoDeEmail)()

        Using Leitor As IDataReader = DBHelper.obtenhaReader(sql.ToString(), QuantidadeDeItens, OffSet)
            Try
                While Leitor.Read()
                    Historicos.Add(ObtenhaHistorico(Leitor))
                End While
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Historicos
    End Function

    Public Function ObtenhaQuantidadeDeHistoricoDeEmails(ByVal Filtro As IFiltro) As Integer Implements IMapeadorDeHistoricoDeEmail.ObtenhaQuantidadeDeHistoricoDeEmails
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.criarNovoDbHelper()
        Dim sql = New StringBuilder()

        sql.Append(Filtro.ObtenhaQuery())
        
        Using Leitor As IDataReader = DBHelper.obtenhaReader(Filtro.ObtenhaQueryParaQuantidade())
            Try
                If Leitor.Read() Then Return UtilidadesDePersistencia.getValorInteger(Leitor, "QUANTIDADE")
            Finally
                Leitor.Close()
            End Try
        End Using

        Return 0
    End Function

    Private Function ObtenhaHistorico(Leitor As IDataReader) As IHistoricoDeEmail
        Dim Historico As IHistoricoDeEmail = FabricaGenerica.GetInstancia().CrieObjeto(Of IHistoricoDeEmail)()

        With Historico
            .ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
            .Data = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "DATA").Value
            .PossuiAnexo = UtilidadesDePersistencia.GetValorBooleano(Leitor, "POSSUIANEXO")

            If Not UtilidadesDePersistencia.EhNulo(Leitor, "ASSUNTO") Then
                .Assunto = UtilidadesDePersistencia.GetValorString(Leitor, "ASSUNTO")
            End If

            .Remetente = UtilidadesDePersistencia.GetValorString(Leitor, "REMETENTE")

            If Not UtilidadesDePersistencia.EhNulo(Leitor, "MENSAGEM") Then
                .Mensagem = UtilidadesDePersistencia.GetValorString(Leitor, "MENSAGEM")
            End If

            .DestinatariosEmCopia = UtilidadesDePersistencia.MapeieStringParaListaDeString(UtilidadesDePersistencia.GetValorString(Leitor, "DESTINATARIOSCC"), "|"c)

            If Not UtilidadesDePersistencia.EhNulo(Leitor, "DESTINATARIOSCCO") Then
                .DestinatariosEmCopiaOculta = UtilidadesDePersistencia.MapeieStringParaListaDeString(UtilidadesDePersistencia.GetValorString(Leitor, "DESTINATARIOSCCO"), "|"c)
            End If

            If Not UtilidadesDePersistencia.EhNulo(Leitor, "CONTEXTO") Then
                .Contexto = UtilidadesDePersistencia.GetValorString(Leitor, "CONTEXTO")
            End If
            
        End With

        Return Historico
    End Function

End Class
