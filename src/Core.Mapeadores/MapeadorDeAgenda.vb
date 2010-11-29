﻿Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Public Class MapeadorDeAgenda
    Implements IMapeadorDeAgenda

    Private Sub InsiraConfiguracao(ByVal ConfiguracaoDaAgenda As IConfiguracaoDeAgendaDoUsuario)
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO NCL_CNFAGENDAUSU (")
        Sql.Append("IDPESSOA, HORAINICO, HORAFIM, INTERVALO, IDPESSOAPADRAO)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(ConfiguracaoDaAgenda.Pessoa.ID.ToString, ", "))
        Sql.Append(String.Concat(ConfiguracaoDaAgenda.HorarioDeInicio.ToString("HHmm"), ", "))
        Sql.Append(String.Concat(ConfiguracaoDaAgenda.HorarioDeTermino.ToString("HHmm"), ", "))
        Sql.Append(String.Concat(ConfiguracaoDaAgenda.IntervaloEntreOsCompromissos.ToString("HHmm"), ", "))
        Sql.Append(String.Concat(ConfiguracaoDaAgenda.PessoaPadraoAoAcessarAAgenda.ID.ToString, ")"))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Private Function MontaObjeto(ByVal Leitor As IDataReader, ByVal Pessoa As IPessoa) As IConfiguracaoDeAgendaDoUsuario
        Dim Configuracao As IConfiguracaoDeAgendaDoUsuario

        Configuracao = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDeAgendaDoUsuario)()
        Configuracao.HorarioDeInicio = UtilidadesDePersistencia.getValorHourMinute(Leitor, "HORAINICO").Value
        Configuracao.HorarioDeTermino = UtilidadesDePersistencia.getValorHourMinute(Leitor, "HORAFIM").Value
        Configuracao.IntervaloEntreOsCompromissos = UtilidadesDePersistencia.getValorHourMinute(Leitor, "INTERVALO").Value
        Configuracao.PessoaPadraoAoAcessarAAgenda = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOAPADRAO"))
        Configuracao.Pessoa = Pessoa
        Return Configuracao
    End Function

    Public Function InsiraCompromisso(ByVal Compromisso As ICompromisso) As Long Implements IMapeadorDeAgenda.InsiraCompromisso
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        Compromisso.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO NCL_COMPROMISSO (")
        Sql.Append("ID, IDPESSOA, INICIO, FIM, ASSUNTO, LOCAL, DESCRICAO, STATUS)")
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
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Compromisso.Descricao), "', "))
        Else
            Sql.Append("NULL, ")
        End If

        Sql.Append(String.Concat("'", Compromisso.Status.ID.ToString, "')"))

        DBHelper.ExecuteNonQuery(Sql.ToString)
        Return Compromisso.ID.Value
    End Function

    Public Sub ModifiqueCompromisso(ByVal Compromisso As ICompromisso) Implements IMapeadorDeAgenda.ModifiqueCompromisso
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_COMPROMISSO SET ")
        Sql.Append(String.Concat(" INICIO = ", Compromisso.Inicio.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat(" FIM = ", Compromisso.Fim.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat(" ASSUNTO = '", UtilidadesDePersistencia.FiltraApostrofe(Compromisso.Assunto), "', "))
        Sql.Append(String.Concat(" STATUS = '", Compromisso.Status.ID.ToString, "', "))


        If Not String.IsNullOrEmpty(Compromisso.Local) Then
            Sql.Append(String.Concat(" LOCAL = '", UtilidadesDePersistencia.FiltraApostrofe(Compromisso.Local), "', "))
        Else
            Sql.Append(" LOCAL = NULL, ")
        End If

        If Not String.IsNullOrEmpty(Compromisso.Descricao) Then
            Sql.Append(String.Concat(" DESCRICAO = '", UtilidadesDePersistencia.FiltraApostrofe(Compromisso.Descricao), "'"))
        Else
            Sql.Append(" DESCRICAO = NULL")
        End If

        Sql.Append(String.Concat(" WHERE ID = ", Compromisso.ID.Value.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaCompromisso(ByVal ID As Long) As ICompromisso Implements IMapeadorDeAgenda.ObtenhaCompromisso
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        Sql.Append(" SELECT ID, IDPESSOA, INICIO, FIM, ASSUNTO, LOCAL, DESCRICAO, STATUS FROM NCL_COMPROMISSO WHERE ")
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

        Sql.Append(" SELECT ID, IDPESSOA, INICIO, FIM, ASSUNTO, LOCAL, DESCRICAO, STATUS FROM NCL_COMPROMISSO WHERE ")
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

        Compromisso.Proprietario = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA"))

        Compromisso.Status = StatusDoCompromisso.Obtenha(UtilidadesDePersistencia.getValorChar(Leitor, "STATUS"))

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

    Public Function InsiraTarefa(ByVal Tarefa As ITarefa) As Long Implements IMapeadorDeAgenda.InsiraTarefa
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        Tarefa.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO NCL_TAREFA (")
        Sql.Append("ID, IDPESSOA, INICIO, FIM, ASSUNTO, PRIORIDADE, STATUS, DESCRICAO)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Tarefa.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(Tarefa.Proprietario.ID.ToString, ", "))
        Sql.Append(String.Concat(Tarefa.DataDeInicio.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat(Tarefa.DataDeConclusao.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Tarefa.Assunto), "', "))
        Sql.Append(String.Concat("'", Tarefa.Prioridade.ID.ToString, "', "))
        Sql.Append(String.Concat("'", Tarefa.Status.ID.ToString, "', "))

        If Not String.IsNullOrEmpty(Tarefa.Descricao) Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Tarefa.Descricao), "')"))
        Else
            Sql.Append("NULL)")
        End If

        DBHelper.ExecuteNonQuery(Sql.ToString)
        Return Tarefa.ID.Value
    End Function

    Public Sub ModifiqueTarefa(ByVal Tarefa As ITarefa) Implements IMapeadorDeAgenda.ModifiqueTarefa
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_COMPROMISSO SET ")
        Sql.Append(String.Concat(" INICIO = ", Tarefa.DataDeInicio.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat(" FIM = ", Tarefa.DataDeConclusao.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat(" ASSUNTO = '", UtilidadesDePersistencia.FiltraApostrofe(Tarefa.Assunto), "', "))
        Sql.Append(String.Concat(" PRIORIDADE = '", Tarefa.Prioridade.ID.ToString, "', "))
        Sql.Append(String.Concat(" STATUS = '", Tarefa.Status.ID.ToString, "', "))

        If Not String.IsNullOrEmpty(Tarefa.Descricao) Then
            Sql.Append(String.Concat(" DESCRICAO = '", UtilidadesDePersistencia.FiltraApostrofe(Tarefa.Descricao), "'"))
        Else
            Sql.Append(" DESCRICAO = NULL")
        End If

        Sql.Append(String.Concat(" WHERE ID = ", Tarefa.ID.Value.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Private Function MontaObjetoTarefa(ByVal Leitor As IDataReader) As ITarefa
        Dim Tarefa As ITarefa

        Tarefa = FabricaGenerica.GetInstancia.CrieObjeto(Of ITarefa)()
        Tarefa.Assunto = UtilidadesDePersistencia.GetValorString(Leitor, "ASSUNTO")

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "DESCRICAO") Then
            Tarefa.Descricao = UtilidadesDePersistencia.GetValorString(Leitor, "DESCRICAO")
        End If

        Tarefa.DataDeConclusao = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "FIM").Value
        Tarefa.DataDeInicio = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "INICIO").Value
        Tarefa.Prioridade = PrioridadeDaTarefa.Obtenha(UtilidadesDePersistencia.getValorChar(Leitor, "PRIORIDADE"))
        Tarefa.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
        Tarefa.Proprietario = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA"))
        Tarefa.Status = StatusDaTarefa.Obtenha(UtilidadesDePersistencia.getValorChar(Leitor, "STATUS"))

        Return Tarefa
    End Function

    Public Function ObtenhaTarefa(ByVal ID As Long) As ITarefa Implements IMapeadorDeAgenda.ObtenhaTarefa
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        Sql.Append(" SELECT ID, IDPESSOA, INICIO, FIM, ASSUNTO, PRIORIDADE, DESCRICAO, STATUS FROM NCL_TAREFA WHERE ")
        Sql.Append(String.Concat("ID = ", ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Return MontaObjetoTarefa(Leitor)
            End If
        End Using

        Return Nothing
    End Function

    Public Function ObtenhaTarefas(ByVal IDProprietario As Long) As IList(Of ITarefa) Implements IMapeadorDeAgenda.ObtenhaTarefas
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Tarefas As IList(Of ITarefa) = New List(Of ITarefa)

        Sql.Append(" SELECT ID, IDPESSOA, INICIO, FIM, ASSUNTO, PRIORIDADE, DESCRICAO, STATUS FROM NCL_TAREFA WHERE ")
        Sql.Append(String.Concat("IDPESSOA = ", IDProprietario.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Tarefas.Add(MontaObjetoTarefa(Leitor))
            End While
        End Using

        Return Tarefas
    End Function

    Public Sub RemovaTarefa(ByVal ID As Long) Implements IMapeadorDeAgenda.RemovaTarefa
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM NCL_TAREFA")
        Sql.Append(" WHERE ID = " & ID.ToString)

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaCompromissos(ByVal IDProprietario As Long, _
                                        ByVal DataInicio As Date, _
                                        ByVal DataFim As Date?) As IList(Of ICompromisso) Implements IMapeadorDeAgenda.ObtenhaCompromissos
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Compromissos As IList(Of ICompromisso) = New List(Of ICompromisso)

        Sql.Append(" SELECT ID, IDPESSOA, INICIO, FIM, ASSUNTO, LOCAL, DESCRICAO, STATUS FROM NCL_COMPROMISSO WHERE")
        Sql.Append(String.Concat(" IDPESSOA = ", IDProprietario.ToString))
        Sql.Append(String.Concat(" AND INICIO >= ", DataInicio.ToString("yyyyMMdd") & "000000"))

        If DataFim.HasValue Then
            Sql.Append(String.Concat(" AND FIM <= ", DataFim.Value.ToString("yyyyMMdd") & "235959"))
        End If

        Sql.Append(" ORDER BY INICIO")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Compromissos.Add(MontaObjetoCompromisso(Leitor))
            End While
        End Using

        Return Compromissos
    End Function

    Public Function ObtenhaTarefas(ByVal IDProprietario As Long, _
                                   ByVal DataInicio As Date, _
                                   ByVal DataFim As Date?) As IList(Of ITarefa) Implements IMapeadorDeAgenda.ObtenhaTarefas

        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Tarefas As IList(Of ITarefa) = New List(Of ITarefa)

        Sql.Append(" SELECT ID, IDPESSOA, INICIO, FIM, ASSUNTO, PRIORIDADE, DESCRICAO, STATUS FROM NCL_TAREFA WHERE")
        Sql.Append(String.Concat(" IDPESSOA = ", IDProprietario.ToString))
        Sql.Append(String.Concat(" AND INICIO >= ", DataInicio.ToString("yyyyMMdd") & "000000"))

        If DataFim.HasValue Then
            Sql.Append(String.Concat(" AND FIM <= ", DataFim.Value.ToString("yyyyMMdd") & "235959"))
        End If

        Sql.Append(" ORDER BY INICIO")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Tarefas.Add(MontaObjetoTarefa(Leitor))
            End While
        End Using

        Return Tarefas
    End Function

    Public Function InsiraLembrete(ByVal Lembrete As ILembrete) As Long Implements IMapeadorDeAgenda.InsiraLembrete
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        Lembrete.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO NCL_LEMBRETE (")
        Sql.Append("ID, IDPESSOA, INICIO, FIM, ASSUNTO, STATUS, LOCAL, DESCRICAO)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Lembrete.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(Lembrete.Proprietario.ID.ToString, ", "))
        Sql.Append(String.Concat(Lembrete.Inicio.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat(Lembrete.Fim.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Lembrete.Assunto), "', "))
        Sql.Append(String.Concat("'", Lembrete.Status.ID.ToString, "', "))

        If Not String.IsNullOrEmpty(Lembrete.Local) Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Lembrete.Local), "', "))
        Else
            Sql.Append("NULL, ")
        End If

        If Not String.IsNullOrEmpty(Lembrete.Descricao) Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Lembrete.Descricao), "')"))
        Else
            Sql.Append("NULL)")
        End If

        DBHelper.ExecuteNonQuery(Sql.ToString)
        Return Lembrete.ID.Value
    End Function

    Public Sub ModifiqueLembrete(ByVal Lembrete As ILembrete) Implements IMapeadorDeAgenda.ModifiqueLembrete
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_LEMBRETE SET ")
        Sql.Append(String.Concat(" INICIO = ", Lembrete.Inicio.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat(" FIM = ", Lembrete.Fim.ToString("yyyyMMddHHmmss"), ", "))
        Sql.Append(String.Concat(" ASSUNTO = '", UtilidadesDePersistencia.FiltraApostrofe(Lembrete.Assunto), "', "))
        Sql.Append(String.Concat(" STATUS = '", Lembrete.Status.ID.ToString, "', "))

        If Not String.IsNullOrEmpty(Lembrete.Local) Then
            Sql.Append(String.Concat(" LOCAL = '", UtilidadesDePersistencia.FiltraApostrofe(Lembrete.Local), "', "))
        Else
            Sql.Append(" LOCAL = NULL, ")
        End If

        If Not String.IsNullOrEmpty(Lembrete.Descricao) Then
            Sql.Append(String.Concat(" DESCRICAO = '", UtilidadesDePersistencia.FiltraApostrofe(Lembrete.Descricao), "'"))
        Else
            Sql.Append(" DESCRICAO = NULL")
        End If

        Sql.Append(String.Concat(" WHERE ID = ", Lembrete.ID.Value.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaLembrete(ByVal ID As Long) As ILembrete Implements IMapeadorDeAgenda.ObtenhaLembrete
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        Sql.Append(" SELECT ID, IDPESSOA, INICIO, FIM, ASSUNTO, LOCAL, DESCRICAO, STATUS FROM NCL_LEMBRETE WHERE ")
        Sql.Append(String.Concat("ID = ", ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Return MontaObjetoLembrete(Leitor)
            End If
        End Using

        Return Nothing
    End Function

    Public Function ObtenhaLembretes(ByVal IDProprietario As Long) As IList(Of ILembrete) Implements IMapeadorDeAgenda.ObtenhaLembretes
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Lembretes As IList(Of ILembrete) = New List(Of ILembrete)

        Sql.Append(" SELECT ID, IDPESSOA, INICIO, FIM, ASSUNTO, LOCAL, DESCRICAO, STATUS FROM NCL_LEMBRETE WHERE ")
        Sql.Append(String.Concat("IDPESSOA = ", IDProprietario.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Lembretes.Add(MontaObjetoLembrete(Leitor))
            End While
        End Using

        Return Lembretes
    End Function

    Public Function ObtenhaLembretes(ByVal IDProprietario As Long, _
                                     ByVal DataInicio As Date, _
                                     ByVal DataFim As Date?) As IList(Of ILembrete) Implements IMapeadorDeAgenda.ObtenhaLembretes
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Lembretes As IList(Of ILembrete) = New List(Of ILembrete)

        Sql.Append(" SELECT ID, IDPESSOA, INICIO, FIM, ASSUNTO, LOCAL, DESCRICAO, STATUS FROM NCL_LEMBRETE WHERE")
        Sql.Append(String.Concat(" IDPESSOA = ", IDProprietario.ToString))
        Sql.Append(String.Concat(" AND INICIO >= ", DataInicio.ToString("yyyyMMdd") & "000000"))

        If DataFim.HasValue Then
            Sql.Append(String.Concat(" AND FIM <= ", DataFim.Value.ToString("yyyyMMdd") & "235959"))
        End If

        Sql.Append(" ORDER BY INICIO")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Lembretes.Add(MontaObjetoLembrete(Leitor))
            End While
        End Using

        Return Lembretes
    End Function

    Public Sub RemovaLembrete(ByVal ID As Long) Implements IMapeadorDeAgenda.RemovaLembrete
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM NCL_LEMBRETE")
        Sql.Append(" WHERE ID = " & ID.ToString)

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Private Function MontaObjetoLembrete(ByVal Leitor As IDataReader) As ILembrete
        Dim Lembrete As ILembrete

        Lembrete = FabricaGenerica.GetInstancia.CrieObjeto(Of ILembrete)()
        Lembrete.Assunto = UtilidadesDePersistencia.GetValorString(Leitor, "ASSUNTO")

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "DESCRICAO") Then
            Lembrete.Descricao = UtilidadesDePersistencia.GetValorString(Leitor, "DESCRICAO")
        End If

        Lembrete.Fim = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "FIM").Value
        Lembrete.Inicio = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "INICIO").Value

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "LOCAL") Then
            Lembrete.Local = UtilidadesDePersistencia.GetValorString(Leitor, "LOCAL")
        End If

        Lembrete.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
        Lembrete.Proprietario = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA"))
        Lembrete.Status = StatusDoCompromisso.Obtenha(UtilidadesDePersistencia.getValorChar(Leitor, "STATUS"))

        Return Lembrete
    End Function

    Public Sub ModifiqueConfiguracao(ByVal ConfiguracaoDaAgenda As IConfiguracaoDeAgendaDoUsuario) Implements IMapeadorDeAgenda.ModifiqueConfiguracao
        Me.RemovaConfiguracao(ConfiguracaoDaAgenda.Pessoa.ID.Value)
        Me.InsiraConfiguracao(ConfiguracaoDaAgenda)
    End Sub

    Public Function ObtenhaConfiguracao(ByVal Pessoa As IPessoa) As IConfiguracaoDeAgendaDoUsuario Implements IMapeadorDeAgenda.ObtenhaConfiguracao
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim ConfiguracaoDeAgendaDoUsuario As IConfiguracaoDeAgendaDoUsuario = Nothing

        Sql.Append("SELECT HORAINICO, HORAFIM, INTERVALO, IDPESSOAPADRAO FROM NCL_CNFAGENDAUSU WHERE ")
        Sql.Append(String.Concat("IDPESSOA = ", Pessoa.ID.Value.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Return MontaObjeto(Leitor, Pessoa)
            End If
        End Using

        Return Nothing
    End Function

    Public Function ObtenhaConfiguracao(ByVal IDPessoa As Long) As IConfiguracaoDeAgendaDoUsuario Implements IMapeadorDeAgenda.ObtenhaConfiguracao
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Configuracao As IConfiguracaoDeAgendaDoUsuario = Nothing

        Sql.Append(" SELECT HORAINICO, HORAFIM, NOME, INTERVALO, ID, IDPESSOAPADRAO FROM NCL_CNFAGENDAUSU, NCL_PESSOA WHERE ")
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

    Public Sub RemovaConfiguracao(ByVal ID As Long) Implements IMapeadorDeAgenda.RemovaConfiguracao
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM NCL_CNFAGENDAUSU")
        Sql.Append(" WHERE IDPESSOA = " & ID.ToString)

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

End Class