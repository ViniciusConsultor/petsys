Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Fabricas

Public Class MapeadorDeAgenda
    Implements IMapeadorDeAgenda

    Public Sub Insira(ByVal Agenda As IAgenda) Implements IMapeadorDeAgenda.Insira
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO NCL_AGENDA (")
        Sql.Append("IDPESSOA, HORAINICO, HORAFIM)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Agenda.Pessoa.ID.ToString, ", "))
        Sql.Append(String.Concat(Agenda.HorarioDeInicio.ToString("HHmm"), ", "))
        Sql.Append(String.Concat(Agenda.HorarioDeTermino.ToString("HHmm"), ") "))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modifique(ByVal Agenda As IAgenda) Implements IMapeadorDeAgenda.Modifique

    End Sub

    Public Function ObtenhaAgenda(ByVal Pessoa As IPessoa) As IAgenda Implements IMapeadorDeAgenda.ObtenhaAgenda
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Agenda As IAgenda = Nothing

        Sql.Append("SELECT HORAINICO, HORAFIM FROM NCL_AGENDA WHERE ")
        Sql.Append(String.Concat("IDPESSOA = ", Pessoa.ID.Value.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Return MontaObjeto(Leitor, Pessoa)
            End If
        End Using

        Return Nothing
    End Function

    Private Function MontaObjeto(ByVal Leitor As IDataReader, ByVal Pessoa As IPessoa) As IAgenda
        Dim Agenda As IAgenda

        Agenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IAgenda)()
        Agenda.HorarioDeInicio = UtilidadesDePersistencia.getValorHourMinute(Leitor, "HORAINICO").Value
        Agenda.HorarioDeTermino = UtilidadesDePersistencia.getValorHourMinute(Leitor, "HORAFIM").Value

        Return Agenda
    End Function

    Public Sub Remova(ByVal ID As Long) Implements IMapeadorDeAgenda.Remova
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM NCL_AGENDA")
        Sql.Append(" WHERE IDPESSOA = " & ID.ToString)

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaAgenda(ByVal IDPessoa As Long) As IAgenda Implements IMapeadorDeAgenda.ObtenhaAgenda
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Agenda As IAgenda = Nothing

        Sql.Append(" SELECT HORAINICO, HORAFIM, NOME, ID FROM NCL_AGENDA, NCL_PESSOA WHERE ")
        Sql.Append(String.Concat("IDPESSOA = ", IDPessoa.ToString))
        Sql.Append(" AND ID = IDPESSOA")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Dim Pessoa As IPessoa
                Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()
                Pessoa.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")
                Return MontaObjeto(Leitor, Pessoa)
            End If
        End Using

        Return Nothing
    End Function
End Class