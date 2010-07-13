Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces

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
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_AGENDA SET ")
        Sql.Append(String.Concat("HORAINICO = ", Agenda.HorarioDeInicio.ToString("HHmm"), ", "))
        Sql.Append(String.Concat("HORAFIM = ", Agenda.HorarioDeTermino.ToString("HHmm")))
        Sql.Append(" WHERE IDPESSOA = " & Agenda.Pessoa.ID.Value.ToString)
        DBHelper.ExecuteNonQuery(Sql.ToString)
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

    Public Sub InsiraCompromisso(ByVal Compromisso As ICompromisso) Implements IMapeadorDeAgenda.InsiraCompromisso
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        Compromisso.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO NCL_COMPROMISSO (")
        Sql.Append("ID, IDPESSOA, INICIO, FIM, ASSUNTO, LOCAL, DESCRICAO)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Compromisso.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(Compromisso.Proprietario.ID.ToString, ", "))
        Sql.Append(String.Concat(Compromisso.Inicio.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat(Compromisso.Fim.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Compromisso.Assunto), "', "))

        If Not String.IsNullOrEmpty(Compromisso.Local) Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Compromisso.Local), "', "))
        Else
            Sql.Append("NULL, ")
        End If

        If Not String.IsNullOrEmpty(Compromisso.Descricao) Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Compromisso.Descricao), "')"))
        Else
            Sql.Append("NULL)")
        End If

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub ModifiqueCompromisso(ByVal Compromisso As ICompromisso) Implements IMapeadorDeAgenda.ModifiqueCompromisso
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        Compromisso.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("UPDATE NCL_COMPROMISSO SET ")
        Sql.Append(String.Concat(" INICIO = ", Compromisso.Inicio.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat(" FIM = ", Compromisso.Fim.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat(" ASSUNTO = '", UtilidadesDePersistencia.FiltraApostrofe(Compromisso.Assunto), "', "))

        If Not String.IsNullOrEmpty(Compromisso.Local) Then
            Sql.Append(String.Concat(" LOCAL = '", UtilidadesDePersistencia.FiltraApostrofe(Compromisso.Local), "', "))
        Else
            Sql.Append(" LOCAL = NULL, ")
        End If

        If Not String.IsNullOrEmpty(Compromisso.Descricao) Then
            Sql.Append(String.Concat(" DESCRICAO = '", UtilidadesDePersistencia.FiltraApostrofe(Compromisso.Descricao), "')"))
        Else
            Sql.Append(" DESCRICAO = NULL)")
        End If

        Sql.Append(String.Concat(" WHERE ID = ", Compromisso.ID.Value.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaCompromisso(ByVal ID As Long) As ICompromisso Implements IMapeadorDeAgenda.ObtenhaCompromisso
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        Sql.Append(" SELECT ID, IDPESSOA, INICIO, FIM, ASSUNTO, LOCAL, DESCRICAO FROM NCL_COMPROMISSO WHERE ")
        Sql.Append(String.Concat("ID = ", ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Return MontaObjetoCompromisso(Leitor)
            End If
        End Using

        Return Nothing
    End Function

    Public Function ObtenhaCompromissos(ByVal IDProprietario As Long) As IList(Of ICompromisso) Implements IMapeadorDeAgenda.ObtenhaCompromissos
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Compromissos As IList(Of ICompromisso) = New List(Of ICompromisso)

        Sql.Append(" SELECT ID, IDPESSOA, INICIO, FIM, ASSUNTO, LOCAL, DESCRICAO FROM NCL_COMPROMISSO WHERE ")
        Sql.Append(String.Concat("IDPESSOA = ", IDProprietario.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Compromissos.Add(MontaObjetoCompromisso(Leitor))
            End While
        End Using

        Return Compromissos
    End Function

    Private Function MontaObjetoCompromisso(ByVal Leitor As IDataReader) As ICompromisso
        Dim Compromisso As ICompromisso

        Compromisso = FabricaGenerica.GetInstancia.CrieObjeto(Of ICompromisso)()
        Compromisso.Assunto = UtilidadesDePersistencia.GetValorString(Leitor, "ASSUNTO")

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "DESCRICAO") Then
            Compromisso.Descricao = UtilidadesDePersistencia.GetValorString(Leitor, "DESCRICAO")
        End If

        Compromisso.Fim = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "FIM").Value
        Compromisso.Inicio = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "INICIO").Value

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "LOCAL") Then
            Compromisso.Local = UtilidadesDePersistencia.GetValorString(Leitor, "LOCAL")
        End If

        Compromisso.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")

        Compromisso.Proprietario = FabricaDePessoaFisicaLazyLoad.Crie(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA"))

        Return Compromisso
    End Function

    Public Sub RemovaCompromisso(ByVal ID As Long) Implements IMapeadorDeAgenda.RemovaCompromisso
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM NCL_COMPROMISSO")
        Sql.Append(" WHERE ID = " & ID.ToString)

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

End Class