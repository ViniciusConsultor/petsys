Imports System.Web
Imports System.IO
Imports Compartilhados.Fabricas
Imports System.Configuration

Public Class Util

    Public Shared Function ExecutandoServidorWeb() As Boolean
        Return Not IsNothing(HttpContext.Current)
    End Function

    Public Shared Function ConstruaCredencial() As Credencial
        Dim Principal As Principal = FabricaDeContexto.GetInstancia.GetContextoAtual

        If Principal Is Nothing Then
            Throw New Exception("Não é possível criar credencial fora do servidor Web.")
        End If

        Dim Credencial As Credencial = New Credencial(Principal.Conexao, _
                                                      Principal.Usuario)

        Return Credencial
    End Function

    Private Shared Function ObtenhaPastaConfiguradaPorVariavelDeAmbiente() As String
        Dim environmentVariable As String = Environment.GetEnvironmentVariable("SIMPLE_PATH", EnvironmentVariableTarget.Machine)

        If (environmentVariable Is Nothing) Then
            Throw New ApplicationException("O caminho dos arquivos de configuração não foi configurado corretamente.")
        End If

        Return environmentVariable
    End Function

    Public Shared Function ObtenhaCaminhoDaPastaDoServidorDeAplicacao() As String
        Return ObtenhaPastaConfiguradaPorVariavelDeAmbiente()
    End Function

    Public Shared Function ObtenhaCaminhoDeConfiguracaoDoServicoDeConexao() As String
        Dim Configuracao As KeyValueConfigurationElement
        Dim Caminho As String

        If ExecutandoServidorWeb() Then
            Caminho = HttpContext.Current.Request.PhysicalApplicationPath & "bin" & Path.DirectorySeparatorChar
        Else
            Caminho = ObtenhaPastaConfiguradaPorVariavelDeAmbiente()
        End If

        Configuracao = ConfigurationManager.OpenExeConfiguration(Path.Combine(Caminho, "Core.Servicos.Local.dll")).AppSettings.Settings("Provider")

        If Configuracao Is Nothing Then Throw New Exception("O arquivo de configuração Core.Servicos.Local.config não foi encontrado.")
        Return Configuracao.Value
    End Function

    Public Shared Function ObtenhaCaminhoArquivoXMLDeGatilho() As String
        Dim Caminho As String

        If ExecutandoServidorWeb() Then
            Caminho = HttpContext.Current.Request.PhysicalApplicationPath & "bin" & Path.DirectorySeparatorChar & "gatilhos.xml"
        Else
            Caminho = ObtenhaPastaConfiguradaPorVariavelDeAmbiente() & Path.DirectorySeparatorChar & "gatilhos.xml"
        End If

        Return Caminho
    End Function

    Public Shared Function SistemaUtilizaSQLUpperCase() As Boolean
        Dim Configuracao As KeyValueConfigurationElement
        Dim Caminho As String

        If ExecutandoServidorWeb() Then
            Caminho = HttpContext.Current.Request.PhysicalApplicationPath & "bin" & Path.DirectorySeparatorChar
        Else
            Caminho = ObtenhaPastaConfiguradaPorVariavelDeAmbiente()
        End If

        Configuracao = ConfigurationManager.OpenExeConfiguration(Path.Combine(Caminho, "Core.Servicos.Local.dll")).AppSettings.Settings("SistemaUtilizaSQLUpperCase")
        Return CBool(Configuracao.Value)
    End Function

    Public Shared Function ObtenhaStringDeConexao() As String
        Dim Configuracao As KeyValueConfigurationElement
        Dim Caminho As String

        If ExecutandoServidorWeb() Then
            Caminho = HttpContext.Current.Request.PhysicalApplicationPath & "bin" & Path.DirectorySeparatorChar
        Else
            Caminho = ObtenhaPastaConfiguradaPorVariavelDeAmbiente()
        End If

        Configuracao = ConfigurationManager.OpenExeConfiguration(Path.Combine(Caminho, "Core.Servicos.Local.dll")).AppSettings.Settings("Conexao")
        Return Configuracao.Value
    End Function

    Public Shared Function ObtenhaTipoDeDistribuicao() As String
        Dim Configuaracao As String = System.Configuration.ConfigurationManager.AppSettings("TipoDistribuicao")

        If String.IsNullOrEmpty(Configuaracao) Then Configuaracao = "Local"
        Return Configuaracao
    End Function

    Public Shared Function GetDiretorioLog() As String
        Return HttpContext.Current.Request.PhysicalApplicationPath & Path.DirectorySeparatorChar & "LOG" & Path.DirectorySeparatorChar
    End Function

    Public Shared Function ObtenhaSkinPadrao() As String
        Dim Configuracao As KeyValueConfigurationElement
        Dim Caminho As String

        If ExecutandoServidorWeb() Then
            Caminho = HttpContext.Current.Request.PhysicalApplicationPath & "bin" & Path.DirectorySeparatorChar
        Else
            Caminho = ObtenhaPastaConfiguradaPorVariavelDeAmbiente()
        End If

        Configuracao = ConfigurationManager.OpenExeConfiguration(Path.Combine(Caminho, "Core.Servicos.Local.dll")).AppSettings.Settings("SkinPadrao")
        Return Configuracao.Value
    End Function

    Public Shared Function ObtenhaImagemPadrao() As String
        Dim Configuracao As KeyValueConfigurationElement
        Dim Caminho As String

        If ExecutandoServidorWeb() Then
            Caminho = HttpContext.Current.Request.PhysicalApplicationPath & "bin" & Path.DirectorySeparatorChar
        Else
            Caminho = ObtenhaPastaConfiguradaPorVariavelDeAmbiente()
        End If

        Configuracao = ConfigurationManager.OpenExeConfiguration(Path.Combine(Caminho, "Core.Servicos.Local.dll")).AppSettings.Settings("ImagemPadrao")
        Return Configuracao.Value
    End Function

End Class