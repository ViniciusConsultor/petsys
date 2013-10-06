Imports Compartilhados
Imports Core.Interfaces
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados.Interfaces
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Mapeadores

Public Class MapeadorDePais
    Implements IMapeadorDePais
    
    Public Sub Excluir(ByVal Id As Long) Implements IMapeadorDePais.Excluir
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("DELETE FROM NCL_PAIS")
        SQL.Append(String.Concat(" WHERE ID = ", Id))

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Sub Inserir(ByVal Pais As IPais) Implements IMapeadorDePais.Inserir
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        Pais.ID = GeradorDeID.getInstancia.getProximoID

        SQL.Append("INSERT INTO NCL_PAIS (ID, NOME, SIGLA)")
        SQL.Append(" VALUES ( ")
        SQL.Append(String.Concat(Pais.ID, ", "))
        SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pais.Nome), "', "))

        If Not String.IsNullOrEmpty(Pais.Sigla) Then
            SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Pais.Sigla), "'"))
        Else
            SQL.Append("NULL")
        End If

        SQL.Append(")")

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Sub Modificar(ByVal Pais As IPais) Implements IMapeadorDePais.Modificar
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("UPDATE NCL_PAIS SET ")
        SQL.Append(String.Concat("NOME = '", UtilidadesDePersistencia.FiltraApostrofe(Pais.Nome), "', "))
        SQL.Append(String.Concat("SIGLA = ", UtilidadesDePersistencia.FiltraApostrofe(Pais.Sigla)))
        

        SQL.Append(" WHERE ")
        SQL.Append(String.Concat("ID = ", Pais.ID))

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Function ObtenhaPais(ByVal Id As Long) As IPais Implements IMapeadorDePais.ObtenhaPais
        Dim SQL As New StringBuilder

        SQL = ObtenhaSQL()
        SQL.Append(String.Concat(" WHERE ID = ", Id))
        Return ObtenhaPais(SQL)
    End Function

    Private Function ObtenhaPais(ByVal SQL As StringBuilder) As IPais
        Dim Paises As IList(Of IPais)
        Dim Pais As IPais = Nothing

        Paises = ObtenhaPaises(SQL, Integer.MaxValue)

        If Not Paises.Count = 0 Then
            Pais = Paises.Item(0)
        End If

        Return Pais
    End Function

    Private Function ObtenhaPaises(ByVal SQL As StringBuilder, ByVal QuantidadeMaxima As Integer) As IList(Of IPais)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Paises As IList(Of IPais)
        Dim Pais As IPais

        Paises = New List(Of IPais)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString, QuantidadeMaxima)
            Try
                While Leitor.Read
                    Pais = FabricaGenerica.GetInstancia.CrieObjeto(Of IPais)()
                    Pais.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                    Pais.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")
                    
                    If Not IsDBNull(Leitor("SIGLA")) Then
                        Pais.Sigla = UtilidadesDePersistencia.GetValorString(Leitor, "SIGLA")
                    End If

                    Paises.Add(Pais)
                End While
            Finally
                Leitor.Close()
            End Try

        End Using

        Return Paises
    End Function

    Private Function ObtenhaSQL() As StringBuilder
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ID, NOME, SIGLA")
        SQL.Append(" FROM NCL_PAIS")

        Return SQL
    End Function

    Public Function ObtenhaPaisesPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPais) Implements IMapeadorDePais.ObtenhaPaisesPorNomeComoFiltro
        Dim SQL As New StringBuilder

        SQL = ObtenhaSQL()

        If Not String.IsNullOrEmpty(Nome) Then
            SQL.Append(String.Concat(" WHERE NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'"))
        End If

        SQL.Append(" ORDER BY NOME")
        Return ObtenhaPaises(SQL, QuantidadeMaximaDeRegistros)
    End Function

End Class
