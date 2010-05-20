Imports Compartilhados
Imports Estoque.Interfaces.Negocio
Imports Estoque.Interfaces.Mapeadores
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces

Public Class MapeadorDeGrupoDeProduto
    Implements IMapeadorDeGrupoDeProduto

    Public Sub Atualizar(ByVal GrupoDeProdutos As IGrupoDeProduto) Implements IMapeadorDeGrupoDeProduto.Atualizar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE ETQ_GRPPRODUTO SET ")
        Sql.Append(String.Concat("NOME = '", UtilidadesDePersistencia.FiltraApostrofe(GrupoDeProdutos.Nome), "', "))
        Sql.Append(String.Concat("PRCCOMISSAO = ", UtilidadesDePersistencia.TPVd(GrupoDeProdutos.PorcentagemDeComissao)))
        Sql.Append(String.Concat(" WHERE ID = ", GrupoDeProdutos.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Inserir(ByVal GrupoDeProdutos As IGrupoDeProduto) Implements IMapeadorDeGrupoDeProduto.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        GrupoDeProdutos.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO ETQ_GRPPRODUTO (")
        Sql.Append("ID, NOME, PRCCOMISSAO)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(GrupoDeProdutos.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(GrupoDeProdutos.Nome), "', "))
        Sql.Append(String.Concat(UtilidadesDePersistencia.TPVd(GrupoDeProdutos.PorcentagemDeComissao), ")"))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaGrupoDeProdutos(ByVal ID As Long) As IGrupoDeProduto Implements IMapeadorDeGrupoDeProduto.ObtenhaGrupoDeProdutos
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME, PRCCOMISSAO")
        Sql.Append(" FROM ETQ_GRPPRODUTO")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        Return ObtenhaGruposDeProdutos(Sql.ToString, 1)(0)
    End Function

    Private Function ObtenhaGruposDeProdutos(ByVal SQL As String, _
                                             ByVal QuantidadeDeRegistros As Integer) As IList(Of IGrupoDeProduto)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Grupos As IList(Of IGrupoDeProduto)
        Dim Grupo As IGrupoDeProduto

        Grupos = New List(Of IGrupoDeProduto)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            While Leitor.Read AndAlso Grupos.Count < QuantidadeDeRegistros
                Grupo = FabricaGenerica.GetInstancia.CrieObjeto(Of IGrupoDeProduto)()
                Grupo.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Grupo.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")
                Grupo.PorcentagemDeComissao = UtilidadesDePersistencia.getValorDouble(Leitor, "PRCCOMISSAO")

                Grupos.Add(Grupo)
            End While
        End Using

        Return Grupos
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IMapeadorDeGrupoDeProduto.Remover
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM ETQ_GRPPRODUTO")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaGruposDeProdutosPorNome(ByVal Nome As String, _
                                                   ByVal QuantidadeDeRegistros As Integer) As IList(Of IGrupoDeProduto) Implements IMapeadorDeGrupoDeProduto.ObtenhaGruposDeProdutosPorNome
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME, PRCCOMISSAO")
        Sql.Append(" FROM ETQ_GRPPRODUTO")

        If Not String.IsNullOrEmpty(Nome) Then
            Sql.Append(String.Concat(" WHERE NOME LIKE '", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'"))
        End If

        Return ObtenhaGruposDeProdutos(Sql.ToString, QuantidadeDeRegistros)
    End Function

End Class