Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Fabricas

Public Class MapeadorDeTipoDeEndereco
    Implements IMapeadorDeTipoDeEndereco

    Private Function ObtenhaSQL() As StringBuilder
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ID, NOME")
        SQL.Append(" FROM NCL_TIPO_ENDERECO")

        Return SQL
    End Function

    Public Sub Insira(ByVal Tipo As ITipoDeEndereco) Implements IMapeadorDeTipoDeEndereco.Insira
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        Tipo.ID = GeradorDeID.getInstancia.getProximoID

        SQL.Append("INSERT INTO NCL_TIPO_ENDERECO (ID, NOME)")
        SQL.Append(" VALUES ( ")
        SQL.Append(String.Concat(Tipo.ID, ", "))
        SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Tipo.Nome), "')"))

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Sub Modificar(ByVal Tipo As ITipoDeEndereco) Implements IMapeadorDeTipoDeEndereco.Modificar
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("UPDATE NCL_TIPO_ENDERECO SET ")
        SQL.Append(String.Concat("NOME = '", UtilidadesDePersistencia.FiltraApostrofe(Tipo.Nome), "' "))
        SQL.Append(" WHERE ")
        SQL.Append(String.Concat("ID = ", Tipo.ID))

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Function Obtenha(ByVal ID As Long) As ITipoDeEndereco Implements IMapeadorDeTipoDeEndereco.Obtenha
        Dim SQL As New StringBuilder

        SQL = ObtenhaSQL()
        SQL.Append(String.Concat(" WHERE ID = ", ID))
        Return ObtenhaGrupos(SQL, Integer.MaxValue)(0)
    End Function

    Private Function ObtenhaGrupos(ByVal SQL As StringBuilder, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of ITipoDeEndereco)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Tipos As IList(Of ITipoDeEndereco)
        Dim Tipo As ITipoDeEndereco

        Tipos = New List(Of ITipoDeEndereco)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString, QuantidadeMaximaDeRegistros)
            Try
                While Leitor.Read
                    Tipo = FabricaGenerica.GetInstancia.CrieObjeto(Of ITipoDeEndereco)()
                    Tipo.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                    Tipo.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

                    Tipos.Add(Tipo)
                End While
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Tipos
    End Function

    Public Function ObtenhaPorNome(ByVal Filtro As String, ByVal Quantidade As Integer) As IList(Of ITipoDeEndereco) Implements IMapeadorDeTipoDeEndereco.ObtenhaPorNome
        Dim SQL As New StringBuilder

        SQL = ObtenhaSQL()

        If Not String.IsNullOrEmpty(Filtro) Then
            SQL.Append(String.Concat(" WHERE NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(Filtro), "%'"))
        End If

        Return ObtenhaGrupos(SQL, Quantidade)
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IMapeadorDeTipoDeEndereco.Remover
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("DELETE FROM NCL_TIPO_ENDERECO")
        SQL.Append(String.Concat(" WHERE ID = ", ID))

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

End Class
