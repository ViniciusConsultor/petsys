Imports PetSys.Interfaces.Mapeadores
Imports PetSys.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Public Class MapeadorDeVermifugos
    Implements IMapeadorDeVermifugos

    Public Sub Excluir(ByVal ID As Long) Implements IMapeadorDeVermifugos.Excluir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM PET_VERMIFUGO")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Inserir(ByRef Vermifugo As IVermifugo) Implements IMapeadorDeVermifugos.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        Vermifugo.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO PET_VERMIFUGO (")
        Sql.Append("ID, IDANIMAL, NOME, DATA, OBSERVACAO, ")
        Sql.Append("PROXDOSEEM, IDVETERINARIORESP) ")
        Sql.Append("VALUES (")
        Sql.Append(String.Concat(Vermifugo.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(Vermifugo.AnimalQueRecebeu.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Vermifugo.Nome), "', "))
        Sql.Append(String.Concat(Vermifugo.Data.ToString("yyyyMMdd"), ", "))

        If Not String.IsNullOrEmpty(Vermifugo.Observacao) Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Vermifugo.Observacao), "', "))
        Else
            Sql.Append("NULL, ")
        End If

        If Vermifugo.ProximaDoseEm.HasValue Then
            Sql.Append(String.Concat(Vermifugo.ProximaDoseEm.Value.ToString("yyyyMMdd"), ", "))
        Else
            Sql.Append("NULL, ")
        End If

        Sql.Append(String.Concat(Vermifugo.VeterinarioQueReceitou.Pessoa.ID, ")"))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modificar(ByVal Vermifugo As IVermifugo) Implements IMapeadorDeVermifugos.Modificar

    End Sub

    Public Function ObtenhaVermifugo(ByVal ID As Long) As IVermifugo Implements IMapeadorDeVermifugos.ObtenhaVermifugo
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, IDANIMAL, NOME, DATA, OBSERVACAO, PROXDOSEEM, IDVETERINARIORESP ")
        Sql.Append("FROM PET_VERMIFUGO ")
        Sql.Append(String.Concat("WHERE ID = ", ID.ToString))

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Vermifugo As IVermifugo = Nothing

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Vermifugo = Me.MontaObjetoVermifugo(Leitor)
            End If
        End Using

        Return Vermifugo
    End Function

    Public Function ObtenhaVermifugosDoAnimal(ByVal IdAnimal As Long) As IList(Of IVermifugo) Implements IMapeadorDeVermifugos.ObtenhaVermifugosDoAnimal
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, IDANIMAL, NOME, DATA, OBSERVACAO, PROXDOSEEM, IDVETERINARIORESP ")
        Sql.Append("FROM PET_VERMIFUGO ")
        Sql.Append(String.Concat("WHERE IDANIMAL = ", IdAnimal.ToString))
        Sql.Append(" ORDER BY DATA")

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Vermifugos As IList(Of IVermifugo) = New List(Of IVermifugo)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Vermifugos.Add(Me.MontaObjetoVermifugo(Leitor))
            End While
        End Using

        Return Vermifugos
    End Function

    Private Function MontaObjetoVermifugo(ByVal Leitor As IDataReader) As IVermifugo
        Dim Vermifugo As IVermifugo

        Vermifugo = FabricaGenerica.GetInstancia.CrieObjeto(Of IVermifugo)()
        Vermifugo.AnimalQueRecebeu = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IAnimal)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDANIMAL"))
        Vermifugo.Data = UtilidadesDePersistencia.getValorDate(Leitor, "DATA").Value
        Vermifugo.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
        Vermifugo.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "OBSERVACAO") Then
            Vermifugo.Observacao = UtilidadesDePersistencia.GetValorString(Leitor, "OBSERVACAO")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "PROXDOSEEM") Then
            Vermifugo.ProximaDoseEm = UtilidadesDePersistencia.getValorDate(Leitor, "PROXDOSEEM").Value
        End If

        Vermifugo.VeterinarioQueReceitou = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IVeterinario)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDVETERINARIORESP"))
        Return Vermifugo
    End Function

    Public Function ObtenhaVermifugos(ByVal IDs As IList(Of Long)) As IList(Of IVermifugo) Implements IMapeadorDeVermifugos.ObtenhaVermifugos
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, IDANIMAL, NOME, DATA, OBSERVACAO, PROXDOSEEM, IDVETERINARIORESP ")
        Sql.Append("FROM PET_VERMIFUGO ")
        Sql.Append("WHERE ID IN (")

        Dim IDsSTR As New StringBuilder

        For Each ID As Long In IDs
            IDsSTR.Append(String.Concat(ID.ToString, ","))
        Next

        IDsSTR = IDsSTR.Remove(IDsSTR.Length - 1, 1)

        Sql.Append(String.Concat(IDsSTR.ToString, ")"))

        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Vermifugos As IList(Of IVermifugo) = New List(Of IVermifugo)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Vermifugos.Add(Me.MontaObjetoVermifugo(Leitor))
            End While
        End Using

        Return Vermifugos
    End Function
End Class
