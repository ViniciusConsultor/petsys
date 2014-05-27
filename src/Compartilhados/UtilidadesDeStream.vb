Imports System.IO
Imports System.Text

Public Class UtilidadesDeStream

    Public Shared Function ConvertaArquivoAnsiParaUtf8(arquivoTxtAnsi As Stream) As StreamReader
        Dim Bytes As Byte() = TransformaStreamEmArrayDeBytes(arquivoTxtAnsi)

        Dim arquivoConvertido = New MemoryStream(Encoding.Convert(Encoding.[Default], Encoding.UTF8, Bytes))
        Return New StreamReader(arquivoConvertido, Encoding.UTF8, True)
    End Function

    Public Shared Function TransformaStreamEmArrayDeBytes(arquivo As Stream) As Byte()
        Dim streamLength As Integer = Convert.ToInt32(arquivo.Length)
        Dim fileData(streamLength) As Byte

        arquivo.Read(fileData, 0, streamLength)
        arquivo.Close()

        Return fileData
    End Function

    Public Shared Function TransformeArrayBytesEmStream(ByVal ArrayBytes As Byte(), ByVal nomeDoArquivo As String) As Stream
        Dim fs As FileStream = New FileStream(Path.Combine(Path.GetTempPath(), nomeDoArquivo), FileMode.Create)

        fs.Write(ArrayBytes, 0, ArrayBytes.Length)
        fs.Flush()
        fs.Close()
        Return fs
    End Function


End Class
