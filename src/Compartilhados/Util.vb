Imports System.Web
Imports System.IO

Imports System.Configuration
Imports System.Xml

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
                                                      Principal.Usuario, Principal.EmpresaLogada)

        Return Credencial
    End Function

    Public Shared Function ObtenhaCaminhoDaPastaDoServidorDeAplicacao() As String
        Dim Caminho As String

        If ExecutandoServidorWeb() Then
            Caminho = HttpContext.Current.Request.PhysicalApplicationPath & "bin" & Path.DirectorySeparatorChar
        Else
            Caminho = Environment.CurrentDirectory()
        End If

        Return Caminho
    End Function

    Public Shared Function ObtenhaCaminhoDeConfiguracaoDoServicoDeConexao() As String
        Dim Configuracao As KeyValueConfigurationElement
        Dim Caminho As String = ObtenhaCaminhoDaPastaDoServidorDeAplicacao()

        Configuracao = ConfigurationManager.OpenExeConfiguration(Path.Combine(Caminho, "Core.Servicos.Local.dll")).AppSettings.Settings("Provider")

        If Configuracao Is Nothing Then Throw New Exception("O arquivo de configuração Core.Servicos.Local.config não foi encontrado.")
        Return Configuracao.Value
    End Function

    Public Shared Sub SalveConfiguracaoDeConexao(ByVal Conexao As IConexao)
        Dim Caminho As String = ObtenhaCaminhoDaPastaDoServidorDeAplicacao()
        Dim Xml As XmlDocument
        
        Xml = New XmlDocument
        Xml.Load(Path.Combine(Caminho, "Core.Servicos.Local.dll.config"))
        End Sub

    Public Shared Function ObtenhaCaminhoArquivoXMLDeGatilho() As String
        Return ObtenhaCaminhoDaPastaDoServidorDeAplicacao()
    End Function

    Public Shared Function SistemaUtilizaSQLUpperCase() As Boolean
        Dim Configuracao As KeyValueConfigurationElement
        Dim Caminho As String = ObtenhaCaminhoDaPastaDoServidorDeAplicacao()

        Configuracao = ConfigurationManager.OpenExeConfiguration(Path.Combine(Caminho, "Core.Servicos.Local.dll")).AppSettings.Settings("SistemaUtilizaSQLUpperCase")
        Return CBool(Configuracao.Value)
    End Function

    Public Shared Function ObtenhaStringDeConexao() As String
        Dim Configuracao As KeyValueConfigurationElement
        Dim Caminho As String = ObtenhaCaminhoDaPastaDoServidorDeAplicacao()

        Configuracao = ConfigurationManager.OpenExeConfiguration(Path.Combine(Caminho, "Core.Servicos.Local.dll")).AppSettings.Settings("Conexao")
        Return Configuracao.Value
    End Function

    Public Shared Function ObtenhaServidorDeAplicao() As String
        Dim Configuracao As String = System.Configuration.ConfigurationManager.AppSettings("ServidorDeAplicacao")

        If String.IsNullOrEmpty(Configuracao) Then Configuracao = "localhost"
        Return Configuracao
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
        Dim Caminho As String = ObtenhaCaminhoDaPastaDoServidorDeAplicacao()

        Configuracao = ConfigurationManager.OpenExeConfiguration(Path.Combine(Caminho, "Core.Servicos.Local.dll")).AppSettings.Settings("SkinPadrao")
        Return Configuracao.Value
    End Function

    Public Shared Function ObtenhaImagemPadrao() As String
        Dim Configuracao As KeyValueConfigurationElement
        Dim Caminho As String = ObtenhaCaminhoDaPastaDoServidorDeAplicacao()

        Configuracao = ConfigurationManager.OpenExeConfiguration(Path.Combine(Caminho, "Core.Servicos.Local.dll")).AppSettings.Settings("ImagemPadrao")
        Return Configuracao.Value
    End Function

End Class