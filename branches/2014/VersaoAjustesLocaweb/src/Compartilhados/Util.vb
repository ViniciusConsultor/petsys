Imports System.Web
Imports System.IO
Imports System.Configuration
Imports Ionic.Zip

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

    Public Shared Function ConstruaCredencial(ByVal conexao As IConexao) As Credencial
        Dim Credencial As Credencial = New Credencial(conexao, _
                                                     Nothing, Nothing)

        Return Credencial
    End Function

    Public Shared Function ObtenhaCaminhoDaPastaDoServidorDeAplicacao() As String
        Dim Caminho As String

        If ExecutandoServidorWeb() Then
            Caminho = HttpRuntime.AppDomainAppPath & "bin" & Path.DirectorySeparatorChar
        Else

            Try
                Caminho = HttpRuntime.AppDomainAppPath & "bin" & Path.DirectorySeparatorChar
            Catch ex As Exception
                Caminho = Environment.CurrentDirectory()
            End Try

        End If

        Return Caminho
    End Function

    Public Shared Function ObtenhaCaminhoDeConfiguracaoDoServicoDeConexao() As String
        Return ConfigurationManager.AppSettings("Provider")
    End Function

    Public Shared Function ObtenhaCaminhoArquivoXMLDeGatilho() As String
        Return String.Concat(ObtenhaCaminhoDaPastaDoServidorDeAplicacao(), "gatilhos.xml")
    End Function

    Public Shared Function ObtenhaCaminhoArquivoXMLDeSchedule() As String
        Return String.Concat(ObtenhaCaminhoDaPastaDoServidorDeAplicacao(), "schedules.xml")
    End Function

    Public Shared Function SistemaUtilizaSQLUpperCase() As Boolean
        Return ConfigurationManager.AppSettings("SistemaUtilizaSQLUpperCase").ToLower().Equals("true")
    End Function

    Public Shared Function ObtenhaStringDeConexao() As String
        Return ConfigurationManager.AppSettings("Conexao")
    End Function

    Public Shared Function GetDiretorioLog() As String
        Return HttpRuntime.AppDomainAppPath & Path.DirectorySeparatorChar & "LOG" & Path.DirectorySeparatorChar
    End Function

    Public Shared Function GetDiretorioLoads() As String
        Return HttpRuntime.AppDomainAppPath & Path.DirectorySeparatorChar & "LOADS" & Path.DirectorySeparatorChar
    End Function

    Public Shared Function ObtenhaSkinPadrao() As String
        Return ConfigurationManager.AppSettings("SkinPadrao")
    End Function

    Public Shared Function ObtenhaImagemPadrao() As String
        Return ConfigurationManager.AppSettings("ImagemPadrao")
    End Function

    Public Shared Sub DescompacteArquivoZip(ByVal nomeECaminhoArquivoZip As String, ByVal CaminhoDestino As String)
        Using zip1 As ZipFile = ZipFile.Read(nomeECaminhoArquivoZip)

            Dim e As ZipEntry

            For Each e In zip1
                e.Extract(CaminhoDestino, ExtractExistingFileAction.OverwriteSilently)
            Next
        End Using
    End Sub

    Public Shared Sub CrieDiretorio(ByVal nomeDiretorio As String)
        If Not Directory.Exists(nomeDiretorio) Then Directory.CreateDirectory(nomeDiretorio)
    End Sub
End Class