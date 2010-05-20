Imports System.Web.SessionState
Imports Compartilhados
Imports System.Text
Imports System.Reflection
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Negocio

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim MensagemDeLog As String = ObtenhaMensagemDeLog()

        EscritorDeLog.escrevaLog(MensagemDeLog)
        System.Web.HttpContext.Current.Server.Transfer("~/Erro.aspx", True)
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

    Private Function ObtenhaMensagemDeLog() As String
        Dim Erro As Exception
        Dim DataDoErro As Date = Now
        Dim Mensagem As New StringBuilder
        Dim Versao As String

        Dim assembly As Reflection.Assembly = GetType(WorkSpace).Assembly

        Versao = CType((assembly.GetCustomAttributes(GetType(AssemblyFileVersionAttribute), False)(0)), AssemblyFileVersionAttribute).Version

        Erro = Server.GetLastError

        Mensagem.AppendLine("Foi gerado uma exceção ")
        Mensagem.AppendLine("Versão : " & Versao)
        Mensagem.AppendLine("Data do erro : " & DataDoErro.ToString("dd/MM/yyyy"))
        Mensagem.AppendLine("Hora do erro : " & DataDoErro.ToString("HH:mm:ss"))

        Do While Not Erro Is Nothing
            Mensagem.AppendLine("Origem do erro : " & Erro.Source)
            Mensagem.AppendLine("Descrição do erro : " & Erro.Message)
            Mensagem.AppendLine("Método origem : " & Erro.ToString)
            Mensagem.AppendLine("Resumo : " & Erro.StackTrace)
            Mensagem.AppendLine("-------------------------------------------------------------")
            Erro = Erro.InnerException
        Loop

        If Not FabricaDeContexto.GetInstancia.GetContextoAtual Is Nothing AndAlso _
           Not FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario Is Nothing Then
            Dim Usuario As Usuario = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario

            Mensagem.AppendLine("Nome do operador      : " & Usuario.Nome)
        End If


        Return Mensagem.ToString
    End Function

End Class