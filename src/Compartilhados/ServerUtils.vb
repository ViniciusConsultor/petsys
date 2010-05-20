Imports System.Runtime.Remoting.Messaging
Imports Compartilhados.DBHelper

Public Class ServerUtils

    Private Const SERVER_UTILS As String = "SERVER_UTILS"
    Private Shared LockInstancia As New Object
    Private DBHelperPadrao As IDBHelper
    Private LockDBHelper As New Object
    Private ListaDeDbHelpers As IList(Of IDBHelper) = New List(Of IDBHelper)
    Private Credencial As ICredencial

    'construtor privado
    Private Sub New()
    End Sub

    Private Shared Function GetInstancia() As ServerUtils
        Dim ServerUtils As ServerUtils

        ServerUtils = CType(CallContext.GetData(SERVER_UTILS), ServerUtils)

        If ServerUtils Is Nothing Then
            SyncLock lockInstancia
                ServerUtils = CType(CallContext.GetData(SERVER_UTILS), ServerUtils)
                If ServerUtils Is Nothing Then
                    ServerUtils = New ServerUtils
                    CallContext.SetData(SERVER_UTILS, ServerUtils)
                End If
            End SyncLock
        End If

        Return ServerUtils
    End Function

    Private Function p_getDBHelper() As IDBHelper
        SyncLock LockDBHelper
            If DBHelperPadrao Is Nothing Then
                Dim Conexao As IConexao = Me.p_getCredencial.Conexao

                DBHelperPadrao = DBHelperFactory.Create(Conexao)
            End If

            Return DBHelperPadrao
        End SyncLock
    End Function

    Private Function p_criarNovoDBHelper() As IDBHelper
        Dim Conexao As IConexao = Me.p_getCredencial.Conexao
        Dim DBHelper As IDBHelper = DBHelperFactory.Create(Conexao)

        ListaDeDbHelpers.Add(DBHelper)

        Return DBHelper
    End Function

    Private Sub p_setCredencial(ByVal Credencial As ICredencial)
        Me.Credencial = Credencial
    End Sub

    Private Function p_getCredencial() As ICredencial
        Return Me.Credencial
    End Function

    Private Sub p_libereRecursos()
        If Not Me.DBHelperPadrao Is Nothing Then
            Me.DBHelperPadrao.Dispose()
        End If

        'Libera possiveis IDbHelper's alocados
        For Each DBHelperAdicional As IDBHelper In ListaDeDbHelpers
            DBHelperAdicional.Dispose()
        Next
    End Sub

    '**************** MÉTODOS SHARED **************

    Public Shared Sub setCredencial(ByVal Credencial As ICredencial)
        ServerUtils.GetInstancia.p_setCredencial(Credencial)
    End Sub

    Public Shared Function getCredencial() As ICredencial
        Return ServerUtils.GetInstancia.p_getCredencial()
    End Function

    Public Shared Function getDBHelper() As IDBHelper
        Return ServerUtils.GetInstancia.p_getDBHelper
    End Function

    Public Shared Function criarNovoDbHelper() As IDBHelper
        Return ServerUtils.getInstancia.p_criarNovoDBHelper
    End Function

    Public Shared Sub libereRecursos()
        ServerUtils.GetInstancia.p_libereRecursos()

        SyncLock lockInstancia
            CallContext.SetData(SERVER_UTILS, Nothing)
        End SyncLock
    End Sub

    Public Shared Function EstaIniciado() As Boolean
        Dim ServerUtils As ServerUtils

        ServerUtils = CType(CallContext.GetData(SERVER_UTILS), ServerUtils)

        If ServerUtils Is Nothing Then
            Return False
        ElseIf ServerUtils.Credencial Is Nothing Then
            Return False
        End If

        Return True
    End Function

    Public Shared Sub BeginTransaction()
        ServerUtils.getDBHelper.BeginTransaction()
    End Sub

    Public Shared Sub RollbackTransaction()
        ServerUtils.getDBHelper.RollbackTransaction()
    End Sub

    Public Shared Sub CommitTransaction()
        ServerUtils.getDBHelper.CommitTransaction()
    End Sub

End Class