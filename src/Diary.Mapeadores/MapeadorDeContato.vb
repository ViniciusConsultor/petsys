﻿Imports Diary.Interfaces.Mapeadores
Imports Diary.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Fabricas

Public Class MapeadorDeContato
    Implements IMapeadorDeContato

    Public Sub Inserir(ByVal Contato As IContato) Implements IMapeadorDeContato.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO DRY_CONTATO (")
        Sql.Append("IDPESSOA, TIPOPESSOA, CARGO, OBSERVACOES)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Contato.Pessoa.ID.ToString, ", "))
        Sql.Append(String.Concat(Contato.Pessoa.Tipo.ID, ", "))

        If String.IsNullOrEmpty(Contato.Cargo) Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat("'", Contato.Cargo, "', "))
        End If

        If String.IsNullOrEmpty(Contato.Observacoes) Then
            Sql.Append("NULL)")
        Else
            Sql.Append(String.Concat("'", Contato.Observacoes, "')"))
        End If

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modificar(ByVal Contato As IContato) Implements IMapeadorDeContato.Modificar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE DRY_CONTATO SET")

        If String.IsNullOrEmpty(Contato.Cargo) Then
            Sql.Append(" CARGO = NULL, ")
        Else
            Sql.Append(String.Concat(" CARGO = '", Contato.Cargo, "', "))
        End If

        If String.IsNullOrEmpty(Contato.Observacoes) Then
            Sql.Append(" OBSERVACOES = NULL")
        Else
            Sql.Append(String.Concat(" OBSERVACOES = '", Contato.Observacoes, "'"))
        End If

        Sql.Append(String.Concat(" WHERE IDPESSOA = ", Contato.Pessoa.ID.Value))


        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function Obtenha(ByVal Pessoa As IPessoa) As IContato Implements IMapeadorDeContato.Obtenha
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Contato As IContato = Nothing

        Sql.Append("SELECT IDPESSOA, TIPOPESSOA, CARGO, OBSERVACOES FROM DRY_CONTATO WHERE ")
        Sql.Append(String.Concat("IDPESSOA = ", Pessoa.ID.Value.ToString, " AND "))
        Sql.Append(String.Concat("TIPOPESSOA = ", Pessoa.Tipo.ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Contato = FabricaGenerica.GetInstancia.CrieObjeto(Of IContato)(New Object() {Pessoa})

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "CARGO") Then
                    Contato.Cargo = UtilidadesDePersistencia.GetValorString(Leitor, "CARGO")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "OBSERVACOES") Then
                    Contato.Observacoes = UtilidadesDePersistencia.GetValorString(Leitor, "OBSERVACOES")
                End If
            End If
        End Using

        Return Contato
    End Function

    Public Function Obtenha(ByVal ID As Long) As IContato Implements IMapeadorDeContato.Obtenha
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Contato As IContato = Nothing
        Dim Tipo As TipoDePessoa
        Dim Pessoa As IPessoa = Nothing

        Sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.NOME, NCL_PESSOA.TIPO,")
        Sql.Append("DRY_CONTATO.IDPESSOA, DRY_CONTATO.TIPOPESSOA, CARGO, OBSERVACOES ")
        Sql.Append("FROM NCL_PESSOA, DRY_CONTATO ")
        Sql.Append("WHERE DRY_CONTATO.IDPESSOA = NCL_PESSOA.ID ")
        Sql.Append("AND DRY_CONTATO.TIPOPESSOA = NCL_PESSOA.TIPO ")
        Sql.Append(String.Concat("AND IDPESSOA = ", ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Tipo = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPO"))

                If Tipo.Equals(TipoDePessoa.Fisica) Then
                    Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()
                Else
                    Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaJuridica)()
                End If

                Pessoa.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

                Contato = FabricaGenerica.GetInstancia.CrieObjeto(Of IContato)(New Object() {Pessoa})

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "CARGO") Then
                    Contato.Cargo = UtilidadesDePersistencia.GetValorString(Leitor, "CARGO")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "OBSERVACOES") Then
                    Contato.Observacoes = UtilidadesDePersistencia.GetValorString(Leitor, "OBSERVACOES")
                End If
            End If
        End Using

        Return Contato
    End Function

    Public Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                             ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IContato) Implements IMapeadorDeContato.ObtenhaPorNomeComoFiltro
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Contato As IContato = Nothing
        Dim Pessoa As IPessoa = Nothing
        Dim Tipo As TipoDePessoa
        Dim Contatos As IList(Of IContato) = New List(Of IContato)

        Sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.NOME, NCL_PESSOA.TIPO,")
        Sql.Append("DRY_CONTATO.IDPESSOA, DRY_CONTATO.TIPOPESSOA, CARGO, OBSERVACOES ")
        Sql.Append("FROM NCL_PESSOA, DRY_CONTATO ")
        Sql.Append("WHERE DRY_CONTATO.IDPESSOA = NCL_PESSOA.ID ")
        Sql.Append("AND DRY_CONTATO.TIPOPESSOA = NCL_PESSOA.TIPO ")

        If Not String.IsNullOrEmpty(Nome) Then
            Sql.Append(String.Concat("AND NOME LIKE '", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'"))
        End If

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read AndAlso Contatos.Count < QuantidadeMaximaDeRegistros
                Tipo = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPO"))

                If Tipo.Equals(TipoDePessoa.Fisica) Then
                    Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()
                Else
                    Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaJuridica)()
                End If

                Pessoa.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

                Contato = FabricaGenerica.GetInstancia.CrieObjeto(Of IContato)(New Object() {Pessoa})

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "CARGO") Then
                    Contato.Cargo = UtilidadesDePersistencia.GetValorString(Leitor, "CARGO")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "OBSERVACOES") Then
                    Contato.Observacoes = UtilidadesDePersistencia.GetValorString(Leitor, "OBSERVACOES")
                End If

                Contatos.Add(Contato)
            End While
        End Using

        Return Contatos
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IMapeadorDeContato.Remover
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM DRY_CONTATO")
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", ID.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

End Class