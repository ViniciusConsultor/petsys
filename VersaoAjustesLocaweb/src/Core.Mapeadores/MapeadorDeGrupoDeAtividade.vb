Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Fabricas

Public Class MapeadorDeGrupoDeAtividade
    Implements IMapeadorDeGrupoDeAtividade

    Private Function ObtenhaSQL() As StringBuilder
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ID, NOME")
        SQL.Append(" FROM NCL_GRUPO_DE_ATIVIDADE")

        Return SQL
    End Function

    Public Sub Insira(ByVal GrupoDeAtividade As IGrupoDeAtividade) Implements IMapeadorDeGrupoDeAtividade.Insira
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        GrupoDeAtividade.ID = GeradorDeID.getInstancia.getProximoID

        SQL.Append("INSERT INTO NCL_GRUPO_DE_ATIVIDADE (ID, NOME)")
        SQL.Append(" VALUES ( ")
        SQL.Append(String.Concat(GrupoDeAtividade.ID, ", "))
        SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(GrupoDeAtividade.Nome), "')"))

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Sub Modificar(ByVal GrupoDeAtividade As IGrupoDeAtividade) Implements IMapeadorDeGrupoDeAtividade.Modificar
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("UPDATE NCL_GRUPO_DE_ATIVIDADE SET ")
        SQL.Append(String.Concat("NOME = '", UtilidadesDePersistencia.FiltraApostrofe(GrupoDeAtividade.Nome), "' "))
        SQL.Append(" WHERE ")
        SQL.Append(String.Concat("ID = ", GrupoDeAtividade.ID))

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Function Obtenha(ByVal ID As Long) As IGrupoDeAtividade Implements IMapeadorDeGrupoDeAtividade.Obtenha
        Dim SQL As New StringBuilder

        SQL = ObtenhaSQL()
        SQL.Append(String.Concat(" WHERE ID = ", ID))
        Return ObtenhaGrupos(SQL, Integer.MaxValue)(0)
    End Function

    Private Function ObtenhaGrupos(ByVal SQL As StringBuilder, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IGrupoDeAtividade)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Grupos As IList(Of IGrupoDeAtividade)
        Dim Grupo As IGrupoDeAtividade

        Grupos = New List(Of IGrupoDeAtividade)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString, QuantidadeMaximaDeRegistros)
            Try
                While Leitor.Read
                    Grupo = FabricaGenerica.GetInstancia.CrieObjeto(Of IGrupoDeAtividade)()
                    Grupo.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                    Grupo.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

                    Grupos.Add(Grupo)
                End While
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Grupos
    End Function

    Public Function ObtenhaPorNome(ByVal Filtro As String, ByVal Quantidade As Integer) As IList(Of IGrupoDeAtividade) Implements IMapeadorDeGrupoDeAtividade.ObtenhaPorNome
        Dim SQL As New StringBuilder

        SQL = ObtenhaSQL()

        If Not String.IsNullOrEmpty(Filtro) Then
            SQL.Append(String.Concat(" WHERE NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(Filtro), "%'"))
        End If

        Return ObtenhaGrupos(SQL, Quantidade)
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IMapeadorDeGrupoDeAtividade.Remover
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("DELETE FROM NCL_GRUPO_DE_ATIVIDADE")
        SQL.Append(String.Concat(" WHERE ID = ", ID))

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

End Class
