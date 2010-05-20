Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Compartilhados.DBHelper
Imports System.Text
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces

Public Class MapeadorDeGrupo
    Implements IMapeadorDeGrupo

    Public Sub Excluir(ByVal Id As Long) Implements IMapeadorDeGrupo.Excluir
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("DELETE FROM NCL_GRUPO")
        SQL.Append(String.Concat(" WHERE ID = ", Id))

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Function Existe(ByVal Grupo As IGrupo) As Boolean Implements IMapeadorDeGrupo.Existe
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper

        SQL.Append("SELECT COUNT(*) AS QUANTIDADE FROM NCL_GRUPO")
        SQL.Append(" WHERE NOME = '")
        SQL.Append(UtilidadesDePersistencia.FiltraApostrofe(Grupo.Nome))
        SQL.Append("'")

        If Not Grupo.ID Is Nothing Then
            SQL.Append(" AND ID <> ")
            SQL.Append(Grupo.ID)
        End If

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            If Leitor.Read Then
                Return UtilidadesDePersistencia.getValorInteger(Leitor, "QUANTIDADE") <> 0
            End If
        End Using
    End Function

    Public Sub Inserir(ByVal Grupo As IGrupo) Implements IMapeadorDeGrupo.Inserir
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        Grupo.ID = GeradorDeID.getInstancia.getProximoID

        SQL.Append("INSERT INTO NCL_GRUPO (ID, NOME, STATUS)")
        SQL.Append(" VALUES ( ")
        SQL.Append(String.Concat(Grupo.ID, ", "))
        SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Grupo.Nome), "', "))
        SQL.Append(String.Concat("'", Grupo.Status.ID, "')"))

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Sub Modificar(ByVal Grupo As IGrupo) Implements IMapeadorDeGrupo.Modificar
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("UPDATE NCL_GRUPO SET ")
        SQL.Append(String.Concat("NOME = '", UtilidadesDePersistencia.FiltraApostrofe(Grupo.Nome), "', "))
        SQL.Append(String.Concat("STATUS = '", Grupo.Status.ID, "'"))
        SQL.Append(" WHERE ")
        SQL.Append(String.Concat("ID = ", Grupo.ID))

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Function ObtenhaGrupo(ByVal Id As Long) As IGrupo Implements IMapeadorDeGrupo.ObtenhaGrupo
        Dim SQL As New StringBuilder

        SQL = ObtenhaSQL()
        SQL.Append(String.Concat(" WHERE ID = ", Id))
        Return ObtenhaGrupo(SQL)
    End Function

    Private Function ObtenhaGrupo(ByVal SQL As StringBuilder) As IGrupo
        Dim Grupos As IList(Of IGrupo)
        Dim Grupo As IGrupo = Nothing

        Grupos = ObtenhaGrupos(SQL, Integer.MaxValue)

        If Not Grupos.Count = 0 Then
            Grupo = Grupos.Item(0)
        End If

        Return Grupo
    End Function

    Public Function ObtenhaGruposPorNomeComoFiltro(ByVal Nome As String, _
                                                   ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IGrupo) Implements IMapeadorDeGrupo.ObtenhaGruposPorNomeComoFiltro
        Dim SQL As New StringBuilder

        SQL = ObtenhaSQL()

        If Not String.IsNullOrEmpty(Nome) Then
            SQL.Append(String.Concat(" WHERE NOME LIKE '", UtilidadesDePersistencia.FiltraApostrofe(Nome).ToUpper, "%'"))
        End If

        Return ObtenhaGrupos(SQL, QuantidadeMaximaDeRegistros)
    End Function

    Private Function ObtenhaGrupos(ByVal SQL As StringBuilder, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IGrupo)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Grupos As IList(Of IGrupo)
        Dim Grupo As IGrupo

        Grupos = New List(Of IGrupo)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            While Leitor.Read AndAlso Grupos.Count < QuantidadeMaximaDeRegistros
                Grupo = FabricaGenerica.GetInstancia.CrieObjeto(Of IGrupo)()
                Grupo.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Grupo.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")
                Grupo.Status = StatusDoGrupo.ObtenhaStatus(UtilidadesDePersistencia.getValorChar(Leitor, "STATUS"))

                Grupos.Add(Grupo)
            End While
        End Using

        Return Grupos
    End Function

    Private Function ObtenhaSQL() As StringBuilder
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ID, NOME, STATUS")
        SQL.Append(" FROM NCL_GRUPO")

        Return SQL
    End Function

    'TODO: implementar
    Public Function EstaSendoUtilizado(ByVal Id As Long) As Boolean Implements IMapeadorDeGrupo.EstaSendoUtilizado
        Return False
    End Function

End Class