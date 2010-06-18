Imports LotoFacil.Interfaces.Mapeadores
Imports LotoFacil.Interfaces.Negocio
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports System.Text
Imports Compartilhados.Interfaces

Public Class MapeadorDeAposta
    Implements IMapeadorDeAposta

    Private Const SQL_APOSTA As String = "SELECT ID, NOME, NUMCONCURSO, DTCONCURSO, INDICE, DEZENAS FROM LTF_APOSTA, LTF_JOGO, LTF_DEZENA" & _
                                         " WHERE LTF_JOGO.IDAPOSTA = ID" & _
                                         " AND LTF_DEZENA.IDAPOSTA = LTF_JOGO.IDAPOSTA" & _
                                         " AND LTF_DEZENA.IDJOGO = LTF_JOGO.IDJOGO "
    Private Const SQL_ORDER_BY As String = " ORDER BY ID,INDICE"

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

            Jogo.ID = GeradorDeID.getInstancia.getProximoID

            Sql.Append("INSERT INTO LTF_JOGO (")
            Sql.Append("IDAPOSTA, IDJOGO)")
            Sql.Append(" VALUES (")
            Sql.Append(String.Concat(Aposta.ID.Value.ToString, ", "))
            Sql.Append(String.Concat(Jogo.ID.Value.ToString, ")"))
            DBHelper.ExecuteNonQuery(Sql.ToString)

            Sql = New StringBuilder
            Sql.Append("INSERT INTO LTF_DEZENA (")
            Sql.Append("IDAPOSTA, IDJOGO, INDICE, DEZENAS)")
            Sql.Append(" VALUES (")
            Sql.Append(String.Concat(Aposta.ID.Value.ToString, ", "))
            Sql.Append(String.Concat(Jogo.ID.Value.ToString, ", "))
            Sql.Append(String.Concat(Aposta.Jogos.IndexOf(Jogo), ", "))
            Sql.Append(String.Concat("'", Jogo.DezenasToString, "')"))
            DBHelper.ExecuteNonQuery(Sql.ToString)
        Next
    End Sub

    Public Function ObtenhaAposta(ByVal NumeroDoConcurso As Integer) As IAposta Implements IMapeadorDeAposta.ObtenhaAposta
        Dim Sql As New StringBuilder
        Dim Apostas As IList(Of IAposta)

        Sql.Append(SQL_APOSTA)
        Sql.Append(" AND NUMCONCURSO = " & NumeroDoConcurso.ToString)
        Sql.Append(SQL_ORDER_BY)

        Apostas = Me.ObtenhaApostasInterno(Sql.ToString, 1)

        If Apostas.Count = 0 Then Return Nothing

        Return Apostas.Item(0)
    End Function

    Private Function ObtenhaApostasInterno(ByVal Sql As String, _
                                           ByVal QuantidadeDeItens As Integer) As IList(Of IAposta)
        Dim DBHelper As IDBHelper
        Dim Apostas As IList(Of IAposta)
        Dim Aposta As IAposta = Nothing

        Apostas = New List(Of IAposta)
        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read AndAlso Apostas.Count < QuantidadeDeItens
                Dim ID As Long

                ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")

                If Aposta Is Nothing OrElse Aposta.ID <> ID Then
                    Dim Concurso As IConcurso

                    Concurso = FabricaDeConcurso.CrieObjeto(UtilidadesDePersistencia.getValorInteger(Leitor, "NUMCONCURSO"), _
                                                            UtilidadesDePersistencia.getValorDate(Leitor, "DTCONCURSO").Value)

                    Aposta = FabricaDeAposta.CrieObjeto(Concurso)
                    Aposta.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                    Aposta.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")
                    Apostas.Add(Aposta)
                End If

                Dim Jogo As IJogo = FabricaDeJogo.CrieObjeto()
                Jogo.AdicionaDezenas(UtilidadesDePersistencia.GetValorString(Leitor, "DEZENAS"))
                Aposta.AdicionaJogo(Jogo)
            End While
        End Using

        Return Apostas
    End Function

    Public Function ObtenhaAposta(ByVal ID As Long) As IAposta Implements IMapeadorDeAposta.ObtenhaAposta
        Dim Sql As New StringBuilder
        Dim Apostas As IList(Of IAposta)

        Sql.Append(SQL_APOSTA)
        Sql.Append(" AND ID = " & ID.ToString)
        Sql.Append(SQL_ORDER_BY)

        Apostas = Me.ObtenhaApostasInterno(Sql.ToString, 1)

        If Apostas.Count = 0 Then Return Nothing

        Return Apostas.Item(0)
    End Function

    Public Function ObtenhaApostas(ByVal NomeAposta As String, _
                                   ByVal QuantidadeDeItens As Integer) As IList(Of IAposta) Implements IMapeadorDeAposta.ObtenhaApostas
        Dim Sql As New StringBuilder

        Sql.Append(SQL_APOSTA)

        If Not String.IsNullOrEmpty(NomeAposta) Then
            Sql.Append(" AND NOME LIKE '" & UtilidadesDePersistencia.FiltraApostrofe(NomeAposta).ToUpper & "%'")
        End If

        Sql.Append(SQL_ORDER_BY)

        Return Me.ObtenhaApostasInterno(Sql.ToString, QuantidadeDeItens)
    End Function

End Class