Imports Estoque.Interfaces.Mapeadores
Imports Estoque.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Fabricas

Public Class MapeadorDeProduto
    Implements IMapeadorDeProduto

    Public Sub AtualizarMarcaDeProduto(ByVal Marca As IMarcaDeProduto) Implements IMapeadorDeProduto.AtualizarMarcaDeProduto
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE ETQ_MARCAPRODUTO SET ")
        Sql.Append(String.Concat("NOME = '", UtilidadesDePersistencia.FiltraApostrofe(Marca.Nome), "'"))
        Sql.Append(String.Concat(" WHERE ID = ", Marca.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub InserirMarcaDeProduto(ByVal Marca As IMarcaDeProduto) Implements IMapeadorDeProduto.InserirMarcaDeProduto
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Marca.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO ETQ_MARCAPRODUTO (")
        Sql.Append("ID, NOME)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Marca.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Marca.Nome), "')"))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaMarcaDeProduto(ByVal ID As Long) As IMarcaDeProduto Implements IMapeadorDeProduto.ObtenhaMarcaDeProduto

        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME")
        Sql.Append(" FROM ETQ_MARCAPRODUTO")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        Return ObtenhaMarcasDePodutos(Sql.ToString, 1)(0)
    End Function

    Private Function ObtenhaMarcasDePodutos(ByVal SQL As String, _
                                            ByVal QuantidadeDeRegistros As Integer) As IList(Of IMarcaDeProduto)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Marcas As IList(Of IMarcaDeProduto)
        Dim Marca As IMarcaDeProduto

        Marcas = New List(Of IMarcaDeProduto)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            While Leitor.Read AndAlso Marcas.Count < QuantidadeDeRegistros
                Marca = FabricaGenerica.GetInstancia.CrieObjeto(Of IMarcaDeProduto)()
                Marca.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Marca.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

                Marcas.Add(Marca)
            End While
        End Using

        Return Marcas
    End Function

    Public Function ObtenhaMarcasDeProdutosPorNome(ByVal Nome As String, _
                                                   ByVal QuantidadeDeRegistros As Integer) As IList(Of IMarcaDeProduto) Implements IMapeadorDeProduto.ObtenhaMarcasDeProdutosPorNome
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME")
        Sql.Append(" FROM ETQ_MARCAPRODUTO")

        If Not String.IsNullOrEmpty(Nome) Then
            Sql.Append(String.Concat(" WHERE NOME LIKE '", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'"))
        End If

        Return ObtenhaMarcasDePodutos(Sql.ToString, QuantidadeDeRegistros)
    End Function

    Public Sub RemoverMarcaDeProduto(ByVal ID As Long) Implements IMapeadorDeProduto.RemoverMarcaDeProduto
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM ETQ_MARCAPRODUTO")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

End Class