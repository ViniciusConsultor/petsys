Imports System.Runtime.CompilerServices
Imports System.IO
Imports log4net.Config
Imports log4net
Imports System.Reflection

<Serializable()> _
Public Class Logger

    Private _log As ILog
    Private _versao As String
    Private Shared _instancia As Logger

    Private Const USUARIO As String = "Usuário: "
    Private Const VERSAO As String = "Versão do sistema: "
    Private Const MARCADOR_DE_ERRO As String = "[XXX] "

    Private Sub New()
        XmlConfigurator.Configure(New FileInfo(Path.Combine(Util.ObtenhaCaminhoDaPastaDoServidorDeAplicacao(), "Compartilhados.dll.config")))
        
        _log = LogManager.GetLogger("FileLog")

        Dim assembly As Assembly = GetType(Logger).Assembly
        _versao = CType((assembly.GetCustomAttributes(GetType(AssemblyFileVersionAttribute), False)(0)), AssemblyFileVersionAttribute).Version
    End Sub

    <MethodImpl(MethodImplOptions.Synchronized)> _
    Public Shared Function GetInstancia() As Logger
        If _instancia Is Nothing Then _instancia = New Logger()

        Return _instancia
    End Function

    Private Function ObtenhaInfoUsuario() As String
        If Not FabricaDeContexto.GetInstancia.GetContextoAtual Is Nothing AndAlso _
           Not FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario Is Nothing Then
            Dim usuarioLogado As Usuario = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario

            Return " [" & USUARIO & usuarioLogado.Nome + "] "
        End If

        Return String.Empty
    End Function

    Private Function ObtenhaVersao() As String
        If String.IsNullOrEmpty(_versao) Then Return String.Empty

        Return " [" & VERSAO & _versao + "] "
    End Function

    Public Sub Debug(mensagem As String)
        If _log.IsDebugEnabled Then
            _log.Debug(ObtenhaInfoUsuario() & mensagem)
        End If
    End Sub

    Public Sub Debug(mensagem As String, exc As Exception)
        If _log.IsDebugEnabled Then
            _log.Debug(ObtenhaInfoUsuario() & mensagem, exc)
        End If
    End Sub

    Public Sub Info(mensagem As String)
        If _log.IsInfoEnabled Then
            _log.Info(ObtenhaInfoUsuario() & mensagem)
        End If
    End Sub

    Public Sub Info(mensagem As String, exc As Exception)
        If _log.IsInfoEnabled Then
            _log.Info(ObtenhaInfoUsuario() & mensagem, exc)
        End If
    End Sub

    Public Sub Warn(mensagem As String)
        If _log.IsWarnEnabled Then
            _log.Warn(ObtenhaInfoUsuario() & mensagem)
        End If
    End Sub

    Public Sub Warn(mensagem As String, exc As Exception)
        If _log.IsWarnEnabled Then
            _log.Warn(ObtenhaInfoUsuario() & mensagem, exc)
        End If
    End Sub

    Public Sub Erro(mensagem As String, exc As Exception)
        If _log.IsErrorEnabled Then
            _log.Error(ObtenhaInfoUsuario() & ObtenhaVersao() & MARCADOR_DE_ERRO & mensagem, exc)
        End If
    End Sub

End Class