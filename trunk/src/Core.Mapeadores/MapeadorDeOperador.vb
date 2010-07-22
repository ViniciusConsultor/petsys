﻿Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Interfaces.Core.Servicos
Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Public Class MapeadorDeOperador
    Implements IMapeadorDeOperador

    Public Sub Inserir(ByRef Operador As IOperador) Implements IMapeadorDeOperador.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO NCL_OPERADOR (")
        Sql.Append("IDPESSOA, TIPOPESSOA, LOGIN, STATUS)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Operador.Pessoa.ID.ToString, ", "))
        Sql.Append(String.Concat(Operador.Pessoa.Tipo.ID, ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Operador.Login), "', "))
        Sql.Append(String.Concat("'", Operador.Status.ID, "')"))

        DBHelper.ExecuteNonQuery(Sql.ToString, False)
        InsiraGrupos(DBHelper, Operador)
    End Sub

    Private Sub InsiraGrupos(ByVal DBHelper As IDBHelper, ByVal Operador As IOperador)
        Dim Sql As StringBuilder

        For Each Grupo As IGrupo In Operador.ObtenhaGrupos
            Sql = New StringBuilder
            Sql.Append("INSERT INTO NCL_GRPOPE (IDOPERADOR, IDGRUPO) VALUES ( ")
            Sql.Append(Operador.Pessoa.ID.Value.ToString & ", ")
            Sql.Append(Grupo.ID.Value.ToString & ")")
            DBHelper.ExecuteNonQuery(Sql.ToString)
        Next
    End Sub

    Public Sub Modificar(ByVal Operador As IOperador) Implements IMapeadorDeOperador.Modificar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_OPERADOR SET ")
        Sql.Append(String.Concat("LOGIN = '", UtilidadesDePersistencia.FiltraApostrofe(Operador.Login), "', "))
        Sql.Append(String.Concat("STATUS = '", Operador.Status.ID, "'"))
        Sql.Append(" WHERE IDPESSOA = ")
        Sql.Append(Operador.Pessoa.ID.Value)

        DBHelper.ExecuteNonQuery(Sql.ToString)

        RemovaGrupos(DBHelper, Operador.Pessoa.ID.Value)
        InsiraGrupos(DBHelper, Operador)
    End Sub

    Public Sub Remover(ByVal ID As Long) Implements IMapeadorDeOperador.Remover
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        RemovaGrupos(DBHelper, ID)
        Sql.Append(String.Concat("DELETE FROM NCL_OPERADOR WHERE IDPESSOA = ", ID.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaOperadorPorLogin(ByVal Login As String) As IOperador Implements IMapeadorDeOperador.ObtenhaOperadorPorLogin
        Dim Sql As New StringBuilder

        Sql.Append("SELECT IDPESSOA, TIPOPESSOA, LOGIN, STATUS")
        Sql.Append(" FROM NCL_OPERADOR")
        Sql.Append(String.Concat(" WHERE LOGIN = '", UtilidadesDePersistencia.FiltraApostrofe(Login), "'"))

        Return Me.ObtenhaOperador(Sql.ToString)
    End Function

    Private Function ObtenhaOperador(ByVal Sql As String) As IOperador
        Dim DBHelper As IDBHelper
        Dim Operador As IOperador = Nothing

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Dim Pessoa As IPessoaFisica

                Pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA"))
                Operador = FabricaGenerica.GetInstancia.CrieObjeto(Of IOperador)(New Object() {Pessoa})
                Operador.Login = UtilidadesDePersistencia.GetValorString(Leitor, "LOGIN")
                Operador.Status = StatusDoOperador.ObtenhaStatus(UtilidadesDePersistencia.getValorChar(Leitor, "STATUS"))
            End If
        End Using

        If Not Operador Is Nothing Then
            Operador.AdicioneGrupos(ObtenhaGruposDoOperador(Operador.Pessoa.ID.Value))
        End If

        Return Operador
    End Function

    Private Sub RemovaGrupos(ByVal DBHelper As IDBHelper, ByVal IDOperador As Long)
        DBHelper.ExecuteNonQuery("DELETE FROM NCL_GRPOPE WHERE IDOPERADOR = " & IDOperador.ToString)
    End Sub

    Private Function ObtenhaGruposDoOperador(ByVal IdOperador As Long) As IList(Of IGrupo)
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim ListaDeIds As New List(Of Long)
        Dim Grupos As IList(Of IGrupo) = New List(Of IGrupo)

        Sql.Append("SELECT IDGRUPO FROM NCL_GRPOPE ")
        Sql.Append(String.Concat("WHERE IDOPERADOR = ", IdOperador))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                ListaDeIds.Add(UtilidadesDePersistencia.GetValorLong(Leitor, "IDGRUPO"))
            End While
        End Using

        Dim MapeadorDeGrupo As IMapeadorDeGrupo = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupo)()

        For Each Id As Long In ListaDeIds
            Grupos.Add(MapeadorDeGrupo.ObtenhaGrupo(Id))
        Next

        Return Grupos
    End Function

    Public Function ObtenhaOperador(ByVal Pessoa As IPessoa) As IOperador Implements IMapeadorDeOperador.ObtenhaOperador
        Dim Sql As New StringBuilder

        Sql.Append("SELECT IDPESSOA, TIPOPESSOA, LOGIN, STATUS")
        Sql.Append(" FROM NCL_OPERADOR")
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", Pessoa.ID.Value.ToString))

        Return Me.ObtenhaOperador(Sql.ToString)
    End Function

End Class