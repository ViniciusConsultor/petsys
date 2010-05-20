Imports System.IO

Public Class EscritorDeLog

    Private Shared arquivo As StreamWriter
    Private Shared objetoLock As New Object

    Private Shared Sub criaDiretorio(ByVal diretorio As String)
        Try
            Directory.CreateDirectory(diretorio)
        Catch ex As Exception
            Throw New Exception("Erro ao criar o diretorio de log. " & ex.Message)
        End Try

    End Sub

    Private Shared Sub criaArquivo()
        Dim diretorio As String

        Try
            diretorio = Util.GetDiretorioLog()
            criaDiretorio(diretorio)
            arquivo = New StreamWriter(diretorio & "Log" & Now.ToString("ddMMyyyyhhmmssms") & System.Threading.Thread.CurrentThread.ManagedThreadId & ".txt")

        Catch ex As Exception
            Throw New Exception("Erro ao criar o arquivo de log. " & ex.Message)
        End Try

    End Sub

    Private Shared Sub fechaArquivo()

        Try
            arquivo.Close()
        Catch ex As Exception
            Throw New Exception("Erro ao fechar o arquivo de log. " & ex.Message)
        End Try

    End Sub

    Public Shared Sub escrevaLog(ByVal log As IList)
        criaArquivo()
        For Each itemDoLog As String In log
            arquivo.WriteLine(itemDoLog)
        Next
        fechaArquivo()
    End Sub

    Public Shared Sub escrevaLog(ByVal log As String)
        criaArquivo()
        arquivo.Write(log)
        fechaArquivo()
    End Sub
End Class